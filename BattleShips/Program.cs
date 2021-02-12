using System;
using System.Diagnostics;

namespace BattleShips
{
    class Program
    {  
        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 30);
            StartGame();
        }

        public static void StartGame()
        {
            Console.Clear();

            Console.Title = "BattleShips";
            Console.WriteLine("Welcome to BattleShips!");
            Console.WriteLine();
            Console.WriteLine("Please press N for a new game or Q to exit?");
            StartMenu();
        }

        private static void StartMenu()
        { 
            ConsoleKeyInfo cKey = Console.ReadKey();

            if (cKey.KeyChar == 'N')
            {
                NewGame();
            }
            else if (cKey.KeyChar == 'Q')
            {
                System.Environment.Exit(0);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Wrong character entered, please try N or Q?");
                StartMenu();
            }
        }

        private static void NewGame()
        {
            Console.Clear();

            Game game = new Game();

            //Place ships on board
            game.PlaceShip(5, "Battleship"); //Battleship size 5
            game.PlaceShip(4, "Destroyer 1"); //Destroyer 1 size 4
            game.PlaceShip(4, "Destroyer 2"); //Destroyer 2 size 4 

            //Debug placed ships
            foreach ( var cord in BattleShips.Game.TargetBoard.Locations )
            {
                if (cord.Value.Occupier != null)
                {
                    Debug.Print(cord.Key.ToString() + ": " + cord.Value.Occupier.Name.ToString());
                }
            }

            game.PlayRound();
        }
    }
}
