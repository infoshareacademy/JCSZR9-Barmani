using DrinkItUpBusinessLogic;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpWebApp.DAL.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpTests
{
    public class SeparatedMainAlcoholServiceTest
    {
        [Fact]
        public async Task MainAlcoholService_AddMainAlcohol_ReturnAddedMainAlcohol()
        {
            //Assing
            var serviceContainer = new Container();
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();
            var mainAlcoholDto = new MainAlcoholDto { Name = "Spirytus" };

            //Act
            var testMainAlcohol = await mainAlcoholService.AddMainAlcohol(mainAlcoholDto);

            //Assert
            testMainAlcohol.Name.Should().Be(mainAlcoholDto.Name);

            //Clearing Context
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task MainAlcoholService_GetAll_ReturnsAllMA()
        {
            //Assign
            var serviceContainer = new Container();
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();
            var mainAlcoholDtos = new List<MainAlcoholDto>
            {
                new MainAlcoholDto { Name = "Wódka" },
                new MainAlcoholDto { Name = "Gin" },
                new MainAlcoholDto { Name = "Rum" },
                new MainAlcoholDto { Name = "Likier" },
                new MainAlcoholDto { Name = "Spirytus" },
            };

            foreach (var item in mainAlcoholDtos)
            {
                await mainAlcoholService.AddMainAlcohol(item);
            }

            //Act
            var result = await mainAlcoholService.GetAll();
            
            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Count.Should().Be(5);

            serviceContainer.EndOfTest();

        }

        [Fact]
        public async Task MAService_GetById_ReturnsMAById()
        {
            //Assign
            var serviceContainer = new Container();
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();
            var mainAlcoholDtos = new List<MainAlcoholDto>
            {
                new MainAlcoholDto { Name = "Wódka" },
                new MainAlcoholDto { Name = "Gin" },
                new MainAlcoholDto { Name = "Rum" },
                new MainAlcoholDto { Name = "Likier" },
                new MainAlcoholDto { Name = "Spirytus" },
            };

            foreach (var item in mainAlcoholDtos)
            {
                await mainAlcoholService.AddMainAlcohol(item);
            }

            //Act
            var searchedMANameId = 5;
            var testedMAId = await mainAlcoholService.GetById(5);

            //Assert
            testedMAId.MainAlcoholId.Should().Be(searchedMANameId);
            testedMAId.Name.Should().NotBeNullOrEmpty();

            //Clearing Context
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task MAService_GetByName_ReturnsMAByName()
        {
            //Assing
            var serviceContainer = new Container();
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();
            var mainAlcoholDtos = new List<MainAlcoholDto>
            {
                new MainAlcoholDto { Name = "Wódka" },
                new MainAlcoholDto { Name = "Gin" },
                new MainAlcoholDto { Name = "Rum" },
                new MainAlcoholDto { Name = "Likier" },
                new MainAlcoholDto { Name = "Spirytus" },
            };

            foreach (var item in mainAlcoholDtos)
            {
                await mainAlcoholService.AddMainAlcohol(item);
            }

            //Act
            var searchedMAName = "Gin";
            var testedMAName = await mainAlcoholService.GetByName(searchedMAName);

            //Assert
            testedMAName.Name.Should().Be(searchedMAName);
            testedMAName.Name.Should().NotBeNullOrEmpty();

            //Clearing Context
            serviceContainer.EndOfTest();
        }
    }

    //IsMainAlcoholUsed

    //IsMainAlcoholUnique

    //Remove

    //Update
}