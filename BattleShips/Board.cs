using System;
using System.Collections.Generic;

namespace BattleShips
{
    class Board
    {
        public IDictionary<String, Coordinate> Locations;
        public static char[] cRows = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        //Game game = new Game();

        public Board()
        {
            Locations = new Dictionary<string, Coordinate>();

            foreach (char c in cRows)
            {              
                for (int j = 1; j < 11; j++)
                {
                    Locations.Add(c.ToString() + j.ToString(), new Coordinate(c.ToString() + j.ToString()));                    
                }
            }
        }

        public void DisplayBoard(string sType)
        {
            Console.Clear();

            foreach (char c in cRows)
            {
                string[] sLine = new string[10];

                for (int i = 1; i <= 10; i++)
                {
                    if (Game.TargetBoard.Locations[c + i.ToString()].Fired) //If fired check hits
                    {
                        if (Game.TargetBoard.Locations[c + i.ToString()].Occupier != null) //If occupied
                        {
                            if (Game.TargetBoard.Locations[c + i.ToString()].Occupier.Coords.Contains(c + i.ToString())) //Coordinate in ship's coods, so got fire
                            {  
                                sLine[i - 1] = "O"; //Show hit
                            }
                            else
                            {
                                if (sType == "Fire")
                                {
                                    sLine[i - 1] = "-"; //Not fired yet, hide ship on fire board
                                }
                                else //Target board
                                {
                                    sLine[i - 1] = Game.TargetBoard.Locations[c + i.ToString()].Occupier.Name.Substring(0, 1); //Show first letter if not hit on target board
                                }                                
                            }
                        }
                        else
                        {
                            sLine[i - 1] = "X"; //Missed fire
                        }  
                    }
                    else
                    {
                        if (sType == "Fire")
                        {
                            sLine[i - 1] = "-"; //Not fired yet
                        }
                        else //Target board
                        {
                            if (Game.TargetBoard.Locations[c + i.ToString()].Occupier != null)
                            {
                                sLine[i - 1] = Game.TargetBoard.Locations[c + i.ToString()].Occupier.Name.Substring(0, 1); //Show first letter if not hit
                            }
                            else
                            {
                                sLine[i - 1] = "-"; //Not fired yet
                            } 
                        }                        
                    }
                }

                Console.WriteLine(String.Format("{0,5}|{1,5}|{2,5}|{3,5}|{4,5}|{5,5}|{6,5}|{7,5}|{8,5}|{9,5}|{10,5}|",
                    c, sLine[0], sLine[1], sLine[2], sLine[3], sLine[4], sLine[5], sLine[6], sLine[7], sLine[8], sLine[9]));
            }

            Console.WriteLine(String.Format("{0,5}|{1,5}|{2,5}|{3,5}|{4,5}|{5,5}|{6,5}|{7,5}|{8,5}|{9,5}|{10,5}|",
                    "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"));
            Console.WriteLine();
        }
    }
}
