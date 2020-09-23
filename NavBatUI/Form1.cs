using NavBatProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NavBatUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            preparedShip = new eShip(new List<eCell>());
            board1 = new eBoard();
            board2 = new eBoard();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadPanel(tableLayoutPanel1);
            LoadPanel(tableLayoutPanel2);
        }
        
        private void SwitchPanelsStatus()
        {
            //Point p = tableLayoutPanel1.Location;
            //tableLayoutPanel1.Location = tableLayoutPanel2.Location;
            //tableLayoutPanel2.Location = p;
            bool visible = tableLayoutPanel1.Visible;
            tableLayoutPanel1.Visible = tableLayoutPanel2.Visible;
            tableLayoutPanel2.Visible = visible;
            if(needPrepare)
            {
                isPrepareFirstBoard = !isPrepareFirstBoard;
            }
        }

        private void LoadPanel(TableLayoutPanel _panel)
        {
            for (int i = 1; i < 11; ++i)
            {
                for (int j = 1; j < 11; ++j)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Width = WIDTH;
                    pictureBox.Height = HEIGHT;
                    pictureBox.BackColor = Color.White;
                    _panel.Controls.Add(pictureBox, i, j);
                    pictureBox.Click += new EventHandler(CellClick);
                }
                Label hLabel = new Label();
                hLabel.Width = WIDTH;
                hLabel.Height = HEIGHT;
                hLabel.BackColor = Color.Gray;
                hLabel.Text = ((char)('A' + i - 1)).ToString();
                _panel.Controls.Add(hLabel, i, 0);

                Label vLabel = new Label();
                vLabel.Width = WIDTH;
                vLabel.Height = HEIGHT;
                vLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                vLabel.BackColor = Color.Gray;
                vLabel.Text = i == 10 ? "10" : ((char)('1' + i - 1)).ToString();
                _panel.Controls.Add(vLabel, 0, i);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        bool isPrepareFirstBoard = false;
        bool needPrepare = true;
        eShip preparedShip = null;
        eBoard board1 = null;
        eBoard board2 = null;


        private TableLayoutPanel GetPanel()
        {
            if (needPrepare)
            {
                return isPrepareFirstBoard ? tableLayoutPanel1 : tableLayoutPanel2;
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (needPrepare)
            {
                TableLayoutPanel panel = GetPanel();
                List<eCell> cells = preparedShip.Cells();
                if (GetBoard().AddShip(preparedShip))
                {
                    foreach (eCell c in cells)
                    {
                        System.Windows.Forms.Control clr = panel.GetControlFromPosition(c.X, c.Y);
                        clr.BackColor = Color.Green;
                    }
                    cells.Clear();
                    SwitchPanelsStatus();
                }
            }
        }

        void ResetPreparedPictureBoxes()
        {
            List<eCell> cells = preparedShip.Cells();
            foreach (eCell c in cells)
            {
                System.Windows.Forms.Control clr = GetPanel().GetControlFromPosition(c.X, c.Y);
                clr.BackColor = Color.White;
            }
            cells.Clear();
        }
        private void CellClick(object sender, EventArgs e)
        {
            TableLayoutPanelCellPosition pos = GetPanel().GetCellPosition((Control)sender);
            label1.Text = $"col:{pos.Column}, row:{pos.Row}";
            if(needPrepare)
            {
                PictureBox pictureBox = (PictureBox)sender;
                if (pictureBox.BackColor == Color.Green)
                {
                    ResetPreparedPictureBoxes();
                    return;
                }
                List<eCell> cells = preparedShip.Cells();
                eCell newShipCell = new eCell(pos.Column, pos.Row);
                if(!cells.Contains(newShipCell))
                {
                    preparedShip.AddCell(newShipCell);
                    if(!preparedShip.IsValid())
                    {
                        ResetPreparedPictureBoxes();
                        preparedShip.AddCell(newShipCell);
                    }
                    preparedShip.Cells(cells);
                    pictureBox.BackColor = Color.Yellow;
                }

            }
        }
    }
}
