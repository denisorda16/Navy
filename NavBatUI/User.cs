using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NavBatProject
{
    class eUser
    {
        public eUser(string _name) { Name = _name; }
        public void Init(eBoard _myBoard, eBoard _hisBoard, TableLayoutPanel _myPanel, TableLayoutPanel _hisPanel)
        {
            myBoard = _myBoard;
            hisBoard = _hisBoard;
            myBoard.Subscribe(this);
            myPanel = _myPanel;
            hisPanel = _hisPanel;
        }
        public string Name
        {
            get;
        }
        public void WaitTurn(bool _on)
        {
            myPanel.Visible = _on;
            hisPanel.Visible = _on;
        }
        public string ConsolePrint()
        {
            string result;
            result = myBoard.ConsolePrint(this) + "\n\n" + hisBoard.ConsolePrint(this);
            return result;

        }

        public TableLayoutPanel MyPanel() { return myPanel; }
        public TableLayoutPanel EnemyPanel() { return hisPanel; }

        private eBoard myBoard = null;
        private eBoard hisBoard = null;
        private TableLayoutPanel myPanel = null;
        private TableLayoutPanel hisPanel = null;
    }
}
