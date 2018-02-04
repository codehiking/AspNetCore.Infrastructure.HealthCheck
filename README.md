# HTTP Health checking for ASP.NET applications.

HTTP health checking is a common way to balance load amongs instances of the same HTTP service. Instances hosting the service are reponsible of exposing an HTTP route that will generally indicates their respective states through the use of HTTP status.

- An HTTP 2xx generally means that the service is up and running.
- An HTTP 5xx is used to indicates that the service is unavailable.

The HTTP response may or may not response with a content. Some load-balancers are designed to check for a specific response like "UP" in the content rather than checking the HTTP status code.

This repository gives an example of a ASP.NET Core Middleware used to expose the status of the HTTP service to the load-balancer located upstream of the HTTP traffic.

Here is how you can configure Dependency Injection for your application:

```csharp
public void ConfigureServices (IServiceCollection services)
{
    services.AddMvc();
    services.AddHttpHealthService();
}
```

Then, you need to add the middleware to the requests pipeline.

```csharp
// By default, "/status" will be trap by the middleware.
app.UseHttpHealthCheck();
app.UseMvc();
```

Finally, when the application is initialized, just toggle the global switch and you're done.

```csharp
// The API is now fully initialized.
HttpHealthService.ToggleState(Health.Up);
```

### Read the current status

```
curl -X GET -i http://localhost:5000/status
```

### Rolling upgrade scenario, change current state

Unavailaibility is not necessary caused by issues on the current service. It can also means that a rolling upgrade of the service is ongoing on a specific instance.

The middleware supports PUT operation.

```
curl -X PUT -i http://localhost:5000/status --data Down
```

### Testing the API

The default route is /status. It can be configured in the Startup.cs file this way:

```csharp
public void ConfigureServices (IServiceCollection services)
{
    services.AddMvc();
    services.AddHttpHealthService("/myhealthcheckroute");
}
```