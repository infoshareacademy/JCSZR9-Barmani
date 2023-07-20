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
    public class SeparatedIngredientServiceTest
    {
        private static readonly Container _container = new Container();

        [Fact]
        public async Task IngredientService_Add_ReturnAddedIngredientName()
        {
            //Assign
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "chili", UnitId = 1 };

            //Act
            var testUnit = await ingredientService.Add(ingredientDto);

            //Assert
            testUnit.Name.Should().Be(ingredientDto.Name);
            testUnit.UnitId.Should().Be(ingredientDto.UnitId);
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task IngredientService_Add_IngredientWithoutNameThrowsException()
        {
            //Assign
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "", UnitId = 1 };
            var unitService = serviceContainer.GetUnitService();
            await unitService.AddUnit(new UnitDto { Name = "trochę" });

            //Act
            Func<Task> act = async () => await ingredientService.Add(ingredientDto);


            //Assert
            await act.Should().ThrowAsync<Exception>();


            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task IngredientService_Add_IngredientWithoutUnitIdNotAddedToDatabase()
        {
            //Assign
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "chili" };
            var unitService = serviceContainer.GetUnitService();
            await unitService.AddUnit(new UnitDto { Name = "trochę" });

            //Act
            Func<Task> act = async () => await ingredientService.Add(ingredientDto);


            //Assert
            await act.Should().ThrowAsync<Exception>();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task IngredientService_Add_ReturnsMultipleIngredients()
        {
            //Assign
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var unitService = serviceContainer.GetUnitService();
            var unitHighestId = 10;

            for (int i = 0; i < unitHighestId; i++)
            {
                var unitDto = new UnitDto { Name = RandomValues.RandomNameGenerator(24) };
                unitService.AddUnit(unitDto);
            }

            var testSize = 1000;
            var ingredientNameMaxLenght = 50;
            for (int i = 0; i < testSize; i++)
            {
                var ingredientDto = new IngredientDto
                {
                    Name = RandomValues.RandomNameGenerator(ingredientNameMaxLenght),
                    UnitId = RandomValues.RandomIdGenerator(unitHighestId)
                };

                //Act
                await ingredientService.Add(ingredientDto);
            }

            var result = await ingredientService.GetAllIngredientsWithUnits();
            
            //Assert
            result.Should().HaveCount(testSize);
            serviceContainer.EndOfTest();
        }


        [Fact]
        public async Task IngredientService_IngredientIsUsed_ReturnsFalse()
        {
            //Assign
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "chili", UnitId = 1 };
            await ingredientService.Add(ingredientDto);

            //Act
            var result = await ingredientService.IngredientIsUsed(1);

            //Assert
            result.Should().BeFalse();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task IngredientService_GetAllIngredientsWithUnits_ReturnAllIngrediets()
        {
            //Assign
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var unitService = serviceContainer.GetUnitService();
            await unitService.AddUnit(new UnitDto { Name = "trochę" });
            var ingredientDto1 = new IngredientDto { Name = "chili", UnitId = 1 };
            var ingredientDto2 = new IngredientDto { Name = "mięta", UnitId = 1 };
            await ingredientService.Add(ingredientDto1);
            await ingredientService.Add(ingredientDto2);

            //Act
            var result = await ingredientService.GetAllIngredientsWithUnits();

            //Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task IngredientService_GetAllIngredientsWithUnits_ReturnEmptyList()
        {
            //Assign
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();

            //Act
            var result = await ingredientService.GetAllIngredientsWithUnits();

            //Assert
            result.Should().HaveCount(0);
            serviceContainer.EndOfTest();
        }


        [Fact]
        public async Task IngredientService_GetById_ReturnIngredientsById()
        {
            //Assign
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "chili", UnitId = 1 };
            await ingredientService.Add(ingredientDto);

            //Act
            var testGetById = await ingredientService.GetById(1);

            //Assert
            testGetById.IngredientId.Should().Be(1);
            testGetById.Name.Should().Be(ingredientDto.Name);
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task IngredientService_GetById_ReturnEmptyIngredient()
        {
            //Assign
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();

            //Act
            var testGetById = await ingredientService.GetById(1);

            //Assert
            testGetById.Name.Should().BeNull();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task IngredientService_IsIngredientUnique_ReturnsFalse()
        {
            //Assing
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "chili", UnitId = 1 };
            await ingredientService.Add(ingredientDto);

            //Act
            var result = await ingredientService.IsIngredientUnique(ingredientDto);

            //Assert
            result.Should().BeFalse();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task IngredientService_IsIngredientUnique_EmptyIngredientNameReturnsException()
        {
            //Assing
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "", UnitId = 1 };

            
            //Act
            Func<Task> act = async () => await ingredientService.IsIngredientUnique(ingredientDto);


            //Assert
            await act.Should().ThrowAsync<Exception>();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task IngredientService_IsIngredientUnique_EmptyIngredientUnitReturnsFalse()
        {
            //Assing
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "Chili" };

            //Act
            Func<Task> act = async () => await ingredientService.IsIngredientUnique(ingredientDto);


            //Assert
            await act.Should().ThrowAsync<Exception>();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task IngredientService_Update_ReturnsUpdatedDto()
        {
            //Assign
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "chili", UnitId = 1 };
            await ingredientService.Add(ingredientDto);
            ingredientDto = await ingredientService.GetById(1);
            var ingredientDtoUpdated = new IngredientDto { IngredientId = 1, Name = "mięta", UnitId = 1 };
            serviceContainer.DetachModel();

            //Act
            await ingredientService.Update(ingredientDtoUpdated);
            var ingredientFromDatabase = await ingredientService.GetById(1);

            //Assert
            ingredientDtoUpdated.Name.Should().Be(ingredientFromDatabase.Name);
            serviceContainer.EndOfTest();
        }


        [Fact]
        public async Task IngredientService_Update_ReturnsMultipleUpdatedIngredients()
        {
            //Assign
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var unitService = serviceContainer.GetUnitService();
            var unitHighestId = 10;

            for (int i = 0; i < unitHighestId; i++)
            {
                var unitDto = new UnitDto { Name = RandomValues.RandomNameGenerator(24) };
                unitService.AddUnit(unitDto);
            }

            var testSize = 1000;
            var ingredientNameMaxLenght = 50;
            for (int i = 0; i < testSize; i++)
            {
                var ingredientDto = new IngredientDto
                {
                    Name = RandomValues.RandomNameGenerator(ingredientNameMaxLenght),
                    UnitId = RandomValues.RandomIdGenerator(unitHighestId)
                };

                await ingredientService.Add(ingredientDto);
            }

            //Act
            var ingredientDtos = await ingredientService.GetAllIngredientsWithUnits();
            for (int i = 1;i <= testSize; i++)
            {
                var ingredientToUpdate = new IngredientDto
                {
                    IngredientId = i,
                    Name = RandomValues.RandomNameGenerator(ingredientNameMaxLenght),
                    UnitId = RandomValues.RandomIdGenerator(unitHighestId) 
                };
                serviceContainer.DetachModel();
                await ingredientService.Update(ingredientToUpdate);
            }

            //Assert
            for (int i = 1; i <= testSize; i++)
            { 
                var updatedIngredient = await ingredientService.GetById(i);
                var notUpdatedIngredient = ingredientDtos.FirstOrDefault(id => id.IngredientId == i);
                updatedIngredient.Name.Should().NotBe(notUpdatedIngredient.Name);
            }
            serviceContainer.EndOfTest();
        }


        [Fact]
        public async Task IngredientService_Update_EmptyIngredientThrowsException()
        {
            //Assign
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "chili", UnitId = 1 };
            await ingredientService.Add(ingredientDto);
            ingredientDto = await ingredientService.GetById(1);
            var ingredientDtoUpdated = new IngredientDto { IngredientId = 1, Name = "", UnitId = 1 };
            serviceContainer.DetachModel();

            //Act
            
            Func<Task> act = async () => await ingredientService.Update(ingredientDtoUpdated);


            //Assert
            await act.Should().ThrowAsync<Exception>();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task IngredientService_Update_EmptyIngredientUnitIdNotUpdatedInDatabase()
        {
            //Assign
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "chili", UnitId = 1 };
            await ingredientService.Add(ingredientDto);
            ingredientDto = await ingredientService.GetById(1);
            var ingredientDtoUpdated = new IngredientDto { IngredientId = 1, Name = "mięta" };
            serviceContainer.DetachModel();

            //Act
            Func<Task> act = async () => await ingredientService.Update(ingredientDtoUpdated);


            //Assert
            await act.Should().ThrowAsync<Exception>();
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task UnitServices_Remove_CheckingDeletingOfUnit()
        {
            //Asign
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var ingredientDto = new IngredientDto { Name = "mięta", UnitId = 1 };
            await ingredientService.Add(ingredientDto);

            //Act
            await ingredientService.Remove(1);

            //var ingredients = await ingredientService.GetAll();
            var ingredients = await ingredientService.GetAllIngredientsWithUnits();

            //Assert
            ingredients.Should().HaveCount(0);
            serviceContainer.EndOfTest();
        }

        [Fact]
        public async Task UnitServices_Remove_IdIsNotInDatabaseReturnsFalse()
        {
            //Asign
            var serviceContainer = _container;
            var ingredientService = serviceContainer.GetIngredientService();
            var unitService = serviceContainer.GetUnitService();
            await unitService.AddUnit(new UnitDto { Name = "trochę" });
            var ingredientDto = new IngredientDto { Name = "mięta", UnitId = 1 };
            await ingredientService.Add(ingredientDto);

            //Act
            var result = await ingredientService.Remove(2);

            //var ingredients = await ingredientService.GetAll();
            var ingredients = await ingredientService.GetAllIngredientsWithUnits();

            //Assert
            ingredients.Should().HaveCount(1);
            result.Should().BeFalse();
            serviceContainer.EndOfTest();
        }
    }
}
