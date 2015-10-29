using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringApp.Models;

namespace FlooringApp.UI.Workflows
{
    public static class MainMenu
    {
        public static void Execute()
        {
            string input = "";
            int origWidth = Console.WindowWidth;
            int origHeight = Console.WindowHeight;

            Console.SetWindowSize(origWidth = 120, origHeight = 35);

            do
            {
                
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒");
                Console.WriteLine(@"                                                                                                                       ");
                Console.WriteLine(@"                    ###                                                                                                ");
                Console.WriteLine(@"                  #######                                                                                              ");
                Console.WriteLine(@"                ###########                                                                                            ");
                Console.WriteLine(@"           #########.+#########                                                                                        ");
                Console.WriteLine(@"     #############...+################                                                                                 ");
                Console.WriteLine(@"     #########=......+################     1. Display Orders                                                           ");
                Console.WriteLine(@"     ####............+################                                                                                 ");
                Console.WriteLine(@"     ####.....######.+##+.....########     2. Add an Order                                                             ");
                Console.WriteLine(@"     ####...###....###+..:###,..######                                                                                 ");
                Console.WriteLine(@"     ####.##,.......#..:######,.######     3. Edit an Order                                                            ");
                Console.WriteLine(@"     ####.##...........########..#####                                                                                 ");
                Console.WriteLine(@"     ####.##,..........########..#####     4. Remove an Order                                                          ");
                Console.WriteLine(@"     ####..,##,........###############                                                                                 ");
                Console.WriteLine(@"     ####....###,......###############                                                                                 ");
                Console.WriteLine(@"     ####......###,....####.......+###     5. Quit                                                                     ");
                Console.WriteLine(@"     ####........:###..#########..+###                                                                                 ");
                Console.WriteLine(@"     ####..........:##.#########..+###                                                                                 ");
                Console.WriteLine(@"     ####.......... ##.#########..+###                                                                                 ");
                Console.WriteLine(@"     ####:.##...... ##.#########..####                                                                                 ");
                Console.WriteLine(@"      ####.##...... ##..#######:..###                                                                                  ");
                Console.WriteLine(@"      #####.###,..,####..:####..#####                                                                                  ");
                Console.WriteLine(@"       #####..######.+###,....######                                                                                   ");
                Console.WriteLine(@"        #####:.......+############                                                                                     ");
                Console.WriteLine(@"         ######,.....+###########                                                                                      ");
                Console.WriteLine(@"          ######....+##########            Software Guild                                                              ");
                Console.WriteLine(@"            ######..+########               Flooring co.                                                               ");
                Console.WriteLine(@"              #############                                                                                            ");
                Console.WriteLine(@"                #########                                                                                              ");
                Console.WriteLine(@"                  #####                                                                                                ");
                Console.WriteLine(@"                                                                                                                       ");
                Console.WriteLine(@"▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒");
                Console.WriteLine(@"                                                                                                                       ");
                Console.ResetColor();
                Console.Write("Pick an option: ");

                input = Console.ReadLine();

                if (input != "5")
                {
                    ProcessChoice(input);
                }

            } while (input != "5");

        }

        private static void ProcessChoice(string choice)
        {

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;

            switch (choice)
            {
                case "1":
                    var dowf = new DisplayOrderWorkflow();
                    dowf.Execute();
                    break;
                case "2":
                    var cowf = new CreateOrderWorkflow();
                    cowf.Execute();
                    break;
                case "3":
                    var eowf = new EditOrderWorkflow();
                    eowf.Execute();
                    break;
                case "4":
                    var rowf = new RemoveOrderWorkflow();
                    rowf.Execute();
                    break;
                default:
                    Logger.Warning("Invalid user input", "Main Menu - Process Choice");
                    Console.WriteLine("{0} is an invalid entry!", choice);
                    Console.WriteLine();
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
