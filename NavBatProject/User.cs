using System;
using System.Collections.Generic;
using System.Text;

namespace NavBatProject
{
    class eUser
    {
        public eUser (eBoard _myBoard, eBoard _hisBoard)
        {
            myBoard = _myBoard;
            hisBoard = _hisBoard;
            myBoard.Subscribe(this);
        }
        public string ConsolePrint()
        {
            string result;
            result = myBoard.ConsolePrint(this) + "\n\n" + hisBoard.ConsolePrint(this);
            return result;

        }
        private eBoard myBoard = null;
        private eBoard hisBoard = null;
    }
}
