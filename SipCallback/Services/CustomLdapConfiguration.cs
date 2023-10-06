using System.DirectoryServices.Protocols;
using LinqToLdap;
using Microsoft.Extensions.Options;
using SipCallback.Options;
using SipCallback.Ucm;

namespace SipCallback.Services;

public class CustomLdapConfiguration : LdapConfiguration
{
	public CustomLdapConfiguration(IOptions<LdapOptions> ldapOptions)
	{
		DisablePaging();
		AddMapping(new UserMap());

		ConfigureFactory(ldapOptions.Value.Server)
			.AuthenticateBy(AuthType.Anonymous)
			.UsePort(ldapOptions.Value.Port)
			.ProtocolVersion(ldapOptions.Value.ProtocolVersion);
	}
}