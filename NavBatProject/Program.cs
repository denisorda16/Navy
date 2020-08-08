using System;
using System.Collections.Generic;

namespace NavBatProject
{
    class Program
    {
        static void Main(string[] args)
        {
            eBoard board = new eBoard();
            board.CheckHit(2, 2);
            Console.WriteLine(board.ConsolePrint());
        }
    }
}
