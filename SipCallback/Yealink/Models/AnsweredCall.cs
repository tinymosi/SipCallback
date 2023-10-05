namespace SipCallback.Yealink.Models;

public record AnsweredCall(int CalledNumber, int ActiveUser) : Call(CalledNumber, ActiveUser);