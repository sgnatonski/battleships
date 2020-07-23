namespace Battleships.Src
{
    public interface IFleetAdmiral
    {
        bool IsDefeated { get; }
        void ShootAt(IFleetAdmiral opponent);
        IFleet Fleet { get; }
        int LastShotResult { get; }
    }
}