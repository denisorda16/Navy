using NavBatUI;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            UITools.LoadPanel(_userPanelThree, CellClick);
            UITools.LoadPanel(_userPanelFour, CellClick);
            user1.Init(userBoard1, userBoard2, _userPanelOne, _userPanelThree);
            user2.Init(userBoard2, userBoard1, _userPanelTwo, _userPanelFour);
        }

        private TableLayoutPanel GetEnemyPanel()
        {
            if (isStarted)
            {
                return firstPlayerTurn ? user1.EnemyPanel() : user2.EnemyPanel();
            }
            return null;
        }

        private eUser GetEnemyUser()
        {
            if (isStarted)
            {
                return !firstPlayerTurn ? user1 : user2;
            }
            return null;
        }

        private void CellClick(object sender, EventArgs e)
        {
            if (!isStarted)
            {
                return;
            }
            {
                Control c = (Control)sender;
                if (!GetEnemyPanel().Contains(c)) return;
                TableLayoutPanelCellPosition pos = GetEnemyPanel().GetCellPosition(c);
                Turn(pos.Column - 1, pos.Row - 1);
            }
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
            OnPlayerTurned(isHitted, x, y);
        }

        private void OnPlayerTurned(bool _isHitted, int _x, int _y)
        {
            Color color = _isHitted ? Color.Red : Color.Yellow; 
          
            System.Windows.Forms.Control clrOnline = GetEnemyPanel().GetControlFromPosition(_x + 1, _y + 1);
            clrOnline.BackColor = color;
            System.Windows.Forms.Control clrOffline = GetEnemyUser().MyPanel().GetControlFromPosition(_x + 1, _y + 1);
            clrOffline.BackColor = color;
        }

        private eUser user1 = null;
        private eUser user2 = null;
        private eBoard userBoard1 = null;
        private eBoard userBoard2 = null;
        private bool firstPlayerTurn = true;
        private bool isStarted = false;
    }
}
