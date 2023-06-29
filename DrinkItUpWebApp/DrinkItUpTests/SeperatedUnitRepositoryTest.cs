using DrinkItUpWebApp.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using FluentAssertions;
using System.Net.Http.Json;
using System.Net;
namespace DrinkItUpTests
{
    public class SeperatedUnitRepositoryTest
    {
        private readonly HttpClient _client;

        public SeperatedUnitRepositoryTest()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                            typeof(DbContextOptions<DrinkContext>));

                    services.Remove(descriptor);

                    services.AddDbContext<DrinkContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDatabase");
                    });

                });
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile(configPath);
                });
            });
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task GetAllUnits_ViewUnits_ShouldReturnView()
        {
            var response = await _client.GetAsync("Unit/Index");

            Assert.NotNull(response);
        }
    }
}