using Battleships.Src;

namespace Tests
{
    public class TestRandomProvider : IRandomProvider
    {
        private readonly int val;

        public TestRandomProvider(int val)
        {
            this.val = val;
        }
        public int Next(int maxValue)
        {
            return this.val;
        }
    }
}