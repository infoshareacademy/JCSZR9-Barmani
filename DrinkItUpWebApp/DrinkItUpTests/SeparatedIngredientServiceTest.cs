using DrinkItUpBusinessLogic;
using DrinkItUpBusinessLogic.DTOs;
using DrinkItUpBusinessLogic.Interfaces;
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

        //GetAllIngredientsWithUnits nie działa
        //GetById nie działa
        //IsIngredientUnique nie działa

        [Fact]
        public async Task IngredientService_GetAllIngredientsWithUnits_ReturnAllUnits()
        {
            //Assign
            var serviceContainer = new Container();
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto1 = new IngredientDto { Name = "chili" };
            var ingredientDto2 = new IngredientDto { Name = "mięta" };
            await ingredientService.Add(ingredientDto1);
            await ingredientService.Add(ingredientDto2);

            //Act
            var result = await ingredientService.GetAllIngredientsWithUnits();


            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);

            //Clearing Context
            serviceContainer.EndOfTest();
        }

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

        [Fact]
        public async Task IngredientService_IsIngredientUnique_ReturnsFalse()
        {
            //Assing
            var serviceContainer = new Container();
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "chili" };
            await ingredientService.Add(ingredientDto);

            //Act
            var result = await ingredientService.IsIngredientUnique(ingredientDto.Name);

            //Assert
            result.Should().BeFalse();

            //Clearing Context
            serviceContainer.EndOfTest();
        }


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


        [Fact]
        public async Task UnitServices_Remove_CheckingDeletingOfUnit()
        {
            //Asign
            var serviceContainer = new Container();
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "mięta" };
            await ingredientService.Add(ingredientDto);

            //Act
            await ingredientService.Remove(1);
            //var ingredients = await ingredientService.GetAll();
            var ingredients = await ingredientService.GetAllIngredientsWithUnits();
            //Assert
            ingredients.Should().HaveCount(0);
            serviceContainer.EndOfTest();
        }



    }
}
