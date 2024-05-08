using LinqToLdap;

namespace SipCallback.Services;

public class CustomDirectoryContext : DirectoryContext
{
    public CustomDirectoryContext(ILdapConfiguration ldapConfiguration) : base(ldapConfiguration)
    {
    }
}