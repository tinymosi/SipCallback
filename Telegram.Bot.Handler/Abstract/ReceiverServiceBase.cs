using Telegram.Bot.Polling;

namespace Telegram.Bot.Handler.Abstract;

/// <summary>
///     An abstract class to compose Receiver Service and Update Handler classes
/// </summary>
/// <typeparam name="TUpdateHandler">Update Handler to use in Update Receiver</typeparam>
public abstract class ReceiverServiceBase<TUpdateHandler> : IReceiverService
    where TUpdateHandler : IUpdateHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<ReceiverServiceBase<TUpdateHandler>> _logger;
    private readonly ReceiverOptions _receiverOptions;
    private readonly IUpdateHandler _updateHandler;

    internal ReceiverServiceBase(
        ITelegramBotClient botClient,
        TUpdateHandler updateHandler,
        ReceiverOptions receiverOptions,
        ILogger<ReceiverServiceBase<TUpdateHandler>> logger)
    {
        _botClient = botClient;
        _updateHandler = updateHandler;
        _receiverOptions = receiverOptions;
        _logger = logger;
    }

    /// <summary>
    ///     Start to service Updates with provided Update Handler class
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    public async Task ReceiveAsync(CancellationToken stoppingToken)
    {
        // // ToDo: we can inject ReceiverOptions through IOptions container
        // var receiverOptions = new ReceiverOptions
        // {
        //     AllowedUpdates = Array.Empty<UpdateType>(),
        //     ThrowPendingUpdates = true
        // };

        var me = await _botClient.GetMeAsync(stoppingToken);
        _logger.LogInformation("Start receiving updates for {BotName}", me.Username ?? "My Awesome Bot");

        // Start receiving updates
        await _botClient.ReceiveAsync(
            _updateHandler,
            _receiverOptions,
            stoppingToken);
    }
}