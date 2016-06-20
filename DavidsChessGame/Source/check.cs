/*
Code that makes sure player is not in check
  *checkMate function might need optimization
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
        private bool check(square[,] squares)
        {
            Piece king = new Piece();

            Panel[,] pan = new Panel[8, 8];
            Array.Copy(game.renderer, pan, game.renderer.Length);

            MoveBoard[,] movboard = new MoveBoard[8, 8];
            Array.Copy(game.MovBoard, movboard, game.MovBoard.Length);

            //work here
            if (userTurn)
            {
                for (int a = 0; a < 8; a++)
                {
                    for (int b = 0; b < 8; b++)
                    {
                        if (game.userColor == Color.White)
                        {
                            if (squares[a, b].name == "wking")
                            {
                                king.x = a;
                                king.y = b;
                                king.type = PTypes.king;
                                break;
                            }
                        }
                        else
                        {
                            if (squares[a, b].name == "bking")
                            {
                                king.x = a;
                                king.y = b;
                                king.type = PTypes.king;
                                break;
                            }
                        }
                    }
                }

                //is check?
                foreach (Piece pce in game.opponentPieces)
                {
                    if (pce.alive)
                    {
                        userTurn = false;
                        showValidMoves(pce, squares, ref movboard, ref pan);
                        userTurn = true;


                        if (movboard[king.x, king.y].moveable)
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                for (int a = 0; a < 8; a++)
                {
                    for (int b = 0; b < 8; b++)
                    {
                        if (game.userColor != Color.White)
                        {
                            if (squares[a, b].name == "wking")
                            {
                                king.x = a;
                                king.y = b;
                                king.type = PTypes.king;
                                break;
                            }
                        }
                        else
                        {
                            if (squares[a, b].name == "bking")
                            {
                                king.x = a;
                                king.y = b;
                                king.type = PTypes.king;
                                break;
                            }
                        }
                    }
                }

                foreach (Piece pce in game.userPieces)
                {
                    if (pce.alive)
                    {
                        userTurn = true;
                        showValidMoves(pce, squares, ref movboard, ref pan);
                        userTurn = false;

                        if (movboard[king.x, king.y].moveable)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool checkMate(square[,] squares)
        {
            //TODO
            //Fix issue where in multiplayer, pieces switch from alive to dead
            //                **Should be fixed, keep testing
            //More checkMate() optimizations might be required
            square[,] nsqrs = new square[8, 8];
            Array.Copy(squares, nsqrs, squares.Length);

            MoveBoard[,] movboard = new MoveBoard[8, 8];

            MoveBoard[,] nmovbrd = new MoveBoard[8, 8];

            Panel[,] pan = new Panel[8, 8];
            Array.Copy(game.renderer, pan, game.renderer.Length);

            Piece[] oppPieces = new Piece[16];
            Array.Copy(game.opponentPieces, oppPieces, game.opponentPieces.Length);

            //define both kings
            Piece bking = new Piece();
            Piece wking = new Piece();

            for (int a = 0; a < 8; a++)
            {
                for (int b = 0; b < 8; b++)
                {
                    if (squares[a, b].name == "bking")
                    {
                        bking.name = "bking";
                        bking.type = PTypes.king;
                        bking.x = a;
                        bking.y = b;
                    }
                    else if (squares[a, b].name == "wking")
                    {
                        wking.name = "wking";
                        wking.type = PTypes.king;
                        wking.x = a;
                        wking.y = b;
                    }
                }
            }

            bool inCheck = false;
            if (userTurn)
            {
                foreach (Piece pice in game.userPieces)
                {
                    showValidMoves(pice, nsqrs, ref movboard, ref pan);

                    Array.Copy(movboard, nmovbrd, movboard.Length);
                    Array.Copy(game.opponentPieces, oppPieces, game.opponentPieces.Length);

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

                    inCheck = false;
                    for (int a = 0; a < 8; a++)
                    {
                        for (int b = 0; b < 8; b++)
                        {
                            Array.Copy(nmovbrd, movboard, movboard.Length);
                            if (movboard[a, b].moveable)
                            {
                                move(pice, a, b, true, ref nsqrs, out inCheck);
                                Array.Copy(oppPieces, game.opponentPieces, game.opponentPieces.Length);
                                if (inCheck)
                                {
                                    inCheck = true;
                                }
                                else
                                {
                                    //reset background color
                                    for (int x = 0; x < 8; x++)
                                    {
                                        for (int y = 0; y < 8; y++)
                                        {
                                            if (x % 2 == 0)
                                            {
                                                game.renderer[x, y].BackColor = (y % 2 == 0) ? Color.White : Color.Gray;
                                            }
                                            else
                                            {
                                                game.renderer[x, y].BackColor = (y % 2 == 0) ? Color.Gray : Color.White;
                                            }
                                        }
                                    }
                                    Array.Copy(oppPieces, game.opponentPieces, game.opponentPieces.Length);
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Piece pice in game.opponentPieces)
                {
                    showValidMoves(pice, nsqrs, ref movboard, ref pan);

                    inCheck = false;
                    for (int a = 0; a < 8; a++)
                    {
                        for (int b = 0; b < 8; b++)
                        {
                            if (movboard[a, b].moveable)
                            {
                                move(bking, a, b, true, ref nsqrs, out inCheck);
                                Array.Copy(oppPieces, game.opponentPieces, game.opponentPieces.Length);
                                if (inCheck)
                                {
                                    inCheck = true;
                                }
                                else
                                {
                                    //reset background color
                                    for (int x = 0; x < 8; x++)
                                    {
                                        for (int y = 0; y < 8; y++)
                                        {
                                            if (x % 2 == 0)
                                            {
                                                game.renderer[x, y].BackColor = (y % 2 == 0) ? Color.White : Color.Gray;
                                            }
                                            else
                                            {
                                                game.renderer[x, y].BackColor = (y % 2 == 0) ? Color.Gray : Color.White;
                                            }
                                        }
                                    }
                                    Array.Copy(oppPieces, game.opponentPieces, game.opponentPieces.Length);
                                    return false;
                                }
                            }
                        }
                    }

                }
                //reset background color
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                        if (x % 2 == 0)
                        {
                            game.renderer[x, y].BackColor = (y % 2 == 0) ? Color.White : Color.Gray;
                        }
                        else
                        {
                            game.renderer[x, y].BackColor = (y % 2 == 0) ? Color.Gray : Color.White;
                        }
                    }
                }
            }

                Array.Copy(oppPieces, game.opponentPieces, game.opponentPieces.Length);
                if (inCheck)
                {
                    return true;
                }
                else
                {
                    return false;
                }

        }
    }
}
