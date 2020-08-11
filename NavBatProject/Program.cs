using System;
using System.Collections.Generic;

namespace NavBatProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<eCell> eCells1 = new List<eCell>();
            eCells1.Add (new eCell(3, 3));
            eCells1.Add (new eCell(3, 4));
            eCells1.Add (new eCell(3, 5));
            List<eCell> eCells2 = new List<eCell>();
            eCells2.Add(new eCell(4, 3));
            eCells2.Add(new eCell(4, 4));
            eCells2.Add(new eCell(4, 5));
            List<eCell> eCells3 = new List<eCell>();
            eCells3.Add(new eCell(5, 3));
            eCells3.Add(new eCell(5, 4));
            eCells3.Add(new eCell(5, 5));
            List<eCell> eCells4 = new List<eCell>();
            eCells4.Add(new eCell(7, 3));
            eCells4.Add(new eCell(7, 4));
            eCells4.Add(new eCell(7, 5));
            eShip ship1 = new eShip(eCells1);
            eShip ship2 = new eShip(eCells2);
            eShip ship3 = new eShip(eCells3);
            eShip ship4 = new eShip(eCells4);
            eBoard board1 = new eBoard();
            eBoard board2 = new eBoard();
            eUser user1 = new eUser(board1, board2);
            eUser user2 = new eUser(board2, board1);
            board1.AddShip(ship1);
            board1.AddShip(ship3);
            board2.AddShip(ship2);
            board2.AddShip(ship4);
            board2.CheckHit(2, 2);
            Console.WriteLine(user1.ConsolePrint());
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine(user2.ConsolePrint());
        }
    }
}
