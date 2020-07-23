
using System;

namespace Battleships.Src
{
    public class ConsoleReadShotStrategy : IShotStrategy
    {
        private readonly ICoordinateBoundary boundary;

        public ConsoleReadShotStrategy(ICoordinateBoundary boundary)
        {
            this.boundary = boundary;
        }

        public BattleCoordinate Resolve
        {
            get
            {
                BattleCoordinate coord = null;
                while(coord == null)
                {
                    Console.WriteLine("Enter your target coordinate:");
                    coord = boundary.CoordinateFromInput(Console.ReadLine());
                    if (coord == null)
                    {
                        Console.WriteLine($"Please use valid coordinate.");
                    }
                }
                return coord;
            }
        }
    }
}