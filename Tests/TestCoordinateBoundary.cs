using System;
using System.Collections.Generic;
using System.Linq;
using Battleships.Src;

namespace Tests
{
    public class TestCoordinateBoundary : ICoordinateBoundary
    {
        private CoordinateBoundary inner = new CoordinateBoundary();
        private List<string> columnValues;
        private List<string> rowValues;
        
        public TestCoordinateBoundary(int columns, int rows)
        {
            this.columnValues = Enumerable.Range(65, columns).Select(i => new String(Convert.ToChar(i), 1).ToLower()).ToList();
            this.rowValues = Enumerable.Range(1, rows).Select(x => x.ToString()).ToList();
        }

        public IEnumerable<string> ColumnValues => this.columnValues;
        public IEnumerable<string> RowValues => this.rowValues;

        public BattleCoordinate CoordinateFromInput(string input) => inner.CoordinateFromInput(input);

        public string CoordinateToString(BattleCoordinate coord) => inner.CoordinateToString(coord);
    }
}