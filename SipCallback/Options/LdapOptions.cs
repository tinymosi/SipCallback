using FluentValidation;

namespace SipCallback.Options;

public class LdapOptions
{
	public const string SectionName = "Ldap";

	public string Server { get; init; } = null!;
	public int Port { get; init; }
	public int ProtocolVersion { get; init; }
}

public class LdapOptionsValidator : AbstractValidator<LdapOptions>
{
	public LdapOptionsValidator()
	{
		RuleFor(x => x.Server).NotEmpty();
		RuleFor(x => x.Port).NotEmpty();
		RuleFor(x => x.ProtocolVersion).NotEmpty();
	}
}