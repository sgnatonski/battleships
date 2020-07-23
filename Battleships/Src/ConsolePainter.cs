using System;
using Battleships.Enum;

namespace Battleships.Src
{
    public class ConsolePainter
    {
        public void DrawBoards(IFleetAdmiral playerAdmiral, IFleetAdmiral enemyAdmiral)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("        PLAYER HITS                ENEMY HITS     ");
            Console.WriteLine("    a b c d e f g h i j       a b c d e f g h i j ");
            Console.WriteLine("   +-------------------+     +-------------------+");
            Console.WriteLine("  1|                   |     |                   |");
            Console.WriteLine("  2|                   |     |                   |");
            Console.WriteLine("  3|                   |     |                   |");
            Console.WriteLine("  4|                   |     |                   |");
            Console.WriteLine("  5|                   |     |                   |");
            Console.WriteLine("  6|                   |     |                   |");
            Console.WriteLine("  7|                   |     |                   |");
            Console.WriteLine("  8|                   |     |                   |");
            Console.WriteLine("  9|                   |     |                   |");
            Console.WriteLine(" 10|                   |     |                   |");
            Console.WriteLine("   +-------------------+     +-------------------+");
            Console.WriteLine();

            foreach (var item in enemyAdmiral.Fleet.ShotsRecorded)
            {
                DrawHit(item.Key, item.Value, 4, 4);
            }
            foreach (var item in playerAdmiral.Fleet.ShotsRecorded)
            {
                DrawHit(item.Key, item.Value, 30, 4);
            }

            if (playerAdmiral.LastShotResult > 0)
            {
                Console.WriteLine($"Enemy {(ShipSize)playerAdmiral.LastShotResult} hit (size {playerAdmiral.LastShotResult})");
            }

            if (enemyAdmiral.LastShotResult > 0)
            {
                Console.WriteLine($"Player {(ShipSize)enemyAdmiral.LastShotResult} hit (size {enemyAdmiral.LastShotResult})");
            }
            
            Console.WriteLine();
        }

        public void DrawSummary(IFleetAdmiral playerAdmiral, IFleetAdmiral enemyAdmiral)
        {
            if (enemyAdmiral.IsDefeated)
            {
                Console.WriteLine("You won!");
            }

            if (playerAdmiral.IsDefeated)
            {
                Console.WriteLine("Enemy won!");
            }
        }

        private void DrawHit(BattleCoordinate coord, ShotResult result, int offsetLeft, int offsetTop)
        {
            var cLeft = Console.CursorLeft;
            var cTop = Console.CursorTop;

            Console.SetCursorPosition(offsetLeft + coord.ColumnNumber * 2, offsetTop + coord.RowNumber);
            switch (result)
            {
                case ShotResult.MISS:
                    Console.Write(".");
                    break;
                case ShotResult.HIT:
                    Console.Write("x");
                    break;
                case ShotResult.SINK:
                    Console.Write("■");
                    break;
            }

            Console.SetCursorPosition(cLeft, cTop);
        }
    }
}