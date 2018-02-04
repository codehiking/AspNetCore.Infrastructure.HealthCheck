using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHike.Infrastructure.HealthCheck.Http {
    /// <summary>
    /// Provides extensions.
    /// </summary>
    public static class HealthCheckExtensions
    {
        /// <summary>
        /// Add HTTP health service to the provided <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="healthService"></param>
        public static IServiceCollection AddHttpHealthService (this IServiceCollection services, IHttpHealthService healthService)
        {
            return services.AddSingleton (healthService);
        }

        /// <summary>
        /// Add default HTTP health service to the provided <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="healthService"></param>
        public static IServiceCollection AddHttpHealthService (this IServiceCollection services)
        {
            return services.AddSingleton<IHttpHealthService>(HttpHealthService.Default);
        }

        /// <summary>
        /// Add HTTP base health check middleware to the <see cref="IApplicationBuilder"/> request execution pipe.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="path">The path to use as healhcheck route.</param>
        public static void UseHttpHealthCheck (this IApplicationBuilder builder, string path = "/status") {
            builder.Map (path, (app) => {
                app.Run ((ctx) => {
                    IHttpHealthService service = ctx.RequestServices.GetRequiredService<IHttpHealthService> ();

                    if (HttpMethod.Get.Method.Equals (ctx.Request.Method)) {
                        IHttpHealthService checker = ctx.RequestServices.GetRequiredService<IHttpHealthService> ();

                        if (!checker.IsHealthy ()) {
                            ctx.Response.StatusCode = 503;
                        }
                    } else if (HttpMethod.Put.Method.Equals (ctx.Request.Method)) {
                        if (service != null) {
                            service.HttpPutRequestReceived (ctx);
                        }
                    }

                    return Task.CompletedTask;
                });
            });
        }
    }
}