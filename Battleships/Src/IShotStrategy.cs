namespace Battleships.Src
{
    public interface IShotStrategy
    {
        BattleCoordinate Resolve { get; }
    }
}