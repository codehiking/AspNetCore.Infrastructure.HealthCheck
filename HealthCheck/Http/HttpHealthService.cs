using System;
using System.Collections.Generic;
using System.Text;

namespace CodeHike.Infrastructure.HealthCheck.Http
{
    public static class HttpHealthService
    {
        internal static HttpHealthCheck Default = new HttpHealthCheck (Health.Initializing, Health.Up);
        public static void ToggleState(Health health)
        {
            Default.ToggleState(health);
        }
    }
}