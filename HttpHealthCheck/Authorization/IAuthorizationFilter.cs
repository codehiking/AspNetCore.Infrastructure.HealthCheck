using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HttpHealthCheck.Authorization
{
    public interface IAuthorizationFilter
    {
        Task<bool> FilterAsync(HttpContext request);
    }
}