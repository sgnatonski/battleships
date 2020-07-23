using System;
using System.Linq;
using Battleships.Src;
using Xunit;

namespace Tests
{
    public class CoordinateBoundaryTests
    {        
        [Theory]
        [InlineData("a1")]
        [InlineData("j10")]
        [InlineData("c5")]
        [InlineData("A1")]
        [InlineData("J10")]
        [InlineData("C5")]
        public void WhenValidInput_ShouldParseCaseInsensitive(string value)
        {
            var cb = new CoordinateBoundary();
            var result = cb.CoordinateFromInput(value);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("a1000")]
        [InlineData("")]
        [InlineData("c0")]
        [InlineData("-c-1")]
        [InlineData(null)]
        [InlineData("Bb")]
        [InlineData("j11")]
        [InlineData("K10")]
        public void WhenInvalidInput_ShouldReturnNull(string value)
        {
            var cb = new CoordinateBoundary();
            var result = cb.CoordinateFromInput(value);
            Assert.Null(result);
        }

        [Fact]
        public void WhenBoundaryColumnCoordinate_ShouldBeLastColumn()
        {
            var cb = new CoordinateBoundary();
            var result = cb.CoordinateFromInput($"{cb.ColumnValues.Last()}{cb.RowValues.First()}");

            Assert.NotNull(result);

            Assert.True(result.IsLastColumn);
        }

        [Fact]
        public void WhenBoundaryRowCoordinate_ShouldBeLastRow()
        {
            var cb = new CoordinateBoundary();
            var result = cb.CoordinateFromInput($"{cb.ColumnValues.First()}{cb.RowValues.Last()}");

            Assert.NotNull(result);

            Assert.True(result.IsLastRow);
        }

        [Fact]
        public void WhenIsLastColumn_ShouldThrowIfNextColumn()
        {
            var cb = new CoordinateBoundary();
            var result = cb.CoordinateFromInput($"{cb.ColumnValues.Last()}{cb.RowValues.First()}");

            Assert.NotNull(result);

            Assert.Throws<ArgumentOutOfRangeException>(() => new BattleCoordinate(result.ColumnNumber + 1, result.RowNumber, result));
        }

        [Fact]
        public void WhenNotLastColumn_ShouldNotThrowIfNextColumn()
        {
            var cb = new CoordinateBoundary();
            var result = cb.CoordinateFromInput($"{cb.ColumnValues.First()}{cb.RowValues.First()}");

            Assert.NotNull(result);

            new BattleCoordinate(result.ColumnNumber + 1, result.RowNumber, result);
        }

        [Fact]
        public void WhenIsLastRow_ShouldThrowIfNextRow()
        {
            var cb = new CoordinateBoundary();
            var result = cb.CoordinateFromInput($"{cb.ColumnValues.First()}{cb.RowValues.Last()}");

            Assert.NotNull(result);

            Assert.Throws<ArgumentOutOfRangeException>(() => new BattleCoordinate(result.ColumnNumber, result.RowNumber + 1, result));
        }

        [Fact]
        public void WhenNotLastRow_ShouldNotThrowIfNextRow()
        {
            var cb = new CoordinateBoundary();
            var result = cb.CoordinateFromInput($"{cb.ColumnValues.First()}{cb.RowValues.First()}");

            Assert.NotNull(result);

            new BattleCoordinate(result.ColumnNumber, result.RowNumber + 1, result);
        }
    }
}
