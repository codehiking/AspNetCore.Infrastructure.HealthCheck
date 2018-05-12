using HttpHealthCheck;
using HttpHealthCheck.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApiExample
{
    public class Startup
    {
        public Startup (IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            Logger = logger;
        }
        
        public IConfiguration Configuration { get; }
        
        public ILogger<Startup> Logger { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services)
        {
            // Get JWT token secret from configuration.
            string secret = Configuration.GetValue<string>("Secret");

            // Setting basic authorization layer.
            InMemoryAuthorizationRepository repository = new InMemoryAuthorizationRepository();
            repository.AddIdentity("1234567890");

            services.AddHttpHealthService(new JwtAuthorizationFilter(repository, secret));
            services.AddMvc ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment ())
            {
                app.UseDeveloperExceptionPage ();
            }

            app.UseHttpHealthCheck ();

            app.UseMvc ();

            HttpHealthService.ToggleState(Health.Up);

            Logger.LogInformation ("Application is ready.");
        }
    }
}