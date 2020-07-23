using System;
using System.Linq;
using Battleships.Enum;
using Battleships.Exceptions;
using Battleships.Src;
using Xunit;

namespace Tests
{
    public class FleetBuilderTests
    {
        private readonly IRandomProvider random = new TestRandomProvider(0);

        [Fact]
        public void WhenAbleToFitAllShips_ShouldNotThrow()
        {
            var cb = new TestCoordinateBoundary(4, 4);
            var builder = new FleetBuilder(cb, random);

            var ships = new[] 
            {
                ShipSize.Destroyer
            };

            var fleet = builder.BuildFleet(ships);

            Assert.NotNull(fleet);
            Assert.True(fleet.Any());
            Assert.True(fleet.First().Coordinates.Any());
            Assert.True(fleet.First().Coordinates.First().ToString(cb) == "a1");
            Assert.True(fleet.First().Coordinates.Last().ToString(cb) == "a4");
        }

        [Fact]
        public void WhenUnableToFitAllShips_ShouldThrow()
        {
            var cb = new TestCoordinateBoundary(3, 3);
            var builder = new FleetBuilder(cb, random);

            var ships = new[] 
            {
                ShipSize.Battleship
            };

            Assert.Throws<FleetAllocationException>(() => builder.BuildFleet(ships));
        }

        [Theory]
        [InlineData(4, 4, new ShipSize[0])]
        [InlineData(4, 1, new[] { ShipSize.Destroyer })]
        [InlineData(4, 2, new[] { ShipSize.Destroyer, ShipSize.Destroyer })]
        [InlineData(4, 3, new[] { ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer })]
        [InlineData(4, 4, new[] { ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer })]
        [InlineData(4, 8, new[] { ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer })]
        [InlineData(1, 4, new[] { ShipSize.Destroyer })]
        [InlineData(2, 4, new[] { ShipSize.Destroyer, ShipSize.Destroyer })]
        [InlineData(3, 4, new[] { ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer })]
        [InlineData(8, 4, new[] { ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer, ShipSize.Destroyer })]
        public void WhenAbleToFitAllShips_NoShipShouldOverlap(int colums, int rows, ShipSize[] ships)
        {
            var cb = new TestCoordinateBoundary(colums, rows);
            var builder = new FleetBuilder(cb, random);

            var fleet = builder.BuildFleet(ships);

            Assert.NotNull(fleet);
            Assert.True(fleet.Count() == ships.Length);

            var allAllocatedCoordinates = fleet.SelectMany(f => f.Coordinates).ToList();
            var distinctAllocatedCoordinates = allAllocatedCoordinates.Distinct(new BattleCoordinateEqualityComparer()).ToList();
            Assert.True(allAllocatedCoordinates.Count == distinctAllocatedCoordinates.Count);
        }
    }
}
