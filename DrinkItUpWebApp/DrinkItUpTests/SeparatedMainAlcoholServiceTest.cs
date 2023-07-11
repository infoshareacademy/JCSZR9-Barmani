using DrinkItUpBusinessLogic;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
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
        private static readonly Container _container = new Container();

        [Fact]
        public async Task MainAlcoholService_AddMainAlcohol_ReturnAddedMainAlcohol()
        {
            //Assing
            var serviceContainer = _container;
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();
            var mainAlcoholDto = new MainAlcoholDto { Name = "Spirytus" };

            //Act
            var testMainAlcohol = await mainAlcoholService.AddMainAlcohol(mainAlcoholDto);

            //Assert
            testMainAlcohol.Name.Should().Be(mainAlcoholDto.Name);
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task MainAlcoholService_GetAll_ReturnsAllMA()
        {
            //Assign
            var serviceContainer = _container;
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
            var serviceContainer = _container;
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
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task MaService_GetById_ReturnEmptyMa()
        {
            //Assign
            var serviceContainer = _container;
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();

            //Act
            var result = await mainAlcoholService.GetById(1);

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().BeNull();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task MAService_GetByName_ReturnsMAByName()
        {
            //Assing
            var serviceContainer = _container;
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
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task MAServices_IsMAUsed_ReturnsFalse()
        {
            //Assign
            var serviceContainer = _container;
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();
            var mainAlcoholDto = new MainAlcoholDto { Name = "Spirytus" };

            //Act
            var result = await mainAlcoholService.IsMainAlcoholUsed(1);

            //Assert
            result.Should().BeFalse();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task MAServices_IsMAUnique_ReturnsFalse()
        {
            //Assing
            var serviceContainer = _container;
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
            var result = await mainAlcoholService.IsMainAlcoholUnique("Gin");

            //Assert
            result.Should().BeFalse();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task MAServices_Remove_ReturnsCorrectMAAmount()
        {
            //Assing
            var serviceContainer = _container;
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
            await mainAlcoholService.Remove(3);
            var mainAlcohols = await mainAlcoholService.GetAll();

            //Assert
            mainAlcohols.Should().HaveCount(4);
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task MAServices_Update_ReturnsUpdatedMAName()
        {
            //Assing
            var serviceContainer = _container;
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();
            var mainAlcoholDtos = new List<MainAlcoholDto>
            {
                new MainAlcoholDto { Name = "Wodka" },
                new MainAlcoholDto { Name = "Gin" },
                new MainAlcoholDto { Name = "Rum" },
                new MainAlcoholDto { Name = "Likier" },
                new MainAlcoholDto { Name = "Spirytus" },
            };

            foreach (var item in mainAlcoholDtos)
            {
                await mainAlcoholService.AddMainAlcohol(item);
            }

            var mainAlcoholToUpdate = new MainAlcoholDto { MainAlcoholId = 1, Name = "Wódka" };
            serviceContainer.DetachModel();

            //Act
            await mainAlcoholService.Update(mainAlcoholToUpdate);
            var updatedMA = await mainAlcoholService.GetById(1);

            //Assert
            updatedMA.Name.Should().Be(mainAlcoholToUpdate.Name);
            serviceContainer.EndOfTest();
        }
    }
}