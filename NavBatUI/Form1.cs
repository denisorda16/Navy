﻿using NavBatProject;
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

            board1 = new eBoard();
            board2 = new eBoard();
            preparer = new PrepareGame(board1,
                                       board2,
                                       tableLayoutPanel1,
                                       tableLayoutPanel2,
                                       label1);
        }

        PrepareGame preparer = null;
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
            preparer.OnPreparedBoards -= OnGamePrepared;
            UITools.LoadPanel(tableLayoutPanel3, CellClick);
            UITools.LoadPanel(tableLayoutPanel4, CellClick);
            tableLayoutPanel3.Visible = true; 
        }

        private void CellClick(object sender, EventArgs e)
        {
        }



        private void button2_Click(object sender, EventArgs e)
        {
            preparer.OnItemPrepared();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
