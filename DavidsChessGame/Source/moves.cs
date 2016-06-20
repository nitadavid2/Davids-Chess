/*
Function that shows valid moves and function to move pieces
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
using System.Web;
using System.Net;

namespace DavidsChessGame
{
    public partial class Form1 : Form
    {
        private void showValidMoves(Piece pce, square[,] squares, ref MoveBoard[,] movboard, ref Panel[,] renderer)
        {
            //reset valid move board
            for (int a = 0; a < 8; a++)
            {
                for (int b = 0; b < 8; b++)
                {
                    movboard[a, b].moveable = false;
                }
            }

            int x = pce.x;
            int y = pce.y;
            string name = pce.name;
            PTypes type = pce.type;

            switch (pce.type)
            {
                case PTypes.pawn:
                    if (y > 0 && y < 7)
                    {
                        //check if 12 o'clock occupied
                        if (userTurn)
                        {
                            if (!isOccupied(squares[x, y - 1]))
                            {
                                renderer[x, y - 1].BackColor = Color.Blue;
                                movboard[x, y - 1].moveable = true;

                                if (pce.unMoved)
                                {
                                    if (!isOccupied(squares[x, y - 2]))
                                    {
                                        renderer[x, y - 2].BackColor = Color.Blue;
                                        movboard[x, y - 2].moveable = true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (!isOccupied(squares[x, y + 1]))
                            {
                                renderer[x, y + 1].BackColor = Color.Blue;
                                movboard[x, y + 1].moveable = true;

                                if (pce.unMoved)
                                {
                                    if (!isOccupied(squares[x, y + 2]))
                                    {
                                        renderer[x, y + 2].BackColor = Color.Blue;
                                        movboard[x, y + 2].moveable = true;
                                    }
                                }
                            }
                        }

                        if (x > 0)
                        {
                            //check if 11 o'clock is attackable
                            if (userTurn)
                            {
                                if (isOccupied(squares[x - 1, y - 1]))
                                {
                                    for (int i = 0; i < 16; i++)
                                    {
                                        if (game.opponentPieces[i].name == squares[x - 1, y - 1].name)
                                        {
                                            renderer[x - 1, y - 1].BackColor = Color.Red;
                                            movboard[x - 1, y - 1].moveable = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (isOccupied(squares[x - 1, y + 1]))
                                {
                                    for (int i = 0; i < 16; i++)
                                    {
                                        if (game.userPieces[i].name == squares[x - 1, y + 1].name)
                                        {
                                            renderer[x - 1, y + 1].BackColor = Color.Red;
                                            movboard[x - 1, y + 1].moveable = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (x < 7)
                        {
                            //check if 1 o'clock is attackable
                            if (userTurn)
                            {
                                if (isOccupied(squares[x + 1, y - 1]))
                                {
                                    for (int i = 0; i < 16; i++)
                                    {
                                        if (game.opponentPieces[i].name == squares[x + 1, y - 1].name)
                                        {
                                            renderer[x + 1, y - 1].BackColor = Color.Red;
                                            movboard[x + 1, y - 1].moveable = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (isOccupied(squares[x + 1, y + 1]))
                                {
                                    for (int i = 0; i < 16; i++)
                                    {
                                        if (game.userPieces[i].name == squares[x + 1, y + 1].name)
                                        {
                                            renderer[x + 1, y + 1].BackColor = Color.Red;
                                            movboard[x + 1, y + 1].moveable = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                    }
                    break;

                case PTypes.rook:
                    bool test = true;

                    //12 o'clock
                    if (y > 0)
                    {
                        for (int a = y - 1; a > -1; a -= 1)
                        {
                            if (isOccupied(squares[x, a]))
                            {
                                //occupied, check if friendly
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[x, a].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[x, a].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                //not friendly, mark red.
                                if (test)
                                {
                                    renderer[x, a].BackColor = Color.Red;
                                    movboard[x, a].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[x, a].BackColor = Color.Blue;
                                movboard[x, a].moveable = true;
                            }
                        }
                    }

                    test = true;
                    //3 o'clock
                    if (x < 7)
                    {
                        for (int a = x + 1; a < 8; a++)
                        {
                            if (isOccupied(squares[a, y]))
                            {
                                //occupied, check if friendly
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[a, y].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[a, y].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                //not friendly, mark red.
                                if (test)
                                {
                                    renderer[a, y].BackColor = Color.Red;
                                    movboard[a, y].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[a, y].BackColor = Color.Blue;
                                movboard[a, y].moveable = true;
                            }
                        }
                    }

                    test = true;
                    //6 o'clock
                    if (y < 7)
                    {
                        for (int a = y + 1; a < 8; a++)
                        {
                            if (isOccupied(squares[x, a]))
                            {
                                //occupied, check if friendly
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[x, a].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[x, a].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                //not friendly, mark red.
                                if (test)
                                {
                                    renderer[x, a].BackColor = Color.Red;
                                    movboard[x, a].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[x, a].BackColor = Color.Blue;
                                movboard[x, a].moveable = true;
                            }
                        }
                    }

                    test = true;
                    //9 o'clock
                    if (x > 0)
                    {
                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            if (isOccupied(squares[a, y]))
                            {
                                //occupied, check if friendly
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[a, y].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[a, y].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                //not friendly, mark red.
                                if (test)
                                {
                                    renderer[a, y].BackColor = Color.Red;
                                    movboard[a, y].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[a, y].BackColor = Color.Blue;
                                movboard[a, y].moveable = true;
                            }
                        }
                    }

                    break;
                case PTypes.knight:
                    // up 1, left 2
                    if (x > 1 && y > 0)
                    {
                        test = true;
                        if (isOccupied(squares[x - 2, y - 1]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x - 2, y - 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x - 2, y - 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x - 2, y - 1].BackColor = Color.Red;
                                movboard[x - 2, y - 1].moveable = true;
                            }

                        }
                        else
                        {
                            //not occupied
                            renderer[x - 2, y - 1].BackColor = Color.Blue;
                            movboard[x - 2, y - 1].moveable = true;
                        }
                    }

                    // up 2, left 1
                    if (x > 0 && y > 1)
                    {
                        test = true;
                        if (isOccupied(squares[x - 1, y - 2]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x - 1, y - 2].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x - 1, y - 2].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x - 1, y - 2].BackColor = Color.Red;
                                movboard[x - 1, y - 2].moveable = true;
                            }

                        }
                        else
                        {
                            //not occupied
                            renderer[x - 1, y - 2].BackColor = Color.Blue;
                            movboard[x - 1, y - 2].moveable = true;
                        }
                    }

                    // up 2, right 1
                    if (x < 7 && y > 1)
                    {
                        test = true;
                        if (isOccupied(squares[x + 1, y - 2]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x + 1, y - 2].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x + 1, y - 2].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x + 1, y - 2].BackColor = Color.Red;
                                movboard[x + 1, y - 2].moveable = true;
                            }

                        }
                        else
                        {
                            //not occupied
                            renderer[x + 1, y - 2].BackColor = Color.Blue;
                            movboard[x + 1, y - 2].moveable = true;
                        }
                    }

                    // up 1, right 2
                    if (x < 6 && y > 0)
                    {
                        test = true;
                        if (isOccupied(squares[x + 2, y - 1]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x + 2, y - 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x + 2, y - 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x + 2, y - 1].BackColor = Color.Red;
                                movboard[x + 2, y - 1].moveable = true;
                            }

                        }
                        else
                        {
                            //not occupied
                            renderer[x + 2, y - 1].BackColor = Color.Blue;
                            movboard[x + 2, y - 1].moveable = true;
                        }
                    }

                    // down 1, left 2
                    if (x > 1 && y < 7)
                    {
                        test = true;
                        if (isOccupied(squares[x - 2, y + 1]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x - 2, y + 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x - 2, y + 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x - 2, y + 1].BackColor = Color.Red;
                                movboard[x - 2, y + 1].moveable = true;
                            }

                        }
                        else
                        {
                            //not occupied
                            renderer[x - 2, y + 1].BackColor = Color.Blue;
                            movboard[x - 2, y + 1].moveable = true;
                        }
                    }

                    // down 2, left 1
                    if (x > 0 && y < 6)
                    {
                        test = true;
                        if (isOccupied(squares[x - 1, y + 2]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x - 1, y + 2].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x - 1, y + 2].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x - 1, y + 2].BackColor = Color.Red;
                                movboard[x - 1, y + 2].moveable = true;
                            }

                        }
                        else
                        {
                            //not occupied
                            renderer[x - 1, y + 2].BackColor = Color.Blue;
                            movboard[x - 1, y + 2].moveable = true;
                        }
                    }

                    // down 2, right 1
                    if (x < 7 && y < 6)
                    {
                        test = true;
                        if (isOccupied(squares[x + 1, y + 2]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x + 1, y + 2].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x + 1, y + 2].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x + 1, y + 2].BackColor = Color.Red;
                                movboard[x + 1, y + 2].moveable = true;
                            }


                        }
                        else
                        {
                            //not occupied
                            renderer[x + 1, y + 2].BackColor = Color.Blue;
                            movboard[x + 1, y + 2].moveable = true;
                        }
                    }

                    // down 1, right 2
                    if (x < 6 && y < 7)
                    {
                        test = true;
                        if (isOccupied(squares[x + 2, y + 1]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x + 2, y + 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x + 2, y + 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x + 2, y + 1].BackColor = Color.Red;
                                movboard[x + 2, y + 1].moveable = true;
                            }

                        }
                        else
                        {
                            //not occupied
                            renderer[x + 2, y + 1].BackColor = Color.Blue;
                            movboard[x + 2, y + 1].moveable = true;
                        }
                    }

                    break;
                case PTypes.bishop:

                    // 1:30 o'clock
                    int b = y;
                    test = true;
                    for (int a = x + 1; a < 8; a++)
                    {
                        b -= 1;
                        if (b > -1)
                        {
                            if (isOccupied(squares[a, b]))
                            {
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                if (test)
                                {
                                    renderer[a, b].BackColor = Color.Red;
                                    movboard[a, b].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[a, b].BackColor = Color.Blue;
                                movboard[a, b].moveable = true;
                            }
                        }
                    }

                    // 4:30 o'clock
                    b = y;
                    test = true;
                    for (int a = x + 1; a < 8; a++)
                    {
                        b++;
                        if (b < 8)
                        {
                            if (isOccupied(squares[a, b]))
                            {
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                if (test)
                                {
                                    renderer[a, b].BackColor = Color.Red;
                                    movboard[a, b].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[a, b].BackColor = Color.Blue;
                                movboard[a, b].moveable = true;
                            }
                        }
                    }

                    // 7:30 o'clock
                    b = y;
                    test = true;
                    for (int a = x - 1; a > -1; a -= 1)
                    {
                        b++;
                        if (b < 8)
                        {
                            if (isOccupied(squares[a, b]))
                            {
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                if (test)
                                {
                                    renderer[a, b].BackColor = Color.Red;
                                    movboard[a, b].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[a, b].BackColor = Color.Blue;
                                movboard[a, b].moveable = true;
                            }
                        }
                    }

                    // 11:30 o'clock
                    b = y;
                    test = true;
                    for (int a = x - 1; a > -1; a -= 1)
                    {
                        b -= 1;
                        if (b > -1)
                        {
                            if (isOccupied(squares[a, b]))
                            {
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                if (test)
                                {
                                    renderer[a, b].BackColor = Color.Red;
                                    movboard[a, b].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[a, b].BackColor = Color.Blue;
                                movboard[a, b].moveable = true;
                            }
                        }
                    }

                    break;
                case PTypes.queen:
                    test = true;

                    //12 o'clock
                    if (y > 0)
                    {
                        for (int a = y - 1; a > -1; a -= 1)
                        {
                            if (isOccupied(squares[x, a]))
                            {
                                //occupied, check if friendly
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[x, a].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[x, a].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                //not friendly, mark red.
                                if (test)
                                {
                                    renderer[x, a].BackColor = Color.Red;
                                    movboard[x, a].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[x, a].BackColor = Color.Blue;
                                movboard[x, a].moveable = true;
                            }
                        }
                    }

                    test = true;
                    //3 o'clock
                    if (x < 7)
                    {
                        for (int a = x + 1; a < 8; a++)
                        {
                            if (isOccupied(squares[a, y]))
                            {
                                //occupied, check if friendly
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[a, y].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[a, y].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                //not friendly, mark red.
                                if (test)
                                {
                                    renderer[a, y].BackColor = Color.Red;
                                    movboard[a, y].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[a, y].BackColor = Color.Blue;
                                movboard[a, y].moveable = true;
                            }
                        }
                    }

                    test = true;
                    //6 o'clock
                    if (y < 7)
                    {
                        for (int a = y + 1; a < 8; a++)
                        {
                            if (isOccupied(squares[x, a]))
                            {
                                //occupied, check if friendly
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[x, a].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[x, a].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                //not friendly, mark red.
                                if (test)
                                {
                                    renderer[x, a].BackColor = Color.Red;
                                    movboard[x, a].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[x, a].BackColor = Color.Blue;
                                movboard[x, a].moveable = true;
                            }
                        }
                    }

                    test = true;
                    //9 o'clock
                    if (x > 0)
                    {
                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            if (isOccupied(squares[a, y]))
                            {
                                //occupied, check if friendly
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[a, y].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[a, y].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                //not friendly, mark red.
                                if (test)
                                {
                                    renderer[a, y].BackColor = Color.Red;
                                    movboard[a, y].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[a, y].BackColor = Color.Blue;
                                movboard[a, y].moveable = true;
                            }
                        }
                    }

                    // 1:30 o'clock
                    b = y;
                    test = true;
                    for (int a = x + 1; a < 8; a++)
                    {
                        b -= 1;
                        if (b > -1)
                        {
                            if (isOccupied(squares[a, b]))
                            {
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                if (test)
                                {
                                    renderer[a, b].BackColor = Color.Red;
                                    movboard[a, b].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[a, b].BackColor = Color.Blue;
                                movboard[a, b].moveable = true;
                            }
                        }
                    }

                    // 4:30 o'clock
                    b = y;
                    test = true;
                    for (int a = x + 1; a < 8; a++)
                    {
                        b++;
                        if (b < 8)
                        {
                            if (isOccupied(squares[a, b]))
                            {
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                if (test)
                                {
                                    renderer[a, b].BackColor = Color.Red;
                                    movboard[a, b].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[a, b].BackColor = Color.Blue;
                                movboard[a, b].moveable = true;
                            }
                        }
                    }

                    // 7:30 o'clock
                    b = y;
                    test = true;
                    for (int a = x - 1; a > -1; a -= 1)
                    {
                        b++;
                        if (b < 8)
                        {
                            if (isOccupied(squares[a, b]))
                            {
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                if (test)
                                {
                                    renderer[a, b].BackColor = Color.Red;
                                    movboard[a, b].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[a, b].BackColor = Color.Blue;
                                movboard[a, b].moveable = true;
                            }
                        }
                    }

                    // 11:30 o'clock
                    b = y;
                    test = true;
                    for (int a = x - 1; a > -1; a -= 1)
                    {
                        b -= 1;
                        if (b > -1)
                        {
                            if (isOccupied(squares[a, b]))
                            {
                                for (int i = 0; i < 16; i++)
                                {
                                    if (userTurn)
                                    {
                                        if (game.userPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (game.opponentPieces[i].name == squares[a, b].name)
                                        {
                                            test = false;
                                            break;
                                        }
                                    }
                                }

                                if (test)
                                {
                                    renderer[a, b].BackColor = Color.Red;
                                    movboard[a, b].moveable = true;
                                    break;
                                }
                                else
                                {
                                    break;
                                }

                            }
                            else
                            {
                                //not occupied
                                renderer[a, b].BackColor = Color.Blue;
                                movboard[a, b].moveable = true;
                            }
                        }
                    }

                    break;
                case PTypes.king:
                    //12 o'clock
                    test = true;
                    if (y > 0)
                    {
                        if (isOccupied(squares[x, y - 1]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x, y - 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x, y - 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x, y - 1].BackColor = Color.Red;
                                movboard[x, y - 1].moveable = true;
                            }
                        }
                        else
                        {
                            //not occupied
                            renderer[x, y - 1].BackColor = Color.Blue;
                            movboard[x, y - 1].moveable = true;
                        }
                    }

                    //2:30 o'clock
                    test = true;
                    if (x < 7 && y > 0)
                    {
                        if (isOccupied(squares[x + 1, y - 1]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x + 1, y - 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x + 1, y - 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x + 1, y - 1].BackColor = Color.Red;
                                movboard[x + 1, y - 1].moveable = true;
                            }
                        }
                        else
                        {
                            //not occupied
                            renderer[x + 1, y - 1].BackColor = Color.Blue;
                            movboard[x + 1, y - 1].moveable = true;
                        }
                    }

                    //3 o'clock
                    test = true;
                    if (x < 7)
                    {
                        if (isOccupied(squares[x + 1, y]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x + 1, y].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x + 1, y].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x + 1, y].BackColor = Color.Red;
                                movboard[x + 1, y].moveable = true;
                            }
                        }
                        else
                        {
                            //not occupied
                            renderer[x + 1, y].BackColor = Color.Blue;
                            movboard[x + 1, y].moveable = true;
                        }
                    }

                    //4:30 o'clock
                    test = true;
                    if (x < 7 && y < 7)
                    {
                        if (isOccupied(squares[x + 1, y + 1]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x + 1, y + 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x + 1, y + 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x + 1, y + 1].BackColor = Color.Red;
                                movboard[x + 1, y + 1].moveable = true;
                            }
                        }
                        else
                        {
                            //not occupied
                            renderer[x + 1, y + 1].BackColor = Color.Blue;
                            movboard[x + 1, y + 1].moveable = true;
                        }
                    }

                    //6 o'clock
                    test = true;
                    if (y < 7)
                    {
                        if (isOccupied(squares[x, y + 1]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x, y + 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x, y + 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x, y + 1].BackColor = Color.Red;
                                movboard[x, y + 1].moveable = true;
                            }
                        }
                        else
                        {
                            //not occupied
                            renderer[x, y + 1].BackColor = Color.Blue;
                            movboard[x, y + 1].moveable = true;
                        }
                    }

                    //7:30 o'clock
                    test = true;
                    if (x > 0 && y < 7)
                    {
                        if (isOccupied(squares[x - 1, y + 1]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x - 1, y + 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x - 1, y + 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x - 1, y + 1].BackColor = Color.Red;
                                movboard[x - 1, y + 1].moveable = true;
                            }
                        }
                        else
                        {
                            //not occupied
                            renderer[x - 1, y + 1].BackColor = Color.Blue;
                            movboard[x - 1, y + 1].moveable = true;
                        }
                    }

                    //9 o'clock
                    test = true;
                    if (x > 0)
                    {
                        if (isOccupied(squares[x - 1, y]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x - 1, y].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x - 1, y].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x - 1, y].BackColor = Color.Red;
                                movboard[x - 1, y].moveable = true;
                            }
                        }
                        else
                        {
                            //not occupied
                            renderer[x - 1, y].BackColor = Color.Blue;
                            movboard[x - 1, y].moveable = true;
                        }
                    }

                    //11:30 o'clock
                    test = true;
                    if (x > 0 && y > 0)
                    {
                        if (isOccupied(squares[x - 1, y - 1]))
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                if (userTurn)
                                {
                                    if (game.userPieces[i].name == squares[x - 1, y - 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (game.opponentPieces[i].name == squares[x - 1, y - 1].name)
                                    {
                                        test = false;
                                        break;
                                    }
                                }
                            }

                            if (test)
                            {
                                renderer[x - 1, y - 1].BackColor = Color.Red;
                                movboard[x - 1, y - 1].moveable = true;
                            }
                        }
                        else
                        {
                            //not occupied
                            renderer[x - 1, y - 1].BackColor = Color.Blue;
                            movboard[x - 1, y - 1].moveable = true;
                        }
                    }

                    break;
            }
        }

        //***************************************************************************************
        //--------------------------------------------------------------------------------------
        //**************************************************************************************

        private Piece move(Piece pce, int x, int y, bool deletePieces, ref square[,] squares, out bool isCheck)
        {
            WebClient wc = new WebClient();

            //save state
            square[,] nsquare = new square[8, 8];
            Array.Copy(squares, nsquare, nsquare.Length);

            Piece tempPce = pce;

            pce.unMoved = false;

            int oldx = pce.x;
            int oldy = pce.y;

            squares[oldx, oldy].name = null;
            squares[oldx, oldy].type = PTypes.none;

            pce.x = x;
            pce.y = y;

            if (!isOccupied(squares[x, y]))
            {
                //not occupied by opponent
                squares[x, y].name = pce.name;
                squares[x, y].type = pce.type;

                if (check(squares))
                {
                    //if in check, restore state.
                    Array.Copy(nsquare, squares, nsquare.Length);
                    isCheck = true;

                    return tempPce;
                }
                else
                {
                    //otherwise return new state
                    isCheck = false;

                    //send message to server and opponent of multiplayer
                    if (multiplayerMove)
                    {
                        if (multiplayer)
                        {
                            //here
                            String movString = pce.name + "[[]]" + x.ToString() + "[[]]" + y.ToString() + "[[]]" + deletePieces.ToString() + "[[]]";
                            Properties.Settings.Default.movenum = Convert.ToInt32(wc.DownloadString("http://www.davenwarrior.com/cgi-bin/chessgame.cgi?action=move&user=" + Properties.Settings.Default.nick + "&move=" + movString));
                            Properties.Settings.Default.Save();
                        }

                        if (userTurn)
                        {
                            userTurn = false;
                        }
                        else
                        {
                            userTurn = true;
                        }
                    }


                    return pce;
                }
            }
            else
            {
                //occupied by opponent
                if (userTurn)
                {
                    for (int a = 0; a < 16; a++)
                    {
                        if (game.opponentPieces[a].name == squares[x, y].name && deletePieces)
                        {
                            game.deletedOppPieces.Add(game.opponentPieces[a]);
                            game.opponentPieces[a].alive = false;
                        }
                    }
                }
                else
                {
                    //occupied by player
                    for (int a = 0; a < 16; a++)
                    {
                        if (game.userPieces[a].name == squares[x, y].name && deletePieces)
                        {
                            game.deletedUserPieces.Add(game.userPieces[a]);
                            game.userPieces[a].alive = false;
                        }
                    }
                }

                squares[x, y].name = pce.name;
                squares[x, y].type = pce.type;

                if (check(squares))
                {
                    //if in check, restore state
                    Array.Copy(nsquare, squares, nsquare.Length);
                    isCheck = true;

                    return tempPce;
                }
                else
                {
                    //otherwise return new state
                    Array.Copy(squares, squares, nsquare.Length);
                    isCheck = false;

                    //send message to server and opponent of multiplayer
                    if (multiplayerMove)
                    {
                        if (multiplayer)
                        {
                            //here
                            String movString = pce.name + "[[]]" + x.ToString() + "[[]]" + y.ToString() + "[[]]" + deletePieces.ToString();
                            Properties.Settings.Default.movenum = Convert.ToInt32(wc.DownloadString("http://www.davenwarrior.com/cgi-bin/chessgame.cgi?action=move&user=" + Properties.Settings.Default.nick + "&move=" + movString));
                            Properties.Settings.Default.Save();
                        }

                        if (userTurn)
                        {
                            userTurn = false;
                        }
                        else
                        {
                            userTurn = true;
                        }
                    }

                    return pce;
                }
            }

        }

    }
}
