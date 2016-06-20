/*
Shows valid moves and contains logic to move pieces around the board
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

        private void showValidMoves(int x, int y, bool allowEnemy = false)
        {
            if (sqs[x, y].occupied.pCol != color)
            {
                if (!allowEnemy)
                {
                    return;
                }
            }

            if (!userMove)
            {
                color_panel();
                piece piese = sqs[x, y].occupied;
                switch (piese.pType)
                {
                    case pieceTypes.pawn:
                        if (y == 7)
                        {
                            promote(piese);
                            return;
                        }


                        if (x > 0 && x < 7 && y < 7)
                        {
                            piece fPiece = sqs[x, y + 1].occupied;
                            piece ALPiece = sqs[x - 1, y + 1].occupied;
                            piece ARPiece = sqs[x + 1, y + 1].occupied;

                            if (ALPiece.pType != pieceTypes.none && ALPiece.pCol != piese.pCol)
                            {
                                CPanels[x - 1, y + 1].BackColor = Color.DeepPink;
                            }
                            else if (ARPiece.pType != pieceTypes.none && ARPiece.pCol != piese.pCol)
                            {
                                CPanels[x + 1, y + 1].BackColor = Color.DeepPink;
                            }

                            if (fPiece.pType == pieceTypes.none)
                            {
                                CPanels[x, y + 1].BackColor = Color.LightBlue;
                            }
                        }
                        else if (x > 0 && y < 7)
                        {
                            piece fPiece = sqs[x, y + 1].occupied;
                            piece ALPiece = sqs[x - 1, y + 1].occupied;

                            if (ALPiece.pType != pieceTypes.none && ALPiece.pCol != piese.pCol)
                            {
                                CPanels[x - 1, y + 1].BackColor = Color.DeepPink;
                            }

                            if (fPiece.pType == pieceTypes.none)
                            {
                                CPanels[x, y + 1].BackColor = Color.LightBlue;
                            }
                        }
                        else if (x < 7 && y < 7)
                        {
                            piece fPiece = sqs[x, y + 1].occupied;
                            piece ARPiece = sqs[x + 1, y + 1].occupied;

                            if (ARPiece.pType != pieceTypes.none && ARPiece.pCol != piese.pCol)
                            {
                                CPanels[x + 1, y + 1].BackColor = Color.DeepPink;
                            }

                            if (fPiece.pType == pieceTypes.none)
                            {
                                CPanels[x, y + 1].BackColor = Color.LightBlue;
                            }
                        }
                        break;

                    case pieceTypes.rook:
                        //eval to left
                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            piece pce = sqs[a, y].occupied;
                            if (checkForNone(pce))
                            {
                                CPanels[a, y].BackColor = Color.LightBlue;
                            }
                            else
                            {
                                if (pce.pCol != piese.pCol)
                                {
                                    CPanels[a, y].BackColor = Color.DeepPink;
                                }
                                break;
                            }
                        }
                        //eval to right
                        for (int a = x + 1; a < 8; a++)
                        {
                            piece pce = sqs[a, y].occupied;
                            if (checkForNone(pce))
                            {
                                CPanels[a, y].BackColor = Color.LightBlue;
                            }
                            else
                            {
                                if (pce.pCol != piese.pCol)
                                {
                                    CPanels[a, y].BackColor = Color.DeepPink;
                                }
                                break;
                            }
                        }
                        //eval down
                        for (int a = y + 1; a < 8; a++)
                        {
                            piece pce = sqs[x, a].occupied;
                            if (checkForNone(pce))
                            {
                                CPanels[x, a].BackColor = Color.LightBlue;
                            }
                            else
                            {
                                if (pce.pCol != piese.pCol)
                                {
                                    CPanels[x, a].BackColor = Color.DeepPink;
                                }
                                break;
                            }
                        }
                        //eval up
                        for (int a = y - 1; a > -1; a -= 1)
                        {
                            piece pce = sqs[x, a].occupied;
                            if (checkForNone(pce))
                            {
                                CPanels[x, a].BackColor = Color.LightBlue;
                            }
                            else
                            {
                                if (pce.pCol != piese.pCol)
                                {
                                    CPanels[x, a].BackColor = Color.DeepPink;
                                }
                                break;
                            }
                        }

                        break;

                    case pieceTypes.knight:
                        if (x + 1 < 8 && y - 2 > -1)
                        {
                            //eval up 2 right 1
                            if (checkForNone(sqs[x + 1, y - 2].occupied))
                            {
                                CPanels[x + 1, y - 2].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x + 1, y - 2].occupied.pCol != color)
                            {
                                CPanels[x + 1, y - 2].BackColor = Color.DeepPink;
                            }
                        }

                        if (x - 1 > -1 && y - 2 > -1)
                        {
                            //eval up 2 left 1
                            if (checkForNone(sqs[x - 1, y - 2].occupied))
                            {
                                CPanels[x - 1, y - 2].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x - 1, y - 2].occupied.pCol != color)
                            {
                                CPanels[x - 1, y - 2].BackColor = Color.DeepPink;
                            }
                        }

                        if (x + 1 < 8 && y + 2 < 8)
                        {
                            //eval down 2 right 1
                            if (checkForNone(sqs[x + 1, y + 2].occupied))
                            {
                                CPanels[x + 1, y + 2].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x + 1, y + 2].occupied.pCol != color)
                            {
                                CPanels[x + 1, y + 2].BackColor = Color.DeepPink;
                            }
                        }

                        if (x - 1 > -1 && y + 2 < 8)
                        {
                            //eval down 2 left 1
                            if (checkForNone(sqs[x - 1, y + 2].occupied))
                            {
                                CPanels[x - 1, y + 2].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x - 1, y + 2].occupied.pCol != color)
                            {
                                CPanels[x - 1, y + 2].BackColor = Color.DeepPink;
                            }
                        }

                        if (x + 2 < 8 && y - 1 > -1)
                        {
                            //eval right 2 up 1
                            if (checkForNone(sqs[x + 2, y - 1].occupied))
                            {
                                CPanels[x + 2, y - 1].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x + 2, y - 1].occupied.pCol != color)
                            {
                                CPanels[x + 2, y - 1].BackColor = Color.DeepPink;
                            }
                        }

                        if (x + 2 < 8 && y + 1 < 8)
                        {
                            //eval right 2 down 1
                            if (checkForNone(sqs[x + 2, y + 1].occupied))
                            {
                                CPanels[x + 2, y + 1].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x + 2, y + 1].occupied.pCol != color)
                            {
                                CPanels[x + 2, y + 1].BackColor = Color.DeepPink;
                            }
                        }

                        if (x - 2 > -1 && y - 1 > -1)
                        {
                            //eval left 2 up 1
                            if (checkForNone(sqs[x - 2, y - 1].occupied))
                            {
                                CPanels[x - 2, y - 1].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x - 2, y - 1].occupied.pCol != color)
                            {
                                CPanels[x - 2, y - 1].BackColor = Color.DeepPink;
                            }
                        }

                        if (x - 2 > -1 && y + 1 < 8)
                        {
                            //eval left 2 down 1
                            if (checkForNone(sqs[x - 2, y + 1].occupied))
                            {
                                CPanels[x - 2, y + 1].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x - 2, y + 1].occupied.pCol != color)
                            {
                                CPanels[x - 2, y + 1].BackColor = Color.DeepPink;
                            }
                        }

                        break;

                    case pieceTypes.bishop:
                        //move towards 2 o'clock
                        int b = y;
                        for (int a = x + 1; a < 8; a++)
                        {
                            if (b > 0)
                            {
                                b -= 1;
                                if (!checkForNone(sqs[a, b].occupied))
                                {
                                    if (sqs[a, b].occupied.pCol != color)
                                    {
                                        CPanels[a, b].BackColor = Color.DeepPink;
                                    }
                                    b = -10;
                                    break;
                                }
                                CPanels[a, b].BackColor = Color.LightBlue;
                            }
                        }

                        //towards 5 o'clock
                        b = y;
                        for (int a = x + 1; a < 8; a++)
                        {
                            if (b < 7)
                            {
                                b++;
                                if (!checkForNone(sqs[a, b].occupied))
                                {
                                    if (sqs[a, b].occupied.pCol != color)
                                    {
                                        CPanels[a, b].BackColor = Color.DeepPink;
                                    }
                                    b = 10;
                                    break;
                                }
                                CPanels[a, b].BackColor = Color.LightBlue;
                            }
                        }

                        //towards 7 o'clock
                        b = y;
                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            if (b < 7)
                            {
                                b++;
                                if (!checkForNone(sqs[a, b].occupied))
                                {
                                    if (sqs[a, b].occupied.pCol != color)
                                    {
                                        CPanels[a, b].BackColor = Color.DeepPink;
                                    }
                                    b = 10;
                                    break;
                                }
                                CPanels[a, b].BackColor = Color.LightBlue;
                            }
                        }

                        //towards 11 o'clock
                        b = y;
                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            if (b > 0)
                            {
                                b -= 1;
                                if (!checkForNone(sqs[a, b].occupied))
                                {
                                    if (sqs[a, b].occupied.pCol != color)
                                    {
                                        CPanels[a, b].BackColor = Color.DeepPink;
                                    }
                                    b = -10;
                                    break;
                                }
                                CPanels[a, b].BackColor = Color.LightBlue;
                            }
                        }

                        break;

                    case pieceTypes.queen:
                        b = y;
                        int c = y;
                        for (int a = x + 1; a < 8; a++)
                        {
                            if (b > 0 && y > 0)
                            {
                                b -= 1;
                                //2 o'clock
                                if (!checkForNone(sqs[a, b].occupied))
                                {
                                    if (sqs[a, b].occupied.pCol != color)
                                    {
                                        CPanels[a, b].BackColor = Color.DeepPink;
                                    }
                                    b = -10;
                                    break;
                                }
                                CPanels[a, b].BackColor = Color.LightBlue;
                            }
                        }

                        for (int a = x + 1; a < 8; a++)
                        {
                            if (c < 7 && y < 7)
                            {
                                //5 o'clock
                                c++;
                                if (!checkForNone(sqs[a, c].occupied))
                                {
                                    if (sqs[a, c].occupied.pCol != color)
                                    {
                                        CPanels[a, c].BackColor = Color.DeepPink;
                                    }
                                    c = 10;
                                    break;
                                }
                                CPanels[a, c].BackColor = Color.LightBlue;
                            }
                        }

                        int e = x;
                        int f = y;
                        int g = x;
                        int d = y;
                        for (int a = y - 1; a > -1; a -= 1)
                        {
                            //up 
                            if (d > -1)
                            {
                                d -= 1;
                                if (!checkForNone(sqs[x, d].occupied))
                                {
                                    if (sqs[x, d].occupied.pCol != color)
                                    {
                                        CPanels[x, d].BackColor = Color.DeepPink;
                                    }
                                    d = -10;
                                    break;
                                }
                                CPanels[x, d].BackColor = Color.LightBlue;
                            }
                        }

                        for (int a = x + 1; a < 8; a++)
                        {
                            //right
                            if (e < 8)
                            {
                                e++;
                                if (!checkForNone(sqs[e, y].occupied))
                                {
                                    if (sqs[e, y].occupied.pCol != color)
                                    {
                                        CPanels[e, y].BackColor = Color.DeepPink;
                                    }
                                    e = 10;
                                    break;
                                }
                                CPanels[e, y].BackColor = Color.LightBlue;
                            }
                        }

                        for (int a = y + 1; a < 8; a++)
                        {
                            //down
                            if (f < 8)
                            {
                                f++;
                                if (!checkForNone(sqs[x, f].occupied))
                                {
                                    if (sqs[x, f].occupied.pCol != color)
                                    {
                                        CPanels[x, f].BackColor = Color.DeepPink;
                                    }
                                    f = 10;
                                    break;
                                }
                                CPanels[x, f].BackColor = Color.LightBlue;
                            }
                        }

                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            //left
                            if (g > -1)
                            {
                                g -= 1;
                                if (!checkForNone(sqs[g, y].occupied))
                                {
                                    if (sqs[g, y].occupied.pCol != color)
                                    {
                                        CPanels[g, y].BackColor = Color.DeepPink;
                                    }
                                    g = -10;
                                    break;
                                }
                                CPanels[g, y].BackColor = Color.LightBlue;
                            }
                        }

                        int i = y;
                        int h = y;
                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            //7 o'clock
                            if (h < 7 && y < 7)
                            {
                                h++;
                                if (!checkForNone(sqs[a, h].occupied))
                                {
                                    if (sqs[a, h].occupied.pCol != color)
                                    {
                                        CPanels[a, h].BackColor = Color.DeepPink;
                                    }
                                    h = 10;
                                    break;
                                }
                                CPanels[a, h].BackColor = Color.LightBlue;
                            }
                        }

                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            //11 o'clock
                            if (i > 0 && y > 0)
                            {
                                i -= 1;
                                if (!checkForNone(sqs[a, i].occupied))
                                {
                                    if (sqs[a, i].occupied.pCol != color)
                                    {
                                        CPanels[a, i].BackColor = Color.DeepPink;
                                    }
                                    i = -10;
                                    break;
                                }
                                CPanels[a, i].BackColor = Color.LightBlue;
                            }
                        }

                        break;

                    case pieceTypes.king:
                        //up
                        if (y > 0)
                        {
                            if (!checkForNone(sqs[x, y - 1].occupied))
                            {
                                if (sqs[x, y - 1].occupied.pCol != color)
                                {
                                    CPanels[x, y - 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x, y - 1].BackColor = Color.LightBlue;
                            }
                        }
                        //2 o'clock
                        if (y > 0 && x < 7)
                        {
                            if (!checkForNone(sqs[x + 1, y - 1].occupied))
                            {
                                if (sqs[x + 1, y - 1].occupied.pCol != color)
                                {
                                    CPanels[x + 1, y - 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x + 1, y - 1].BackColor = Color.LightBlue;
                            }
                        }
                        //right
                        if (x < 7)
                        {
                            if (!checkForNone(sqs[x + 1, y].occupied))
                            {
                                if (sqs[x + 1, y].occupied.pCol != color)
                                {
                                    CPanels[x + 1, y].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x + 1, y].BackColor = Color.LightBlue;
                            }
                        }
                        //5 o'clock
                        if (y < 7 && x < 7)
                        {
                            if (!checkForNone(sqs[x + 1, y + 1].occupied))
                            {
                                if (sqs[x + 1, y + 1].occupied.pCol != color)
                                {
                                    CPanels[x + 1, y + 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x + 1, y + 1].BackColor = Color.LightBlue;
                            }
                        }
                        //down
                        if (y < 7)
                        {
                            if (!checkForNone(sqs[x, y + 1].occupied))
                            {
                                if (sqs[x, y + 1].occupied.pCol != color)
                                {
                                    CPanels[x, y + 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x, y + 1].BackColor = Color.LightBlue;
                            }
                        }
                        //7 o'clock
                        if (x > 0 && y < 7)
                        {
                            if (!checkForNone(sqs[x - 1, y + 1].occupied))
                            {
                                if (sqs[x - 1, y + 1].occupied.pCol != color)
                                {
                                    CPanels[x - 1, y + 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x - 1, y + 1].BackColor = Color.LightBlue;
                            }
                        }
                        //left
                        if (x > 0)
                        {
                            if (!checkForNone(sqs[x - 1, y].occupied))
                            {
                                if (sqs[x - 1, y].occupied.pCol != color)
                                {
                                    CPanels[x - 1, y].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x - 1, y].BackColor = Color.LightBlue;
                            }
                        }
                        //11 o'clock
                        if (x > 0 && y > 0)
                        {
                            if (!checkForNone(sqs[x - 1, y - 1].occupied))
                            {
                                if (sqs[x - 1, y - 1].occupied.pCol != color)
                                {
                                    CPanels[x - 1, y - 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x - 1, y - 1].BackColor = Color.LightBlue;
                            }
                        }
                        break;

                    default:
                        return;
                }
                return;
            }

            if (allowEnemy)
            {
                color_panel();
                piece piese = sqs[x, y].occupied;
                switch (piese.pType)
                {
                    case pieceTypes.pawn:
                        if (y == 0)
                        {
                            promote(piese);
                            return;
                        }


                        if (x > 0 && x < 7 && y < 7)
                        {
                            piece fPiece = sqs[x, y + 1].occupied;
                            piece ALPiece = sqs[x - 1, y + 1].occupied;
                            piece ARPiece = sqs[x + 1, y + 1].occupied;

                            if (ALPiece.pType != pieceTypes.none && ALPiece.pCol != piese.pCol)
                            {
                                CPanels[x - 1, y + 1].BackColor = Color.DeepPink;
                            }
                            else if (ARPiece.pType != pieceTypes.none && ARPiece.pCol != piese.pCol)
                            {
                                CPanels[x + 1, y + 1].BackColor = Color.DeepPink;
                            }

                            if (fPiece.pType == pieceTypes.none)
                            {
                                CPanels[x, y + 1].BackColor = Color.LightBlue;
                            }
                        }
                        else if (x > 0 && y < 7)
                        {
                            piece fPiece = sqs[x, y + 1].occupied;
                            piece ALPiece = sqs[x - 1, y + 1].occupied;

                            if (ALPiece.pType != pieceTypes.none && ALPiece.pCol != piese.pCol)
                            {
                                CPanels[x - 1, y + 1].BackColor = Color.DeepPink;
                            }

                            if (fPiece.pType == pieceTypes.none)
                            {
                                CPanels[x, y + 1].BackColor = Color.LightBlue;
                            }
                        }
                        else if (x < 7 && y < 7)
                        {
                            piece fPiece = sqs[x, y + 1].occupied;
                            piece ARPiece = sqs[x + 1, y + 1].occupied;

                            if (ARPiece.pType != pieceTypes.none && ARPiece.pCol != piese.pCol)
                            {
                                CPanels[x + 1, y + 1].BackColor = Color.DeepPink;
                            }

                            if (fPiece.pType == pieceTypes.none)
                            {
                                CPanels[x, y + 1].BackColor = Color.LightBlue;
                            }
                        }
                        break;

                    case pieceTypes.rook:
                        //eval to left
                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            piece pce = sqs[a, y].occupied;
                            if (checkForNone(pce))
                            {
                                CPanels[a, y].BackColor = Color.LightBlue;
                            }
                            else
                            {
                                if (pce.pCol != piese.pCol)
                                {
                                    CPanels[a, y].BackColor = Color.DeepPink;
                                }
                                break;
                            }
                        }
                        //eval to right
                        for (int a = x + 1; a < 8; a++)
                        {
                            piece pce = sqs[a, y].occupied;
                            if (checkForNone(pce))
                            {
                                CPanels[a, y].BackColor = Color.LightBlue;
                            }
                            else
                            {
                                if (pce.pCol != piese.pCol)
                                {
                                    CPanels[a, y].BackColor = Color.DeepPink;
                                }
                                break;
                            }
                        }
                        //eval down
                        for (int a = y + 1; a < 8; a++)
                        {
                            piece pce = sqs[x, a].occupied;
                            if (checkForNone(pce))
                            {
                                CPanels[x, a].BackColor = Color.LightBlue;
                            }
                            else
                            {
                                if (pce.pCol != piese.pCol)
                                {
                                    CPanels[x, a].BackColor = Color.DeepPink;
                                }
                                break;
                            }
                        }
                        //eval up
                        for (int a = y - 1; a > -1; a -= 1)
                        {
                            piece pce = sqs[x, a].occupied;
                            if (checkForNone(pce))
                            {
                                CPanels[x, a].BackColor = Color.LightBlue;
                            }
                            else
                            {
                                if (pce.pCol != piese.pCol)
                                {
                                    CPanels[x, a].BackColor = Color.DeepPink;
                                }
                                break;
                            }
                        }

                        break;

                    case pieceTypes.knight:
                        if (x + 1 < 8 && y - 2 > -1)
                        {
                            //eval up 2 right 1
                            if (checkForNone(sqs[x + 1, y - 2].occupied))
                            {
                                CPanels[x + 1, y - 2].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x + 1, y - 2].occupied.pCol != color)
                            {
                                CPanels[x + 1, y - 2].BackColor = Color.DeepPink;
                            }
                        }

                        if (x - 1 > -1 && y - 2 > -1)
                        {
                            //eval up 2 left 1
                            if (checkForNone(sqs[x - 1, y - 2].occupied))
                            {
                                CPanels[x - 1, y - 2].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x - 1, y - 2].occupied.pCol != color)
                            {
                                CPanels[x - 1, y - 2].BackColor = Color.DeepPink;
                            }
                        }

                        if (x + 1 < 8 && y + 2 < 8)
                        {
                            //eval down 2 right 1
                            if (checkForNone(sqs[x + 1, y + 2].occupied))
                            {
                                CPanels[x + 1, y + 2].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x + 1, y + 2].occupied.pCol != color)
                            {
                                CPanels[x + 1, y + 2].BackColor = Color.DeepPink;
                            }
                        }

                        if (x - 1 > -1 && y + 2 < 8)
                        {
                            //eval down 2 left 1
                            if (checkForNone(sqs[x - 1, y + 2].occupied))
                            {
                                CPanels[x - 1, y + 2].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x - 1, y + 2].occupied.pCol != color)
                            {
                                CPanels[x - 1, y + 2].BackColor = Color.DeepPink;
                            }
                        }

                        if (x + 2 < 8 && y - 1 > -1)
                        {
                            //eval right 2 up 1
                            if (checkForNone(sqs[x + 2, y - 1].occupied))
                            {
                                CPanels[x + 2, y - 1].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x + 2, y - 1].occupied.pCol != color)
                            {
                                CPanels[x + 2, y - 1].BackColor = Color.DeepPink;
                            }
                        }

                        if (x + 2 < 8 && y + 1 < 8)
                        {
                            //eval right 2 down 1
                            if (checkForNone(sqs[x + 2, y + 1].occupied))
                            {
                                CPanels[x + 2, y + 1].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x + 2, y + 1].occupied.pCol != color)
                            {
                                CPanels[x + 2, y + 1].BackColor = Color.DeepPink;
                            }
                        }

                        if (x - 2 > -1 && y - 1 > -1)
                        {
                            //eval left 2 up 1
                            if (checkForNone(sqs[x - 2, y - 1].occupied))
                            {
                                CPanels[x - 2, y - 1].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x - 2, y - 1].occupied.pCol != color)
                            {
                                CPanels[x - 2, y - 1].BackColor = Color.DeepPink;
                            }
                        }

                        if (x - 2 > -1 && y + 1 < 8)
                        {
                            //eval left 2 down 1
                            if (checkForNone(sqs[x - 2, y + 1].occupied))
                            {
                                CPanels[x - 2, y + 1].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x - 2, y + 1].occupied.pCol != color)
                            {
                                CPanels[x - 2, y + 1].BackColor = Color.DeepPink;
                            }
                        }

                        break;

                    case pieceTypes.bishop:
                        //move towards 2 o'clock
                        int b = y;
                        for (int a = x + 1; a < 8; a++)
                        {
                            if (b > 0)
                            {
                                b -= 1;
                                if (!checkForNone(sqs[a, b].occupied))
                                {
                                    if (sqs[a, b].occupied.pCol != color)
                                    {
                                        CPanels[a, b].BackColor = Color.DeepPink;
                                    }
                                    b = -10;
                                    break;
                                }
                                CPanels[a, b].BackColor = Color.LightBlue;
                            }
                        }

                        //towards 5 o'clock
                        b = y;
                        for (int a = x + 1; a < 8; a++)
                        {
                            if (b < 7)
                            {
                                b++;
                                if (!checkForNone(sqs[a, b].occupied))
                                {
                                    if (sqs[a, b].occupied.pCol != color)
                                    {
                                        CPanels[a, b].BackColor = Color.DeepPink;
                                    }
                                    b = 10;
                                    break;
                                }
                                CPanels[a, b].BackColor = Color.LightBlue;
                            }
                        }

                        //towards 7 o'clock
                        b = y;
                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            if (b < 7)
                            {
                                b++;
                                if (!checkForNone(sqs[a, b].occupied))
                                {
                                    if (sqs[a, b].occupied.pCol != color)
                                    {
                                        CPanels[a, b].BackColor = Color.DeepPink;
                                    }
                                    b = 10;
                                    break;
                                }
                                CPanels[a, b].BackColor = Color.LightBlue;
                            }
                        }

                        //towards 11 o'clock
                        b = y;
                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            if (b > 0)
                            {
                                b -= 1;
                                if (!checkForNone(sqs[a, b].occupied))
                                {
                                    if (sqs[a, b].occupied.pCol != color)
                                    {
                                        CPanels[a, b].BackColor = Color.DeepPink;
                                    }
                                    b = -10;
                                    break;
                                }
                                CPanels[a, b].BackColor = Color.LightBlue;
                            }
                        }

                        break;

                    case pieceTypes.queen:
                        b = y;
                        int c = y;
                        for (int a = x + 1; a < 8; a++)
                        {
                            if (b > 0 && y > 0 && a < 8)
                            {
                                b -= 1;
                                //2 o'clock
                                if (!checkForNone(sqs[a, b].occupied))
                                {
                                    if (sqs[a, b].occupied.pCol != color)
                                    {
                                        CPanels[a, b].BackColor = Color.DeepPink;
                                    }
                                    b = -10;
                                    break;
                                }
                                CPanels[a, b].BackColor = Color.LightBlue;
                            }
                        }

                        for (int a = x + 1; a < 8; a++)
                        {
                            if (c < 7 && a < 7)
                            {
                                //5 o'clock
                                c++;
                                if (!checkForNone(sqs[a, c].occupied))
                                {
                                    if (sqs[a, c].occupied.pCol != color)
                                    {
                                        CPanels[a, c].BackColor = Color.DeepPink;
                                    }
                                    c = 10;
                                    break;
                                }
                                CPanels[a, c].BackColor = Color.LightBlue;
                            }
                        }

                        int e = x;
                        int f = y;
                        int g = x;
                        int d = y;
                        for (int a = y - 1; a > -1; a -= 1)
                        {
                            //up 
                            if (d > -1)
                            {
                                d -= 1;
                                if (!checkForNone(sqs[x, d].occupied))
                                {
                                    if (sqs[x, d].occupied.pCol != color)
                                    {
                                        CPanels[x, d].BackColor = Color.DeepPink;
                                    }
                                    d = -10;
                                    break;
                                }
                                CPanels[x, d].BackColor = Color.LightBlue;
                            }
                        }

                        for (int a = x + 1; a < 8; a++)
                        {
                            //right
                            if (e < 8)
                            {
                                e++;
                                if (!checkForNone(sqs[e, y].occupied))
                                {
                                    if (sqs[e, y].occupied.pCol != color)
                                    {
                                        CPanels[e, y].BackColor = Color.DeepPink;
                                    }
                                    e = 10;
                                    break;
                                }
                                CPanels[e, y].BackColor = Color.LightBlue;
                            }
                        }

                        for (int a = y + 1; a < 8; a++)
                        {
                            //down
                            if (f < 8)
                            {
                                f++;
                                if (!checkForNone(sqs[x, f].occupied))
                                {
                                    if (sqs[x, f].occupied.pCol != color)
                                    {
                                        CPanels[x, f].BackColor = Color.DeepPink;
                                    }
                                    f = 10;
                                    break;
                                }
                                CPanels[x, f].BackColor = Color.LightBlue;
                            }
                        }

                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            //left
                            if (g > -1)
                            {
                                g -= 1;
                                if (!checkForNone(sqs[g, y].occupied))
                                {
                                    if (sqs[g, y].occupied.pCol != color)
                                    {
                                        CPanels[g, y].BackColor = Color.DeepPink;
                                    }
                                    g = -10;
                                    break;
                                }
                                CPanels[g, y].BackColor = Color.LightBlue;
                            }
                        }

                        int i = y;
                        int h = y;
                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            //7 o'clock
                            if (h < 8 && y < 7)
                            {
                                h++;
                                if (!checkForNone(sqs[a, h].occupied))
                                {
                                    if (sqs[a, h].occupied.pCol != color)
                                    {
                                        CPanels[a, h].BackColor = Color.DeepPink;
                                    }
                                    h = 10;
                                    break;
                                }
                                CPanels[a, h].BackColor = Color.LightBlue;
                            }
                        }

                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            //11 o'clock
                            if (i > 0 && y > 0)
                            {
                                i -= 1;
                                if (!checkForNone(sqs[a, i].occupied))
                                {
                                    if (sqs[a, i].occupied.pCol != color)
                                    {
                                        CPanels[a, i].BackColor = Color.DeepPink;
                                    }
                                    i = -10;
                                    break;
                                }
                                CPanels[a, i].BackColor = Color.LightBlue;
                            }
                        }

                        break;

                    case pieceTypes.king:
                        //up
                        if (y > 0)
                        {
                            if (!checkForNone(sqs[x, y - 1].occupied))
                            {
                                if (sqs[x, y - 1].occupied.pCol != color)
                                {
                                    CPanels[x, y - 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x, y - 1].BackColor = Color.LightBlue;
                            }
                        }
                        //2 o'clock
                        if (y > 0 && x < 7)
                        {
                            if (!checkForNone(sqs[x + 1, y - 1].occupied))
                            {
                                if (sqs[x + 1, y - 1].occupied.pCol != color)
                                {
                                    CPanels[x + 1, y - 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x + 1, y - 1].BackColor = Color.LightBlue;
                            }
                        }
                        //right
                        if (x < 7)
                        {
                            if (!checkForNone(sqs[x + 1, y].occupied))
                            {
                                if (sqs[x + 1, y].occupied.pCol != color)
                                {
                                    CPanels[x + 1, y].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x + 1, y].BackColor = Color.LightBlue;
                            }
                        }
                        //5 o'clock
                        if (y < 7 && x < 7)
                        {
                            if (!checkForNone(sqs[x + 1, y + 1].occupied))
                            {
                                if (sqs[x + 1, y + 1].occupied.pCol != color)
                                {
                                    CPanels[x + 1, y + 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x + 1, y + 1].BackColor = Color.LightBlue;
                            }
                        }
                        //down
                        if (y < 7)
                        {
                            if (!checkForNone(sqs[x, y + 1].occupied))
                            {
                                if (sqs[x, y + 1].occupied.pCol != color)
                                {
                                    CPanels[x, y + 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x, y + 1].BackColor = Color.LightBlue;
                            }
                        }
                        //7 o'clock
                        if (x > 0 && y < 7)
                        {
                            if (!checkForNone(sqs[x - 1, y + 1].occupied))
                            {
                                if (sqs[x - 1, y + 1].occupied.pCol != color)
                                {
                                    CPanels[x - 1, y + 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x - 1, y + 1].BackColor = Color.LightBlue;
                            }
                        }
                        //left
                        if (x > 0)
                        {
                            if (!checkForNone(sqs[x - 1, y].occupied))
                            {
                                if (sqs[x - 1, y].occupied.pCol != color)
                                {
                                    CPanels[x - 1, y].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x - 1, y].BackColor = Color.LightBlue;
                            }
                        }
                        //11 o'clock
                        if (x > 0 && y > 0)
                        {
                            if (!checkForNone(sqs[x - 1, y - 1].occupied))
                            {
                                if (sqs[x - 1, y - 1].occupied.pCol != color)
                                {
                                    CPanels[x - 1, y - 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x - 1, y - 1].BackColor = Color.LightBlue;
                            }
                        }
                        break;

                    default:
                        return;
                }
            }
            else
            {
                color_panel();
                piece piese = sqs[x, y].occupied;
                switch (piese.pType)
                {
                    case pieceTypes.pawn:
                        if (y == 0)
                        {
                            //promote(piese);
                            return;
                        }


                        if (x > 0 && x < 7)
                        {
                            piece fPiece = sqs[x, y - 1].occupied;
                            piece ALPiece = sqs[x - 1, y - 1].occupied;
                            piece ARPiece = sqs[x + 1, y - 1].occupied;

                            if (ALPiece.pType != pieceTypes.none && ALPiece.pCol != piese.pCol)
                            {
                                CPanels[x - 1, y - 1].BackColor = Color.DeepPink;
                            }
                            else if (ARPiece.pType != pieceTypes.none && ARPiece.pCol != piese.pCol)
                            {
                                CPanels[x + 1, y - 1].BackColor = Color.DeepPink;
                            }

                            if (fPiece.pType == pieceTypes.none)
                            {
                                CPanels[x, y - 1].BackColor = Color.LightBlue;
                            }
                        }
                        else if (x > 0)
                        {
                            piece fPiece = sqs[x, y - 1].occupied;
                            piece ALPiece = sqs[x - 1, y - 1].occupied;

                            if (ALPiece.pType != pieceTypes.none && ALPiece.pCol != piese.pCol)
                            {
                                CPanels[x - 1, y - 1].BackColor = Color.DeepPink;
                            }

                            if (fPiece.pType == pieceTypes.none)
                            {
                                CPanels[x, y - 1].BackColor = Color.LightBlue;
                            }
                        }
                        else
                        {
                            piece fPiece = sqs[x, y - 1].occupied;
                            piece ARPiece = sqs[x + 1, y - 1].occupied;

                            if (ARPiece.pType != pieceTypes.none && ARPiece.pCol != piese.pCol)
                            {
                                CPanels[x + 1, y - 1].BackColor = Color.DeepPink;
                            }

                            if (fPiece.pType == pieceTypes.none)
                            {
                                CPanels[x, y - 1].BackColor = Color.LightBlue;
                            }
                        }
                        break;

                    case pieceTypes.rook:
                        //eval to left
                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            piece pce = sqs[a, y].occupied;
                            if (checkForNone(pce))
                            {
                                CPanels[a, y].BackColor = Color.LightBlue;
                            }
                            else
                            {
                                if (pce.pCol != piese.pCol)
                                {
                                    CPanels[a, y].BackColor = Color.DeepPink;
                                }
                                break;
                            }
                        }
                        //eval to right
                        for (int a = x + 1; a < 8; a++)
                        {
                            piece pce = sqs[a, y].occupied;
                            if (checkForNone(pce))
                            {
                                CPanels[a, y].BackColor = Color.LightBlue;
                            }
                            else
                            {
                                if (pce.pCol != piese.pCol)
                                {
                                    CPanels[a, y].BackColor = Color.DeepPink;
                                }
                                break;
                            }
                        }
                        //eval down
                        for (int a = y + 1; a < 8; a++)
                        {
                            piece pce = sqs[x, a].occupied;
                            if (checkForNone(pce))
                            {
                                CPanels[x, a].BackColor = Color.LightBlue;
                            }
                            else
                            {
                                if (pce.pCol != piese.pCol)
                                {
                                    CPanels[x, a].BackColor = Color.DeepPink;
                                }
                                break;
                            }
                        }
                        //eval up
                        for (int a = y - 1; a > -1; a -= 1)
                        {
                            piece pce = sqs[x, a].occupied;
                            if (checkForNone(pce))
                            {
                                CPanels[x, a].BackColor = Color.LightBlue;
                            }
                            else
                            {
                                if (pce.pCol != piese.pCol)
                                {
                                    CPanels[x, a].BackColor = Color.DeepPink;
                                }
                                break;
                            }
                        }

                        break;

                    case pieceTypes.knight:
                        if (x + 1 < 8 && y - 2 > -1)
                        {
                            //eval up 2 right 1
                            if (checkForNone(sqs[x + 1, y - 2].occupied))
                            {
                                CPanels[x + 1, y - 2].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x + 1, y - 2].occupied.pCol != color)
                            {
                                CPanels[x + 1, y - 2].BackColor = Color.DeepPink;
                            }
                        }

                        if (x - 1 > -1 && y - 2 > -1)
                        {
                            //eval up 2 left 1
                            if (checkForNone(sqs[x - 1, y - 2].occupied))
                            {
                                CPanels[x - 1, y - 2].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x - 1, y - 2].occupied.pCol != color)
                            {
                                CPanels[x - 1, y - 2].BackColor = Color.DeepPink;
                            }
                        }

                        if (x + 1 < 8 && y + 2 < 8)
                        {
                            //eval down 2 right 1
                            if (checkForNone(sqs[x + 1, y + 2].occupied))
                            {
                                CPanels[x + 1, y + 2].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x + 1, y + 2].occupied.pCol != color)
                            {
                                CPanels[x + 1, y + 2].BackColor = Color.DeepPink;
                            }
                        }

                        if (x - 1 > -1 && y + 2 < 8)
                        {
                            //eval down 2 left 1
                            if (checkForNone(sqs[x - 1, y + 2].occupied))
                            {
                                CPanels[x - 1, y + 2].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x - 1, y + 2].occupied.pCol != color)
                            {
                                CPanels[x - 1, y + 2].BackColor = Color.DeepPink;
                            }
                        }

                        if (x + 2 < 8 && y - 1 > -1)
                        {
                            //eval right 2 up 1
                            if (checkForNone(sqs[x + 2, y - 1].occupied))
                            {
                                CPanels[x + 2, y - 1].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x + 2, y - 1].occupied.pCol != color)
                            {
                                CPanels[x + 2, y - 1].BackColor = Color.DeepPink;
                            }
                        }

                        if (x + 2 < 8 && y + 1 < 8)
                        {
                            //eval right 2 down 1
                            if (checkForNone(sqs[x + 2, y + 1].occupied))
                            {
                                CPanels[x + 2, y + 1].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x + 2, y + 1].occupied.pCol != color)
                            {
                                CPanels[x + 2, y + 1].BackColor = Color.DeepPink;
                            }
                        }

                        if (x - 2 > -1 && y - 1 > -1)
                        {
                            //eval left 2 up 1
                            if (checkForNone(sqs[x - 2, y - 1].occupied))
                            {
                                CPanels[x - 2, y - 1].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x - 2, y - 1].occupied.pCol != color)
                            {
                                CPanels[x - 2, y - 1].BackColor = Color.DeepPink;
                            }
                        }

                        if (x - 2 > -1 && y + 1 < 8)
                        {
                            //eval left 2 down 1
                            if (checkForNone(sqs[x - 2, y + 1].occupied))
                            {
                                CPanels[x - 2, y + 1].BackColor = Color.LightBlue;
                            }
                            else if (sqs[x - 2, y + 1].occupied.pCol != color)
                            {
                                CPanels[x - 2, y + 1].BackColor = Color.DeepPink;
                            }
                        }

                        break;

                    case pieceTypes.bishop:
                        //move towards 2 o'clock
                        int b = y;
                        for (int a = x + 1; a < 8; a++)
                        {
                            if (b > 0)
                            {
                                b -= 1;
                                if (!checkForNone(sqs[a, b].occupied))
                                {
                                    if (sqs[a, b].occupied.pCol != color)
                                    {
                                        CPanels[a, b].BackColor = Color.DeepPink;
                                    }
                                    b = -10;
                                    break;
                                }
                                CPanels[a, b].BackColor = Color.LightBlue;
                            }
                        }

                        //towards 5 o'clock
                        b = y;
                        for (int a = x + 1; a < 8; a++)
                        {
                            if (b < 7)
                            {
                                b++;
                                if (!checkForNone(sqs[a, b].occupied))
                                {
                                    if (sqs[a, b].occupied.pCol != color)
                                    {
                                        CPanels[a, b].BackColor = Color.DeepPink;
                                    }
                                    b = 10;
                                    break;
                                }
                                CPanels[a, b].BackColor = Color.LightBlue;
                            }
                        }

                        //towards 7 o'clock
                        b = y;
                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            if (b < 7)
                            {
                                b++;
                                if (!checkForNone(sqs[a, b].occupied))
                                {
                                    if (sqs[a, b].occupied.pCol != color)
                                    {
                                        CPanels[a, b].BackColor = Color.DeepPink;
                                    }
                                    b = 10;
                                    break;
                                }
                                CPanels[a, b].BackColor = Color.LightBlue;
                            }
                        }

                        //towards 11 o'clock
                        b = y;
                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            if (b > 0)
                            {
                                b -= 1;
                                if (!checkForNone(sqs[a, b].occupied))
                                {
                                    if (sqs[a, b].occupied.pCol != color)
                                    {
                                        CPanels[a, b].BackColor = Color.DeepPink;
                                    }
                                    b = -10;
                                    break;
                                }
                                CPanels[a, b].BackColor = Color.LightBlue;
                            }
                        }

                        break;

                    case pieceTypes.queen:
                        b = y;
                        int c = y;
                        for (int a = x + 1; a < 8; a++)
                        {
                            if (y > 0 && x > 0)
                            {
                                b -= 1;
                                //2 o'clock
                                if (!checkForNone(sqs[a, b].occupied))
                                {
                                    if (sqs[a, b].occupied.pCol != color)
                                    {
                                        CPanels[a, b].BackColor = Color.DeepPink;
                                    }
                                    b = -10;
                                    break;
                                }
                                CPanels[a, b].BackColor = Color.LightBlue;
                            }
                        }

                        for (int a = x + 1; a < 8; a++)
                        {
                            if (c < 7 && y < 7 && a < 8)
                            {
                                //5 o'clock
                                c++;
                                if (!checkForNone(sqs[a, c].occupied))
                                {
                                    if (sqs[a, c].occupied.pCol != color)
                                    {
                                        CPanels[a, c].BackColor = Color.DeepPink;
                                    }
                                    c = 10;
                                    break;
                                }
                                CPanels[a, c].BackColor = Color.LightBlue;
                            }
                        }

                        int e = x;
                        int f = y;
                        int g = x;
                        int d = y;
                        for (int a = y - 1; a > -1; a -= 1)
                        {
                            //up 
                            if (d > -1)
                            {
                                d -= 1;
                                if (!checkForNone(sqs[x, d].occupied))
                                {
                                    if (sqs[x, d].occupied.pCol != color)
                                    {
                                        CPanels[x, d].BackColor = Color.DeepPink;
                                    }
                                    d = -10;
                                    break;
                                }
                                CPanels[x, d].BackColor = Color.LightBlue;
                            }
                        }

                        for (int a = x + 1; a < 8; a++)
                        {
                            //right
                            if (e < 8)
                            {
                                e++;
                                if (!checkForNone(sqs[e, y].occupied))
                                {
                                    if (sqs[e, y].occupied.pCol != color)
                                    {
                                        CPanels[e, y].BackColor = Color.DeepPink;
                                    }
                                    e = 10;
                                    break;
                                }
                                CPanels[e, y].BackColor = Color.LightBlue;
                            }
                        }

                        for (int a = y + 1; a < 8; a++)
                        {
                            //down
                            if (f < 8)
                            {
                                f++;
                                if (!checkForNone(sqs[x, f].occupied))
                                {
                                    if (sqs[x, f].occupied.pCol != color)
                                    {
                                        CPanels[x, f].BackColor = Color.DeepPink;
                                    }
                                    f = 10;
                                    break;
                                }
                                CPanels[x, f].BackColor = Color.LightBlue;
                            }
                        }

                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            //left
                            if (g > -1)
                            {
                                g -= 1;
                                if (!checkForNone(sqs[g, y].occupied))
                                {
                                    if (sqs[g, y].occupied.pCol != color)
                                    {
                                        CPanels[g, y].BackColor = Color.DeepPink;
                                    }
                                    g = -10;
                                    break;
                                }
                                CPanels[g, y].BackColor = Color.LightBlue;
                            }
                        }

                        int i = y;
                        int h = y;
                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            //7 o'clock
                            if (h < 7 && y < 7)
                            {
                                h++;
                                if (!checkForNone(sqs[a, h].occupied))
                                {
                                    if (sqs[a, h].occupied.pCol != color)
                                    {
                                        CPanels[a, h].BackColor = Color.DeepPink;
                                    }
                                    h = 10;
                                    break;
                                }
                                CPanels[a, h].BackColor = Color.LightBlue;
                            }
                        }

                        for (int a = x - 1; a > -1; a -= 1)
                        {
                            //11 o'clock
                            if (i > -1 && y > 0)
                            {
                                i -= 1;
                                if (!checkForNone(sqs[a, i].occupied))
                                {
                                    if (sqs[a, i].occupied.pCol != color)
                                    {
                                        CPanels[a, i].BackColor = Color.DeepPink;
                                    }
                                    i = -10;
                                    break;
                                }
                                CPanels[a, i].BackColor = Color.LightBlue;
                            }
                        }

                        break;

                    case pieceTypes.king:
                        //up
                        if (y > 0)
                        {
                            if (!checkForNone(sqs[x, y - 1].occupied))
                            {
                                if (sqs[x, y - 1].occupied.pCol != color)
                                {
                                    CPanels[x, y - 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x, y - 1].BackColor = Color.LightBlue;
                            }
                        }
                        //2 o'clock
                        if (y > 0 && x < 7)
                        {
                            if (!checkForNone(sqs[x + 1, y - 1].occupied))
                            {
                                if (sqs[x + 1, y - 1].occupied.pCol != color)
                                {
                                    CPanels[x + 1, y - 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x + 1, y - 1].BackColor = Color.LightBlue;
                            }
                        }
                        //right
                        if (x < 7)
                        {
                            if (!checkForNone(sqs[x + 1, y].occupied))
                            {
                                if (sqs[x + 1, y].occupied.pCol != color)
                                {
                                    CPanels[x + 1, y].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x + 1, y].BackColor = Color.LightBlue;
                            }
                        }
                        //5 o'clock
                        if (y < 7 && x < 7)
                        {
                            if (!checkForNone(sqs[x + 1, y + 1].occupied))
                            {
                                if (sqs[x + 1, y + 1].occupied.pCol != color)
                                {
                                    CPanels[x + 1, y + 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x + 1, y + 1].BackColor = Color.LightBlue;
                            }
                        }
                        //down
                        if (y < 7)
                        {
                            if (!checkForNone(sqs[x, y + 1].occupied))
                            {
                                if (sqs[x, y + 1].occupied.pCol != color)
                                {
                                    CPanels[x, y + 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x, y + 1].BackColor = Color.LightBlue;
                            }
                        }
                        //7 o'clock
                        if (x > 0 && y < 7)
                        {
                            if (!checkForNone(sqs[x - 1, y + 1].occupied))
                            {
                                if (sqs[x - 1, y + 1].occupied.pCol != color)
                                {
                                    CPanels[x - 1, y + 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x - 1, y + 1].BackColor = Color.LightBlue;
                            }
                        }
                        //left
                        if (x > 0)
                        {
                            if (!checkForNone(sqs[x - 1, y].occupied))
                            {
                                if (sqs[x - 1, y].occupied.pCol != color)
                                {
                                    CPanels[x - 1, y].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x - 1, y].BackColor = Color.LightBlue;
                            }
                        }
                        //11 o'clock
                        if (x > 0 && y > 0)
                        {
                            if (!checkForNone(sqs[x - 1, y - 1].occupied))
                            {
                                if (sqs[x - 1, y - 1].occupied.pCol != color)
                                {
                                    CPanels[x - 1, y - 1].BackColor = Color.DeepPink;
                                }
                            }
                            else
                            {
                                CPanels[x - 1, y - 1].BackColor = Color.LightBlue;
                            }
                        }
                        break;

                    default:
                        return;
                }
            }
        }

        //--------------------------------------------------------------------------------------
        //*************************************************************************************
        //-------------------------------------------------------------------------------------

        private void moveto(int x, int y, piece selObj, bool unmove = false, bool attack = false)
        {
            color_panel();

            int oldx = selObj.x;
            int oldy = selObj.y;


            sqs[oldx, oldy].occupied = none;
            CPanels[oldx, oldy].BackgroundImage = null;

            //list of all
            //black
            if (color == pieceCol.black)
            {
                if (selObj.x == bpawn1.x && selObj.y == bpawn1.y)
                {
                    bpawn1.x = x;
                    bpawn1.y = y;
                    sqs[x, y].occupied = bpawn1;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                }
                else if (selObj.x == bpawn2.x && selObj.y == bpawn2.y)
                {
                    bpawn2.x = x;
                    bpawn2.y = y;
                    sqs[x, y].occupied = bpawn2;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                }
                else if (selObj.x == bpawn3.x && selObj.y == bpawn3.y)
                {
                    bpawn3.x = x;
                    bpawn3.y = y;
                    sqs[x, y].occupied = bpawn3;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                }
                else if (selObj.x == bpawn4.x && selObj.y == bpawn4.y)
                {
                    bpawn4.x = x;
                    bpawn4.y = y;
                    sqs[x, y].occupied = bpawn4;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                }
                else if (selObj.x == bpawn5.x && selObj.y == bpawn5.y)
                {
                    bpawn5.x = x;
                    bpawn5.y = y;
                    sqs[x, y].occupied = bpawn5;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                }
                else if (selObj.x == bpawn6.x && selObj.y == bpawn6.y)
                {
                    bpawn6.x = x;
                    bpawn6.y = y;
                    sqs[x, y].occupied = bpawn6;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                }
                else if (selObj.x == bpawn7.x && selObj.y == bpawn7.y)
                {
                    bpawn7.x = x;
                    bpawn7.y = y;
                    sqs[x, y].occupied = bpawn7;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                }
                else if (selObj.x == bpawn8.x && selObj.y == bpawn8.y)
                {
                    bpawn8.x = x;
                    bpawn8.y = y;
                    sqs[x, y].occupied = bpawn8;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                }
                else if (selObj.x == brook1.x && selObj.y == brook1.y)
                {
                    brook1.x = x;
                    brook1.y = y;
                    sqs[x, y].occupied = brook1;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_rook;
                }
                else if (selObj.x == brook2.x && selObj.y == brook2.y)
                {
                    brook2.x = x;
                    brook2.y = y;
                    sqs[x, y].occupied = brook2;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_rook;
                }
                else if (selObj.x == bknight1.x && selObj.y == bknight1.y)
                {
                    bknight1.x = x;
                    bknight1.y = y;
                    sqs[x, y].occupied = bknight1;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_knight;
                }
                else if (selObj.x == bknight2.x && selObj.y == bknight2.y)
                {
                    bknight2.x = x;
                    bknight2.y = y;
                    sqs[x, y].occupied = bknight2;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_knight;
                }
                else if (selObj.x == bbishop1.x && selObj.y == bbishop1.y)
                {
                    bbishop1.x = x;
                    bbishop1.y = y;
                    sqs[x, y].occupied = bbishop1;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_bishop;
                }
                else if (selObj.x == bbishop2.x && selObj.y == bbishop2.y)
                {
                    bbishop2.x = x;
                    bbishop2.y = y;
                    sqs[x, y].occupied = bbishop2;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_bishop;
                }
                else if (selObj.x == bking.x && selObj.y == bking.y)
                {
                    bking.x = x;
                    bking.y = y;
                    sqs[x, y].occupied = bking;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_king;
                }
                else if (selObj.x == bqueen.x && selObj.y == bqueen.y)
                {
                    bqueen.x = x;
                    bqueen.y = y;
                    sqs[x, y].occupied = bqueen;
                    CPanels[x, y].BackgroundImage = Properties.Resources.black_queen;
                }
            }
            //white
            if (color == pieceCol.white)
            {
                if (selObj.x == wpawn1.x && selObj.y == wpawn1.y)
                {
                    wpawn1.x = x;
                    wpawn1.y = y;
                    sqs[x, y].occupied = wpawn1;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                }
                else if (selObj.x == wpawn2.x && selObj.y == wpawn2.y)
                {
                    wpawn2.x = x;
                    wpawn2.y = y;
                    sqs[x, y].occupied = wpawn2;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                }
                else if (selObj.x == wpawn3.x && selObj.y == wpawn3.y)
                {
                    wpawn3.x = x;
                    wpawn3.y = y;
                    sqs[x, y].occupied = wpawn3;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                }
                else if (selObj.x == wpawn4.x && selObj.y == wpawn4.y)
                {
                    wpawn4.x = x;
                    wpawn4.y = y;
                    sqs[x, y].occupied = wpawn4;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                }
                else if (selObj.x == wpawn5.x && selObj.y == wpawn5.y)
                {
                    wpawn5.x = x;
                    wpawn5.y = y;
                    sqs[x, y].occupied = wpawn5;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                }
                else if (selObj.x == wpawn6.x && selObj.y == wpawn6.y)
                {
                    wpawn6.x = x;
                    wpawn6.y = y;
                    sqs[x, y].occupied = wpawn6;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                }
                else if (selObj.x == wpawn7.x && selObj.y == wpawn7.y)
                {
                    wpawn7.x = x;
                    wpawn7.y = y;
                    sqs[x, y].occupied = wpawn7;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                }
                else if (selObj.x == wpawn8.x && selObj.y == wpawn8.y)
                {
                    wpawn8.x = x;
                    wpawn8.y = y;
                    sqs[x, y].occupied = wpawn8;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                }
                else if (selObj.x == wrook1.x && selObj.y == wrook1.y)
                {
                    wrook1.x = x;
                    wrook1.y = y;
                    sqs[x, y].occupied = wrook1;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_rook;
                }
                else if (selObj.x == wrook2.x && selObj.y == wrook2.y)
                {
                    wrook2.x = x;
                    wrook2.y = y;
                    sqs[x, y].occupied = wrook2;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_rook;
                }
                else if (selObj.x == wknight1.x && selObj.y == wknight1.y)
                {
                    wknight1.x = x;
                    wknight1.y = y;
                    sqs[x, y].occupied = wknight1;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_knight;
                }
                else if (selObj.x == wknight2.x && selObj.y == wknight2.y)
                {
                    wknight2.x = x;
                    wknight2.y = y;
                    sqs[x, y].occupied = wknight2;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_knight;
                }
                else if (selObj.x == wbishop1.x && selObj.y == wbishop1.y)
                {
                    wbishop1.x = x;
                    wbishop1.y = y;
                    sqs[x, y].occupied = wbishop1;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_bishop;
                }
                else if (selObj.x == wbishop2.x && selObj.y == wbishop2.y)
                {
                    wbishop2.x = x;
                    wbishop2.y = y;
                    sqs[x, y].occupied = wbishop2;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_bishop;
                }
                else if (selObj.x == wking.x && selObj.y == wking.y)
                {
                    wking.x = x;
                    wking.y = y;
                    sqs[x, y].occupied = wking;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_king;
                }
                else if (selObj.x == wqueen.x && selObj.y == wqueen.y)
                {
                    wqueen.x = x;
                    wqueen.y = y;
                    sqs[x, y].occupied = wqueen;
                    CPanels[x, y].BackgroundImage = Properties.Resources.white_queen;
                }
            }
            pce = sqs[x, y].occupied;

            string answ = isCheck();
            answer = answ;
            //return to original place if in check or checkmate
            if (answ == "check" || answ == "checkmate" || unmove)
            {
                CPanels[x, y].BackgroundImage = null;

                if (color == pieceCol.black)
                {
                    selObj = sqs[x, y].occupied;
                    sqs[x, y].occupied = none;
                    x = oldx;
                    y = oldy;

                    if (selObj.x == bpawn1.x && selObj.y == bpawn1.y)
                    {
                        bpawn1.x = x;
                        bpawn1.y = y;
                        sqs[x, y].occupied = bpawn1;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                    }
                    else if (selObj.x == bpawn2.x && selObj.y == bpawn2.y)
                    {
                        bpawn2.x = x;
                        bpawn2.y = y;
                        sqs[x, y].occupied = bpawn2;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                    }
                    else if (selObj.x == bpawn3.x && selObj.y == bpawn3.y)
                    {
                        bpawn3.x = x;
                        bpawn3.y = y;
                        sqs[x, y].occupied = bpawn3;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                    }
                    else if (selObj.x == bpawn4.x && selObj.y == bpawn4.y)
                    {
                        bpawn4.x = x;
                        bpawn4.y = y;
                        sqs[x, y].occupied = bpawn4;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                    }
                    else if (selObj.x == bpawn5.x && selObj.y == bpawn5.y)
                    {
                        bpawn5.x = x;
                        bpawn5.y = y;
                        sqs[x, y].occupied = bpawn5;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                    }
                    else if (selObj.x == bpawn6.x && selObj.y == bpawn6.y)
                    {
                        bpawn6.x = x;
                        bpawn6.y = y;
                        sqs[x, y].occupied = bpawn6;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                    }
                    else if (selObj.x == bpawn7.x && selObj.y == bpawn7.y)
                    {
                        bpawn7.x = x;
                        bpawn7.y = y;
                        sqs[x, y].occupied = bpawn7;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                    }
                    else if (selObj.x == bpawn8.x && selObj.y == bpawn8.y)
                    {
                        bpawn8.x = x;
                        bpawn8.y = y;
                        sqs[x, y].occupied = bpawn8;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                    }
                    else if (selObj.x == brook1.x && selObj.y == brook1.y)
                    {
                        brook1.x = x;
                        brook1.y = y;
                        sqs[x, y].occupied = brook1;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_rook;
                    }
                    else if (selObj.x == brook2.x && selObj.y == brook2.y)
                    {
                        brook2.x = x;
                        brook2.y = y;
                        sqs[x, y].occupied = brook2;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_rook;
                    }
                    else if (selObj.x == bknight1.x && selObj.y == bknight1.y)
                    {
                        bknight1.x = x;
                        bknight1.y = y;
                        sqs[x, y].occupied = bknight1;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_knight;
                    }
                    else if (selObj.x == bknight2.x && selObj.y == bknight2.y)
                    {
                        bknight2.x = x;
                        bknight2.y = y;
                        sqs[x, y].occupied = bknight2;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_knight;
                    }
                    else if (selObj.x == bbishop1.x && selObj.y == bbishop1.y)
                    {
                        bbishop1.x = x;
                        bbishop1.y = y;
                        sqs[x, y].occupied = bbishop1;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_bishop;
                    }
                    else if (selObj.x == bbishop2.x && selObj.y == bbishop2.y)
                    {
                        bbishop2.x = x;
                        bbishop2.y = y;
                        sqs[x, y].occupied = bbishop2;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_bishop;
                    }
                    else if (selObj.x == bking.x && selObj.y == bking.y)
                    {
                        bking.x = x;
                        bking.y = y;
                        sqs[x, y].occupied = bking;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_king;
                    }
                    else if (selObj.x == bqueen.x && selObj.y == bqueen.y)
                    {
                        bqueen.x = x;
                        bqueen.y = y;
                        sqs[x, y].occupied = bqueen;
                        CPanels[x, y].BackgroundImage = Properties.Resources.black_queen;
                    }
                }
                //white
                if (color == pieceCol.white)
                {
                    selObj = sqs[x, y].occupied;
                    sqs[x, y].occupied = none;
                    x = oldx;
                    y = oldy;

                    if (selObj.x == wpawn1.x && selObj.y == wpawn1.y)
                    {
                        wpawn1.x = x;
                        wpawn1.y = y;
                        sqs[x, y].occupied = wpawn1;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                    }
                    else if (selObj.x == wpawn2.x && selObj.y == wpawn2.y)
                    {
                        wpawn2.x = x;
                        wpawn2.y = y;
                        sqs[x, y].occupied = wpawn2;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                    }
                    else if (selObj.x == wpawn3.x && selObj.y == wpawn3.y)
                    {
                        wpawn3.x = x;
                        wpawn3.y = y;
                        sqs[x, y].occupied = wpawn3;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                    }
                    else if (selObj.x == wpawn4.x && selObj.y == wpawn4.y)
                    {
                        wpawn4.x = x;
                        wpawn4.y = y;
                        sqs[x, y].occupied = wpawn4;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                    }
                    else if (selObj.x == wpawn5.x && selObj.y == wpawn5.y)
                    {
                        wpawn5.x = x;
                        wpawn5.y = y;
                        sqs[x, y].occupied = wpawn5;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                    }
                    else if (selObj.x == wpawn6.x && selObj.y == wpawn6.y)
                    {
                        wpawn6.x = x;
                        wpawn6.y = y;
                        sqs[x, y].occupied = wpawn6;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                    }
                    else if (selObj.x == wpawn7.x && selObj.y == wpawn7.y)
                    {
                        wpawn7.x = x;
                        wpawn7.y = y;
                        sqs[x, y].occupied = wpawn7;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                    }
                    else if (selObj.x == wpawn8.x && selObj.y == wpawn8.y)
                    {
                        wpawn8.x = x;
                        wpawn8.y = y;
                        sqs[x, y].occupied = wpawn8;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                    }
                    else if (selObj.x == wrook1.x && selObj.y == wrook1.y)
                    {
                        wrook1.x = x;
                        wrook1.y = y;
                        sqs[x, y].occupied = wrook1;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_rook;
                    }
                    else if (selObj.x == wrook2.x && selObj.y == wrook2.y)
                    {
                        wrook2.x = x;
                        wrook2.y = y;
                        sqs[x, y].occupied = wrook2;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_rook;
                    }
                    else if (selObj.x == wknight1.x && selObj.y == wknight1.y)
                    {
                        wknight1.x = x;
                        wknight1.y = y;
                        sqs[x, y].occupied = wknight1;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_knight;
                    }
                    else if (selObj.x == wknight2.x && selObj.y == wknight2.y)
                    {
                        wknight2.x = x;
                        wknight2.y = y;
                        sqs[x, y].occupied = wknight2;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_knight;
                    }
                    else if (selObj.x == wbishop1.x && selObj.y == wbishop1.y)
                    {
                        wbishop1.x = x;
                        wbishop1.y = y;
                        sqs[x, y].occupied = wbishop1;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_bishop;
                    }
                    else if (selObj.x == wbishop2.x && selObj.y == wbishop2.y)
                    {
                        wbishop2.x = x;
                        wbishop2.y = y;
                        sqs[x, y].occupied = wbishop2;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_bishop;
                    }
                    else if (selObj.x == wking.x && selObj.y == wking.y)
                    {
                        wking.x = x;
                        wking.y = y;
                        sqs[x, y].occupied = wking;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_king;
                    }
                    else if (selObj.x == wqueen.x && selObj.y == wqueen.y)
                    {
                        wqueen.x = x;
                        wqueen.y = y;
                        sqs[x, y].occupied = wqueen;
                        CPanels[x, y].BackgroundImage = Properties.Resources.white_queen;
                    }
                }
                if (userMove && !unmove)
                {
                    MessageBox.Show("Check, can't move there!");
                }
            }
            if (answ == "checkmate")
            {
                if (userMove)
                {
                    winner = color.ToString();
                }
                else
                {
                    if (color == pieceCol.black)
                    {
                        color = pieceCol.white;
                    }
                    else
                    {
                        color = pieceCol.black;
                    }
                }
                MessageBox.Show("CheckMate! " + winner + " Wins!");
                button1_Click(null, null);
            }
            if (answ == "clean" || attack == true)
            {
                if (userMove)
                {
                    compMove();
                }
            }
        }

    }
}
