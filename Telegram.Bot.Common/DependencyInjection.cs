using IL.FluentValidation.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Common.Options;

namespace Telegram.Bot.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddTelegramBotCommon(this IServiceCollection services)
    {
        services.AddOptions<TelegramOptions>()
            .BindConfiguration(TelegramOptions.SectionName)
            .FluentValidate().With<TelegramOptionsValidator>()
            .ValidateOnStart();

        return services;
    }
}