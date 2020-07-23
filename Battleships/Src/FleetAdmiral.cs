using System.Collections.Generic;
using Battleships.Exceptions;

namespace Battleships.Src
{
    public class FleetAdmiral : IFleetAdmiral
    {
        private Fleet fleet;
        private readonly IShotStrategy strategy;

        public FleetAdmiral(IShotStrategy strategy, IEnumerable<Ship> ships)
        {
            this.strategy = strategy;
            this.fleet = new Fleet(ships);
        }

        public void ShootAt(IFleetAdmiral opponent)
        {
            this.LastShotResult = opponent.Fleet.AcceptShot(strategy.Resolve ?? throw new BattleCoordinateException());
        }

        public IFleet Fleet => this.fleet;

        public bool IsDefeated => !this.fleet.AnyShipOperational;

        public int LastShotResult { get; private set; }
    }
}