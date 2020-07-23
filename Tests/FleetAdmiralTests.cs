using System.Linq;
using Battleships.Enum;
using Battleships.Exceptions;
using Battleships.Src;
using Xunit;

namespace Tests
{
    public class FleetAdmiralTests
    {
        [Fact]
        public void WhenCreatedFleet_ShouldNotBeDefeated()
        {
            var cb = new TestCoordinateBoundary(4, 4);
            var random = new TestRandomProvider(0);
            var strategy = new TestShotStrategy(cb);
            var builder = new FleetBuilder(cb, random);
            var admiral = new FleetAdmiral(strategy, builder.BuildFleet(new [] { ShipSize.Destroyer }));

            Assert.False(admiral.IsDefeated);
        }

        [Fact]
        public void WhenRandomlyShot_ShouldRecordRandomShot()
        {
            var cb = new TestCoordinateBoundary(4, 4);
            var random = new TestRandomProvider(0);
            var strategy = new RandomShotStrategy(cb, random);
            var builder = new FleetBuilder(cb, random);
            var admiral1 = new FleetAdmiral(strategy, builder.BuildFleet(new [] { ShipSize.Destroyer }));
            var admiral2 = new FleetAdmiral(strategy, builder.BuildFleet(new [] { ShipSize.Destroyer }));

            admiral1.ShootAt(admiral2);
            Assert.True(admiral2.Fleet.ShotsRecorded.Any());
            Assert.True(admiral2.Fleet.ShotsRecorded.First().Key.ToString(cb) == "a1");
        }

        [Fact]
        public void WhenGivenCorrectCoordinates_ShouldRecordShot()
        {
            var cb = new TestCoordinateBoundary(4, 4);
            var random = new TestRandomProvider(0);
            var strategy = new TestShotStrategy(cb);
            var builder = new FleetBuilder(cb, random);
            var admiral1 = new FleetAdmiral(strategy, builder.BuildFleet(new [] { ShipSize.Destroyer }));
            var admiral2 = new FleetAdmiral(strategy, builder.BuildFleet(new [] { ShipSize.Destroyer }));

            strategy.SetCoordinate("a1");
            admiral1.ShootAt(admiral2);
            Assert.True(admiral2.Fleet.ShotsRecorded.Any());
            Assert.True(admiral2.Fleet.ShotsRecorded.First().Key.ToString(cb) == "a1");
        }

        [Fact]
        public void WhenGivenIncorrectCoordinates_ShouldNotRecordShot()
        {
            var cb = new TestCoordinateBoundary(4, 4);
            var random = new TestRandomProvider(0);
            var strategy = new TestShotStrategy(cb);
            var builder = new FleetBuilder(cb, random);
            var admiral1 = new FleetAdmiral(strategy, builder.BuildFleet(new [] { ShipSize.Destroyer }));
            var admiral2 = new FleetAdmiral(strategy, builder.BuildFleet(new [] { ShipSize.Destroyer }));

            strategy.SetCoordinate("xx");
            Assert.Throws<BattleCoordinateException>(() => admiral1.ShootAt(admiral2));
            Assert.False(admiral2.Fleet.ShotsRecorded.Any());
        }

        [Fact]
        public void WhenAllShipsSunk_ShouldBeDefeated()
        {
            var cb = new TestCoordinateBoundary(4, 1);
            var random = new TestRandomProvider(0);
            var strategy = new TestShotStrategy(cb);
            var builder = new FleetBuilder(cb, random);
            var admiral1 = new FleetAdmiral(strategy, builder.BuildFleet(new [] { ShipSize.Destroyer }));
            var admiral2 = new FleetAdmiral(strategy, builder.BuildFleet(new [] { ShipSize.Destroyer }));

            strategy.SetCoordinate("a1");
            admiral1.ShootAt(admiral2);
            strategy.SetCoordinate("b1");
            admiral1.ShootAt(admiral2);
            strategy.SetCoordinate("c1");
            admiral1.ShootAt(admiral2);
            strategy.SetCoordinate("d1");
            admiral1.ShootAt(admiral2);

            Assert.True(admiral2.IsDefeated);
        }
    }
}