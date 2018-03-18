using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeHike.Microservices.HttpHealthCheck.Authorization
{
    public class InMemoryAuthorizationRepository : IAuthorizationRepository
    {
        private readonly IList<string> _validIdentities = new List<string>();

        public void AddIdentity(string identity)
        {
            _validIdentities.Add(identity);
        }

        public Task<bool> IsExistingIdentityAsync(string identity)
        {
            return Task.FromResult(_validIdentities.Contains(identity));
        }
    }
}