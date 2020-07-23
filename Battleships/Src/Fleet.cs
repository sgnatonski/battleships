using System.Collections.Generic;
using System.Linq;
using Battleships.Enum;

namespace Battleships.Src
{
    public class Fleet : IFleet
    {
        private readonly IEnumerable<Ship> battleships;
        private readonly IDictionary<BattleCoordinate, ShotResult> coordinateHistory = new Dictionary<BattleCoordinate, ShotResult>(new BattleCoordinateEqualityComparer());

        public Fleet(IEnumerable<Ship> battleships) => this.battleships = battleships;

        private Ship FindShipAt(BattleCoordinate coord)
        {
            foreach (var ship in this.battleships)
            {
                if (ship.Coordinates.Contains(coord, new BattleCoordinateEqualityComparer()))
                {
                    return ship;
                }
            }
            return null;
        }

        public int AcceptShot(BattleCoordinate coord)
        {
            var ship = this.FindShipAt(coord);

            if (ship == null)
            {
                this.coordinateHistory[coord] = ShotResult.MISS;
                return 0;
            }

            ship.Hit(coord);

            if (ship.IsSunk)
            {
                foreach (var item in ship.Coordinates)
                {
                    this.coordinateHistory[item] = ShotResult.SINK;
                }
            }
            else
            {
                this.coordinateHistory[coord] = ShotResult.HIT;
            }

            return ship.Coordinates.Count();
        }

        public IEnumerable<KeyValuePair<BattleCoordinate, ShotResult>> ShotsRecorded => this.coordinateHistory.ToList();

        public bool AnyShipOperational => this.battleships.Any(s => !s.IsSunk);
    }
}