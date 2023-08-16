using IL.FluentValidation.Extensions.Options;
using SipCallback;
using SipCallback.Options;
using SipCallback.Yealink;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddMemoryCache()
	.AddHttpClient()
	.AddProblemDetails()
	.Configure<RouteHandlerOptions>(options => options.ThrowOnBadRequest = true)
	.AddScoped<CallHandler>();

builder.Services.AddOptions<TelegramOptions>()
	.BindConfiguration(TelegramOptions.SectionName)
	.FluentValidate().With<TelegramOptionsValidator>()
	.ValidateOnStart();

builder.WebHost
	.UseKestrel(options => options.ConfigureEndpointDefaults(listenOptions => listenOptions.UseConnectionLogging()));

var app = builder.Build();

app
	.UseDeveloperExceptionPage()
	.UseStatusCodePages(async statusCodeContext
		=> await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
			.ExecuteAsync(statusCodeContext.HttpContext))
	.UseExceptionHandler(exceptionHandlerApp
		=> exceptionHandlerApp.Run(async context
			=> await Results.Problem()
				.ExecuteAsync(context)));

app.MapYealinkEndpoints();

app.Run();