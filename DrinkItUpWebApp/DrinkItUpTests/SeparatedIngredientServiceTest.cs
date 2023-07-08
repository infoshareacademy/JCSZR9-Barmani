using DrinkItUpBusinessLogic;
using DrinkItUpBusinessLogic.DTOs;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkItUpTests
{
    public class SeparatedIngredientServiceTest
    {

        ////////////////////do dokończenia
        /////[Fact]

        //public async Task IngredientService_GetAllIngredientsWithUnits_()
        //{
        //    //Assign
        //    var serviceContainer = new Container();
        //    var ingredientsWithUnitsService = serviceContainer.GetIngredientService();
        //    var unitDto = new UnitDto { Name = "jednostka" };

        //    //Act
        //    var testUnit = await unitService.AddUnit(unitDto);

        //    //Assert
        //    testUnit.Name.Should().Be(unitDto.Name);

        //    //Clearing Context

        //    serviceContainer.EndOfTest();
        //}

        [Fact]
        public async Task IngredientService_GetById_ReturnIngredientsById()
        {
            //Assign
            var serviceContainer = new Container();
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { IngredientId = 1 };

            //Act
            var testGetById = await ingredientService.GetById(1);

            //Assert
            testGetById.IngredientId.Should().Be(1);
            //testGetById.Id.Should().Be(ingredientDto.IngredientId);

            //Clearing Context
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task IngredientService_Add_ReturnAddedIngredientName()
        {
            //Assign
            var serviceContainer = new Container();
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "chili" };

            //Act
            var testUnit = await ingredientService.Add(ingredientDto);

            //Assert
            testUnit.Name.Should().Be(ingredientDto.Name);

            //Clearing Context
            serviceContainer.EndOfTest();
        }


        [Fact]
        public async Task IngredientService_IngredientIsUsed_ReturnsFalse()
        {
            //Assign
            var serviceContainer = new Container();
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "chili" };
            await ingredientService.Add(ingredientDto);

            //Act
            var result = await ingredientService.IngredientIsUsed(1);

            //Assert
            result.Should().BeFalse();

            //Clearing Context
            serviceContainer.EndOfTest();
        }

        //[Fact]
        //public async Task IngredientService_IsIngredientUnique_ReturnsFalse()
        //{
        //    //Assing
        //    var serviceContainer = new Container();
        //    var ingredientService = serviceContainer.GetIngredientService();
        //    var ingredientDto = new IngredientDto { Name = "chili" };
        //    await ingredientService.Add(ingredientDto);

        //    //Act
        //    var result = await ingredientService.IsIngredientUnique(ingredientDto.Name);

        //    //Assert
        //    result.Should().BeFalse();

        //    //Clearing Context
        //    serviceContainer.EndOfTest();
        //}


        [Fact]
        public async Task IngredientService_Update_ReturnsUpdatedDto()
        {
            //Assign
            var serviceContainer = new Container();
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "chili" };
            await ingredientService.Add(ingredientDto);
            ingredientDto = await ingredientService.GetById(1);
            var ingredientDtoUpdated = new IngredientDto { IngredientId = 1, Name = "mięta" };

            serviceContainer.DetachModel();
            //Act

            await ingredientService.Update(ingredientDtoUpdated);
            var ingredientFromDatabase = await ingredientService.GetById(1);

            //Assert
            ingredientDtoUpdated.Name.Should().Be(ingredientFromDatabase.Name);

            serviceContainer.EndOfTest();
        }

        //Medody do otestowania
        //GetAllIngredientsWithUnits
        //GetById
        //Add
        //IngredientIsUsed
        //IsIngredientUnique
        //Update
        //Remove
    }
}
