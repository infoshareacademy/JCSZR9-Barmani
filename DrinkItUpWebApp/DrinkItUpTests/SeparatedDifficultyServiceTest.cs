using DrinkItUpBusinessLogic.DTOs;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpTests
{
    public class SeparatedDifficultyServiceTest
    {
        private static readonly Container _container = new Container();

        [Fact]
        public async Task DifficultyService_AddDifficulty_ReturnAddedDifficultyName()
        {
            //Assign
            var serviceContainer = _container;
            var difficultyService = serviceContainer.GetDifficultyService();
            var difficultyDto = new DifficultyDto { Name = "Trudny" };

            //Act
            var testDifficulty = await difficultyService.AddDifficulty(difficultyDto);

            //Assert 
            testDifficulty.Name.Should().Be(difficultyDto.Name);
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task DifficultyService_GetById_ReturnDifficultyByID()
        {
            //Assign
            var serviceContainer = _container;
            var difficultyService = serviceContainer.GetDifficultyService();
            var difficultyDto = new DifficultyDto { Name = "Trudny" };
            await difficultyService.AddDifficulty(difficultyDto);

            //Act
            var result = await difficultyService.GetById(1);

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(difficultyDto.Name);
            result.DifficultyId.Should().Be(1);
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task DifficultyService_GetById_ReturnEmptyDifficulty()
        {
            //Assign
            var serviceContainer = _container;
            var difficultyService = serviceContainer.GetDifficultyService();

            //Act
            var result = await difficultyService.GetById(1);

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().BeNull();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task DifficultyService_GetByName_ReturnDifficulty()
        {
            //Assign
            var serviceContainer = _container;
            var difficultyService = serviceContainer.GetDifficultyService();
            var difficultyDto = new DifficultyDto { Name = "Trudny" };
            await difficultyService.AddDifficulty(difficultyDto);

            //Act
            var result = await difficultyService.GetByName("Trudny");

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(difficultyDto.Name);
            result.DifficultyId.Should().Be(1);
            serviceContainer.EndOfTest();
        }


        [Fact]
        public async Task DifficultyService_GetAll_ReturnAllDifficulty()
        {
            //Assign
            var serviceContainer = _container;
            var difficultyService = serviceContainer.GetDifficultyService();
            var difficultyDto1 = new DifficultyDto { Name = "Trudny" };
            var difficultyDto2 = new DifficultyDto { Name = "Średni" };
            await difficultyService.AddDifficulty(difficultyDto1);
            await difficultyService.AddDifficulty(difficultyDto2);

            //Act
            var result = await difficultyService.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Count().Should().Be(2);
            //do wyboru jedna z dwoch powyzszych metod budowania result
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task DifficultyService_IsDifficultyUsed_ReturnsFalse()
        {
            //Assign
            var serviceContainer = _container;
            var difficultyService = serviceContainer.GetDifficultyService();
            var difficultyDto = new DifficultyDto { Name = "Trudny" };
            await difficultyService.AddDifficulty(difficultyDto);

            //Act
            var result = await difficultyService.IsDifficultyUsed(1);

            //Assert
            result.Should().BeFalse();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task DifficultyService_IsUnitUnique_ReturnsFalse()
        {
            //Assing
            var serviceContainer = _container;
            var difficultyService = serviceContainer.GetDifficultyService();
            var difficultyDto = new DifficultyDto { Name = "Trudny" };
            await difficultyService.AddDifficulty(difficultyDto);

            //Act
            var result = await difficultyService.IsDifficultyUnique(difficultyDto.Name);

            //Assert
            result.Should().BeFalse();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task DifficultyService_Update_ReturnsUpdatedDto()
        {
            //Assign
            var serviceContainer = _container;
            var difficultyService = serviceContainer.GetDifficultyService();
            var difficultyDto = new DifficultyDto { Name = "Trudny" };
            await difficultyService.AddDifficulty(difficultyDto);
            var difficultyDtoUpdated = new DifficultyDto { DifficultyId = 1, Name = "Łatwy" };

            serviceContainer.DetachModel();
            //Act

            await difficultyService.Update(difficultyDtoUpdated);
            var difficultyFromDatabase = await difficultyService.GetById(1);

            //Assert
            difficultyDtoUpdated.Name.Should().Be(difficultyFromDatabase.Name);
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task DifficultyServices_Remove_CheckingDeletingOfDifficulty()
        {
            //Asign
            var serviceContainer = _container;
            var difficultyService = serviceContainer.GetDifficultyService();
            var difficultyDto = new DifficultyDto { Name = "Trudny" };
            await difficultyService.AddDifficulty(difficultyDto);

            //Act
            await difficultyService.Remove(1);
            var difficulties = await difficultyService.GetAll();

            //Assert
            difficulties.Should().HaveCount(0);
            serviceContainer.EndOfTest();
        }
    }
}
