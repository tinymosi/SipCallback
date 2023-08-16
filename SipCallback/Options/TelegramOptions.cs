using FluentValidation;

namespace SipCallback.Options;

public class TelegramOptions
{
	public const string SectionName = "Telegram";

	public required string Token { get; set; }
	public required string ChatId { get; set; }
}

public class TelegramOptionsValidator : AbstractValidator<TelegramOptions>
{
	public TelegramOptionsValidator()
	{
		RuleFor(x => x.Token).NotEmpty();
		RuleFor(x => x.ChatId).NotEmpty();
	}
}