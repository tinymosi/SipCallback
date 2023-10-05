namespace SipCallback.Yealink.Models;

public record MissedCall(int CalledNumber, int ActiveUser) : Call(CalledNumber, ActiveUser);