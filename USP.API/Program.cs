using FluentValidation.AspNetCore;
using USP.API.Filters;
using USP.API.Services;
using USP.Application;
using USP.Infrastructure;
using USP.Worker;

var builder = WebApplication.CreateBuilder(args);

// Kestrel configuration (if you want to customize the ports or other options)
builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(5041);  // HTTP
    options.ListenAnyIP(5042, listenOptions =>
    {
        listenOptions.UseHttps();  // HTTPS
    });
});

// Add services and middlewares...
builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilter>());
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IProductService, ProductService>();

builder.Services.AddHostedService<NotifyUserWorker>();
builder.Services.AddHostedService<UpdateProductEmbeddedWorker>();



builder.Services.AddCors(options =>
{
     options.AddPolicy("AllowAll",
         policy =>
         {
             policy.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
         });
});

var app = builder.Build();

app.UseCors("AllowAll"); // Use the policy defined above

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.MapControllers();
app.Run();


public partial class Program { }