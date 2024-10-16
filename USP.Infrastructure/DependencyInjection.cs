using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Entities;
using USP.Application.Common.Interfaces;
using USP.Infrastructure.Services;

namespace USP.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetSection("MongoDbConfiguration");

        var db = conn.GetSection("DbName").Value!;

        string conString = "mongodb+srv://lukaantic:1AnWTvqrKe9pqGwu@cluster2022240803.8jtoc.mongodb.net/";

        Task.Run(async () => { await DB.InitAsync(db, MongoClientSettings.FromConnectionString(conString)); })
            .GetAwaiter()
            .GetResult();
        
        services.AddSingleton<IProductService, ProductService>();
        services.AddSingleton<IUserService, UserService>();

        return services;
    }
}