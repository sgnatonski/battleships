using System;

namespace Battleships.Src
{
    public class RandomProvider : IRandomProvider
    {
        private readonly Random random = new Random();

        public int Next(int maxValue)
        {
            return this.random.Next(maxValue);
        }
    }
}