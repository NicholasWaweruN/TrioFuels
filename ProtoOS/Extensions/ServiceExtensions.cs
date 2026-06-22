using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace OnfonSms;

public static class OnfonServiceExtensions
{
    /// <summary>
    /// Registers OnfonSmsService with a typed HttpClient that injects the
    /// AccessKey header on every request.
    ///
    /// Usage in Program.cs:
    ///   builder.Services.AddOnfonSms(builder.Configuration);
    /// </summary>
    public static IServiceCollection AddOnfonSms(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<OnfonSettings>(configuration.GetSection(OnfonSettings.Section));

        services
            .AddHttpClient<ISmsService, OnfonSmsService>((sp, client) =>
            {
                var settings = sp.GetRequiredService<IOptions<OnfonSettings>>().Value;

                client.BaseAddress = new Uri(settings.BaseUrl);
                client.DefaultRequestHeaders.Add("AccessKey", settings.AccessKey);
            });

        return services;
    }
}
