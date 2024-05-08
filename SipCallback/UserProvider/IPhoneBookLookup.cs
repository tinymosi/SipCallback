namespace SipCallback.UserProvider;

public interface IPhoneBookEntry
{
    public int AccountNumber { get; init; }
    public string? LastName { get; init; }
    public string? FirstName { get; init; }
}

public interface IPhoneBookLookup
{
    public Task<IPhoneBookEntry?> FindAsync(int accountNumber);
}