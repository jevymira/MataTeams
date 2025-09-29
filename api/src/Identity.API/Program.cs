

using System.Text;
using EventBus;
using Identity.API.Data;
using Identity.API.Model;
using Identity.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseSeeding((context, _) =>
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        var testUser = context.Set<ApplicationUser>()
            .FirstOrDefault(u => u.UserName == "user");
        if (testUser == null)
        {
            testUser = new ApplicationUser()
            {
                UserName = builder.Configuration["SeedUser:UserName"],
                NormalizedUserName = builder.Configuration["SeedUser:UserName"]!.ToUpper(),
                Email = builder.Configuration["SeedUser:Email"],
                NormalizedEmail = builder.Configuration["SeedUser:Email"]!.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            // Underlying PasswordHasher used by ASP.NET Core Identity.
            testUser.PasswordHash = hasher
                .HashPassword(testUser, builder.Configuration["SeedUser:Password"]!);
            context.Set<ApplicationUser>().Add(testUser);
            context.SaveChanges();
        }

        Console.WriteLine(hasher
            .VerifyHashedPassword(testUser, testUser.PasswordHash, builder.Configuration["SeedUser:Password"]!)
            .ToString()
        );
    });
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<JwtManager>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,

        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"]))
    };
});
builder.Services.AddProducerRabbitMq();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(option => option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();