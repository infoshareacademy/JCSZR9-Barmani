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
        private readonly HttpClient _client = new DrinktItUpWebFactory().GetClient();


        [Fact]
        public async Task GetAllUnits_ViewUnits_ShouldReturnView()
        {
            var response = await _client.GetAsync("Unit/Index");

            Assert.NotNull(response);
        }
    }
}