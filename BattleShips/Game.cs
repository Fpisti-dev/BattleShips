using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShips
{
    class Game
    {
        public static Board TargetBoard;
        public List<Ship> Ships = new List<Ship>();

        public Game()
        {
            TargetBoard = new Board(); //Clear board for new game
            Ships.Clear(); //Clear list of ships
        }

        public void PlaceShip(int iSize, string sName) //Place ships
        {
            Ship ship = new Ship(sName, iSize);

            bool bFree = false;

            while (!bFree)
            {
                Random rand = new Random();
                int iStartRow = rand.Next(0, 10);
                int iStartColumn = rand.Next(1, 11);
                int iOrientation = rand.Next(2); //0 horizontal, 1 vertical

                //Check board boundaries
                if (iOrientation == 0)
                {
                    if (iStartColumn + iSize > 9)
                    {
                        bFree = false;
                        continue;
                    }
                }
                else
                {
                    if (iStartRow + iSize > 10)
                    {
                        bFree = false;
                        continue;
                    }
                }

                //Check used fields
                bool bUsed = false;
                if (iOrientation == 0)
                {
                    for (int i = 0; i < iSize; i++)
                    {
                        if (TargetBoard.Locations[Board.cRows[iStartRow].ToString() + (iStartColumn + i).ToString()].Occupier != null)
                        {
                            bUsed = true;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < iSize; i++)
                    {
                        if (TargetBoard.Locations[Board.cRows[iStartRow + i].ToString() + (iStartColumn).ToString().ToString()].Occupier != null)
                        {
                            bUsed = true;
                            break;
                        }
                    }
                }

                if (bUsed)
                {
                    continue;
                }

                //If coordinates free place ship
                if (iOrientation == 0)
                {
                    for (int i = 0; i < iSize; i++)
                    {
                        TargetBoard.Locations[Board.cRows[iStartRow].ToString() + (iStartColumn + i).ToString()].Occupier = (Ship)ship;
                    }

                }
                else
                {
                    for (int i = 0; i < iSize; i++)
                    {
                        TargetBoard.Locations[Board.cRows[iStartRow + i].ToString() + (iStartColumn).ToString()].Occupier = (Ship)ship;
                    }
                }

                Ships.Add((Ship)ship);
                bFree = true;                
            }
        }

        public void PlayRound()
        {
            bool bValid = false;

            TargetBoard.DisplayBoard("Fire");

            Console.WriteLine("Please enter a coordinate to fire, example: (A5) or 'N' for new game or 'Q' to Exit");
            string sFire = Console.ReadLine();

            if (sFire == "Q")
            {
                Console.Clear();

                TargetBoard.DisplayBoard("Target");

                Console.WriteLine();
                Console.WriteLine("You have exited the game. You can look the opponent's board above.");
                Console.WriteLine();
                Console.WriteLine("Please press a key to continue");
                Console.ReadKey();
                Program.StartGame();

            }
            else if (sFire == "N")
            {
                Program.StartGame();
            }
            else
            {
                //Validate coordinate
                try
                {
                    char cRow = Convert.ToChar(sFire.Substring(0, 1));
                    string sColunm = sFire.Substring(1);

                    if (Board.cRows.Contains(cRow) && (Convert.ToInt32(sColunm) > 0 && Convert.ToInt32(sColunm) < 11))
                    {
                        bValid = true;

                    }
                    else
                    {
                        InvalidCoordinate("Invalid");
                    }
                }
                catch
                {
                    InvalidCoordinate("Invalid");
                }
            }

            if (bValid)
            {
                Fire(TargetBoard.Locations[sFire]);
            }
        }        
        

        private void Fire(Coordinate cord)
        {
            if (TargetBoard.Locations[cord.Location].Fired)
            {
                InvalidCoordinate("Used");
            }
            else
            {
                TargetBoard.Locations[cord.Location].Fired = true;

                if (TargetBoard.Locations[cord.Location].Occupier == null)
                {
                    Console.WriteLine("Miss!");
                }
                else
                {
                    TargetBoard.Locations[cord.Location].Occupier.Hits++;
                    TargetBoard.Locations[cord.Location].Occupier.Coords.Add(cord.Location);

                    Console.WriteLine("Hit!");

                    if (TargetBoard.Locations[cord.Location].Occupier.Hits >= TargetBoard.Locations[cord.Location].Occupier.Size)
                    {
                        TargetBoard.Locations[cord.Location].Occupier.Sunk = true;
                        Console.WriteLine("Sunk! " + TargetBoard.Locations[cord.Location].Occupier.Name);
                    }
                }

                Console.ReadKey();

                //Check enf of game
                if (!Ships.All(x => x.Sunk))
                {
                    PlayRound();
                }
                else
                {
                    EndOfGame();
                }
            }            
        }

        private void InvalidCoordinate(string sMessage)
        {
            Console.WriteLine(sMessage + " coordinate. Please press a key and try again?");
            Console.ReadKey();
            PlayRound();
        }

        private void EndOfGame()
        {
            TargetBoard.DisplayBoard("Fire");

            Console.WriteLine();
            Console.WriteLine("Congratulation! You win, all ships destroyed.");
            Console.WriteLine();
            Console.WriteLine("Please press a key to continue");
            Console.ReadKey();
            Program.StartGame();
        }

    }
}
