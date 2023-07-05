using Microsoft.EntityFrameworkCore;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories;
using AutoMapper;
using DrinkItUpWebApp.MapperProfile;
using DrinkItUpBusinessLogic;
using DrinkItUpBusinessLogic.DTOs;
using FluentAssertions;
using DrinkItUpBusinessLogic.Interfaces;
using System.ComponentModel.Design;

namespace DrinkItUpTests
{
    public class SeparatedUnitServiceTest
    {
        

        [Fact]
        public async Task UnitService_AddUnit_ReturnAddedUnitName()
        {
            //Assign
            var serviceContainer = new Container();
            var unitService = serviceContainer.GetUnitService();
            var unitDto = new UnitDto { Name = "jednostka" };

            //Act
            var testUnit = await unitService.AddUnit(unitDto);

            //Assert
            testUnit.Name.Should().Be(unitDto.Name);

            //Clearing Context

            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task UnitService_GetById_ReturnUnitByID()
        {
            //Assign
            var serviceContainer = new Container();
            var unitService = serviceContainer.GetUnitService();
            var unitDto = new UnitDto { Name = "jednostka" };
            await unitService.AddUnit(unitDto);

            //Act
            var result = await unitService.GetById(1);

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(unitDto.Name);
            result.UnitId.Should().Be(1);

            serviceContainer.EndOfTest();
        }

        //test na wzor:
        //wszystko co moze zwrocic null powinno byc otestowane na dwa sposoby
        //do otestowania wszystkie serwisy
        [Fact]
        public async Task UnitService_GetById_ReturnEmptyUnit()
        {
            //Assign
            var serviceContainer = new Container();
            var unitService = serviceContainer.GetUnitService();

            //Act
            var result = await unitService.GetById(1);

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().BeNull();
        }

        [Fact]
        public async Task UnitService_GetAll_ReturnAllUnits()
        {
            //Assign
            var unitService = new UnitService(_unitRepository, mapper, _ingredientRepository);
            var unitDto1 = new UnitDto { Name = "Kopa" };
            var unitDto2 = new UnitDto { Name = "Mendel" };
            await unitService.AddUnit(unitDto1);
            await unitService.AddUnit(unitDto2);

            //Act
            var result = await unitService.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Count().Should().Be(2);
            //do wyboru jedna z dwoch powyzszych metod budowania result
        }



        [Fact]
        public async Task UnitService_IsUnitUsed_ReturnsFalse()
        {
            //Assign
            var unitService = new UnitService(_unitRepository, mapper, _ingredientRepository);
            var unitDto = new UnitDto { Name = "Kopa" };
            await unitService.AddUnit(unitDto);

            //Act
            var result = await unitService.IsUnitUsed(1);

            //Assert
            result.Should().BeFalse();

        }

        [Fact]
        public async Task UnitService_IsUnitUnique_ReturnsFalse()
        {
            //Assing
            var unitService = new UnitService(_unitRepository, mapper, _ingredientRepository);
            var unitDto = new UnitDto { Name = "Mendel" };
            await unitService.AddUnit(unitDto);

            //Act
            var result = await unitService.IsUnitUnique(unitDto.Name);

            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task UnitService_GetAll_ReturnAllUnits()
        {
            //Assign
            var serviceContainer = new Container();
            var unitService = serviceContainer.GetUnitService();
            var unitDto1 = new UnitDto { Name = "Kopa" };
            var unitDto2 = new UnitDto { Name = "Mendel" };
            await unitService.AddUnit(unitDto1);
            await unitService.AddUnit(unitDto2);

            //Act
            var result = await unitService.GetAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Count().Should().Be(2);
            //do wyboru jedna z dwoch powyzszych metod budowania result
            serviceContainer.EndOfTest();
        }



        [Fact]
        public async Task UnitService_IsUnitUsed_ReturnsFalse()
        {
            //Assign
            var serviceContainer = new Container();
            var unitService = serviceContainer.GetUnitService();
            var unitDto = new UnitDto { Name = "Kopa" };
            await unitService.AddUnit(unitDto);

            //Act
            var result = await unitService.IsUnitUsed(1);

            //Assert
            result.Should().BeFalse();

            serviceContainer.EndOfTest();

        }

        [Fact]
        public async Task UnitService_IsUnitUnique_ReturnsFalse()
        {
            //Assing
            var serviceContainer = new Container();
            var unitService = serviceContainer.GetUnitService();
            var unitDto = new UnitDto { Name = "Mendel" };
            await unitService.AddUnit(unitDto);

            //Act
            var result = await unitService.IsUnitUnique(unitDto.Name);

            //Assert
            result.Should().BeFalse();

            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task UnitService_Update_ReturnsUpdatedDto()
        //test nie przechodzi!!
        {
            //Assign
            var serviceContainer = new Container();
            var unitService = serviceContainer.GetUnitService();
            var unitDto = new UnitDto { Name = "jednostka" };
            await unitService.AddUnit(unitDto);
            unitDto = await unitService.GetById(1);
            var unitDtoUpdated = new UnitDto { UnitId = 1, Name = "Tuzin" };

            serviceContainer.DetachModel();
            //Act

            await unitService.Update(unitDtoUpdated);
            var unitFromDatabase = await unitService.GetById(1);

            //Assert
            unitDtoUpdated.Name.Should().Be(unitFromDatabase.Name);

            serviceContainer.EndOfTest();
        }
    }
}
