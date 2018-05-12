using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HttpHealthCheck.Authorization
{
    public interface IAuthorizationRepository
    {
        Task<bool> IsExistingIdentityAsync(string identity);
    }
}