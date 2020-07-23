using System.Collections.Generic;
using Battleships.Enum;

namespace Battleships.Src
{
    public interface IFleet
    {
        IEnumerable<KeyValuePair<BattleCoordinate, ShotResult>> ShotsRecorded { get; }
        bool AnyShipOperational { get; }

        int AcceptShot(BattleCoordinate coord);
    }
}