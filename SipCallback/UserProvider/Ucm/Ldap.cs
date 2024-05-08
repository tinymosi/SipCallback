using LinqToLdap;

namespace SipCallback.UserProvider.Ucm;

public class Ldap : IPhoneBookLookup
{
    private readonly IDirectoryContext _context;

    public Ldap(IDirectoryContext context)
    {
        _context = context;
    }

    public Task<IPhoneBookEntry?> FindAsync(int accountNumber)
    {
        return _context.Query<User>().FirstOrDefault(x => x.AccountNumber == accountNumber);
    }
}