using System.Diagnostics;
using System.Text;

namespace ASPNetCoreIntro
{
    public static class ConfigSample
    {
        public static async Task AppSettings(HttpContext context, IConfigurationRoot config)
        {
            string? settings = config["AppSettings:SiteName"];
            Debug.Assert(settings != null);
            await context.Response.WriteAsync(settings.Div());
        }

        public static async Task ReadDatabaseConnection(HttpContext context, IConfigurationRoot config) {
            string? connectionString = config["Data:DefaultConnection:ConnectionString"];
            Debug.Assert(connectionString != null);
            await context.Response.WriteAsync(connectionString.Div());
        }

        public static async Task UserSecret(HttpContext context, IConfigurationRoot config)
        {
            string? secret = config["secret1"];
            if (string.IsNullOrEmpty(secret))
            {
                var sb = new StringBuilder();
                sb.Append(@"Use ""Manage User Secrets"" from the context menu while selecting the project".Div());
                sb.Append("And create the secret named 'secret1'".Div());
                await context.Response.WriteAsync(sb.ToString());
            }
            else
            { 
                await context.Response.WriteAsync(secret.Div());
            }
        }
    }
}
