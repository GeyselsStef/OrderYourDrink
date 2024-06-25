namespace OrderYourDrink.Configuration
{

    public static class CorsSettings
    {
        public static string AllowAll => "AllowAll";
        public static string AllowTest => "AllowTest";
        public static string AllowProd => "AllowProd";
    }

    public static class ConfigCORSExtensions
    {
            public static void AddCorsSettings(this IServiceCollection services)
            {
                services.AddCors(config =>
                {
                    config.AddPolicy(CorsSettings.AllowAll, policy =>
                    {
                        policy.AllowAnyOrigin();
                        policy.AllowAnyMethod();
                        policy.AllowAnyHeader();
                    });
                    config.AddPolicy(CorsSettings.AllowTest, policy =>
                    {
                        policy.SetIsOriginAllowedToAllowWildcardSubdomains();
                        policy.SetIsOriginAllowed(origin =>origin=="null" || new Uri(origin).Host == "localhost");
                        policy.WithOrigins("https://ashy-field-000da0603.5.azurestaticapps.net");
                        policy.AllowAnyMethod();
                        policy.AllowAnyHeader();
                    });
                    config.AddPolicy(CorsSettings.AllowProd, policy =>
                    {
                        policy.WithOrigins("https://proud-wave-071652803.5.azurestaticapps.net");
                        policy.AllowAnyMethod();
                        policy.AllowAnyHeader();
                    });
                });
        }
    }
}
