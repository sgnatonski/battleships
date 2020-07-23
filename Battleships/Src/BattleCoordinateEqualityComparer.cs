using System;
using System.Collections.Generic;

namespace Battleships.Src
{
    public class BattleCoordinateEqualityComparer : IEqualityComparer<BattleCoordinate>
    {
        public bool Equals(BattleCoordinate x, BattleCoordinate y)
        {
            return x.RowNumber == y.RowNumber && x.ColumnNumber == y.ColumnNumber;
        }

        public int GetHashCode(BattleCoordinate obj)
        {
            int hash = 13;
            hash = (hash * 7) + obj.RowNumber.GetHashCode();
            hash = (hash * 7) + obj.ColumnNumber.GetHashCode();
            return hash;
        }
    }
}