using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NavBatProject
{
    class Game
    {
        public Game(eUser _user1, eUser _user2)
        {
            user1 = _user1;
            user2 = _user2;
        }
        public void Init(eBoard _userBoard1,
                         eBoard _userBoard2, 
                         TableLayoutPanel _userPanelOne, 
                         TableLayoutPanel _userPanelTwo, 
                         TableLayoutPanel _userPanelThree, 
                         TableLayoutPanel _userPanelFour)
        {
            userBoard1 = _userBoard1;
            userBoard2 = _userBoard2;
            user1.Init(userBoard1, userBoard2, _userPanelOne, _userPanelThree);
            user2.Init(userBoard2, userBoard1, _userPanelTwo, _userPanelFour);
        }
        public void Start()
        {
            isStarted = true;
            user1.WaitTurn(true);
            user2.WaitTurn(false);
        }
        private void Turn(int x, int y)
        {
            bool isHitted = firstPlayerTurn ? userBoard2.CheckHit(x, y) : userBoard1.CheckHit(x, y);
            if (! isHitted )
            {
                firstPlayerTurn = !firstPlayerTurn;
                user1.WaitTurn(firstPlayerTurn);
                user2.WaitTurn(!firstPlayerTurn);
            }
            
        }
        private eUser user1 = null;
        private eUser user2 = null;
        private eBoard userBoard1 = null;
        private eBoard userBoard2 = null;
        private bool firstPlayerTurn = true;
        private bool isStarted = false;
    }
}
