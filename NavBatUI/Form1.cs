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
            user1 = new eUser("Ivan");
            user2 = new eUser("Denis");
            board1 = new eBoard();
            board2 = new eBoard();
            preparer = new PrepareGame(board1,
                                       board2,
                                       tableLayoutPanel1,
                                       tableLayoutPanel2,
                                       label1);
        }

        PrepareGame preparer = null;
        Game       game      = null;
        eUser      user1     = null;
        eUser      user2     = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            preparer.LoadPanels();
            preparer.OnPreparedBoards += OnGamePrepared;
        }
        
        eBoard board1 = null;
        eBoard board2 = null;


        private void OnGamePrepared()
        {
            button2.Visible = false;
            button3.Visible = false;
            preparer.OnPreparedBoards -= OnGamePrepared;
          
            preparer = null;

            game = new Game(user1, user2);
            game.Init(board1, board2, tableLayoutPanel1, tableLayoutPanel2, tableLayoutPanel3, tableLayoutPanel4);
            game.Start();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            preparer.OnItemPrepared();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            timer1.Enabled = true;
            preparer.SimulateInit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(preparer != null && !preparer.SimulateNext())
            {
                timer1.Enabled = false;
            }
        }
    }
}
