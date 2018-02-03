using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Infrastructure.HealthCheck.Http {
    public static class HttpHealthService {
        public static EnumHttpHealthCheck<Health> Default = new EnumHttpHealthCheck<Health> (Health.Initializing, Health.Up);
    }
}