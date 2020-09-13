using System;
using System.Collections.Generic;
using System.Text;

namespace NavBatProject
{
    class Game
    {
        public Game(eUser _user1, eUser _user2)
        {
            user1 = _user1;
            user2 = _user2;
        }
        public void Init(eBoard _userBoard1, eBoard _userBoard2)
        {
            userBoard1 = _userBoard1;
            userBoard2 = _userBoard2;
            user1.Init(userBoard1, userBoard2);
            user2.Init(userBoard2, userBoard1);
        }
        public void Start ()
        {
            while (true)
            {
                string name = firstPlayerTurn ? user1.Name : user2.Name;
                Console.Clear();
                Console.WriteLine(firstPlayerTurn ? user1.ConsolePrint() : user2.ConsolePrint());
                Console.WriteLine($"Player {name} turn");
                Console.WriteLine("enter x:");
                int x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("enter y:");
                int y = Convert.ToInt32(Console.ReadLine());
                Turn(x, y);
            }
        }
        private void Turn(int x, int y)
        {
            bool isHitted = firstPlayerTurn ? userBoard2.CheckHit(x, y) : userBoard1.CheckHit(x, y);
            if (! isHitted )
            {
                firstPlayerTurn = !firstPlayerTurn;
            }
            
        }
        private eUser user1 = null;
        private eUser user2 = null;
        private eBoard userBoard1 = null;
        private eBoard userBoard2 = null;
        private bool firstPlayerTurn = true;
    }
}
