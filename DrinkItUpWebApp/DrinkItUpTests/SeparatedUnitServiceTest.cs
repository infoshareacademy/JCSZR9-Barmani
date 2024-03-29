﻿using Microsoft.EntityFrameworkCore;
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
        private static readonly Container _container = new Container();

        [Fact]
        public async Task UnitService_AddUnit_ReturnAddedUnitName()
        {
            //Assign
            var serviceContainer = _container;
            var unitService = serviceContainer.GetUnitService();
            var unitDto = new UnitDto { Name = "jednostka" };

            //Act
            var testUnit = await unitService.AddUnit(unitDto);

            //Assert
            testUnit.Name.Should().Be(unitDto.Name);
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task UnitService_AddUnit_EmptyUnitDtoThrowsException()
        {
            //Assign
            var serviceContainer = _container;
            var unitService = serviceContainer.GetUnitService();
            var unitDto = new UnitDto();

            //Act
            Func<Task> func = async () => await unitService.AddUnit(unitDto);

            //Assert
            await func.Should().ThrowAsync<Exception>();
            serviceContainer.EndOfTest();
        }



        [Fact]
        public async Task UnitService_AddUnit_ReturnAddedMultipleUnitNames()
        {
            //Assign
            var serviceContainer = _container;
            var unitService = serviceContainer.GetUnitService();
            var testSize = 2000;
            var unitNameMaxLength = 24;

            var unitDtos = GetListofRandomUnits(testSize, unitNameMaxLength);

            //Act
            foreach (var unitDto in unitDtos)
            {
                await unitService.AddUnit(unitDto);
            }
            var result = await unitService.GetAll();

            //Assert
            result.Should().HaveCount(testSize);

            serviceContainer.EndOfTest();
        }


        [Fact]
        public async Task UnitService_GetById_ReturnUnitByID()
        {
            //Assign
            var serviceContainer = _container;
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

        [Fact]
        public async Task UnitService_GetById_ReturnEmptyUnit()
        {
            //Assign
            var serviceContainer = _container;
            var unitService = serviceContainer.GetUnitService();

            //Act
            var result = await unitService.GetById(1);

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().BeNull();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task UnitService_GetAll_ReturnAllUnits()
        {
            //Assign
            var serviceContainer = _container;
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
        public async Task UnitService_GetAll_ReturnEmptyList()
        {
            //Assign
            var serviceContainer = _container;
            var unitService = serviceContainer.GetUnitService();

            //Act
            var result = await unitService.GetAll();

            //Assert
            result.Should().HaveCount(0);
            //do wyboru jedna z dwoch powyzszych metod budowania result
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task UnitService_IsUnitUsed_ReturnsFalse()
        {
            //Assign
            var serviceContainer = _container;
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
            var serviceContainer = _container;
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
        public async Task UnitService_IsUnitUnique_EmptyStringThrowsException()
        {
            //Assing
            var serviceContainer = _container;
            var unitService = serviceContainer.GetUnitService();
            var unitDto = new UnitDto { Name = "Mendel" };
            await unitService.AddUnit(unitDto);

            //Act
            Func<Task> func = async () => await unitService.IsUnitUnique("");

            //Assert
            await func.Should().ThrowAsync<Exception>();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task UnitService_Update_ReturnsUpdatedDto()
        {
            //Assign
            var serviceContainer = _container;
            var unitService = serviceContainer.GetUnitService();
            var unitDto = new UnitDto { Name = "jednostka" };
            await unitService.AddUnit(unitDto);
            var unitDtoUpdated = new UnitDto { UnitId = 1, Name = "Tuzin" };

            serviceContainer.DetachModel();

            //Act
            await unitService.Update(unitDtoUpdated);
            var unitFromDatabase = await unitService.GetById(1);

            //Assert
            unitDtoUpdated.Name.Should().Be(unitFromDatabase.Name);
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task UnitService_Update_EmptyNameThrowException()
        {
            //Assign
            var serviceContainer = _container;
            var unitService = serviceContainer.GetUnitService();
            var unitDto = new UnitDto { Name = "jednostka" };
            await unitService.AddUnit(unitDto);
            var unitDtoUpdated = new UnitDto { UnitId = 1, Name = "" };

            serviceContainer.DetachModel();

            //Act
            Func<Task> func = async () => await unitService.Update(unitDtoUpdated);

            //Assert
            await func.Should().ThrowAsync<Exception>();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task UnitService_Update_ReturnsMultipleUpdatedDtos()
        {
            //Assign
            var serviceContainer = _container;
            var unitService = serviceContainer.GetUnitService();
            
            var testSize = 2000;
            var unitNameMaxLength = 24;

            var unitDtos = GetListofRandomUnits(testSize, unitNameMaxLength);
            
            foreach (var unitDto in unitDtos)
            {
                await unitService.AddUnit(unitDto);
            }
            serviceContainer.DetachModel();

            //Act
            unitDtos = await unitService.GetAll();
            foreach (var unitDto in unitDtos)
            {
                var unitDtoToUpdate = new UnitDto { UnitId = unitDto.UnitId, Name = RandomValues.RandomNameGenerator(unitNameMaxLength) };
                serviceContainer.DetachModel();
                await unitService.Update(unitDtoToUpdate);
                
            }

            //Assert
            for (int i = 1; i <= testSize; i++)
            {

                var unitFromDatabase = await unitService.GetById(i);
                var unitToCheck = unitDtos.FirstOrDefault(u => u.UnitId == i);
                unitToCheck.Name.Should().NotBe(unitFromDatabase.Name);
            }

            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task UnitServices_Remove_CheckingDeletingOfUnit()
        {
            //Asign
            var serviceContainer = _container;
            var unitService = serviceContainer.GetUnitService();
            var unitDto = new UnitDto { Name = "łokieć" };
            await unitService.AddUnit(unitDto);

            //Act
            await unitService.Remove(1);
            var units = await unitService.GetAll();

            //Assert
            units.Should().HaveCount(0);
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task UnitServices_Remove_IdIsNotInDatabase()
        {
            //Asign
            var serviceContainer = _container;
            var unitService = serviceContainer.GetUnitService();
            var unitDto = new UnitDto { Name = "łokieć" };
            await unitService.AddUnit(unitDto);

            //Act
            var IsRemoved = await unitService.Remove(2);
            var units = await unitService.GetAll();

            //Assert
            units.Should().HaveCount(1);
            IsRemoved.Should().BeFalse();

            serviceContainer.EndOfTest();
        }


        private List<UnitDto> GetListofRandomUnits(int testSize, int unitNameMaxLength)
        {
            var unitDtos = new List<UnitDto>();
            for (int i = 0; i < testSize; i++)
            {
                var unitDto = new UnitDto { Name = RandomValues.RandomNameGenerator(unitNameMaxLength) };
                unitDtos.Add(unitDto);
            }
            return unitDtos;
        }
    }
}
