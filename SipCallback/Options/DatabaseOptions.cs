using FluentValidation;

namespace SipCallback.Options;

public class DatabaseOptions
{
    public const string SectionName = "Database";

    public string Server { get; init; } = null!;

    public int Port { get; init; }

    public string DbName { get; init; } = null!;

    public string Username { get; init; } = null!;

    public string Password { get; init; } = null!;
}

public class DatabaseOptionsValidator : AbstractValidator<DatabaseOptions>
{
    public DatabaseOptionsValidator()
    {
        RuleFor(x => x.Server).NotEmpty();
        RuleFor(x => x.Port).NotEmpty();
        RuleFor(x => x.DbName).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}