using System;

namespace Battleships.Src
{
    public class BattleCoordinate
    {
        private readonly int columnNumber;
        private readonly int rowNumber;
        private readonly int columnCount;
        private readonly int rowCount;

        public BattleCoordinate(int columnNumber, int rowNumber, BattleCoordinate coord)
        : this(columnNumber, rowNumber, coord.columnCount, coord.rowCount)
        {
        }

        public BattleCoordinate(int columnNumber, int rowNumber, int columnCount, int rowCount)
        {
            if (columnNumber >= columnCount)
            {
                throw new ArgumentOutOfRangeException("Column number must be less than total column count");
            }
            if (rowNumber >= rowCount)
            {
                throw new ArgumentOutOfRangeException("Row number must be less than total row count");
            }
            this.columnNumber = columnNumber;
            this.rowNumber = rowNumber;
            this.columnCount = columnCount;
            this.rowCount = rowCount;
        }

        public int ColumnNumber => this.columnNumber;
        public int RowNumber => this.rowNumber;
        public bool IsLastColumn => this.columnNumber == this.columnCount - 1;
        public bool IsLastRow => this.rowNumber == this.rowCount - 1;

        public string ToString(ICoordinateBoundary boundary) => boundary.CoordinateToString(this);
    }
}