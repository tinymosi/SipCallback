using IL.FluentValidation.Extensions.Options;
using LinqToLdap;
using Microsoft.AspNetCore.Mvc;
using SipCallback.Options;
using SipCallback.Services;
using SipCallback.Ucm;
using SipCallback.Yealink;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<TelegramOptions>()
	.BindConfiguration(TelegramOptions.SectionName)
	.FluentValidate().With<TelegramOptionsValidator>()
	.ValidateOnStart();

builder.Services.AddOptions<LdapOptions>()
	.BindConfiguration(LdapOptions.SectionName)
	.FluentValidate().With<LdapOptionsValidator>()
	.ValidateOnStart();

builder.Services
	.AddMemoryCache()
	.AddHttpClient()
	.AddProblemDetails()
	.Configure<RouteHandlerOptions>(options => options.ThrowOnBadRequest = false)
	.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true)
	.AddScoped<TelegramNotificationHandler>();

builder.Services
	.AddSingleton<ILdapConfiguration, CustomLdapConfiguration>()
	.AddTransient<IDirectoryContext, CustomDirectoryContext>()
	.AddTransient<Ldap>();

builder.WebHost
	.UseKestrel(options => options.ConfigureEndpointDefaults(listenOptions => listenOptions.UseConnectionLogging()));

var app = builder.Build();

app
	.UseHttpLogging()
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