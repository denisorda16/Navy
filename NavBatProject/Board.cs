using System;
using System.Collections.Generic;
using System.Text;

namespace NavBatProject
{
    class eBoard
    {
        public eBoard()
        {
            cells = new List<eCell>();
            ships = new List<eShip>();
            for (int i = 0; i<10;++i)
                for (int j = 0; j<10;++j)
                {
                    cells.Add(new eCell(i, j));
                }
        }
        public bool AreAllShipsKilled()
        {
            int currentKilledShips = 0;
            foreach(eShip ship in ships)
            {
                if (!ship.IsAlive())
                {
                    currentKilledShips ++;
                }
            }
            return currentKilledShips==ships.Count;
        }
        public bool CheckHit(int x, int y)
        {
            foreach (eCell cell in cells)
            {
                if (cell.X == x && cell.Y == y)
                {
                    if (cell.Type == Cell.eType.HITTED)
                    {
                        break;
                    }
                    else
                    {
                        cell.OnHittedCell();
                        return true;
                    }
                }
            }
            foreach (eShip ship in ships)
            {

                if (ship.CheckHit(x, y))
                {
                    return true;
                }
            }
                return false;
        }
        public bool IsValid()
        {
            return true;
        }
        protected List<eCell> cells = null;
        protected List<eShip> ships = null;
        public bool AddShip (eShip ship)
        {
            return false;
        }
        public override string ToString()
        {
            return $"boardSize ={cells.Count}, cells[{string.Join(",", cells)}], shipsSize = {ships.Count}, ships[{string.Join(",", ships)}]";
        }
        //to do:kill after working with ui
        public string ConsolePrint()
        {
            string result;
            string horDiv = "----------------------";
            string vertDiv = "|";
            string upTitle = " |A|B|C|D|E|F|G|H|I|J|";
            result = horDiv + "\n" + upTitle + "\n" + horDiv;
            for (int i = 0; i < 10; ++i)
            {
                string line = (i).ToString() + vertDiv;
                for (int j = 0; j < 10; ++j)
                {
                    eCell cell = cells[10 * i + j];
                    switch(cell.Type)
                    {
                        case eCell.Type.ALIVE: line += "B" + vertDiv;break;
                        case eCell.Type.EMPTY: line += "?" + vertDiv;break;
                        case eCell.Type.HITTED: line += "$" + vertDiv;break;
                        case eCell.Type.MISSED: line += "#" + vertDiv;break;
                    }
                }
                    result += "\n"+ line + "\n" + horDiv;            
            }
                return result;
        }

    }
}
