/*
Initialization and Basic Functionalty
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DavidsChess
{
    public partial class ChessHost : Form
    {

        private void button1_Click(object sender, EventArgs e)//init new game
        {
            string difficulty = "";
            string color = "";
            int diff = 0;

            Form2 frm2 = new Form2();
            if (frm2.ShowDialog() == DialogResult.OK)
            {
                difficulty = frm2.retDiff;
                if (difficulty == "Easy")
                {
                    diff = 3;
                }
                else if (difficulty == "Normal")
                {
                    diff = 4;
                }
                else
                {
                    diff = 5;
                }

                color = frm2.retCol;

                if (MessageBox.Show("Confirm You want to start new game!", "Are You Sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (color == "White")
                    {
                        game nGame = new game();
                        nGame.col = pieceCol.white;
                        userMove = true;
                        nGame.difficulty = diff;
                        listView1.Items.Clear();
                        startGame(nGame);
                    }
                    else
                    {
                        game nGame = new game();
                        nGame.col = pieceCol.black;
                        userMove = false;
                        nGame.difficulty = diff;
                        listView1.Items.Clear();
                        startGame(nGame);
                    }

                }
            }

        }

        //********************************************************************************

        private void startGame(game sGame)
        {
            firstTurn = true;
            color_panel();
            color = sGame.col;
            difficulty = sGame.difficulty;

            if (sGame.col == pieceCol.white)
            {
                //define characteristics of each piece 
                //top side of board
                bpawn1.pType = pieceTypes.pawn; bpawn1.pCol = pieceCol.black; bpawn1.x = 0; bpawn1.y = 1; bpawn1.condition = pCond.alive;
                bpawn2.pType = pieceTypes.pawn; bpawn2.pCol = pieceCol.black; bpawn2.x = 1; bpawn2.y = 1; bpawn2.condition = pCond.alive;
                bpawn3.pType = pieceTypes.pawn; bpawn3.pCol = pieceCol.black; bpawn3.x = 2; bpawn3.y = 1; bpawn3.condition = pCond.alive;
                bpawn4.pType = pieceTypes.pawn; bpawn4.pCol = pieceCol.black; bpawn4.x = 3; bpawn4.y = 1; bpawn4.condition = pCond.alive;
                bpawn5.pType = pieceTypes.pawn; bpawn5.pCol = pieceCol.black; bpawn5.x = 4; bpawn5.y = 1; bpawn5.condition = pCond.alive;
                bpawn6.pType = pieceTypes.pawn; bpawn6.pCol = pieceCol.black; bpawn6.x = 5; bpawn6.y = 1; bpawn6.condition = pCond.alive;
                bpawn7.pType = pieceTypes.pawn; bpawn7.pCol = pieceCol.black; bpawn7.x = 6; bpawn7.y = 1; bpawn7.condition = pCond.alive;
                bpawn8.pType = pieceTypes.pawn; bpawn8.pCol = pieceCol.black; bpawn8.x = 7; bpawn8.y = 1; bpawn8.condition = pCond.alive;
                brook1.pType = pieceTypes.rook; brook1.pCol = pieceCol.black; brook1.x = 0; brook1.y = 0; brook1.condition = pCond.alive;
                bknight1.pType = pieceTypes.knight; bknight1.pCol = pieceCol.black; bknight1.x = 1; bknight1.y = 0; bknight1.condition = pCond.alive;
                bbishop1.pType = pieceTypes.bishop; bbishop1.pCol = pieceCol.black; bbishop1.x = 2; bbishop1.y = 0; bbishop1.condition = pCond.alive;
                bqueen.pType = pieceTypes.queen; bqueen.pCol = pieceCol.black; bqueen.x = 3; bqueen.y = 0; bqueen.condition = pCond.alive;
                bking.pType = pieceTypes.king; bking.pCol = pieceCol.black; bking.x = 4; bking.y = 0; bking.condition = pCond.alive;
                bbishop2.pType = pieceTypes.bishop; bbishop2.pCol = pieceCol.black; bbishop2.x = 5; bbishop2.y = 0; bbishop2.condition = pCond.alive;
                bknight2.pType = pieceTypes.knight; bknight2.pCol = pieceCol.black; bknight2.x = 6; bknight2.y = 0; bknight2.condition = pCond.alive;
                brook2.pType = pieceTypes.rook; brook2.pCol = pieceCol.black; brook2.x = 7; brook2.y = 0; brook2.condition = pCond.alive;
                //bottom of board
                wpawn1.pType = pieceTypes.pawn; wpawn1.pCol = pieceCol.white; wpawn1.x = 0; wpawn1.y = 6; wpawn1.condition = pCond.alive;
                wpawn2.pType = pieceTypes.pawn; wpawn2.pCol = pieceCol.white; wpawn2.x = 1; wpawn2.y = 6; wpawn2.condition = pCond.alive;
                wpawn3.pType = pieceTypes.pawn; wpawn3.pCol = pieceCol.white; wpawn3.x = 2; wpawn3.y = 6; wpawn3.condition = pCond.alive;
                wpawn4.pType = pieceTypes.pawn; wpawn4.pCol = pieceCol.white; wpawn4.x = 3; wpawn4.y = 6; wpawn4.condition = pCond.alive;
                wpawn5.pType = pieceTypes.pawn; wpawn5.pCol = pieceCol.white; wpawn5.x = 4; wpawn5.y = 6; wpawn5.condition = pCond.alive;
                wpawn6.pType = pieceTypes.pawn; wpawn6.pCol = pieceCol.white; wpawn6.x = 5; wpawn6.y = 6; wpawn6.condition = pCond.alive;
                wpawn7.pType = pieceTypes.pawn; wpawn7.pCol = pieceCol.white; wpawn7.x = 6; wpawn7.y = 6; wpawn7.condition = pCond.alive;
                wpawn8.pType = pieceTypes.pawn; wpawn8.pCol = pieceCol.white; wpawn8.x = 7; wpawn8.y = 6; wpawn8.condition = pCond.alive;
                wrook1.pType = pieceTypes.rook; wrook1.pCol = pieceCol.white; wrook1.x = 0; wrook1.y = 7; wrook1.condition = pCond.alive;
                wknight1.pType = pieceTypes.knight; wknight1.pCol = pieceCol.white; wknight1.x = 1; wknight1.y = 7; wknight1.condition = pCond.alive;
                wbishop1.pType = pieceTypes.bishop; wbishop1.pCol = pieceCol.white; wbishop1.x = 2; wbishop1.y = 7; wbishop1.condition = pCond.alive;
                wqueen.pType = pieceTypes.queen; wqueen.pCol = pieceCol.white; wqueen.x = 3; wqueen.y = 7; wqueen.condition = pCond.alive;
                wking.pType = pieceTypes.king; wking.pCol = pieceCol.white; wking.x = 4; wking.y = 7; wking.condition = pCond.alive;
                wbishop2.pType = pieceTypes.bishop; wbishop2.pCol = pieceCol.white; wbishop2.x = 5; wbishop2.y = 7; wbishop2.condition = pCond.alive;
                wknight2.pType = pieceTypes.knight; wknight2.pCol = pieceCol.white; wknight2.x = 6; wknight2.y = 7; wknight2.condition = pCond.alive;
                wrook2.pType = pieceTypes.rook; wrook2.pCol = pieceCol.white; wrook2.x = 7; wrook2.y = 7; wrook2.condition = pCond.alive;
                none.pType = pieceTypes.none;

                //draw on board
                for (int a = 0; a < 8; a++)
                {
                    CPanels[a, 1].BackgroundImage = Properties.Resources.black_pawn;
                }
                CPanels[0, 0].BackgroundImage = Properties.Resources.black_rook;
                CPanels[1, 0].BackgroundImage = Properties.Resources.black_knight;
                CPanels[2, 0].BackgroundImage = Properties.Resources.black_bishop;
                CPanels[3, 0].BackgroundImage = Properties.Resources.black_queen;
                CPanels[4, 0].BackgroundImage = Properties.Resources.black_king;
                CPanels[5, 0].BackgroundImage = Properties.Resources.black_bishop;
                CPanels[6, 0].BackgroundImage = Properties.Resources.black_knight;
                CPanels[7, 0].BackgroundImage = Properties.Resources.black_rook;

                for (int b = 0; b < 8; b++)
                {
                    CPanels[b, 6].BackgroundImage = Properties.Resources.white_pawn;
                }
                CPanels[0, 7].BackgroundImage = Properties.Resources.white_rook;
                CPanels[1, 7].BackgroundImage = Properties.Resources.white_knight;
                CPanels[2, 7].BackgroundImage = Properties.Resources.white_bishop;
                CPanels[3, 7].BackgroundImage = Properties.Resources.white_queen;
                CPanels[4, 7].BackgroundImage = Properties.Resources.white_king;
                CPanels[5, 7].BackgroundImage = Properties.Resources.white_bishop;
                CPanels[6, 7].BackgroundImage = Properties.Resources.white_knight;
                CPanels[7, 7].BackgroundImage = Properties.Resources.white_rook;

                //bottom half
                sqs[0, 7].occupied = wrook1;
                sqs[1, 7].occupied = wknight1;
                sqs[2, 7].occupied = wbishop1;
                sqs[3, 7].occupied = wqueen;
                sqs[4, 7].occupied = wking;
                sqs[5, 7].occupied = wbishop2;
                sqs[6, 7].occupied = wknight2;
                sqs[7, 7].occupied = wrook2;
                sqs[0, 6].occupied = wpawn1;
                sqs[1, 6].occupied = wpawn2;
                sqs[2, 6].occupied = wpawn3;
                sqs[3, 6].occupied = wpawn4;
                sqs[4, 6].occupied = wpawn5;
                sqs[5, 6].occupied = wpawn6;
                sqs[6, 6].occupied = wpawn7;
                sqs[7, 6].occupied = wpawn8;
                //top half
                sqs[0, 0].occupied = brook1;
                sqs[1, 0].occupied = bknight1;
                sqs[2, 0].occupied = bbishop1;
                sqs[3, 0].occupied = bqueen;
                sqs[4, 0].occupied = bking;
                sqs[5, 0].occupied = bbishop2;
                sqs[6, 0].occupied = bknight2;
                sqs[7, 0].occupied = brook2;
                sqs[0, 1].occupied = bpawn1;
                sqs[1, 1].occupied = bpawn2;
                sqs[2, 1].occupied = bpawn3;
                sqs[3, 1].occupied = bpawn4;
                sqs[4, 1].occupied = bpawn5;
                sqs[5, 1].occupied = bpawn6;
                sqs[6, 1].occupied = bpawn7;
                sqs[7, 1].occupied = bpawn8;


            }
            else
            {
                //top of board
                wpawn1.pType = pieceTypes.pawn; wpawn1.pCol = pieceCol.white; wpawn1.x = 0; wpawn1.y = 1; wpawn1.condition = pCond.alive;
                wpawn2.pType = pieceTypes.pawn; wpawn2.pCol = pieceCol.white; wpawn2.x = 1; wpawn2.y = 1; wpawn2.condition = pCond.alive;
                wpawn3.pType = pieceTypes.pawn; wpawn3.pCol = pieceCol.white; wpawn3.x = 2; wpawn3.y = 1; wpawn3.condition = pCond.alive;
                wpawn4.pType = pieceTypes.pawn; wpawn4.pCol = pieceCol.white; wpawn4.x = 3; wpawn4.y = 1; wpawn4.condition = pCond.alive;
                wpawn5.pType = pieceTypes.pawn; wpawn5.pCol = pieceCol.white; wpawn5.x = 4; wpawn5.y = 1; wpawn5.condition = pCond.alive;
                wpawn6.pType = pieceTypes.pawn; wpawn6.pCol = pieceCol.white; wpawn6.x = 5; wpawn6.y = 1; wpawn6.condition = pCond.alive;
                wpawn7.pType = pieceTypes.pawn; wpawn7.pCol = pieceCol.white; wpawn7.x = 6; wpawn7.y = 1; wpawn7.condition = pCond.alive;
                wpawn8.pType = pieceTypes.pawn; wpawn8.pCol = pieceCol.white; wpawn8.x = 7; wpawn8.y = 1; wpawn8.condition = pCond.alive;
                wrook1.pType = pieceTypes.rook; wrook1.pCol = pieceCol.white; wrook1.x = 0; wrook1.y = 0; wrook1.condition = pCond.alive;
                wknight1.pType = pieceTypes.knight; wknight1.pCol = pieceCol.white; wknight1.x = 1; wknight1.y = 0; wknight1.condition = pCond.alive;
                wbishop1.pType = pieceTypes.bishop; wbishop1.pCol = pieceCol.white; wbishop1.x = 2; wbishop1.y = 0; wbishop1.condition = pCond.alive;
                wqueen.pType = pieceTypes.queen; wqueen.pCol = pieceCol.white; wqueen.x = 3; wqueen.y = 0; wqueen.condition = pCond.alive;
                wking.pType = pieceTypes.king; wking.pCol = pieceCol.white; wking.x = 4; wking.y = 0; wking.condition = pCond.alive;
                wbishop2.pType = pieceTypes.bishop; wbishop2.pCol = pieceCol.white; wbishop2.x = 5; wbishop2.y = 0; wbishop2.condition = pCond.alive;
                wknight2.pType = pieceTypes.knight; wknight2.pCol = pieceCol.white; wknight2.x = 6; wknight2.y = 0; wknight2.condition = pCond.alive;
                wrook2.pType = pieceTypes.rook; wrook2.pCol = pieceCol.white; wrook2.x = 7; wrook2.y = 0; wrook2.condition = pCond.alive;
                //bottom of board
                bpawn1.pType = pieceTypes.pawn; bpawn1.pCol = pieceCol.black; bpawn1.x = 0; bpawn1.y = 6; bpawn1.condition = pCond.alive;
                bpawn2.pType = pieceTypes.pawn; bpawn2.pCol = pieceCol.black; bpawn2.x = 1; bpawn2.y = 6; bpawn2.condition = pCond.alive;
                bpawn3.pType = pieceTypes.pawn; bpawn3.pCol = pieceCol.black; bpawn3.x = 2; bpawn3.y = 6; bpawn3.condition = pCond.alive;
                bpawn4.pType = pieceTypes.pawn; bpawn4.pCol = pieceCol.black; bpawn4.x = 3; bpawn4.y = 6; bpawn4.condition = pCond.alive;
                bpawn5.pType = pieceTypes.pawn; bpawn5.pCol = pieceCol.black; bpawn5.x = 4; bpawn5.y = 6; bpawn5.condition = pCond.alive;
                bpawn6.pType = pieceTypes.pawn; bpawn6.pCol = pieceCol.black; bpawn6.x = 5; bpawn6.y = 6; bpawn6.condition = pCond.alive;
                bpawn7.pType = pieceTypes.pawn; bpawn7.pCol = pieceCol.black; bpawn7.x = 6; bpawn7.y = 6; bpawn7.condition = pCond.alive;
                bpawn8.pType = pieceTypes.pawn; bpawn8.pCol = pieceCol.black; bpawn8.x = 7; bpawn8.y = 6; bpawn8.condition = pCond.alive;
                brook1.pType = pieceTypes.rook; brook1.pCol = pieceCol.black; brook1.x = 0; brook1.y = 7; brook1.condition = pCond.alive;
                bknight1.pType = pieceTypes.knight; bknight1.pCol = pieceCol.black; bknight1.x = 1; bknight1.y = 7; bknight1.condition = pCond.alive;
                bbishop1.pType = pieceTypes.bishop; bbishop1.pCol = pieceCol.black; bbishop1.x = 2; bbishop1.y = 7; bbishop1.condition = pCond.alive;
                bqueen.pType = pieceTypes.queen; bqueen.pCol = pieceCol.black; bqueen.x = 3; bqueen.y = 7; bqueen.condition = pCond.alive;
                bking.pType = pieceTypes.king; bking.pCol = pieceCol.black; bking.x = 4; bking.y = 7; bking.condition = pCond.alive;
                bbishop2.pType = pieceTypes.bishop; bbishop2.pCol = pieceCol.black; bbishop2.x = 5; bbishop2.y = 7; bbishop2.condition = pCond.alive;
                bknight2.pType = pieceTypes.knight; bknight2.pCol = pieceCol.black; bknight2.x = 6; bknight2.y = 7; bknight2.condition = pCond.alive;
                brook2.pType = pieceTypes.rook; brook2.pCol = pieceCol.black; brook2.x = 7; brook2.y = 7; brook2.condition = pCond.alive;
                none.pType = pieceTypes.none;

                for (int b = 0; b < 8; b++)
                {
                    CPanels[b, 1].BackgroundImage = Properties.Resources.white_pawn;
                }
                CPanels[0, 0].BackgroundImage = Properties.Resources.white_rook;
                CPanels[1, 0].BackgroundImage = Properties.Resources.white_knight;
                CPanels[2, 0].BackgroundImage = Properties.Resources.white_bishop;
                CPanels[3, 0].BackgroundImage = Properties.Resources.white_queen;
                CPanels[4, 0].BackgroundImage = Properties.Resources.white_king;
                CPanels[5, 0].BackgroundImage = Properties.Resources.white_bishop;
                CPanels[6, 0].BackgroundImage = Properties.Resources.white_knight;
                CPanels[7, 0].BackgroundImage = Properties.Resources.white_rook;

                for (int a = 0; a < 8; a++)
                {
                    CPanels[a, 6].BackgroundImage = Properties.Resources.black_pawn;
                }
                CPanels[0, 7].BackgroundImage = Properties.Resources.black_rook;
                CPanels[1, 7].BackgroundImage = Properties.Resources.black_knight;
                CPanels[2, 7].BackgroundImage = Properties.Resources.black_bishop;
                CPanels[3, 7].BackgroundImage = Properties.Resources.black_queen;
                CPanels[4, 7].BackgroundImage = Properties.Resources.black_king;
                CPanels[5, 7].BackgroundImage = Properties.Resources.black_bishop;
                CPanels[6, 7].BackgroundImage = Properties.Resources.black_knight;
                CPanels[7, 7].BackgroundImage = Properties.Resources.black_rook;

                //bottom half
                sqs[0, 7].occupied = brook1;
                sqs[1, 7].occupied = bknight1;
                sqs[2, 7].occupied = bbishop1;
                sqs[3, 7].occupied = bqueen;
                sqs[4, 7].occupied = bking;
                sqs[5, 7].occupied = bbishop2;
                sqs[6, 7].occupied = bknight2;
                sqs[7, 7].occupied = brook2;
                sqs[0, 6].occupied = bpawn1;
                sqs[1, 6].occupied = bpawn2;
                sqs[2, 6].occupied = bpawn3;
                sqs[3, 6].occupied = bpawn4;
                sqs[4, 6].occupied = bpawn5;
                sqs[5, 6].occupied = bpawn6;
                sqs[6, 6].occupied = bpawn7;
                sqs[7, 6].occupied = bpawn8;
                //top half
                sqs[0, 0].occupied = wrook1;
                sqs[1, 0].occupied = wknight1;
                sqs[2, 0].occupied = wbishop1;
                sqs[3, 0].occupied = wqueen;
                sqs[4, 0].occupied = wking;
                sqs[5, 0].occupied = wbishop2;
                sqs[6, 0].occupied = wknight2;
                sqs[7, 0].occupied = wrook2;
                sqs[0, 1].occupied = wpawn1;
                sqs[1, 1].occupied = wpawn2;
                sqs[2, 1].occupied = wpawn3;
                sqs[3, 1].occupied = wpawn4;
                sqs[4, 1].occupied = wpawn5;
                sqs[5, 1].occupied = wpawn6;
                sqs[6, 1].occupied = wpawn7;
                sqs[7, 1].occupied = wpawn8;
            }

            //initialize empty squares
            for (int a = 0; a < 8; a++)
            {
                sqs[a, 2].occupied = none;
                sqs[a, 3].occupied = none;
                sqs[a, 4].occupied = none;
                sqs[a, 5].occupied = none;

                CPanels[a, 2].BackgroundImage = null;
                CPanels[a, 3].BackgroundImage = null;
                CPanels[a, 4].BackgroundImage = null;
                CPanels[a, 5].BackgroundImage = null;
            }

            if (!userMove)
            {
                compMove();
            }
        }

        //**************************************************************************

        private void Pan_Click(object sender, EventArgs e) //get users click
        {
            if (!click2)
            {
                color_panel();
                click2 = true;
                var panel = sender as Panel;
                if (panel == null)
                {
                    return;
                }
                //remove buffer and divide by square's size to get x and y
                gx = (panel.Location.X - 300) / 80;
                gy = (panel.Location.Y - 25) / 80;

                //check if nothing is selected
                piece piec = sqs[gx, gy].occupied;
                if (checkForNone(piec) && CPanels[gx, gy].BackColor != Color.LightBlue && CPanels[gx, gy].BackColor != Color.DeepPink)
                {
                    click2 = false;
                }
                else if (CPanels[gx, gy].BackColor == Color.LightBlue)
                {
                    moveto(gx, gy, selectedObj);
                    click2 = false;
                }
                else if (CPanels[gx, gy].BackColor == Color.DeepPink)
                {
                    attack(gx, gy, selectedObj);
                    click2 = false;
                }
                else
                {
                    showValidMoves(gx, gy);
                    selectedObj = sqs[gx, gy].occupied;
                }
            }
            else
            {
                //first we check if the user switched to another of our pieces
                var panel = sender as Panel;
                if (panel == null)
                {
                    return;
                }
                int hx = (panel.Location.X - 300) / 80;
                int hy = (panel.Location.Y - 25) / 80;

                //check here
                piece piec = sqs[hx, hy].occupied;
                if (checkForNone(piec) == false && piec.pCol == color)
                {
                    click2 = false;
                    color_panel();
                    showValidMoves(hx, hy);
                    selectedObj = sqs[hx, hy].occupied;
                    return;
                }

                if (CPanels[hx, hy].BackColor == Color.LightBlue)
                {
                    moveto(hx, hy, selectedObj);
                }
                else if (CPanels[hx, hy].BackColor == Color.DeepPink)
                {
                    attack(hx, hy, selectedObj);
                }

                click2 = false;
            }

        }

        //*****************************************************************************

        private void color_panel()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    var colW = Color.White;
                    var colB = Color.DarkGray;
                    //color piece
                    if (y % 2 == 0)
                    {
                        CPanels[x, y].BackColor = x % 2 != 0 ? colB : colW;
                    }
                    else
                    {
                        CPanels[x, y].BackColor = x % 2 != 0 ? colW : colB;
                    }
                }
            }
        }

        //*******************************************************************************

        private bool checkForNone(piece pce)
        {
            if (pce.pType != pieceTypes.bishop && pce.pType != pieceTypes.king && pce.pType != pieceTypes.knight && pce.pType != pieceTypes.pawn && pce.pType != pieceTypes.queen && pce.pType != pieceTypes.rook)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //*********************************************************************************
        //*********************************************************************************
        int gx = 0;
        int gy = 0;
        bool click2 = false;
        piece selectedObj;

        squares[,] sqs = new squares[8, 8]; //initialize descriptor for each square
        pieceCol color;

        //black pieces
        piece bpawn1 = new piece();
        piece bpawn2 = new piece();
        piece bpawn3 = new piece();
        piece bpawn4 = new piece();
        piece bpawn5 = new piece();
        piece bpawn6 = new piece();
        piece bpawn7 = new piece();
        piece bpawn8 = new piece();
        piece brook1 = new piece();
        piece brook2 = new piece();
        piece bknight1 = new piece();
        piece bknight2 = new piece();
        piece bbishop1 = new piece();
        piece bbishop2 = new piece();
        piece bqueen = new piece();
        piece bking = new piece();

        //white pieces
        piece wpawn1 = new piece();
        piece wpawn2 = new piece();
        piece wpawn3 = new piece();
        piece wpawn4 = new piece();
        piece wpawn5 = new piece();
        piece wpawn6 = new piece();
        piece wpawn7 = new piece();
        piece wpawn8 = new piece();
        piece wrook1 = new piece();
        piece wrook2 = new piece();
        piece wknight1 = new piece();
        piece wknight2 = new piece();
        piece wbishop1 = new piece();
        piece wbishop2 = new piece();
        piece wqueen = new piece();
        piece wking = new piece();

        piece none = new piece();

        piece pce;
        string answer;

        bool recur = true;

    }
}
