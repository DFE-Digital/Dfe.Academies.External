namespace Dfe.Academies.External.Web.Security;

public static class SecureHeadersDefinitions
{
	private static readonly string[] DefaultSrcExclusions = ["wss://localhost:*/Dfe.Academies.External.Web/"];

	private static readonly string[] ScriptSrcExclusions =
	[
		"https://*.googletagmanager.com", "https://*.google-analytics.com",
		"https://js.monitor.azure.com/scripts/b/ext/ai.clck.2.8.18.min.js",
		"https://js.monitor.azure.com/scripts/b/ai.3.gbl.min.js"
	];

	private static readonly string[] ImageSrcExclusions =
	[
		"https://www.googletagmanager.com", "https://*.google-analytics.com"
	];

	private static readonly string[] ConnectSrcExclusions =
	[
		"https://js.monitor.azure.com/scripts/b/ai.config.1.cfg.json",
		"https://*.in.applicationinsights.azure.com/v2/track", "https://*.googletagmanager.com",
		"https://*.google-analytics.com"
	];

	public static HeaderPolicyCollection GetHeaderPolicyCollection()
	{
		HeaderPolicyCollection policy = new HeaderPolicyCollection()
			.AddFrameOptionsDeny()
			.AddXssProtectionBlock()
			.AddContentTypeOptionsNoSniff()
			.AddReferrerPolicyNoReferrer()
			.RemoveServerHeader()
			.AddCrossOriginOpenerPolicy(builder =>
			{
				builder.SameOrigin();
			})
			.AddCrossOriginEmbedderPolicy(builder =>
			{
				builder.RequireCorp();
			})
			.AddCrossOriginResourcePolicy(builder =>
			{
				builder.SameOrigin();
			})
			.AddContentSecurityPolicy(builder =>
			{
				builder.AddDefaultSrc().Self().From(DefaultSrcExclusions);
				builder.AddScriptSrc().Self().WithNonce().From(ScriptSrcExclusions);
				builder.AddStyleSrc().Self();
				builder.AddFontSrc().Self();
				builder.AddImgSrc().Self().From(ImageSrcExclusions);
				builder.AddFrameSrc().Self();
				builder.AddConnectSrc().Self().From(ConnectSrcExclusions);
			})
			.AddPermissionsPolicy(builder =>
			{
				builder.AddAccelerometer().None();
				builder.AddCamera().None();
				builder.AddGeolocation().None();
				builder.AddGyroscope().None();
				builder.AddMagnetometer().None();
				builder.AddMicrophone().None();
				builder.AddPayment().None();
				builder.AddUsb().None();
			});

		return policy;
	}
}
