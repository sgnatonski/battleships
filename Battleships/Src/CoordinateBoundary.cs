using System.Collections.Generic;
using System.Linq;

namespace Battleships.Src
{
    public class CoordinateBoundary : ICoordinateBoundary
    {
        private List<string> columnValues => new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        private List<string> rowValues => new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

        public IEnumerable<string> ColumnValues => this.columnValues;
        public IEnumerable<string> RowValues => this.rowValues;

        public BattleCoordinate CoordinateFromInput(string input)
        {
            if ((input ?? string.Empty).Length < 2)
            {
                return null;
            }

            var column = input.Substring(0, 1).ToLower();
            var row = input.Substring(1).ToLower();

            if (!this.columnValues.Contains(column))
            {
                return null;
            }

            if (!this.rowValues.Contains(row))
            {
                return null;
            }

            return new BattleCoordinate(
                this.columnValues.IndexOf(column),
                this.rowValues.IndexOf(row),
                this.columnValues.Count,
                this.rowValues.Count
            );
        }

        public string CoordinateToString(BattleCoordinate coord)
        {
            return $"{this.columnValues[coord.ColumnNumber]}{this.rowValues[coord.RowNumber]}";
        }
    }
}