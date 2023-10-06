namespace SipCallback.Yealink.Models;

/// <inheritdoc cref="Call"/>
public record AnsweredCall(int CalledNumber, int ActiveUser) : Call(CalledNumber, ActiveUser);