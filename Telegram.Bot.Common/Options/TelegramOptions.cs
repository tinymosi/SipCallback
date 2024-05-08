using FluentValidation;

namespace Telegram.Bot.Common.Options;

public class TelegramOptions
{
    public const string SectionName = "Telegram";

    public string Token { get; init; } = null!;
}

public class TelegramOptionsValidator : AbstractValidator<TelegramOptions>
{
    public TelegramOptionsValidator()
    {
        RuleFor(x => x.Token).NotEmpty();
    }
}