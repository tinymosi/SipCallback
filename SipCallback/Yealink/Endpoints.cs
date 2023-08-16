using SipCallback.Yealink.Models;

namespace SipCallback.Yealink;

public static class Endpoints
{
	public static WebApplication MapYealinkEndpoints(this WebApplication app)
	{
		var group = app.MapGroup("/help.xml");

		group.MapGet("/answered/{DisplayLocal}",
			async (AnsweredCallModel model, CallHandler handler, CancellationToken ct) => await handler.HandleAnsweredCallAsync(model, ct));

		group.MapGet("/incoming/{DisplayRemote}",
			async (IncomingCallModel model, CallHandler handler, CancellationToken ct) => await handler.HandleIncomingCallAsync(model, ct));

		group.MapGet("/missed/{DisplayRemote}",
			async (MissedCallModel model, CallHandler handler, CancellationToken ct) => await handler.HandleMissedCallAsync(model, ct));

		return app;
	}
}