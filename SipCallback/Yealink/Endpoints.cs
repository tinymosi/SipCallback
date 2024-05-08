using SipCallback.Yealink.Models;

namespace SipCallback.Yealink;

public static class Endpoints
{
    public static WebApplication MapYealinkEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/help.xml");

        group.MapGet("/answered/{from:int}/{to:int}", AnsweredCallHandler);
        group.MapGet("/incoming/{from:int}/{to:int}", IncomingCallHandler);
        group.MapGet("/missed/{from:int}/{to:int}", MissedCallHandler);

        return app;

        async Task<IResult> MissedCallHandler(int from, int to, TelegramNotificationHandler handler,
            CancellationToken ct)
        {
            var call = new MissedCall(from, to);

            await handler.HandleMissedCallAsync(call, ct);

            return TypedResults.Ok(new { result = "ok" });
        }

        async Task<IResult> AnsweredCallHandler(int from, int to, TelegramNotificationHandler handler,
            CancellationToken ct)
        {
            var call = new AnsweredCall(from, to);

            await handler.HandleAnsweredCallAsync(call, ct);

            return TypedResults.Ok(new { result = "ok" });
        }

        async Task<IResult> IncomingCallHandler(int from, int to, TelegramNotificationHandler handler,
            CancellationToken ct)
        {
            var call = new IncomingCall(from, to);

            await handler.HandleIncomingCallAsync(call, ct);

            return TypedResults.Ok(new { result = "ok" });
        }
    }
}