using System;
using System.Collections.Generic;
using System.Text;

namespace NavBatProject
{
    class eCell
    {
        public eCell(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X
        {
            get;
        }
        public int Y
        {
            get;
        }
        //перечесление
        public enum eType
        {
            EMPTY,
            ALIVE,
            MISSED,
            HITTED,
        }
        private eType type = eType.EMPTY;
        public eType Type
        {
            get { return type; }
        }
        public void OnHittedCell()
        {
            switch(type)
            {
                case eType.EMPTY: type = eType.MISSED; break;
                case eType.ALIVE: type = eType.HITTED; break;
            }
        }
        public override string ToString()
        {
            return String.Format("(x={0}, y={1},type={2})",X,Y,type); //dump
        }
        private eShip ship = null;
        public void SetShip(eShip _ship)
        {
            ship = _ship;
            type = eType.ALIVE;
        }
    }
}
