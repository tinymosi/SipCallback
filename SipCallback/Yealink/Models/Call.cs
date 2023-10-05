namespace SipCallback.Yealink.Models;

/// <summary>
/// </summary>
/// <param name="CalledNumber">The phone number of the callee when the IP phone places a call.</param>
/// <param name="ActiveUser">The user part of the SIP URI for the current account when the IP phone receives an incoming call.</param>
public record Call(int CalledNumber, int ActiveUser);