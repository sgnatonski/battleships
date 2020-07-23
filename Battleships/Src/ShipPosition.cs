using System.Collections.Generic;
using System.Linq;
using Battleships.Enum;

namespace Battleships.Src
{
    public class ShipPosition
    {
        public static ShipPosition Vertical(BattleCoordinate start, ShipSize shipSize) => new ShipPosition(start, shipSize, true);
        public static ShipPosition Horizontal(BattleCoordinate start, ShipSize shipSize) => new ShipPosition(start, shipSize, false);

        private ShipPosition(BattleCoordinate start, ShipSize shipSize, bool vertical)
        {
            this.IsValid = true;

            var coordinates = new List<BattleCoordinate>() { start };
            for (int i = 1; i < (int)shipSize; i++)
            {
                var current = coordinates.Last();
                if (vertical)
                {
                    if (current.IsLastRow)
                    {
                        IsValid = false;
                        break;
                    }
                    coordinates.Add(new BattleCoordinate(current.ColumnNumber, current.RowNumber + 1, current));
                }
                else
                {
                    if (current.IsLastColumn)
                    {
                        IsValid = false;
                        break;
                    }
                    coordinates.Add(new BattleCoordinate(current.ColumnNumber + 1, current.RowNumber, current));
                }
            }

            if (IsValid)
            {
                this.Coordinates = coordinates;
            }
        }

        public bool IsValid { get; }

        public IEnumerable<BattleCoordinate> Coordinates { get; }
    }
}