using Microsoft.OpenApi.Models;
using Teams.API.Exceptions;
using Teams.API.Extensions;
using Teams.API.Services;
using Teams.API.Validation;
using Teams.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EditProjectPolicy", policy =>
        policy.Requirements.Add(new IsOwnerRequirement()));
    options.AddPolicy("ManageMembersPolicy", policy =>
        policy.Requirements.Add(new IsLeaderRequirement()));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    var scheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    var requirement = new OpenApiSecurityRequirement
    {
        { scheme, [] }
    };
    o.AddSecurityDefinition("Bearer", scheme);
    o.AddSecurityRequirement(requirement);
    
    // To avoid collisions of nested request/response classes
    // in the Swagger doc, e.g., Project.Request and User.Request.
    o.CustomSchemaIds(type => type.FullName!.Replace("+", "."));
});
builder.AddApplicationServices();
builder.Services.AddTransient<ValidationExceptionHandlingMiddleware>();
builder.Services.AddTeamsMessaging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
app.UseMiddleware<TeamsExceptionHandlerMiddleware>();

app.UseCors(option => option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

app.Run();