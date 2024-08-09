using GameDashboard.Application.Exceptions;
using GameDashboard.Persistence;
using MongoDB.Driver;
using GameDashboard.Application;
using GameDashboard.Domain.Concrete.Identity;
using GameDashboard.Mapper;
using Microsoft.OpenApi.Models;
using GameDashboard.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddMapperService();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.MaxDepth = 1;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().AddNewtonsoftJson();

// Add services to the container.
builder.Services.AddProblemDetails();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddAuthorization(cfg =>
{
    cfg.AddPolicy("UserPolicy", policyBuilder => policyBuilder.RequireRole("User","USER"));

});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("Allow", policy =>
    {
        policy.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
    });
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IkProject API", Version = "v1", Description = "IkProject API swagger client." });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Allow");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();


app.UseExceptionHandler();

app.MapControllers();

app.Run();
