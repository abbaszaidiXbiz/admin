
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;
global using admin.Collections;
global using admin.Models;
global using System.Net;
global using admin.Services;
using System.Diagnostics;
using System.Text;
using admin;
using admin.Services;
using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Account = CloudinaryDotNet.Account;

var builder = WebApplication.CreateBuilder(args);
const string CORS_ORIGINS = "CorsOrigins";

BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

#region MongoIdenity

var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
{
    MongoDbSettings = new MongoDbSettings()
    {
        ConnectionString = "mongodb://localhost:27017",
        DatabaseName = "admin"

    }
    ,
    IdentityOptionsAction = options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.User.RequireUniqueEmail = true;
    }
};

builder.Services.
    ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, Guid>(mongoDbIdentityConfig)
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddRoleManager<RoleManager<ApplicationRole>>();


builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    // ValidateIssuer = true,
                    // ValidateAudience = true,
                    // ValidIssuer = "",
                    IssuerSigningKey = new SymmetricSecurityKey
                    (
                            Encoding.UTF8.GetBytes("ba877439-8a16-441c-8031-abe3f0013872")
                    )



                };
            });

#endregion 

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", config => config
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins(builder.Configuration.GetSection(CORS_ORIGINS).Get<string[]>())
        .AllowCredentials());
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// In production, the Vite files will be served from this directory
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/dist";
});
#region Cloudinary

var cloudName = builder.Configuration.GetValue<string>("Cloudinary:CloudName");
var apiKey = builder.Configuration.GetValue<string>("Cloudinary:ApiKey");
var apiSecret = builder.Configuration.GetValue<string>("Cloudinary:ApiSecret");

if (new[] { cloudName, apiKey, apiSecret }.Any(string.IsNullOrWhiteSpace))
{
    throw new ArgumentException("Please specify Cloudinary account details!");
}

builder.Services.AddSingleton(new Cloudinary(new Account(cloudName, apiKey, apiSecret)));
#endregion
#region Mongo Database Configuration
builder.Services.Configure<MongoDBSetting>(
        builder.Configuration.GetSection(nameof(MongoDBSetting)));

builder.Services.AddSingleton<MongoClient>(sp =>
       new MongoClient(
               sp.GetRequiredService<IOptions<MongoDBSetting>>().Value.ConnectionURI));
builder.Services.AddSingleton<IMongoDatabase>(sp =>
                    {
                        var client = sp.GetRequiredService<MongoClient>();
                        return client
                        .GetDatabase(sp.GetRequiredService<IOptions<MongoDBSetting>>().Value.DatabaseName);

                    });
#endregion

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<FlightService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseSpaStaticFiles();


app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.UseSpa(spa =>
{
    if (app.Environment.IsDevelopment())
        spa.UseViteDevelopmentServer(sourcePath: "ClientApp");
});

app.Run();
