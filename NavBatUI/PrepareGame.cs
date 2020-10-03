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

        public void LoadPanels()
        {
            LoadPanel(panel1);
            LoadPanel(panel2);
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
                    SwitchPanelsStatus();
                }
            }
        }

        private void LoadPanel(TableLayoutPanel _panel)
        {
            for (int i = 1; i < 11; ++i)
            {
                for (int j = 1; j < 11; ++j)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Width = Form1.WIDTH;
                    pictureBox.Height = Form1.HEIGHT;
                    pictureBox.BackColor = Color.White;
                    _panel.Controls.Add(pictureBox, i, j);
                    pictureBox.Click += new EventHandler(CellClick);
                }
                Label hLabel = new Label();
                hLabel.Width = Form1.WIDTH;
                hLabel.Height = Form1.HEIGHT;
                hLabel.BackColor = Color.Gray;
                hLabel.Text = ((char)('A' + i - 1)).ToString();
                _panel.Controls.Add(hLabel, i, 0);

                Label vLabel = new Label();
                vLabel.Width = Form1.WIDTH;
                vLabel.Height = Form1.HEIGHT;
                vLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                vLabel.BackColor = Color.Gray;
                vLabel.Text = i == 10 ? "10" : ((char)('1' + i - 1)).ToString();
                _panel.Controls.Add(vLabel, 0, i);
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
            Point p = panel1.Location;
            panel1.Location     = panel2.Location;
            panel2.Location     = p;
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
