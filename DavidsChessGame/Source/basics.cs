/*
Functions to handle game initialization, handling user clicks, rendering , and other basic definitions
 * Implement :
        1. AI moves (logic tree & chess engine)
        2. Multiplayer (add cancel request if user wants to cancel request)
             **Also, might need to optimize isCheck() and make sure game.squares & game.userPieces are correct at 
               all points of normal game.
        3. Save game state after every move (game history)
             **Might be possible for online play by appending new moves along with old moves in the mySQL "moves" index.
               We seperate them by something and use "movenum" to fetch the one we want (usually most recent one).
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

namespace DavidsChessGame
{
    public partial class Form1 : Form
    {
        Game game = new Game();
        bool fclick = true;
        bool userTurn = true;
        int currentSelection = 0;

        bool multiplayerMove = false;

        bool multiplayer = false;
        int gameid = 0;
        bool firstTurn = false;
        int movenum, id = 0;

        private void newGameButton_Click(object sender, EventArgs e)
        {
            //ask user to choose color and difficulty
            Form2 frm2 = new Form2();
            if (frm2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                userTurn = true;
                fclick = true;
                //removes panels from previous game
                foreach (Panel pan in game.renderer)
                {
                    this.Controls.Remove(pan);
                }

                game.userColor = frm2.color;
                game.difficulty = frm2.diff;
                multiplayer = frm2.multiplayer;
                Properties.Settings.Default.nick = frm2.nick;
                Properties.Settings.Default.movenum = 0;
                Properties.Settings.Default.Save();

                if (multiplayer)
                {
                    Form3 frm3 = new Form3();
                    if (frm3.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        gameid = frm3.id;
                        game.userColor = frm3.col;
                    }
                    timer1.Enabled = true;
                }

                for (int a = 0; a < 8; a++)
                {
                    for (int b = 0; b < 8; b++)
                    {
                        var newPanel = new Panel
                        {
                            Size = new Size(80, 80),
                            Location = new Point(145 + b * 80, 20 + a * 80)
                        };

                        Controls.Add(newPanel);

                        if (a % 2 == 0)
                        {
                            newPanel.BackColor = (b % 2 == 0) ? Color.White : Color.Gray;
                        }
                        else
                        {
                            newPanel.BackColor = (b % 2 == 0) ? Color.Gray : Color.White;
                        }

                        newPanel.Click += pan_click;

                        game.renderer[b, a] = newPanel;
                        game.MovBoard[b, a].moveable = false;
                        game.squares[b, a].name = null;
                        game.squares[b, a].type = PTypes.none;
                    }
                }

                //fill up userPieces[] array, opponentPieces[] array, and squares[] array!
                if (game.userColor == Color.White)
                {
                    game.squares[0, 0].name = "brook1";
                    game.squares[0, 0].type = PTypes.rook;
                    game.squares[1, 0].name = "bknight1";
                    game.squares[1, 0].type = PTypes.knight;
                    game.squares[2, 0].name = "bbishop1";
                    game.squares[2, 0].type = PTypes.bishop;
                    game.squares[3, 0].name = "bqueen";
                    game.squares[3, 0].type = PTypes.queen;
                    game.squares[4, 0].name = "bking";
                    game.squares[4, 0].type = PTypes.king;
                    game.squares[5, 0].name = "bbishop2";
                    game.squares[5, 0].type = PTypes.bishop;
                    game.squares[6, 0].name = "bknight2";
                    game.squares[6, 0].type = PTypes.knight;
                    game.squares[7, 0].name = "brook2";
                    game.squares[7, 0].type = PTypes.rook;

                    game.squares[0, 7].name = "wrook1";
                    game.squares[0, 7].type = PTypes.rook;
                    game.squares[1, 7].name = "wknight1";
                    game.squares[1, 7].type = PTypes.knight;
                    game.squares[2, 7].name = "wbishop1";
                    game.squares[2, 7].type = PTypes.bishop;
                    game.squares[3, 7].name = "wqueen";
                    game.squares[3, 7].type = PTypes.queen;
                    game.squares[4, 7].name = "wking";
                    game.squares[4, 7].type = PTypes.king;
                    game.squares[5, 7].name = "wbishop2";
                    game.squares[5, 7].type = PTypes.bishop;
                    game.squares[6, 7].name = "wknight2";
                    game.squares[6, 7].type = PTypes.knight;
                    game.squares[7, 7].name = "wrook2";
                    game.squares[7, 7].type = PTypes.rook;

                    game.opponentPieces[0].x = 0; game.opponentPieces[0].y = 0; game.opponentPieces[0].name = "brook1"; game.opponentPieces[0].type = PTypes.rook;
                    game.opponentPieces[1].x = 1; game.opponentPieces[1].y = 0; game.opponentPieces[1].name = "bknight1"; game.opponentPieces[1].type = PTypes.knight;
                    game.opponentPieces[2].x = 2; game.opponentPieces[2].y = 0; game.opponentPieces[2].name = "bbishop1"; game.opponentPieces[2].type = PTypes.bishop;
                    game.opponentPieces[3].x = 3; game.opponentPieces[3].y = 0; game.opponentPieces[3].name = "bqueen"; game.opponentPieces[3].type = PTypes.queen;
                    game.opponentPieces[4].x = 4; game.opponentPieces[4].y = 0; game.opponentPieces[4].name = "bking"; game.opponentPieces[4].type = PTypes.king;
                    game.opponentPieces[5].x = 5; game.opponentPieces[5].y = 0; game.opponentPieces[5].name = "bbishop2"; game.opponentPieces[5].type = PTypes.bishop;
                    game.opponentPieces[6].x = 6; game.opponentPieces[6].y = 0; game.opponentPieces[6].name = "bknight2"; game.opponentPieces[6].type = PTypes.knight;
                    game.opponentPieces[7].x = 7; game.opponentPieces[7].y = 0; game.opponentPieces[7].name = "brook2"; game.opponentPieces[7].type = PTypes.rook;
                    game.opponentPieces[8].x = 0; game.opponentPieces[8].y = 1; game.opponentPieces[8].name = "bpawn1"; game.opponentPieces[8].type = PTypes.pawn;
                    game.opponentPieces[9].x = 1; game.opponentPieces[9].y = 1; game.opponentPieces[9].name = "bpawn2"; game.opponentPieces[9].type = PTypes.pawn;
                    game.opponentPieces[10].x = 2; game.opponentPieces[10].y = 1; game.opponentPieces[10].name = "bpawn3"; game.opponentPieces[10].type = PTypes.pawn;
                    game.opponentPieces[11].x = 3; game.opponentPieces[11].y = 1; game.opponentPieces[11].name = "bpawn4"; game.opponentPieces[11].type = PTypes.pawn;
                    game.opponentPieces[12].x = 4; game.opponentPieces[12].y = 1; game.opponentPieces[12].name = "bpawn5"; game.opponentPieces[12].type = PTypes.pawn;
                    game.opponentPieces[13].x = 5; game.opponentPieces[13].y = 1; game.opponentPieces[13].name = "bpawn6"; game.opponentPieces[13].type = PTypes.pawn;
                    game.opponentPieces[14].x = 6; game.opponentPieces[14].y = 1; game.opponentPieces[14].name = "bpawn7"; game.opponentPieces[14].type = PTypes.pawn;
                    game.opponentPieces[15].x = 7; game.opponentPieces[15].y = 1; game.opponentPieces[15].name = "bpawn8"; game.opponentPieces[15].type = PTypes.pawn;

                    game.userPieces[0].x = 0; game.userPieces[0].y = 7; game.userPieces[0].name = "wrook1"; game.userPieces[0].type = PTypes.rook;
                    game.userPieces[1].x = 1; game.userPieces[1].y = 7; game.userPieces[1].name = "wknight1"; game.userPieces[1].type = PTypes.knight;
                    game.userPieces[2].x = 2; game.userPieces[2].y = 7; game.userPieces[2].name = "wbishop1"; game.userPieces[2].type = PTypes.bishop;
                    game.userPieces[3].x = 3; game.userPieces[3].y = 7; game.userPieces[3].name = "wqueen"; game.userPieces[3].type = PTypes.queen;
                    game.userPieces[4].x = 4; game.userPieces[4].y = 7; game.userPieces[4].name = "wking"; game.userPieces[4].type = PTypes.king;
                    game.userPieces[5].x = 5; game.userPieces[5].y = 7; game.userPieces[5].name = "wbishop2"; game.userPieces[5].type = PTypes.bishop;
                    game.userPieces[6].x = 6; game.userPieces[6].y = 7; game.userPieces[6].name = "wknight2"; game.userPieces[6].type = PTypes.knight;
                    game.userPieces[7].x = 7; game.userPieces[7].y = 7; game.userPieces[7].name = "wrook2"; game.userPieces[7].type = PTypes.rook;
                    game.userPieces[8].x = 0; game.userPieces[8].y = 6; game.userPieces[8].name = "wpawn1"; game.userPieces[8].type = PTypes.pawn;
                    game.userPieces[9].x = 1; game.userPieces[9].y = 6; game.userPieces[9].name = "wpawn2"; game.userPieces[9].type = PTypes.pawn;
                    game.userPieces[10].x = 2; game.userPieces[10].y = 6; game.userPieces[10].name = "wpawn3"; game.userPieces[10].type = PTypes.pawn;
                    game.userPieces[11].x = 3; game.userPieces[11].y = 6; game.userPieces[11].name = "wpawn4"; game.userPieces[11].type = PTypes.pawn;
                    game.userPieces[12].x = 4; game.userPieces[12].y = 6; game.userPieces[12].name = "wpawn5"; game.userPieces[12].type = PTypes.pawn;
                    game.userPieces[13].x = 5; game.userPieces[13].y = 6; game.userPieces[13].name = "wpawn6"; game.userPieces[13].type = PTypes.pawn;
                    game.userPieces[14].x = 6; game.userPieces[14].y = 6; game.userPieces[14].name = "wpawn7"; game.userPieces[14].type = PTypes.pawn;
                    game.userPieces[15].x = 7; game.userPieces[15].y = 6; game.userPieces[15].name = "wpawn8"; game.userPieces[15].type = PTypes.pawn;

                }
                else
                {
                    game.squares[0, 0].name = "wrook1";
                    game.squares[0, 0].type = PTypes.rook;
                    game.squares[1, 0].name = "wknight1";
                    game.squares[1, 0].type = PTypes.knight;
                    game.squares[2, 0].name = "wbishop1";
                    game.squares[2, 0].type = PTypes.bishop;
                    game.squares[3, 0].name = "wqueen";
                    game.squares[3, 0].type = PTypes.queen;
                    game.squares[4, 0].name = "wking";
                    game.squares[4, 0].type = PTypes.king;
                    game.squares[5, 0].name = "wbishop2";
                    game.squares[5, 0].type = PTypes.bishop;
                    game.squares[6, 0].name = "wknight2";
                    game.squares[6, 0].type = PTypes.knight;
                    game.squares[7, 0].name = "wrook2";
                    game.squares[7, 0].type = PTypes.rook;

                    game.squares[0, 7].name = "brook1";
                    game.squares[0, 7].type = PTypes.rook;
                    game.squares[1, 7].name = "bknight1";
                    game.squares[1, 7].type = PTypes.knight;
                    game.squares[2, 7].name = "bbishop1";
                    game.squares[2, 7].type = PTypes.bishop;
                    game.squares[3, 7].name = "bqueen";
                    game.squares[3, 7].type = PTypes.queen;
                    game.squares[4, 7].name = "bking";
                    game.squares[4, 7].type = PTypes.king;
                    game.squares[5, 7].name = "bbishop2";
                    game.squares[5, 7].type = PTypes.bishop;
                    game.squares[6, 7].name = "bknight2";
                    game.squares[6, 7].type = PTypes.knight;
                    game.squares[7, 7].name = "brook2";
                    game.squares[7, 7].type = PTypes.rook;

                    game.opponentPieces[0].x = 0; game.opponentPieces[0].y = 0; game.opponentPieces[0].name = "wrook1"; game.opponentPieces[0].type = PTypes.rook;
                    game.opponentPieces[1].x = 1; game.opponentPieces[1].y = 0; game.opponentPieces[1].name = "wknight1"; game.opponentPieces[1].type = PTypes.knight;
                    game.opponentPieces[2].x = 2; game.opponentPieces[2].y = 0; game.opponentPieces[2].name = "wbishop1"; game.opponentPieces[2].type = PTypes.bishop;
                    game.opponentPieces[3].x = 3; game.opponentPieces[3].y = 0; game.opponentPieces[3].name = "wqueen"; game.opponentPieces[3].type = PTypes.queen;
                    game.opponentPieces[4].x = 4; game.opponentPieces[4].y = 0; game.opponentPieces[4].name = "wking"; game.opponentPieces[4].type = PTypes.king;
                    game.opponentPieces[5].x = 5; game.opponentPieces[5].y = 0; game.opponentPieces[5].name = "wbishop2"; game.opponentPieces[5].type = PTypes.bishop;
                    game.opponentPieces[6].x = 6; game.opponentPieces[6].y = 0; game.opponentPieces[6].name = "wknight2"; game.opponentPieces[6].type = PTypes.knight;
                    game.opponentPieces[7].x = 7; game.opponentPieces[7].y = 0; game.opponentPieces[7].name = "wrook2"; game.opponentPieces[7].type = PTypes.rook;
                    game.opponentPieces[8].x = 0; game.opponentPieces[8].y = 1; game.opponentPieces[8].name = "wpawn1"; game.opponentPieces[8].type = PTypes.pawn;
                    game.opponentPieces[9].x = 1; game.opponentPieces[9].y = 1; game.opponentPieces[9].name = "wpawn2"; game.opponentPieces[9].type = PTypes.pawn;
                    game.opponentPieces[10].x = 2; game.opponentPieces[10].y = 1; game.opponentPieces[10].name = "wpawn3"; game.opponentPieces[10].type = PTypes.pawn;
                    game.opponentPieces[11].x = 3; game.opponentPieces[11].y = 1; game.opponentPieces[11].name = "wpawn4"; game.opponentPieces[11].type = PTypes.pawn;
                    game.opponentPieces[12].x = 4; game.opponentPieces[12].y = 1; game.opponentPieces[12].name = "wpawn5"; game.opponentPieces[12].type = PTypes.pawn;
                    game.opponentPieces[13].x = 5; game.opponentPieces[13].y = 1; game.opponentPieces[13].name = "wpawn6"; game.opponentPieces[13].type = PTypes.pawn;
                    game.opponentPieces[14].x = 6; game.opponentPieces[14].y = 1; game.opponentPieces[14].name = "wpawn7"; game.opponentPieces[14].type = PTypes.pawn;
                    game.opponentPieces[15].x = 7; game.opponentPieces[15].y = 1; game.opponentPieces[15].name = "wpawn8"; game.opponentPieces[15].type = PTypes.pawn;

                    game.userPieces[0].x = 0; game.userPieces[0].y = 7; game.userPieces[0].name = "brook1"; game.userPieces[0].type = PTypes.rook;
                    game.userPieces[1].x = 1; game.userPieces[1].y = 7; game.userPieces[1].name = "bknight1"; game.userPieces[1].type = PTypes.knight;
                    game.userPieces[2].x = 2; game.userPieces[2].y = 7; game.userPieces[2].name = "bbishop1"; game.userPieces[2].type = PTypes.bishop;
                    game.userPieces[3].x = 3; game.userPieces[3].y = 7; game.userPieces[3].name = "bqueen"; game.userPieces[3].type = PTypes.queen;
                    game.userPieces[4].x = 4; game.userPieces[4].y = 7; game.userPieces[4].name = "bking"; game.userPieces[4].type = PTypes.king;
                    game.userPieces[5].x = 5; game.userPieces[5].y = 7; game.userPieces[5].name = "bbishop2"; game.userPieces[5].type = PTypes.bishop;
                    game.userPieces[6].x = 6; game.userPieces[6].y = 7; game.userPieces[6].name = "bknight2"; game.userPieces[6].type = PTypes.knight;
                    game.userPieces[7].x = 7; game.userPieces[7].y = 7; game.userPieces[7].name = "brook2"; game.userPieces[7].type = PTypes.rook;
                    game.userPieces[8].x = 0; game.userPieces[8].y = 6; game.userPieces[8].name = "bpawn1"; game.userPieces[8].type = PTypes.pawn;
                    game.userPieces[9].x = 1; game.userPieces[9].y = 6; game.userPieces[9].name = "bpawn2"; game.userPieces[9].type = PTypes.pawn;
                    game.userPieces[10].x = 2; game.userPieces[10].y = 6; game.userPieces[10].name = "bpawn3"; game.userPieces[10].type = PTypes.pawn;
                    game.userPieces[11].x = 3; game.userPieces[11].y = 6; game.userPieces[11].name = "bpawn4"; game.userPieces[11].type = PTypes.pawn;
                    game.userPieces[12].x = 4; game.userPieces[12].y = 6; game.userPieces[12].name = "bpawn5"; game.userPieces[12].type = PTypes.pawn;
                    game.userPieces[13].x = 5; game.userPieces[13].y = 6; game.userPieces[13].name = "bpawn6"; game.userPieces[13].type = PTypes.pawn;
                    game.userPieces[14].x = 6; game.userPieces[14].y = 6; game.userPieces[14].name = "bpawn7"; game.userPieces[14].type = PTypes.pawn;
                    game.userPieces[15].x = 7; game.userPieces[15].y = 6; game.userPieces[15].name = "bpawn8"; game.userPieces[15].type = PTypes.pawn;

                }

                //make sure all pieces alive at start
                for (int a = 0; a < 16; a++)
                {
                    game.userPieces[a].alive = true;
                    game.opponentPieces[a].alive = true;
                }

                for (int a = 0; a < 8; a++)
                {
                    //populate pawns
                    if (game.userColor == Color.White)
                    {
                        game.squares[a, 1].name = "bpawn" + (a + 1).ToString();
                        game.squares[a, 1].type = PTypes.pawn;

                        game.squares[a, 6].name = "wpawn" + (a + 1).ToString();
                        game.squares[a, 6].type = PTypes.pawn;

                    }
                    else
                    {
                        game.squares[a, 1].name = "wpawn" + (a + 1).ToString();
                        game.squares[a, 1].type = PTypes.pawn;

                        game.squares[a, 6].name = "bpawn" + (a + 1).ToString();
                        game.squares[a, 6].type = PTypes.pawn;

                    }

                }

                //set each pawn as unmoved at start
                for (int a = 8; a < 16; a++)
                {
                    game.userPieces[a].unMoved = true;
                    game.opponentPieces[a].unMoved = true;
                }

                render();



                if (game.userColor == Color.Black)
                {
                    userTurn = false;
                    if (multiplayer)
                    {
                        //block until opponent moves
                    }
                    else
                    {
                        //run AI here
                    }
                }

            }

        }

        //*************************************************************************************************

        private void pan_click(object sender, EventArgs e)
        {
            if (userTurn)
            {
                //check for checkmate
                //in multiplayer, this seems to cause oponent piece to switch from alive to dead
                //              **should be fixed, keep testing
                if (checkMate(game.squares))
                {
                    //end game here
                    MessageBox.Show("CHECK MATE!!!    Loser!");
                }

                var panel = sender as Panel;

                int x = (panel.Location.X - 140) / 80;
                int y = (panel.Location.Y - 20) / 80;
                string pname = game.squares[x, y].name;
                PTypes type = game.squares[x, y].type;

                if (fclick)
                {
                    //check which piece is selected
                    for (int a = 0; a < 16; a++)
                    {
                        if (game.userPieces[a].name == pname && game.userPieces[a].type == type)
                        {
                            currentSelection = a;
                            showValidMoves(game.userPieces[a], game.squares, ref game.MovBoard, ref game.renderer);
                            break;
                        }
                    }
                    fclick = false;
                }
                else
                {
                    //move
                    if (game.MovBoard[x, y].moveable)
                    {
                        multiplayerMove = true;
                        bool isCheck = false;
                        game.userPieces[currentSelection] = move(game.userPieces[currentSelection], x, y, true, ref game.squares, out isCheck);
                        multiplayerMove = false;

                        render();

                        //problem here!
                        if (checkMate(game.squares))
                        {
                            MessageBox.Show("CHECK MATE  Loser!!");
                        }

                        //reset background color
                        for (int a = 0; a < 8; a++)
                        {
                            for (int b = 0; b < 8; b++)
                            {
                                if (a % 2 == 0)
                                {
                                    game.renderer[a, b].BackColor = (b % 2 == 0) ? Color.White : Color.Gray;
                                }
                                else
                                {
                                    game.renderer[a, b].BackColor = (b % 2 == 0) ? Color.Gray : Color.White;
                                }
                            }
                        }

                        //if user is in check, alert
                        if (isCheck)
                        {
                            MessageBox.Show("Can't move, in check");
                        }
                    }

                    //reset background colors
                    for (int a = 0; a < 8; a++)
                    {
                        for (int b = 0; b < 8; b++)
                        {
                            if (a % 2 == 0)
                            {
                                game.renderer[a, b].BackColor = (b % 2 == 0) ? Color.White : Color.Gray;
                            }
                            else
                            {
                                game.renderer[a, b].BackColor = (b % 2 == 0) ? Color.Gray : Color.White;
                            }
                        }
                    }

                    fclick = true;
                }
            }

        }

        //*****************************************************************************************
        private void render()
        {
            //render
            for (int b = 0; b < 8; b++)
            {
                for (int a = 0; a < 8; a++)
                {
                    if (game.squares[a, b].name == "bpawn1" || game.squares[a, b].name == "bpawn2" || game.squares[a, b].name == "bpawn3" || game.squares[a, b].name == "bpawn4" || game.squares[a, b].name == "bpawn5" || game.squares[a, b].name == "bpawn6" || game.squares[a, b].name == "bpawn7" || game.squares[a, b].name == "bpawn8")
                    {
                        game.renderer[a, b].BackgroundImage = DavidsChessGame.Properties.Resources.black_pawn;
                    }else if (game.squares[a, b].name == "wpawn1" || game.squares[a, b].name == "wpawn2" || game.squares[a, b].name == "wpawn3" || game.squares[a, b].name == "wpawn4" || game.squares[a, b].name == "wpawn5" || game.squares[a, b].name == "wpawn6" || game.squares[a, b].name == "wpawn7" || game.squares[a, b].name == "wpawn8")
                    {
                        game.renderer[a, b].BackgroundImage = DavidsChessGame.Properties.Resources.white_pawn;
                    }
                    else if (game.squares[a, b].name == "brook1" || game.squares[a, b].name == "brook2")
                    {
                        game.renderer[a, b].BackgroundImage = DavidsChessGame.Properties.Resources.black_rook;
                    }
                    else if (game.squares[a, b].name == "wrook1" || game.squares[a, b].name == "wrook2")
                    {
                        game.renderer[a, b].BackgroundImage = DavidsChessGame.Properties.Resources.white_rook;
                    }
                    else if (game.squares[a, b].name == "bknight1" || game.squares[a, b].name == "bknight2")
                    {
                        game.renderer[a, b].BackgroundImage = DavidsChessGame.Properties.Resources.black_knight;
                    }
                    else if (game.squares[a, b].name == "wknight1" || game.squares[a, b].name == "wknight2")
                    {
                        game.renderer[a, b].BackgroundImage = DavidsChessGame.Properties.Resources.white_knight;
                    }
                    else if (game.squares[a, b].name == "bbishop1" || game.squares[a, b].name == "bbishop2")
                    {
                        game.renderer[a, b].BackgroundImage = DavidsChessGame.Properties.Resources.black_bishop;
                    }
                    else if (game.squares[a, b].name == "wbishop1" || game.squares[a, b].name == "wbishop2")
                    {
                        game.renderer[a, b].BackgroundImage = DavidsChessGame.Properties.Resources.white_bishop;
                    }
                    else if (game.squares[a, b].name == "bqueen")
                    {
                        game.renderer[a, b].BackgroundImage = DavidsChessGame.Properties.Resources.black_queen;
                    }
                    else if (game.squares[a, b].name == "wqueen")
                    {
                        game.renderer[a, b].BackgroundImage = DavidsChessGame.Properties.Resources.white_queen;
                    }
                    else if (game.squares[a, b].name == "bking")
                    {
                        game.renderer[a, b].BackgroundImage = DavidsChessGame.Properties.Resources.black_king;
                    }
                    else if (game.squares[a, b].name == "wking")
                    {
                        game.renderer[a, b].BackgroundImage = DavidsChessGame.Properties.Resources.white_king;
                    }
                    else
                    {
                        game.renderer[a, b].BackgroundImage = null;
                    }
                }
            }
        }

        //*****************************************************************************************
        private bool isOccupied(square sqr)
        {
            if (sqr.name != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //***********************************************************************************

        public class Game
        {
            public Color userColor = new Color();

            public string difficulty = "easy";

            public bool userTurn = true;

            public Panel[,] renderer = new Panel[8, 8];

            public Piece[] userPieces = new Piece[16];
            //  pawn  pawn   pawn     pawn     pawn    pawn    pawn  pawn
            //  rook  knight bishop (q or k) (q or k) bishop knight  rook

            public Piece[] opponentPieces = new Piece[16];
            //  rook  knight bishop (q or k) (q or k) bishop knight  rook
            //  pawn  pawn   pawn     pawn     pawn     pawn   pawn  pawn

            public square[,] squares = new square[8, 8];
            //array that describes what piece occupies each square

            public MoveBoard[,] MovBoard = new MoveBoard[8, 8];
            //board where valid moves are stored

            public List<Piece> deletedUserPieces = new List<Piece>();
            public List<Piece> deletedOppPieces = new List<Piece>();
            //lists of pieces eligible for promotion

        }

        public class board
        {
            public square[,] snapshot = new square[8, 8];
            public square[,] worker = new square[8, 8];
        }

        public struct Piece
        {
            public string name;
            public PTypes type;
            public int x;
            public int y;
            public bool alive;
            public bool unMoved;
        }

        public struct square{
            public string name;
            public PTypes type;
        }

        public struct MoveBoard
        {
            public bool moveable;
        }

        public enum PTypes
        {
            pawn, rook, knight, bishop, queen, king, none
        }
    }
}
