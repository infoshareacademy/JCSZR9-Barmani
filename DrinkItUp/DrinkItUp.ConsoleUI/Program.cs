using DrinkItUp.BusinessLogic.Model;
using DrinkItUp.ConsoleUI;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;



namespace Menu
{
        public class MainMenu2
        {

            private UserInput user;

            public MainMenu2(UserInput user)
            {
                this.user = user;
            }

            public void ShowData2()
            {


                ConsoleKeyInfo key;
                int option = 1;
                int option2 = 1;
                int option3 = 1;
                bool isSelected = false;
                bool isSelected2 = false;
                bool isSelected3 = false;
                int selectedoption3 = 0;
                string color = "✅ \u001b[32m";
                int CurrentYear = DateTime.Now.Year; // obecny rok
                bool Adultuser;




                if (user.selectedoption == 1)
                {
                    Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "XXXXX"));
                }
            }
        }
    
}


