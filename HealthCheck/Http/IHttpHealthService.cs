using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Infrastructure.HealthCheck.Http {
    /// <summary>
    /// Defines a contract to handle health over HTTP requests.
    /// </summary>
    public interface IHttpHealthService {
        /// <summary>
        /// Checker whether the service is healty or not.
        /// </summary>
        bool IsHealthy ();

        Task HttpPutRequestReceived (HttpContext httpContext);
    }
}