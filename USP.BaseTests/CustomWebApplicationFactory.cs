using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MongoDB.Entities;

namespace USP.BaseTests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    public CustomWebApplicationFactory()
    {
        string conString = "mongodb+srv://lukaantic:1AnWTvqrKe9pqGwu@cluster2022240803.8jtoc.mongodb.net/";

        Task.Run(async () =>
            {
                await DB.InitAsync("UspBazaZaTestiranje",
                    MongoClientSettings.FromConnectionString(
                        conString));
            })
            .GetAwaiter()
            .GetResult();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(IHostedService));
        });
    }
}

