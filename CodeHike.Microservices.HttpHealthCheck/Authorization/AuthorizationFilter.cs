using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CodeHike.Microservices.HttpHealthCheck.Authorization
{
    public class JwtAuthorizationFilter : IAuthorizationFilter
    {
        public JwtAuthorizationFilter(IAuthorizationRepository repository, string secret)
        {
            _secret = secret;

            _repository = repository;
        }

        private readonly string _secret;

        private readonly IAuthorizationRepository _repository;
        
        public async Task<bool> FilterAsync(HttpContext ctx)
        {
            string authorization = ctx.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty (authorization))
            {
                // Not authorized
                ctx.Response.StatusCode = 403;
                return false;
            }
            
            string token = null;

            if (authorization.StartsWith ("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authorization.Substring ("Bearer ".Length).Trim ();
            }

            if (string.IsNullOrEmpty (token))
            {
                ctx.Response.StatusCode = 403;
                return false;
            }

            byte[] secretKey = System.Text.Encoding.UTF8.GetBytes(_secret);

            string json = Jose.JWT.Decode(token, secretKey);

            IDictionary<string, string> claims = Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, string>>(json);

            if( claims.ContainsKey("sub"))
            {
                return await _repository.IsExistingIdentityAsync(claims["sub"]);
            }

            return false;
        }
    }
}