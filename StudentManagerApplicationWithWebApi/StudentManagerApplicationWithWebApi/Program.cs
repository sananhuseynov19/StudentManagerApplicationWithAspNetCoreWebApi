using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using StudentManagerApplicationWithWebApi.Data;
using StudentManagerApplicationWithWebApi.Repository;
using StudentManagerApplicationWithWebApi.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using StudentManagerApplicationWithWebApi.Data.Entities;
using StudentManagerApplicationWithWebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using StudentManagerApplicationWithWebApi.RequirementsHandler;
using StudentManagerApplicationWithWebApi.Requirments;

var builder = WebApplication.CreateBuilder(args);
try
{
    Log.Information("Starting Web Host");
    WebApplication.CreateBuilder(args);
}
catch(Exception exc)
{
    Log.Fatal(exc, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}



Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsof", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.File(
    System.IO.Path.Combine("D:\\LogFiles","ApplicationLog","Diognastics.txt"),
    rollingInterval:RollingInterval.Day,
    fileSizeLimitBytes:10*1024*1024,
    retainedFileCountLimit:30,
    rollOnFileSizeLimit:true,
    flushToDiskInterval:TimeSpan.FromSeconds(2))
        .CreateLogger();

builder.Host.UseSerilog();


// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWtToken:JwtKey"])),
        ValidateIssuerSigningKey=true,
        ValidateLifetime=true


    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SananOnly", policy => policy.RequireClaim(ClaimTypes.Role,"Sanan"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanUpdate", policy => policy.AddRequirements(new StudentUpdateRequirement()));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddTransient(typeof(IRepository<>), typeof(EFRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IAuthorizationHandler, IsAdminHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, AgeHandler>();

    


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<AuthMiddleware>();

app.MapControllers();

app.Run();
