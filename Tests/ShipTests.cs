using Battleships.Enum;
using Battleships.Src;
using Xunit;

namespace Tests
{
    public class ShipTests
    {
        [Fact]
        public void When4BulkheadShipHitOnce_ShouldNotSink()
        {
            var cb = new TestCoordinateBoundary(10, 10);
            var coord = cb.CoordinateFromInput("a1");
            var pos = ShipPosition.Horizontal(coord, ShipSize.Destroyer);

            var ship = new Ship(pos);

            Assert.False(ship.IsSunk);

            ship.Hit(coord);

            Assert.False(ship.IsSunk);
        }

        [Fact]
        public void WhenAllShipBulkheadsHit_ShouldSink()
        {
            var cb = new TestCoordinateBoundary(10, 10);
            var coord = cb.CoordinateFromInput("a1");
            var pos = ShipPosition.Horizontal(coord, ShipSize.Destroyer);

            var ship = new Ship(pos);

            Assert.False(ship.IsSunk);

            ship.Hit(coord);
            ship.Hit(new BattleCoordinate(coord.ColumnNumber + 1, coord.RowNumber, coord));
            ship.Hit(new BattleCoordinate(coord.ColumnNumber + 2, coord.RowNumber, coord));
            ship.Hit(new BattleCoordinate(coord.ColumnNumber + 3, coord.RowNumber, coord));

            Assert.True(ship.IsSunk);
        }

        [Fact]
        public void WhenShipHitTwiceAtSameCoords_ShouldNotCountAsHit()
        {
            var cb = new TestCoordinateBoundary(10, 10);
            var coord = cb.CoordinateFromInput("a1");
            var pos = ShipPosition.Horizontal(coord, ShipSize.Destroyer);

            var ship = new Ship(pos);

            Assert.False(ship.IsSunk);

            ship.Hit(coord);
            ship.Hit(new BattleCoordinate(coord.ColumnNumber + 1, coord.RowNumber, coord));
            ship.Hit(new BattleCoordinate(coord.ColumnNumber + 2, coord.RowNumber, coord));
            ship.Hit(coord);

            Assert.False(ship.IsSunk);
        }
    }
}
