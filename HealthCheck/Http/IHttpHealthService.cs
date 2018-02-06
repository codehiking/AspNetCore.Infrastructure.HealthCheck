using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CodeHike.Infrastructure.HealthCheck.Http
{
    /// <summary>
    /// Defines a contract to handle health over HTTP requests.
    /// </summary>
    public interface IHttpHealthService
    {
        /// <summary>
        /// Checker whether the service is healty or not.
        /// </summary>
        bool IsHealthy ();

        /// <summary>
        /// Handles a put request in order to change the current state.
        /// </summary>
        Task HttpPutRequestReceived (HttpContext httpContext);

        /// <summary>
        /// Gets a string representation of the current health.
        /// </summary>
        string Health { get; }
    }
}