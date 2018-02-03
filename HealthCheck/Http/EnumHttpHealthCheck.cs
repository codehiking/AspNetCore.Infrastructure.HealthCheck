using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Infrastructure.HealthCheck.Http {
    /// <summary>
    /// Implements both a health check based on an enumeration provided by the user of the class and a Http
    /// </summary>
    /// <typeparam name="T">The enum type you want to use to represent health stats (Up, Down, Initializing, ...)</typeparam>
    public class EnumHttpHealthCheck<T> : IHttpHealthService where T : struct, IComparable, IFormattable, IConvertible {
        private readonly T _healtyState;
        private T _state;

        public static EnumHttpHealthCheck<Health> Default = new EnumHttpHealthCheck<Health> (Health.Initializing, Health.Up);

        public EnumHttpHealthCheck (T initialState, T healtyState) {
            _healtyState = healtyState;
            _state = initialState;
        }

        public bool IsHealthy () {
            return _healtyState.Equals (_state);
        }

        public void ToggleState (T state) {
            _state = state;
        }

        public Task HttpPutRequestReceived (HttpContext ctx) {
            string bodyStr;
            using (StreamReader reader = new StreamReader (ctx.Request.Body, Encoding.UTF8, true, 1024, true)) {
                bodyStr = reader.ReadToEnd ();
            }

            T state;

            if (Enum.TryParse<T> (bodyStr, true, out state)) {
                ToggleState (state);
            }

            return Task.CompletedTask;
        }
    }
}