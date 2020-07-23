using System.Linq;

namespace Battleships.Src
{
    public class RandomShotStrategy : IShotStrategy
    {
        private readonly ICoordinateBoundary boundary;
        private readonly IRandomProvider random;

        public RandomShotStrategy(ICoordinateBoundary boundary, IRandomProvider random)
        {
            this.boundary = boundary;
            this.random = random;
        }

        public BattleCoordinate Resolve => new BattleCoordinate(
            this.random.Next(boundary.ColumnValues.Count()),
            this.random.Next(boundary.RowValues.Count()),
            boundary.ColumnValues.Count(),
            boundary.RowValues.Count()
        );
    }
}