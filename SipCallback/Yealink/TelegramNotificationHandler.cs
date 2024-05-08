using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SipCallback.UserProvider.Ucm;
using SipCallback.Yealink.Cache;
using SipCallback.Yealink.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SipCallback.Yealink;

public class TelegramNotificationHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly IMemoryCache _cache;
    private readonly Ldap _ldap;
    private readonly IOptions<TelegramOptions> _options;

    public TelegramNotificationHandler(IMemoryCache cache, IHttpClientFactory clientFactory,
        IOptions<TelegramOptions> options, Ldap ldap)
    {
        _cache = cache;
        _options = options;
        _ldap = ldap;

        _bot = new TelegramBotClient(options.Value.Token, clientFactory.CreateClient());
    }

    private string GetUserName(int accountNumber)
    {
        var user = _ldap.FindAsync(accountNumber);
        var username = string.Empty;

        if (user is not null) username = string.Join(' ', user.LastName, user.FirstName);

        return username;
    }

    public async Task HandleIncomingCallAsync(IncomingCall model, CancellationToken ct = default)
    {
        var username = GetUserName(model.CalledNumber);
        var displayName = string.IsNullOrWhiteSpace(username)
            ? model.CalledNumber.ToString()
            : username;

        var message = $"Входящий вызов от {displayName}";

        var result = await _bot.SendTextMessageAsync(_options.Value.ChatId, message, disableNotification: true,
            cancellationToken: ct);

        var cacheEntry = new CallCacheEntry(model.CalledNumber, model.ActiveUser, displayName, result.MessageId);
        _cache.Set(cacheEntry, TimeSpan.FromSeconds(30));
    }

    public async Task HandleMissedCallAsync(MissedCall model, CancellationToken ct = default)
    {
        const string messageTemplate = "Пропущенный звонок от {0}";

        if (_cache.TryGetValue(new CallCacheEntry(model.CalledNumber, model.ActiveUser), out CallCacheEntry? entry))
        {
            var message = string.Format(messageTemplate, entry.Username);

            await _bot.EditMessageTextAsync(new ChatId(_options.Value.ChatId), entry.MessageId!.Value, message,
                cancellationToken: ct);
            _cache.Remove(entry.Key);
        }
        else
        {
            var username = GetUserName(model.CalledNumber);
            var displayName = string.IsNullOrWhiteSpace(username)
                ? model.CalledNumber.ToString()
                : username;

            var message = string.Format(messageTemplate, displayName);

            await _bot.SendTextMessageAsync(new ChatId(_options.Value.ChatId), message, disableNotification: true,
                cancellationToken: ct);
        }
    }

    public async Task HandleAnsweredCallAsync(AnsweredCall model, CancellationToken ct = default)
    {
        if (_cache.TryGetValue(new CallCacheEntry(model.CalledNumber, model.ActiveUser), out CallCacheEntry? entry))
        {
            await _bot.DeleteMessageAsync(_options.Value.ChatId, entry.MessageId!.Value, ct);
            _cache.RemoveCallEntry(entry);
        }
    }
}