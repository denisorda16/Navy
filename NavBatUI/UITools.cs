using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NavBatUI
{
    static class UITools
    {
        public static void LoadPanel(TableLayoutPanel _panel, OnCellClicked _func)
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
                    pictureBox.Click += new EventHandler(_func);
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
        public delegate void OnCellClicked(object sender, EventArgs e);
    }
}
