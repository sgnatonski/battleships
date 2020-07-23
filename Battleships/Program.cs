using System;
using StructureMap;
using Battleships.Enum;
using Battleships.Src;

namespace Battleships
{
    class Program
    {
        static IContainer container = new Container();
        static void Main(string[] args)
        {
            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(Program));
                    _.WithDefaultConventions();
                });
            });

            var consolePainter = container.GetInstance<ConsolePainter>();
            var fleetBuilder = container.GetInstance<IFleetBuilder>();
            var consoleStrategy = container.GetInstance<ConsoleReadShotStrategy>();
            var randomStrategy = container.GetInstance<RandomShotStrategy>();

            var playerAdmiral = new FleetAdmiral(
                consoleStrategy,
                fleetBuilder.BuildFleet(new[] {
                    ShipSize.Battleship,
                    ShipSize.Destroyer,
                    ShipSize.Destroyer
                })
            );
            var enemyAdmiral = new FleetAdmiral(
                randomStrategy,
                fleetBuilder.BuildFleet(new[] {
                    ShipSize.Battleship,
                    ShipSize.Destroyer,
                    ShipSize.Destroyer
                })
            );

            consolePainter.DrawBoards(playerAdmiral, enemyAdmiral);

            while (true)
            {
                playerAdmiral.ShootAt(enemyAdmiral);
                consolePainter.DrawBoards(playerAdmiral, enemyAdmiral);

                if (enemyAdmiral.IsDefeated)
                {
                    break;
                }

                enemyAdmiral.ShootAt(playerAdmiral);
                consolePainter.DrawBoards(playerAdmiral, enemyAdmiral);

                if (playerAdmiral.IsDefeated)
                {
                    break;
                }
            }

            consolePainter.DrawSummary(playerAdmiral, enemyAdmiral);
            Console.ReadKey();
        }
    }
}
