using System.Text;
using System.Text.Json;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Packs.Api.Auth;
using Packs.Api.Mapping;
using Packs.Application;
using Packs.Api.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;


var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["Jwt:Key"]!)),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        ValidateIssuer = true,
        ValidateAudience = true
    };
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy(AuthConstants.AdminUserPolicyName,
        p => p.RequireClaim(AuthConstants.AdminUserClaimName, "true"));
    x.AddPolicy(AuthConstants.TrustedMemberPolicyName,
       p => p.RequireAssertion(c =>
           c.User.HasClaim(m => m is { Type: AuthConstants.AdminUserClaimName, Value: "true" }) ||
           c.User.HasClaim(m => m is { Type: AuthConstants.TrustedMemberClaimName, Value: "true" })));
});

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1.0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
    x.ApiVersionReader = new MediaTypeApiVersionReader("api-version");
}).AddMvc().AddApiExplorer();

builder.Services.AddControllers();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(x => x.OperationFilter<SwaggerDefaultValues>());

builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        foreach (var description in app.DescribeApiVersions())
        {
            x.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName);
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ValidationMappingMiddleware>();

app.MapControllers();

app.Run();
