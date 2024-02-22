namespace Dfe.Academies.External.Web.Security;

public static class SecureHeadersDefinitions
{
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
				builder.AddDefaultSrc().Self().From(new[]
				{
					"wss://localhost:*/Dfe.Academies.External.Web/", 
					"https://*.googletagmanager.com",
					"https://*.google-analytics.com"
				});
				builder.AddScriptSrc().Self().WithNonce().From(new []
				{
					"https://*.googletagmanager.com",
					"https://*.google-analytics.com"
				});
				builder.AddStyleSrc().Self();
				builder.AddFontSrc().Self();
				builder.AddImgSrc().Self().From("https://www.googletagmanager.com");
				builder.AddFrameSrc().Self();
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

