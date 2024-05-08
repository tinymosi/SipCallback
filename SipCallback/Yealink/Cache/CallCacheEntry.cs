namespace SipCallback.Yealink.Cache;

public record CallCacheEntry(int Caller, int Callee, string? Username = null, int? MessageId = null)
{
    public string Key => $"call:{Caller}-{Callee}";
}