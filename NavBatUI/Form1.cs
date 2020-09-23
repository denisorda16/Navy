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
          //  this.tableLayoutPanel1.Size = new System.Drawing.Size(WIDTH * 11, HEIGHT * 11);
            for (int i = 1; i < 11; ++i)
            {
                for (int j = 1; j < 11; ++j)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Width = WIDTH;
                    pictureBox.Height = HEIGHT;
                    pictureBox.BackColor = Color.White;
                    tableLayoutPanel1.Controls.Add( pictureBox, i, j);
                    pictureBox.Click += new EventHandler(CellClick);
                }
                Label hLabel = new Label();
                hLabel.Width = WIDTH;
                hLabel.Height = HEIGHT;
                hLabel.BackColor = Color.Gray;
                hLabel.Text = ((char) ('A' + i - 1)).ToString();
                tableLayoutPanel1.Controls.Add(hLabel, i, 0);

                Label vLabel = new Label();
                vLabel.Width = WIDTH;
                vLabel.Height = HEIGHT;
                vLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                vLabel.BackColor = Color.Gray;
                vLabel.Text = i==10?"10":((char)('1' + i - 1)).ToString();
                tableLayoutPanel1.Controls.Add(vLabel, 0, i);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isPrepareFirstBoard = true;
        }
        bool isPrepareFirstBoard = false;
        bool isPrepareSecondBoard = false;
        eShip preparedShip = null;
        eBoard board1 = null;
        eBoard board2 = null;


        private void button2_Click(object sender, EventArgs e)
        {
            if (isPrepareFirstBoard)
            {
                List<eCell> cells = preparedShip.Cells();
                isPrepareFirstBoard = false;
                if (board1.AddShip(preparedShip))
                {
                    foreach (eCell c in cells)
                    {
                        System.Windows.Forms.Control clr = tableLayoutPanel1.GetControlFromPosition(c.X, c.Y);
                        clr.BackColor = Color.Green;
                    }
                       cells.Clear();
                }
                
            }
        }

        private void CellClick(object sender, EventArgs e)
        {
            TableLayoutPanelCellPosition pos =  tableLayoutPanel1.GetCellPosition((Control)sender);
            label1.Text = $"col:{pos.Column}, row:{pos.Row}";
            if(isPrepareFirstBoard || isPrepareSecondBoard)
            {
                PictureBox pictureBox = (PictureBox)sender;
                if (pictureBox.BackColor == Color.Green) return;
                List<eCell> cells = preparedShip.Cells();
                eCell newShipCell = new eCell(pos.Column, pos.Row);
                if(!cells.Contains(newShipCell))
                {
                    preparedShip.AddCell(newShipCell);
                    if(!preparedShip.IsValid())
                    {
                        foreach(eCell c in cells)
                        {
                            System.Windows.Forms.Control clr = tableLayoutPanel1.GetControlFromPosition(c.X, c.Y);
                            clr.BackColor = Color.White;
                        }
                        cells.RemoveAll((cell)=>
                        {
                            if(!cell.Equal(newShipCell))
                            {
                                cell.Reset();
                                return true;
                            }
                            return false;
                        });
                    }
                    preparedShip.Cells(cells);
                    pictureBox.BackColor = Color.Yellow;
                }

            }
        }
    }
}
