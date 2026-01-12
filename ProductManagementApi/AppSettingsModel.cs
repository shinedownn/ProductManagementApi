namespace ProductManagementApi
{
    // Genel kapsayıcı sınıf (Opsiyonel, ama düzenli tutar)
    public class AppSettingsModel
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public JwtSettings Jwt { get; set; }
        public LoggingSettings Logging { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }

    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
    }

    public class LoggingSettings
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; } // JSON'daki "Microsoft.AspNetCore" ile eşleşmesi için
    }
}
