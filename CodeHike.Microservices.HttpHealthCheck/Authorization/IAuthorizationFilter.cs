using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CodeHike.Microservices.HttpHealthCheck.Authorization
{
    public interface IAuthorizationFilter
    {
        Task<bool> FilterAsync(HttpContext request);
    }
}