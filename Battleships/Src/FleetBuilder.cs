using System;
using System.Collections.Generic;
using System.Linq;
using Battleships.Enum;
using Battleships.Exceptions;

namespace Battleships.Src
{
    public class FleetBuilder : IFleetBuilder
    {
        private IEnumerable<BattleCoordinate> coordinates;
        private readonly IRandomProvider random;

        public FleetBuilder(ICoordinateBoundary boundary, IRandomProvider random)
        {
            this.coordinates = CalculateCoordinatesWithinBoundary(boundary);
            this.random = random;
        }

        private IEnumerable<BattleCoordinate> CalculateCoordinatesWithinBoundary(ICoordinateBoundary boundary)
        {
            var coords = new List<BattleCoordinate>();
            for (int c = 0; c < boundary.ColumnValues.Count(); c++)
            {
                for (int r = 0; r < boundary.RowValues.Count(); r++)
                {
                    coords.Add(new BattleCoordinate(c, r, boundary.ColumnValues.Count(), boundary.RowValues.Count()));                
                }
            }
            return coords;
        }

        private Ship AllocateShip(ShipSize shipSize, IEnumerable<Ship> battleships)
        {
            var eqComparer = new BattleCoordinateEqualityComparer();
            var battleshipCoordinates = battleships.SelectMany(x => x.Coordinates).ToList();

            var availableCoords = this.coordinates
                .Except(battleshipCoordinates, eqComparer)
                .ToList();

            var availablePositions = availableCoords.SelectMany(c => new[]
            {
                ShipPosition.Vertical(c, shipSize),
                ShipPosition.Horizontal(c, shipSize)
            });

            var validPositions = availablePositions.Where(x => x.IsValid).ToList();

            if (!validPositions.Any())
            {
                return null;
            }

            var nonOverlappingPositions = validPositions
                .Where(x => !x.Coordinates.Intersect(battleshipCoordinates, eqComparer).Any())
                .ToList();

            var r = this.random.Next(nonOverlappingPositions.Count);

            var selectedPosition = nonOverlappingPositions[r];

            return new Ship(selectedPosition);
        }

        public IEnumerable<Ship> BuildFleet(IEnumerable<ShipSize> ships)
        {
            var battleships = new List<Ship>();

            foreach (var shipSize in ships)
            {
                var ship = this.AllocateShip(shipSize, battleships);
                if (ship == null)
                {
                    throw new FleetAllocationException();
                }
                battleships.Add(ship);
            }

            return battleships;
        }
    }
}