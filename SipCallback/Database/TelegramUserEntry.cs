namespace SipCallback.Database;

public class TelegramUserEntry
{
    public int Id { get; init; }
    public string? TelegramUserId { get; init; }
    public string ChatId { get; init; } = null!;
    public string? TelegramHandle { get; init; }
    public string? Name { get; init; }
    public int PhonebookEntryId { get; init; }
}