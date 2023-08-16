namespace SipCallback.Yealink.Cache;

public record CallCacheEntry(int Caller, int Callee, int? MessageId = null)
{
	public string Key => $"call:{Caller}-{Callee}";
}