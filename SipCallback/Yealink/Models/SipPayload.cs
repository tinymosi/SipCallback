namespace SipCallback.Yealink.Models;

public record SipPayload
{
	/// <summary> The MAC address of the IP phone. </summary>
	// [FromQuery(Name = "mac")]
	public string? Mac { get; set; }

	/// <summary> The IP address of the IP phone. </summary>
	// [FromQuery(Name = "ip")]
	public string? Ip { get; set; }

	/// <summary> The IP phone model. </summary>
	// [FromQuery(Name = "model")]
	public string? Model { get; set; }

	/// <summary> The firmware version of the IP phone. </summary>
	// [FromQuery(Name = "firmware")]
	public string? Firmware { get; set; }

	/// <summary>
	/// The SIP URI of the current account when the IP phone places a call,
	/// receives an incoming call or establishes a call.
	/// </summary>
	// [FromQuery(Name = "activeUrl")]
	public string? ActiveUrl { get; set; }

	/// <summary>
	/// The user part of the SIP URI for the current account
	/// when the IP phone places a call, receives an incoming
	/// call or establishes a call.
	/// </summary>
	// [FromQuery(Name = "activeUser")]
	public string? ActiveUser { get; set; }

	/// <summary>
	/// The host part of the SIP URI for the current account
	/// when the IP phone places a call, receives an incoming
	/// call or establishes a call.
	/// </summary>
	// [FromQuery(Name = "activeHost")]
	public string? ActiveHost { get; set; }

	/// <summary>
	/// The SIP URI of the caller when the IP phone places a call.
	/// The SIP URI of the callee when the IP phone receives an incoming cal.
	/// </summary>
	// [FromQuery(Name = "local")]
	public string? Local { get; set; }

	/// <summary>
	/// The SIP URI of the callee when the IP phone places a call.
	/// The SIP URI of the caller when the IP phone receives an incoming call.
	/// </summary>
	// [FromQuery(Name = "remote")]
	public string? Remote { get; set; }

	/// <summary>
	/// The display name of the caller when the IP phone places a call.
	/// The display name of the callee when the IP phone receives an incoming call.
	/// </summary>
	// [FromQuery(Name = "displayLocal")]
	public string? DisplayLocal { get; set; }

	/// <summary>
	/// The display name of the callee when the IP phone places a call.
	/// The display name of the caller when the IP phone receives an incoming call.
	/// </summary>
	// [FromQuery(Name = "displayRemote")]
	public string? DisplayRemote { get; set; }

	/// <summary> The call-id of the active call. </summary>
	// [FromQuery(Name = "callId")]
	public string? CallId { get; set; }

	/// <summary> The display name of the caller when the IP phone receives an incoming call. </summary>
	// [FromQuery(Name = "callerId")]
	public string? CallerId { get; set; }

	/// <summary> The phone number of the callee when the IP phone places a call. </summary>
	// [FromQuery(Name = "calledNumber")]
	public string? CalledNumber { get; set; }

	public static ValueTask<SipPayload> BindAsync(HttpContext context)
	{
		var query = context.Request.Query;
		var route = context.Request.RouteValues;

		var result = new SipPayload
		{
			Mac = query[nameof(Mac)],
			Ip = query[nameof(Ip)],
			Model = query[nameof(Model)],
			Firmware = query[nameof(Firmware)],
			ActiveUrl = query[nameof(ActiveUrl)],
			ActiveUser = query[nameof(ActiveUser)],
			ActiveHost = query[nameof(ActiveHost)],
			Local = query[nameof(Local)],
			Remote = query[nameof(Remote)],
			DisplayLocal = route[nameof(DisplayLocal)] as string,
			DisplayRemote = route[nameof(DisplayRemote)] as string,
			CallId = query[nameof(CallId)],
			CallerId = route[nameof(CallerId)] as string,
			CalledNumber = query[nameof(CalledNumber)],
		};

		return ValueTask.FromResult(result);
	}
}