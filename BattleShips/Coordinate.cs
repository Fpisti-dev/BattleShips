using System;

namespace BattleShips
{
    class Coordinate
    {
        public string Location { get; set; }
        public Ship Occupier { get; set; }
        public bool Fired { get; set; }

        public Coordinate(String _location)
        {
            Location = _location;
            Occupier = null;
            Fired = false;
        }
    }
}
