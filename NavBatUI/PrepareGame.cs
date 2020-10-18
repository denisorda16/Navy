using NavBatProject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NavBatUI
{
    class PrepareGame
    {
        public PrepareGame(eBoard _board1,
                           eBoard _board2,
                           TableLayoutPanel _panel1,
                           TableLayoutPanel _panel2, 
                           Label _info)
        {
            board1 = _board1;
            board2 = _board2;
            panel1 = _panel1;
            panel2 = _panel2;
            info = _info;
            preparedShip = new eShip(new List<eCell>());
        }
        private Label            info       = null;
        private TableLayoutPanel panel1     = null;
        private TableLayoutPanel panel2     = null;
        private bool isPrepareFirstBoard    = true;
        private bool needPrepare            = true;
        private eShip preparedShip          = null;
        private eBoard board1               = null;
        private eBoard board2               = null;
        public delegate void PreparedBoardsDelegate();
        public event PreparedBoardsDelegate OnPreparedBoards;
        public void LoadPanels()
        {
            UITools.LoadPanel(panel1, CellClick);
            UITools.LoadPanel(panel2, CellClick);
        }

        public void OnItemPrepared()
        {

            if (needPrepare)
            {
                List<eCell> cells = preparedShip.Cells();
                eBoard board = GetBoard();
                if (board.AddShip(preparedShip))
                {
                    ResetPreparedPictureBoxes(Color.Green);
                    CheckPreparedStatus();
                    SwitchPanelsStatus();
                }
            }
            if(!needPrepare)
            {
                   OnPreparedPanel(panel1, CellClick);
                   OnPreparedPanel(panel2, CellClick);
                   OnPreparedBoards?.Invoke();
            }
        }
        private void CheckPreparedStatus()
        {
            needPrepare = board1.ShipsCount < 10 && board2.ShipsCount < 10;
        }
        private void OnPreparedPanel(TableLayoutPanel _panel, UITools.OnCellClicked _func)
        {
            for (int i = 1; i < 11; ++i)
            {
                for (int j = 1; j < 11; ++j)
                {
                    PictureBox pictureBox = (PictureBox)_panel.GetControlFromPosition(i, j);
                    pictureBox.Click -= new EventHandler(_func);
                }
            }
        }
        

        private TableLayoutPanel GetPanel()
        {
            if (needPrepare)
            {
                return isPrepareFirstBoard ? panel1 : panel2;
            }
            return null;
        }

        private eBoard GetBoard()
        {
            if (needPrepare)
            {
                return isPrepareFirstBoard ? board1 : board2;
            }
            return null;
        }
        private void SwitchPanelsStatus()
        {
            //Point p = panel1.Location;
            //panel1.Location     = panel2.Location;
            //panel2.Location     = p;
            bool visible        = panel1.Visible;
            panel1.Visible      = panel2.Visible;
            panel2.Visible      = visible;
            if (needPrepare)
            {
                isPrepareFirstBoard = !isPrepareFirstBoard;
            }
        }
        
        private void CellClick(object sender, EventArgs e)
        {
            if(!needPrepare) {
                return;
            }
            Control c = (Control)sender;
            if (!GetPanel().Contains(c)) return;
            TableLayoutPanelCellPosition pos = GetPanel().GetCellPosition(c);
            info.Text = $"panel:{isPrepareFirstBoard},col:{pos.Column}, row:{pos.Row}";
            if (needPrepare)
            {
                PictureBox pictureBox = (PictureBox)sender;
                if (pictureBox.BackColor == Color.Green)
                {
                    ResetPreparedPictureBoxes(Color.White);
                    return;
                }
                List<eCell> cells = preparedShip.Cells();
                eCell newShipCell = new eCell(pos.Column-1, pos.Row-1);
                if (!cells.Contains(newShipCell))
                {
                    preparedShip.AddCell(newShipCell);
                    if (!preparedShip.IsValid())
                    {
                        ResetPreparedPictureBoxes(Color.White);
                        preparedShip.AddCell(newShipCell);
                    }
                    preparedShip.Cells(cells);
                    pictureBox.BackColor = Color.Yellow;
                }

            }
        }


        private void ResetPreparedPictureBoxes(Color _color)
        {
            List<eCell> cells = preparedShip.Cells();
            foreach (eCell c in cells)
            {
                System.Windows.Forms.Control clr = GetPanel().GetControlFromPosition(c.X+1, c.Y+1);
                clr.BackColor = _color;
            }
            cells.Clear();
        }
    }
}
