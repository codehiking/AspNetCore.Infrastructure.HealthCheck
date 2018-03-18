using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
namespace CodeHike.Microservices.HttpHealthCheck
{
    /// <summary>
    /// Implements a basic HTTP Health check.
    /// </summary>
    public class HttpHealthCheck : IHttpHealthService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHealthCheck"/> class.
        /// </summary>
        /// <param name="initial">Initial state.</param>
        /// <param name="healthy">Healthy state.</param>
        public HttpHealthCheck (Health initial, Health healthy)
        {
            _healthy = healthy;
            _health = initial;
        }
        ///<summary>
        /// Holds the healthy state.
        /// </summary>
        private readonly Health _healthy;
        ///<summary>
        /// Current state.
        /// </summary>
        private volatile Health _health;
        ///<summary>
        /// Gets a representation of the current health.
        /// </summary>
        public string Health => _health.ToString ();
        /// <summary>
        /// Gets a boolean indicating that the service is healthy.
        /// </summary>
        public bool IsHealthy => _healthy.Equals (_health);
        /// <summary>
        /// Changes the current state.
        /// </summary>
        /// <param name="health">new state.</param>
        public void ToggleState (Health health)
        {
            _health = health;
        }
        /// <summary>
        /// Handles a PUT request in order to change the current state.
        /// </summary>
        public Task HttpPutRequestReceived (HttpContext ctx)
        {
            string bodyStr;
            using (StreamReader reader = new StreamReader (ctx.Request.Body, Encoding.UTF8))
            {
                bodyStr = reader.ReadToEnd ();
            }
            Health health;
            if (Enum.TryParse<Health> (bodyStr, true, out health))
            {
                ToggleState (health);
            }
            return Task.CompletedTask;
        }
    }
}