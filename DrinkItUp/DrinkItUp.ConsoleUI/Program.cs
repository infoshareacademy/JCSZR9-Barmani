using DrinkItUp.BusinessLogic;
using DrinkItUp.BusinessLogic.Entities;
using DrinkItUp.BusinessLogic.Logic;
using DrinkItUp.BusinessLogic.Model;
using DrinkItUp.ConsoleUI.Menu;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Text.Json;

namespace DrinkItUp.ConsoleUI
{
    internal class StartMenu
    {
        static void Main(string[] args)

        {
            var options = new DbContextOptions<DrinkContext>();
            var drinkContext = new DrinkContext(options);

            var migration = new DataMigration(drinkContext);
            migration.DIUToSQL();

            //LoginMenu.ShowLoginMenu();
        }
    }
}
