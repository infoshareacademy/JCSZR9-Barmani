using Microsoft.EntityFrameworkCore;
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
            var testUnit = await unitService.AddUnit(unitDto);

            //Act
            var result = await unitService.GetById(1);

            //Assert

            result.Should().NotBeNull();
            result.Name.Should().Be(unitDto.Name);
            result.UnitId.Should().Be(1);

        }

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

    }
}
