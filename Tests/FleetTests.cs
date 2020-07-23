using System;
using System.Linq;
using Battleships.Enum;
using Battleships.Src;
using Xunit;

namespace Tests
{
    public class FleetTests
    {        
        private Ship CreateShip(string input, ICoordinateBoundary cb)
        {
            var coord = cb.CoordinateFromInput(input);
            var pos = ShipPosition.Horizontal(coord, ShipSize.Destroyer);
            return new Ship(pos);
        }

        [Fact]
        public void WhenShotMissed_ShouldRecordShotAsMissed()
        {
            var cb = new TestCoordinateBoundary(10, 10);
            var ships = new [] 
            { 
                CreateShip("a1", cb)
            };

            var fleet = new Fleet(ships);

            fleet.AcceptShot(cb.CoordinateFromInput("a2"));

            Assert.False(ships[0].IsSunk);
            Assert.True(fleet.ShotsRecorded.Any());
            Assert.True(fleet.ShotsRecorded.Last().Value == ShotResult.MISS);
            Assert.True(fleet.ShotsRecorded.Last().Key.ToString(cb) == "a2");
        }

        [Fact]
        public void WhenShotAtShip_ShouldRecordShotAsHit()
        {
            var cb = new TestCoordinateBoundary(10, 10);
            var ships = new [] 
            { 
                CreateShip("a1", cb)
            };

            var fleet = new Fleet(ships);

            fleet.AcceptShot(cb.CoordinateFromInput("b1"));

            Assert.False(ships[0].IsSunk);
            Assert.True(fleet.ShotsRecorded.Any());
            Assert.True(fleet.ShotsRecorded.Last().Value == ShotResult.HIT);
            Assert.True(fleet.ShotsRecorded.Last().Key.ToString(cb) == "b1");
        }

        [Fact]
        public void WhenShipSunk_ShouldRecordAllShotsAsSunk()
        {
            var cb = new TestCoordinateBoundary(10, 10);
            var ships = new [] 
            { 
                CreateShip("a1", cb),
                CreateShip("g1", cb)
            };

            var fleet = new Fleet(ships);

            fleet.AcceptShot(cb.CoordinateFromInput("a1"));
            fleet.AcceptShot(cb.CoordinateFromInput("b1"));
            fleet.AcceptShot(cb.CoordinateFromInput("c1"));
            fleet.AcceptShot(cb.CoordinateFromInput("d1"));

            Assert.True(ships[0].IsSunk);
            Assert.True(fleet.AnyShipOperational);
            Assert.True(fleet.ShotsRecorded.Any());
            Assert.True(fleet.ShotsRecorded.All(x => x.Value == ShotResult.SINK));
        }

        [Fact]
        public void WhenShipAlreadySunk_ShouldNotRecordShot()
        {
            var cb = new TestCoordinateBoundary(10, 10);
            var ships = new [] 
            { 
                CreateShip("a1", cb),
                CreateShip("g1", cb)
            };

            var fleet = new Fleet(ships);

            fleet.AcceptShot(cb.CoordinateFromInput("a1"));
            fleet.AcceptShot(cb.CoordinateFromInput("b1"));
            fleet.AcceptShot(cb.CoordinateFromInput("c1"));
            fleet.AcceptShot(cb.CoordinateFromInput("d1"));

            Assert.True(ships[0].IsSunk);
            Assert.True(fleet.AnyShipOperational);
            Assert.True(fleet.ShotsRecorded.Any());
            Assert.True(fleet.ShotsRecorded.All(x => x.Value == ShotResult.SINK));

            fleet.AcceptShot(cb.CoordinateFromInput("a1"));
            Assert.True(fleet.ShotsRecorded.Last().Value == ShotResult.SINK);
            Assert.True(fleet.ShotsRecorded.Last().Key.ToString(cb) == "d1");
        }

        [Fact]
        public void WhenAllShipsSunk_ShouldReportNoShipsOperational()
        {
            var cb = new TestCoordinateBoundary(10, 10);
            var ships = new [] 
            { 
                CreateShip("a1", cb)
            };

            var fleet = new Fleet(ships);

            fleet.AcceptShot(cb.CoordinateFromInput("a1"));
            fleet.AcceptShot(cb.CoordinateFromInput("b1"));
            fleet.AcceptShot(cb.CoordinateFromInput("c1"));
            fleet.AcceptShot(cb.CoordinateFromInput("d1"));

            Assert.True(ships[0].IsSunk);
            Assert.False(fleet.AnyShipOperational);
        }
    }
}
