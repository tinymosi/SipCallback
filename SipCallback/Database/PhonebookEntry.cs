using SipCallback.UserProvider;

namespace SipCallback.Database;

public class PhonebookEntry : IPhoneBookEntry
{
    public int Id { get; init; }
    public int AccountNumber { get; init; }
    public string? LastName { get; init; }
    public string? FirstName { get; init; }
}