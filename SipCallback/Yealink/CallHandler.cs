using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SipCallback.Options;
using SipCallback.Yealink.Cache;
using SipCallback.Yealink.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SipCallback.Yealink;

public class CallHandler
{
	private readonly ITelegramBotClient _bot;
	private readonly IMemoryCache _cache;
	private readonly IOptions<TelegramOptions> _options;

	public CallHandler(IMemoryCache cache, IHttpClientFactory clientFactory, IOptions<TelegramOptions> options)
	{
		_cache = cache;
		_options = options;

		_bot = new TelegramBotClient(options.Value.Token, clientFactory.CreateClient());
	}

	public async Task HandleIncomingCallAsync(IncomingCallModel model, CancellationToken ct = default)
	{
		string message = $"Incoming call from {model.DisplayRemote}[{model.CalledNumber}] to {model.ActiveUser}";

		var result = await _bot.SendTextMessageAsync(_options.Value.ChatId, message, disableNotification: true, cancellationToken: ct);

		var cacheEntry = new CallCacheEntry(model.ActiveUser, model.CalledNumber, result.MessageId);
		_cache.Set(cacheEntry, TimeSpan.FromSeconds(30));
	}

	public async Task HandleMissedCallAsync(MissedCallModel model, CancellationToken ct = default)
	{
		string message = $"Missed call from {model.DisplayRemote}[{model.CalledNumber}] to {model.ActiveUser}";

		if (_cache.TryGetValue(new CallCacheEntry(model.ActiveUser, model.CalledNumber), out CallCacheEntry? entry))
		{
			await _bot.EditMessageTextAsync(new ChatId(_options.Value.ChatId), entry!.MessageId!.Value, message, cancellationToken: ct);
			_cache.Remove(entry.Key);
		}
		else
		{
			await _bot.SendTextMessageAsync(new ChatId(_options.Value.ChatId), message, disableNotification: true, cancellationToken: ct);
		}
	}

	public async Task HandleAnsweredCallAsync(AnsweredCallModel model, CancellationToken ct = default)
	{
		if (_cache.TryGetValue(new CallCacheEntry(model.ActiveUser, model.CalledNumber), out CallCacheEntry? entry))
		{
			await _bot.DeleteMessageAsync(_options.Value.ChatId, entry!.MessageId!.Value, ct);
			_cache.Remove(entry);
		}
	}
}