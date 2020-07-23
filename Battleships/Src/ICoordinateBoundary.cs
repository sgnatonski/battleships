using System.Collections.Generic;

namespace Battleships.Src
{
    public interface ICoordinateBoundary
    {
        IEnumerable<string> ColumnValues { get; }
        IEnumerable<string> RowValues { get; }
        BattleCoordinate CoordinateFromInput(string input);
        string CoordinateToString(BattleCoordinate coord);
    }
}