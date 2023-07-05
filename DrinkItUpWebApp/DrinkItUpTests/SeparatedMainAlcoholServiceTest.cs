﻿using DrinkItUpBusinessLogic;
using DrinkItUpBusinessLogic.DTOs;
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
        [Fact]
        public async Task MainAlcoholService_AddMainAlcohol_ReturnAddedMainAlcohol()
        {
            //Assing
            var serviceContainer = new Container();
            var mainAlcoholService = serviceContainer.GetMainAlcoholService();
            var mainAlcoholDto = new MainAlcoholDto { Name = "Spirytus" };

            //Act
            var testMainAlcohol = await mainAlcoholService.AddMainAlcohol(mainAlcoholDto);

            //Assert
            testMainAlcohol.Name.Should().Be(mainAlcoholDto.Name);

            //Clearing Context

            serviceContainer.EndOfTest();

        }
    }
}
    