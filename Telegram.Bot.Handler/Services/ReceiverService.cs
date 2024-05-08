using Telegram.Bot.Handler.Abstract;
using Telegram.Bot.Polling;

namespace Telegram.Bot.Handler.Services;

// Compose Receiver and UpdateHandler implementation
public class ReceiverService(
    ITelegramBotClient botClient,
    UpdateHandler updateHandler,
    ReceiverOptions receiverOptions,
    ILogger<ReceiverServiceBase<UpdateHandler>> logger)
    : ReceiverServiceBase<UpdateHandler>(botClient, updateHandler, receiverOptions, logger);