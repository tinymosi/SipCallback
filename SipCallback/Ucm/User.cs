using LinqToLdap.Mapping;

namespace SipCallback.Ucm;

public class User
{
	public string Cn { get; init; } = null!;

	public int AccountNumber { get; init; }

	public string? FirstName { get; init; }

	public string? LastName { get; init; }

	public string EntryDn { get; init; } = null!;
}

public class UserMap : ClassMap<User>
{
	public override IClassMap PerformMapping(
		string? namingContext = null,
		string? objectCategory = null,
		bool includeObjectCategory = true,
		IEnumerable<string>? objectClasses = null,
		bool includeObjectClasses = true)
	{
		NamingContext("OU=pbx,DC=pbx,DC=com");
		ObjectClasses(new[] { "GsAccount", "GsSIPUser", "simpleSecurityObject" });
		DistinguishedName(x => x.EntryDn);

		Map(x => x.Cn)
			.Named("cn")
			.ReadOnly();

		Map(x => x.AccountNumber)
			.ReadOnly();

		Map(x => x.FirstName)
			.ReadOnly();

		Map(x => x.LastName)
			.ReadOnly();

		return this;
	}
}