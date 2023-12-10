#region

using Microsoft.Extensions.Configuration;

#endregion

namespace TheHub.Web.AcceptanceTests;

public static class ConfigurationHelper
{
    private static readonly IConfiguration _configuration;

    private static string? _baseUrl;

    static ConfigurationHelper()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
    }

    public static string GetBaseUrl()
    {
        if (_baseUrl == null)
        {
            _baseUrl = _configuration["BaseUrl"];

            ArgumentNullException.ThrowIfNull(_baseUrl);

            _baseUrl = _baseUrl.TrimEnd('/');
        }

        return _baseUrl;
    }
}
