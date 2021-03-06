﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NavBatProject
{
    class eShip
    {
        public eShip(List<eCell>_cells)
        {
            Cells(_cells);
        }
    

        public List<eCell> Cells() { return cells; }

        public void AddCell(eCell _cell)
        {
            _cell.SetShip(this);
            cells.Add(_cell);
        }
        public  void Cells(List<eCell> _cells)
        {
            cells = _cells;
            foreach (eCell cell in cells)
            {
                cell.SetShip(this);
            }
        }

    
        protected List<eCell> cells = null;
        public bool IsAlive()
        {
            bool isAlive = false;
            if (cells.Count != 0)
            {
                foreach(eCell cell in cells)
                {
                    if (cell.Type== eCell.eType.ALIVE)
                    {
                        isAlive = true;
                        break;
                    }
                }
               
            }
            return isAlive;
        }
        public override string ToString()
        {
            return  $"size ={cells.Count}, cells[{string.Join(",", cells)}]"; //dump
        }
        public bool IsValid()
        {
            if (cells.Count == 0 || cells.Count > 4) return false;
            bool areVert = true;
            bool areHor = true;
            //todo: bug
            
            eCell firstCell = cells[0];
            eCell minCell = cells[0];
            eCell maxCell = cells[0];
            foreach (eCell cell in cells)
            {
                if (minCell.X > cell.X || minCell.Y > cell.Y)
                {
                    minCell = cell;
                }
                if (maxCell.X < cell.X || maxCell.Y < cell.Y)
                {
                    maxCell = cell;
                }
                if (cell.Type != eCell.eType.ALIVE && cell.Type != eCell.eType.HITTED)
                {
                    return false;
                }
                if (firstCell.X != cell.X)
                {
                    areHor = false;
                }
                if (firstCell.Y != cell.Y)
                {
                    areVert = false;
                }
                if (!areVert && !areHor) break;
            }
            
            return (areVert || areHor)
                && (cells.Count==maxCell.Y - minCell.Y+1 || cells.Count == maxCell.X - minCell.X + 1);
        }
        public bool CheckHit(int x, int y)
        {
            foreach (eCell cell in cells)
            {
                if (cell.X == x && cell.Y == y)
                {
                    if (cell.Type==eCell.eType.HITTED)
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
            return false;
        }
    }
}
