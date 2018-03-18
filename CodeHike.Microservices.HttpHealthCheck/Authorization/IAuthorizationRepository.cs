using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CodeHike.Microservices.HttpHealthCheck.Authorization
{
    public interface IAuthorizationRepository
    {
        Task<bool> IsExistingIdentityAsync(string identity);
    }
}