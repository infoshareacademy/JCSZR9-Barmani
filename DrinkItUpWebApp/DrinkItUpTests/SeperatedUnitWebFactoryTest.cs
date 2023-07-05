using FluentAssertions;
using System.Net.Http.Json;
using System.Net;
using DrinkItUpWebApp.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DrinkItUpTests
{
    public class SeperatedUnitWebFactoryTest
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
            var unitSerialize = JsonConvert.SerializeObject(unit);

            var stringContent = new StringContent(unitSerialize,System.Text.Encoding.UTF8,"application/json");
            //HttpContent content = new JsonContent { Value = unit };

            // Act
            var response = 
            await _client.PostAsync("Unit/Create",stringContent);


            response.EnsureSuccessStatusCode();
            var createdUnit = response.Content;

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            createdUnit.Should().NotBeNull();
        }
    }
}