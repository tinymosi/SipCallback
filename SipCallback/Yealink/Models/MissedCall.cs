namespace SipCallback.Yealink.Models;

/// <inheritdoc cref="Call"/>
public record MissedCall(int CalledNumber, int ActiveUser) : Call(CalledNumber, ActiveUser);