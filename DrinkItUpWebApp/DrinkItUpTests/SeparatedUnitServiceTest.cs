﻿using Microsoft.EntityFrameworkCore;
using DrinkItUpWebApp.DAL.Entities;
using DrinkItUpWebApp.DAL.Repositories;
using AutoMapper;
using DrinkItUpWebApp.MapperProfile;
using DrinkItUpBusinessLogic;
using DrinkItUpBusinessLogic.DTOs;
using FluentAssertions;

namespace DrinkItUpTests
{
    public class SeparatedUnitServiceTest
    {
        private static DbContextOptionsBuilder<DrinkContext> OptionsBuilder()
        {
            return new DbContextOptionsBuilder<DrinkContext>().UseInMemoryDatabase("InMemoryDatabase");
        }
        private static DrinkContext db = new DrinkContext(SeparatedUnitServiceTest.OptionsBuilder().Options);
        private static UnitRepository _unitRepository = new UnitRepository(db);
        private static DrinkIngredientRepository drinkIngredientRepository = new DrinkIngredientRepository(db);
        private static IngredientRepository _ingredientRepository = new IngredientRepository(db, drinkIngredientRepository);
        private static MapperConfiguration configuration = new MapperConfiguration(configurationBuilder => configurationBuilder.AddProfile<MapperProfile>());
        private IMapper mapper = configuration.CreateMapper();

        [Fact]
        public async Task UnitService_AddUnit_ReturnAddedUnitName()
        {
            //Assign
            var unitService = new UnitService(_unitRepository,mapper, _ingredientRepository);
            var unitDto = new UnitDto { Name = "jednostka" };

            //Act
            var testUnit = await unitService.AddUnit(unitDto);

            //Assert
            testUnit.Name.Should().Be(unitDto.Name);
        }

        [Fact]
        public async Task UnitService_GetById_ReturnUnitByID()
        {
            //Assign
            var unitService = new UnitService(_unitRepository, mapper, _ingredientRepository);
            var unitDto = new UnitDto { Name = "jednostka" };
            await unitService.AddUnit(unitDto);

            //Act
            var result = await unitService.GetById(1);

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(unitDto.Name);
            result.UnitId.Should().Be(1);
        }

        //test na wzor:
        //wszystko co moze zwrocic null powinno byc otestowane na dwa sposoby
        //do otestowania wszystkie serwisy
        [Fact]
        public async Task UnitService_GetById_ReturnEmptyUnit()
        {
            //Assign
            var unitService = new UnitService(_unitRepository, mapper, _ingredientRepository);

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
            var unitDto1 = new UnitDto { Name = "Kopa" };
            await unitService.AddUnit(unitDto1);

            //Act
            var result = await unitService.IsUnitUsed(1);

            //Assert
            result.Should().BeFalse();

        }




        //public async Task<bool> IsUnitUsed(int id)
        //{
        //    return await _ingredientRepository.GetAll().Select(i => i.UnitId).ContainsAsync(id);
        //}
    }
}
