using FluentAssertions;
using System.Net.Http.Json;
using System.Net;
using DrinkItUpWebApp.Models;

namespace DrinkItUpTests
{
    public class SeperatedUnitRepositoryTest
    {
        private readonly HttpClient _client = new DrinktItUpWebFactory().GetClient();


        [Fact]
        public async Task Index_ViewUnits_ShouldReturnView()
        {
            var response = await _client.GetAsync("Unit/Index");

            Assert.NotNull(response);
        }

        [Fact]
        public async Task CreateUnit_ReturnsCreatedResult()
        {
            // Arrange

            var unit = new UnitModel { Name = "jednostka"};

            // Act
            var response = await _client.PostAsJsonAsync<UnitModel>("Unit/Create", unit);
            response.EnsureSuccessStatusCode();
            var createdUnit = response.Content;

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            createdUnit.Should().NotBeNull();
        }
    }
}