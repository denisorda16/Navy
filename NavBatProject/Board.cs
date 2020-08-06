using System;
using System.Collections.Generic;
using System.Text;

namespace NavBatProject
{
    class Board
    {
        public Board()
        {
            cells = new List<Cell>();
            ships = new List<Ship>();
            for (int i = 0; i<10;++i)
                for (int j = 0; j<10;++j)
                {
                    cells.Add(new Cell(i, j));
                }
        }
        public bool AreAllShipsKilled()
        {
            int currentKilledShips = 0;
            foreach(Ship ship in ships)
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
            foreach (Cell cell in cells)
            {
                if (cell.X == x && cell.Y == y)
                {
                    if (cell._Type == Cell.Type.HITTED)
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
            foreach (Ship ship in ships)
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
        protected List<Cell> cells = null;
        protected List<Ship> ships = null;
        public bool AddShip (Ship ship)
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
                    Cell cell = cells[10 * i + j];
                    switch(cell._Type)
                    {
                        case Cell.Type.ALIVE: line += "B" + vertDiv;break;
                        case Cell.Type.EMPTY: line += "?" + vertDiv;break;
                        case Cell.Type.HITTED: line += "$" + vertDiv;break;
                        case Cell.Type.MISSED: line += "#" + vertDiv;break;
                    }
                }
                    result += "\n"+ line + "\n" + horDiv;            
            }
                return result;
        }

    }
}
