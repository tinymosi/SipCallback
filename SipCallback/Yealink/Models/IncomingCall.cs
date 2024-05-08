namespace SipCallback.Yealink.Models;

/// <inheritdoc cref="Call" />
public record IncomingCall(int CalledNumber, int ActiveUser) : Call(CalledNumber, ActiveUser);