using Battleships.Src;

namespace Tests
{
    public class TestShotStrategy : IShotStrategy
    {
        private readonly ICoordinateBoundary boundary;

        private BattleCoordinate currentCoordinate;

        public TestShotStrategy(ICoordinateBoundary boundary)
        {
            this.boundary = boundary;
        }

        public void SetCoordinate(string input)
        {
            this.currentCoordinate = boundary.CoordinateFromInput(input);
        }

        public BattleCoordinate Resolve => this.currentCoordinate;
    }
}