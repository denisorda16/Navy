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
                    if (cell.Type == eCell.eType.HITTED)
                    {
                        break;
                    }
                    else
                    {
                        cell.OnHittedCell();
                        if (cell.Type == eCell.eType.HITTED)
                            return true;
                        return false;
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
        protected eUser owner = null;
        public void Subscribe(eUser _owner)
        {
            owner = _owner;
        }
        public void UnSubscribe()
        {
            owner = null;
        }

        public List<eCell> Cells() { return cells; }
        public bool AddShip (eShip ship)
        {
            return NeedAddShip(ship)
                && HasSpace(ship)
                && AddShip_(ship);
        }
        protected bool AddShip_(eShip ship)
        {
            List<eCell> copyShipCells = new List<eCell>();
            foreach (eCell cell in ship.Cells())
            {
                eCell copyItem = cells.Find(delegate (eCell item)
                {
                    return cell.Equals(item);
                });
                if(copyItem == null)
                {
                    return false;
                }
                copyShipCells.Add(copyItem);
            }
            ships.Add(new eShip(copyShipCells));
            return true;
        }
        public int ShipsCount { get { return ships.Count; } } 
        protected bool HasSpace(eShip ship)
        {
            foreach (eCell cell in ship.Cells())
            {
                if (!IsCellsAroundEmpty(cell))
                {
                    return false;
                }
            }
            return true;
        }
    protected bool NeedAddShip(eShip ship)
        {
            int shipSize = ship.Cells().Count;
            if(shipSize <= 0 || shipSize > 4)
            {
                return false;
            }
            List<eShip> res = ships.FindAll(delegate (eShip item)
            {
                return item.Cells().Count == shipSize;
            });

            switch (shipSize)
            {
                case 1: return res.Count < 4;
                case 2: return res.Count < 3;
                case 3: return res.Count < 2;
                case 4: return res.Count < 1;
                default:
                return false;
            }
        }

        protected bool IsCellsAroundEmpty(eCell cell)
        {
            List<eCell> cellsAround = cells.FindAll(delegate (eCell item)
            {
                return item.Type != eCell.eType.EMPTY
                    && ((Math.Abs(item.X - cell.X) == 1 && Math.Abs(item.Y - cell.Y) == 0)
                    || (Math.Abs(item.X - cell.X) == 1 &&Math.Abs(item.Y - cell.Y)==1)
                   || (Math.Abs(item.X - cell.X) == 0  &&Math.Abs(item.Y - cell.Y) == 1)) ;
            });

            return cellsAround.Count == 0;
        }
        public override string ToString()
        {
            return $"boardSize ={cells.Count}, cells[{string.Join(",", cells)}], shipsSize = {ships.Count}, ships[{string.Join(",", ships)}]";
        }
        //to do:kill after working with ui
        public string ConsolePrint(eUser user= null)
        {
            bool isOwner = user == owner;
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
                    line += ConsoleCellPrint(isOwner,cell) + vertDiv;
                   
                    
                }
                    result += "\n"+ line + "\n" + horDiv;            
            }
                return result;
        }
        protected string ConsoleCellPrint(bool isOwner, eCell cell)
        {
           if (isOwner)
            {
                switch (cell.Type)
                {
                    case eCell.eType.ALIVE:    return "B";
                    case eCell.eType.EMPTY:    return "?";
                    case eCell.eType.HITTED:   return "$";
                    case eCell.eType.MISSED:   return "#";
                }
            }
                switch (cell.Type)
                {
                    case eCell.eType.ALIVE:     
                    case eCell.eType.EMPTY:     return "u";
                    case eCell.eType.HITTED:    return "$";
                    case eCell.eType.MISSED:    return "#";
                }
            return "";
        }

    }
}
