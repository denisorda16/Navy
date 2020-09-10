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
    }
}
