using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CodeHike.Infrastructure.HealthCheck.Http
{
    public class HttpHealthCheck : IHttpHealthService
    {
        private readonly Health _healthy;
        private volatile Health _health;

        public HttpHealthCheck (Health initial, Health healthy)
        {
            _healthy = healthy;
            _health = initial;
        }

        public bool IsHealthy ()
        {
            return _healthy.Equals (_health);
        }

        public void ToggleState (Health health)
        {
            _health = health;
        }

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