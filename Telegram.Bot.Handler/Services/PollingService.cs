using Telegram.Bot.Handler.Abstract;

namespace Telegram.Bot.Handler.Services;

public class PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
    : PollingServiceBase<ReceiverService>(serviceProvider, logger);