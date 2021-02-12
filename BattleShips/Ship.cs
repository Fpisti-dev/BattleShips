using System;
using System.Collections.Generic;

namespace BattleShips
{
    class Ship
    {
        public int Size { get; set; } 
        public string Name { get; set; } 
        public int Hits { get; set; }
        public bool Sunk { get; set; }
        public List<string> Coords { get; set; } 

        public Ship(String _name, int size)
        {
            Size = size;
            Name = _name;            
            Hits = 0;
            Sunk = false;
            Coords = new List<string>();
        }
    }
}
