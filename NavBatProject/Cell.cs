using System;
using System.Collections.Generic;
using System.Text;

namespace NavBatProject
{
    class Cell
    {
        public Cell(int x, int y)
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
        public enum Type
        {
            EMPTY,
            ALIVE,
            MISSED,
            HITTED,
        }
        private Type type = Type.EMPTY;
        public Type _Type
        {
            get { return type; }
        }
        public void OnHittedCell()
        {
            switch(type)
            {
                case Type.EMPTY: type = Type.MISSED; break;
                case Type.ALIVE: type = Type.HITTED; break;
                case Type.HITTED: type = Type.MISSED; break;
            }
        }
        public override string ToString()
        {
            return String.Format("(x={0}, y={1},type={2})",X,Y,type); //dump
        }
        private Ship ship = null;
        public void SetShip(Ship _ship)
        {
            ship = _ship;
            type = Type.ALIVE;
        }
    }
}
