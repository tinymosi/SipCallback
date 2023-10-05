namespace SipCallback.Yealink.Models;

public record IncomingCall(int CalledNumber, int ActiveUser) : Call(CalledNumber, ActiveUser);