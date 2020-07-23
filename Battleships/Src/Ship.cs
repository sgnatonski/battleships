
using System.Collections.Generic;
using System.Linq;

namespace Battleships.Src
{
    public class Ship
    {
        private HashSet<BattleCoordinate> hits = new HashSet<BattleCoordinate>(new BattleCoordinateEqualityComparer());

        public Ship(ShipPosition position)
        {
            this.Coordinates = position.Coordinates;
        }

        public IEnumerable<BattleCoordinate> Coordinates { get; }

        public void Hit(BattleCoordinate coord)
        {
            if (!this.hits.Contains(coord))
            {
                this.hits.Add(coord);
            }
        }

        public bool IsSunk => this.hits.Count == this.Coordinates.Count();
    }
}