using System.Linq;
using Battleships.Enum;
using Battleships.Src;
using Xunit;

namespace Tests
{
    public class ShipPositionTests
    {        
        [Theory]
        [InlineData("a1", ShipSize.Battleship)]
        [InlineData("a7", ShipSize.Destroyer)]
        public void ShouldCreateVerticalCoordinates(string value, ShipSize size)
        {
            var cb = new TestCoordinateBoundary(10, 10);
            var coord = cb.CoordinateFromInput(value);
            
            var result = ShipPosition.Vertical(coord, size);

            Assert.True(result.IsValid);
            Assert.NotNull(result.Coordinates);
            Assert.True(result.Coordinates.Count() == (int)size);
        }

        [Theory]
        [InlineData("a7", ShipSize.Battleship)]
        [InlineData("j8", ShipSize.Battleship)]
        public void ShouldCreateInvalidVerticalPosition(string value, ShipSize size)
        {
            var cb = new TestCoordinateBoundary(10, 10);
            var coord = cb.CoordinateFromInput(value);
            
            var result = ShipPosition.Vertical(coord, size);

            Assert.False(result.IsValid);
            Assert.Null(result.Coordinates);
        }

        [Theory]
        [InlineData("a1", ShipSize.Battleship)]
        [InlineData("g1", ShipSize.Destroyer)]
        public void ShouldCreateHorizontalCoordinates(string value, ShipSize size)
        {
            var cb = new TestCoordinateBoundary(10, 10);
            var coord = cb.CoordinateFromInput(value);
            
            var result = ShipPosition.Horizontal(coord, size);

            Assert.True(result.IsValid);
            Assert.NotNull(result.Coordinates);
            Assert.True(result.Coordinates.Count() == (int)size);
        }

        [Theory]
        [InlineData("j1", ShipSize.Battleship)]
        [InlineData("j8", ShipSize.Battleship)]
        public void ShouldCreateInvalidHorizontalPosition(string value, ShipSize size)
        {
            var cb = new TestCoordinateBoundary(10, 10);
            var coord = cb.CoordinateFromInput(value);
            
            var result = ShipPosition.Horizontal(coord, size);

            Assert.False(result.IsValid);
            Assert.Null(result.Coordinates);
        }
    }
}
