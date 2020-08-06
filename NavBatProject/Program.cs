using System;
using System.Collections.Generic;

namespace NavBatProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //Cell cell1 = new Cell(2,5);
            //Console.WriteLine(cell1.ToString());
            //List<Cell> list = new List<Cell>();
            //list.Add(new Cell(2, 5));
            //list.Add(new Cell(2, 6));
            //list.Add(new Cell(2, 7));
            //list.Add(new Cell(2, 8));
            //Ship ship = new Ship(list);
            //Console.WriteLine(ship.ToString());
            //ship.CheckHit(2, 5);
            //Console.WriteLine(ship.ToString());
            //Console.WriteLine(ship.IsAlive());
            //ship.CheckHit(2, 6);
            //Console.WriteLine(ship.ToString());
            //Console.WriteLine(ship.IsAlive());
            //ship.CheckHit(2, 7);
            //Console.WriteLine(ship.ToString());
            //Console.WriteLine(ship.IsAlive());
            //ship.CheckHit(2, 8);
            //Console.WriteLine(ship.ToString());
            //Console.WriteLine(ship.IsAlive());
            Board board = new Board();
            board.CheckHit(2, 2);
            Console.WriteLine(board.ConsolePrint());
            //Ура мы разобрались с Гитом
        }
    }
}
