using System.Collections.Generic;
using Battleships.Enum;

namespace Battleships.Src
{
    public interface IFleetBuilder
    {
        IEnumerable<Ship> BuildFleet(IEnumerable<ShipSize> ships);
    }
}