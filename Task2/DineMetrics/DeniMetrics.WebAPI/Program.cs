using DeniMetrics.WebAPI.Configurations;
using DeniMetrics.WebAPI.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var builderConfig = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var configuration = builderConfig.Build();

builder.Configuration.AddConfiguration(configuration);

// Add services to the container.
builder.Services
    .AddInfrastructure()
    .AddCustomIdentity(configuration);

// Add Swagger services
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "DeniMetrics API",
        Description = "API documentation for DeniMetrics",
        Contact = new OpenApiContact
        {
            Name = "Support",
            Email = "support@denimetrics.com"
        }
    });

    // Optionally, add security definition for JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Authorization: Bearer {token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeniMetrics API v1");
        c.RoutePrefix = string.Empty; // Set Swagger at the root
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<ExceptionsHandlingMiddleware>();

app.MapControllers();

app.Run();
