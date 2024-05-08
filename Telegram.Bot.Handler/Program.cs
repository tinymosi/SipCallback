using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Common;
using Telegram.Bot.Common.Options;
using Telegram.Bot.Handler.Services;
using Telegram.Bot.Polling;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTelegramBotCommon();

builder.Services.Configure<ReceiverOptions>(options =>
{
    options.AllowedUpdates = [];
    options.ThrowPendingUpdates = true;
});

builder.Services.AddScoped<UpdateHandler>();
builder.Services.AddScoped<ReceiverService>();
builder.Services.AddHostedService<PollingService>();

builder.Services.AddHttpClient("telegram_bot_client")
    .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
    {
        var telegramOptions = sp.GetService<IOptions<TelegramOptions>>()?.Value;

        ArgumentNullException.ThrowIfNull(telegramOptions);

        TelegramBotClientOptions options = new(telegramOptions.Token);
        return new TelegramBotClient(options, httpClient);
    });

var host = builder.Build();

await host.RunAsync();