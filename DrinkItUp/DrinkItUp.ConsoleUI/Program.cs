using DrinkItUp.BusinessLogic;
using DrinkItUp.BusinessLogic.Logic;
using DrinkItUp.BusinessLogic.Model;
using DrinkItUp.ConsoleUI.Menu;
using System;
using System.Drawing;
using System.Text.Json;

namespace DrinkItUp.ConsoleUI
{
    internal class StartMenu
    {
        static void Main(string[] args)

        {
            LoginMenu.ShowLoginMenu();
        }
    }
}
