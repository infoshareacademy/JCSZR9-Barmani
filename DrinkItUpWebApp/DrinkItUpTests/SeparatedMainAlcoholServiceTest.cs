using DrinkItUpBusinessLogic;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
using DrinkItUpWebApp.DAL.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public async Task MainAlcoholService_AddMainAlcohol_ReturnMultipleAddedMA()
        {
            //Assing
            var serviceContainer = _container;
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();
            int expectedMANamesAmount = 500;
            int maxMANameCharactersLenght = 25;
            for (int i = 0;  i < expectedMANamesAmount; i++) 
            {
                var itemsToAdd = RandomValues.RandomNameGenerator(maxMANameCharactersLenght);
                var mainAlcoholDto = new MainAlcoholDto { Name = itemsToAdd };

                //Act
                var testMainAlcohols = await mainAlcoholService.AddMainAlcohol(mainAlcoholDto);
            }

            var result = await mainAlcoholService.GetAll();

            //Assert
            result.Should().HaveCount(expectedMANamesAmount);
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

        [Fact]
        public async Task MAServices_Add_EmptyMADtoThrowsException()
        {
            //Assing
            var serviceContainer = _container;
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();
            var mainAlcoholDto = new MainAlcoholDto { Name = "" };

            //Act
            Func<Task> func = async () => await mainAlcoholService.AddMainAlcohol(mainAlcoholDto);

            //Assert
            await func.Should().ThrowAsync<Exception>();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task MAServices_GetAll_ReturnsEmptyList()
        {
            //Assign
            var serviceContainer = _container;
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();

            //Act
            var result = await mainAlcoholService.GetAll();

            //Assert
            result.Should().BeNullOrEmpty();
            result.Count.Should().Be(0);
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task MAServices_IsMAUnique_EmptyStringThrowsExeption()
        {
            //Assing
            var serviceContainer = _container;
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();

            //Act
            Func<Task> func = async () => await mainAlcoholService.IsMainAlcoholUnique(string.Empty);

            //Assert
            await func.Should().ThrowAsync<Exception>();
            serviceContainer.EndOfTest();

        }

        [Fact]
        public async Task MAServices_Update_EmptyMANameThrowsExeption()
        {
            //Assing
            var serviceContainer = _container;
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();
            var item = new MainAlcoholDto { Name = "Wodka" };
            await mainAlcoholService.AddMainAlcohol(item);

            var mainAlcoholToUpdate = new MainAlcoholDto { MainAlcoholId = 1, Name = "" };
            serviceContainer.DetachModel();

            //Act
            Func<Task> func = async () => await mainAlcoholService.Update(mainAlcoholToUpdate);

            //Assert
            await func.Should().ThrowAsync<Exception>();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task MAServices_Remove_IdIsNotInDBReturnsFalse()
        {
            //Assing
            var serviceContainer = _container;
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();
            var item = new MainAlcoholDto { Name = "Wódka" };
            await mainAlcoholService.AddMainAlcohol(item);

            //Act
            var result = await mainAlcoholService.Remove(8);
            var mainAlcohols = await mainAlcoholService.GetAll();

            //Assert
            mainAlcohols.Should().HaveCount(1);
            result.Should().BeFalse();
            serviceContainer.EndOfTest();
        }


        [Fact]
        public async Task MAServices_Update_ReturnsMultipleUpdatedMANames()
        {

            //Assing
            var serviceContainer = _container;
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();
            int expectedMANamesAmount = 500;
            int maxMANameCharactersLenght = 25;

            for (int i = 0; i < expectedMANamesAmount; i++)
            {
                var itemsToAdd = RandomValues.RandomNameGenerator(maxMANameCharactersLenght);
                var mainAlcoholDto = new MainAlcoholDto { Name = itemsToAdd };
                await mainAlcoholService.AddMainAlcohol(mainAlcoholDto);
            }

            //Act
            var mainAlcoholDtos = await mainAlcoholService.GetAll();

            for (int i = 1; i <= expectedMANamesAmount; i++)
            {
                var mainAlcoholsToUpdate = new MainAlcoholDto
                {
                    MainAlcoholId = i,
                    Name = RandomValues.RandomNameGenerator(maxMANameCharactersLenght),
                };
                serviceContainer.DetachModel();
                await mainAlcoholService.Update(mainAlcoholsToUpdate);
            }

            //Assert
            for (int i = 1; i <= expectedMANamesAmount; i++)
            {
                var updatedMA = await mainAlcoholService.GetById(i);
                var notUpdatedMA = mainAlcoholDtos.FirstOrDefault(id => id.MainAlcoholId == i);
                updatedMA.Name.Should().NotBe(notUpdatedMA.Name);
            }
            serviceContainer.EndOfTest();
        }
    }
}