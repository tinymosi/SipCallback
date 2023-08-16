namespace SipCallback.Yealink.Models;

public record MissedCallModel
{
	/// <summary> The display name of the caller when the IP phone receives an incoming call. </summary>
	public required string DisplayRemote { get; init; }

	/// <summary> The user part of the SIP URI for the current account when the IP phone receives an incoming call. </summary>
	public required int ActiveUser { get; init; }

	/// <summary> The phone number of the callee when the IP phone places a call. </summary>
	public required int CalledNumber { get; init; }

	public static ValueTask<MissedCallModel> BindAsync(HttpContext context)
	{
		var query = context.Request.Query;
		var route = context.Request.RouteValues;

		var result = new MissedCallModel
		{
			ActiveUser = int.Parse(query[nameof(ActiveUser)]),
			DisplayRemote = route[nameof(DisplayRemote)] as string,
			CalledNumber = int.Parse(query[nameof(CalledNumber)]),
		};

		return ValueTask.FromResult(result);
	}
}