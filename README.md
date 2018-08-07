# ASP.NET Core - HTTP ETag Middleware

[By Mozilla](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/ETag)

> The ETag HTTP response header is an identifier for a specific version of a resource. It allows caches to be more efficient, and saves bandwidth, as a web server does not need to send a full response if the content has not changed. 

## Configuration

The following code shows how to enable the Response ETag Middleware

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddResponseETag();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        app.UseResponseETag();
    }
}
