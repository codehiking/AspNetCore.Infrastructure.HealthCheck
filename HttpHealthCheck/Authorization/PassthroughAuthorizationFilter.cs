using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HttpHealthCheck.Authorization
{
    public class PassthroughAuthorizationFilter : IAuthorizationFilter
    {
        public Task<bool> FilterAsync(HttpContext ctx) => Task.FromResult(true);
    }
}