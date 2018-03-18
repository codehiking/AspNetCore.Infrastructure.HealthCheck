using CodeHike.Microservices.HttpHealthCheck;
using CodeHike.Microservices.HttpHealthCheck.Authorization;
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
            string secret = Configuration.GetValue<string>("Secret");

            InMemoryAuthorizationRepository repository = new InMemoryAuthorizationRepository();

            repository.AddIdentity("1234567890");

            services.AddSingleton<IAuthorizationFilter>(new JwtAuthorizationFilter(repository, secret));

            Logger.LogInformation($"Used secret: {secret}");

            services.AddMvc ();

            services.AddHttpHealthService ();
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

            HttpHealthService.ToggleState (Health.Up);

            Logger.LogInformation ("Application is ready.");
        }
    }
}