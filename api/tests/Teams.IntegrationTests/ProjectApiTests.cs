using MassTransit.Initializers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Teams.API.Features.Projects;
using Teams.API.Features.Projects.GetAllProjects;
using Teams.API.Features.Users;
using Teams.Infrastructure;
using Testcontainers.MsSql;
using Testcontainers.RabbitMq;

namespace Teams.IntegrationTests;

public class IntegrationTests : IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    private readonly RabbitMqContainer _rabbitMqContainer = new RabbitMqBuilder("rabbitmq:3.11")
        .WithPortBinding(5672)
        .WithUsername("guest")
        .WithPassword("guest")
        .Build();

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();
        await _rabbitMqContainer.StartAsync();
    }

    public Task DisposeAsync()
    {
        return Task.WhenAll(
            _msSqlContainer.DisposeAsync().AsTask(),
            _rabbitMqContainer.DisposeAsync().AsTask()
        );
    }

    public sealed class ProjectApiTests : IClassFixture<IntegrationTests>, IDisposable
    {
        private readonly WebApplicationFactory<Program> _webApplicationFactory;

        private readonly HttpClient _httpClient;

        public ProjectApiTests(IntegrationTests fixture)
        {
            var clientOptions = new WebApplicationFactoryClientOptions();
            clientOptions.AllowAutoRedirect = false;

            _webApplicationFactory = new CustomWebApplicationFactory(fixture);
            _httpClient = _webApplicationFactory.CreateClient(clientOptions);
        }

        public void Dispose()
        {
            _webApplicationFactory.Dispose();
        }

        private sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
        {
            private readonly string _connectionString;

            public CustomWebApplicationFactory(IntegrationTests fixture)
            {
                _connectionString = fixture._msSqlContainer.GetConnectionString();
            }

            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
                builder.ConfigureServices(services =>
                {
                    services.Remove(services.SingleOrDefault(service => typeof(DbContextOptions<TeamDbContext>) == service.ServiceType));
                    // services.Remove(services.SingleOrDefault(service => typeof(DbConnection) == service.ServiceType));
                    services.AddDbContext<TeamDbContext>((_, option) => option.UseSqlServer(_connectionString));

                    var serviceProvider = services.BuildServiceProvider();
                    using var scope = serviceProvider.CreateScope();
                    var dbContext = scope.ServiceProvider.GetService<TeamDbContext>();
                    // Non-async EnsureCreated to account for non-async UseSeeding.
                    dbContext.Database.EnsureCreated();

                    services.RemoveAll<IAuthenticationSchemeProvider>();
                    services.RemoveAll<IAuthenticationHandlerProvider>();
                    services.RemoveAll<IConfigureOptions<AuthenticationOptions>>();
                    services.AddAuthentication(defaultScheme: "TestScheme")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                            "TestScheme", options => { });
                });
            }
        }

        [Fact]
        public async Task Get_Projects()
        {
            var client = _httpClient;

            var response = await client.GetAsync("/api/projects");
            var content = await response.Content.ReadFromJsonAsync<GetAllProjectsResponse>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(content.Projects);
        }

        [Fact]
        public async Task Get_Recommendations_AuthenticationSuccess()
        {
            var client = _httpClient;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            var response = await client.GetAsync("/api/users/me/recommendations");

            var content = await response.Content.ReadFromJsonAsync<GetRecommendationsResponse>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(content.Items);
        }

        [Fact]
        public async Task Get_Project_CopiesExist()
        {
            var client = _httpClient;

            using var scope = _webApplicationFactory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TeamDbContext>();

            var projectId = await dbContext.Projects
                .SingleOrDefaultAsync(p => p.Name.Equals("ARCS: RecyCOOL"))
                .Select(p => p.Id);

            var response = await client.GetAsync($"/api/projects/{projectId}");
            var content = await response.Content.ReadFromJsonAsync<GetProjectById.Response>();

            Assert.NotEmpty(content.CopyIds);
        }
    }
}
