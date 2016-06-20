/*
Logic for handling piece attacking another piece
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

        private void attack(int x, int y, piece selObj, bool undo = false)
        {
            pce = sqs[x, y].occupied;
            if (pce.pType != pieceTypes.king) //we should be in checkmate if this happens, but better safe than sorry
            {
                moveto(x, y, selObj, false, true);

                if (answer == "clean" && undo == false)
                {
                    deletedPieces.Add(pce.pType.ToString());

                    if (pce.pCol == pieceCol.black)
                    {
                        //white
                        if (pce.x == wpawn1.x && pce.y == wpawn1.y)
                        {
                            wpawn1.condition = pCond.dead;
                        }
                        else if (pce.x == wpawn2.x && pce.y == wpawn2.y)
                        {
                            wpawn2.condition = pCond.dead;
                        }
                        else if (pce.x == wpawn3.x && pce.y == wpawn3.y)
                        {
                            wpawn3.condition = pCond.dead;
                        }
                        else if (pce.x == wpawn4.x && pce.y == wpawn4.y)
                        {
                            wpawn4.condition = pCond.dead;
                        }
                        else if (pce.x == wpawn5.x && pce.y == wpawn5.y)
                        {
                            wpawn5.condition = pCond.dead;
                        }
                        else if (pce.x == wpawn6.x && pce.y == wpawn6.y)
                        {
                            wpawn6.condition = pCond.dead;
                        }
                        else if (pce.x == wpawn7.x && pce.y == wpawn7.y)
                        {
                            wpawn7.condition = pCond.dead;
                        }
                        else if (pce.x == wpawn8.x && pce.y == wpawn8.y)
                        {
                            wpawn8.condition = pCond.dead;
                        }
                        else if (pce.x == wrook1.x && pce.y == wrook1.y)
                        {
                            wrook1.condition = pCond.dead;
                        }
                        else if (pce.x == wrook2.x && pce.y == wrook2.y)
                        {
                            wrook2.condition = pCond.dead;
                        }
                        else if (pce.x == wknight1.x && pce.y == wknight1.y)
                        {
                            wknight1.condition = pCond.dead;
                        }
                        else if (pce.x == wknight2.x && pce.y == wknight2.y)
                        {
                            wknight2.condition = pCond.dead;
                        }
                        else if (pce.x == wbishop1.x && pce.y == wbishop1.y)
                        {
                            wbishop1.condition = pCond.dead;
                        }
                        else if (pce.x == wbishop2.x && pce.y == wbishop2.y)
                        {
                            wbishop2.condition = pCond.dead;
                        }
                        else if (pce.x == wking.x && pce.y == wking.y)
                        {
                            wking.condition = pCond.dead;
                        }
                        else if (pce.x == wqueen.x && pce.y == wqueen.y)
                        {
                            wqueen.condition = pCond.dead;
                        }
                    }
                    else
                    {
                        //black
                        if (pce.x == bpawn1.x && pce.y == bpawn1.y)
                        {
                            bpawn1.condition = pCond.dead;
                        }
                        else if (pce.x == bpawn2.x && pce.y == bpawn2.y)
                        {
                            bpawn2.condition = pCond.dead;
                        }
                        else if (pce.x == bpawn3.x && pce.y == bpawn3.y)
                        {
                            bpawn3.condition = pCond.dead;
                        }
                        else if (pce.x == bpawn4.x && pce.y == bpawn4.y)
                        {
                            bpawn4.condition = pCond.dead;
                        }
                        else if (pce.x == bpawn5.x && pce.y == bpawn5.y)
                        {
                            bpawn5.condition = pCond.dead;
                        }
                        else if (pce.x == bpawn6.x && pce.y == bpawn6.y)
                        {
                            bpawn6.condition = pCond.dead;
                        }
                        else if (pce.x == bpawn7.x && pce.y == bpawn7.y)
                        {
                            bpawn7.condition = pCond.dead;
                        }
                        else if (pce.x == bpawn8.x && pce.y == bpawn8.y)
                        {
                            bpawn8.condition = pCond.dead;
                        }
                        else if (pce.x == brook1.x && pce.y == brook1.y)
                        {
                            brook1.condition = pCond.dead;
                        }
                        else if (pce.x == brook2.x && pce.y == brook2.y)
                        {
                            brook2.condition = pCond.dead;
                        }
                        else if (pce.x == bknight1.x && pce.y == bknight1.y)
                        {
                            bknight1.condition = pCond.dead;
                        }
                        else if (pce.x == bknight2.x && pce.y == bknight2.y)
                        {
                            bknight2.condition = pCond.dead;
                        }
                        else if (pce.x == bbishop1.x && pce.y == bbishop1.y)
                        {
                            bbishop1.condition = pCond.dead;
                        }
                        else if (pce.x == bbishop2.x && pce.y == bbishop2.y)
                        {
                            bbishop2.condition = pCond.dead;
                        }
                        else if (pce.x == bking.x && pce.y == bking.y)
                        {
                            bking.condition = pCond.dead;
                        }
                        else if (pce.x == bqueen.x && pce.y == bqueen.y)
                        {
                            bqueen.condition = pCond.dead;
                        }
                    }
                }
                else
                {
                    if (color == pieceCol.black)
                    {
                        //white
                        if (pce.x == wpawn1.x && pce.y == wpawn1.y)
                        {
                            sqs[x, y].occupied = wpawn1;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                        }
                        else if (pce.x == wpawn2.x && pce.y == wpawn2.y)
                        {
                            sqs[x, y].occupied = wpawn2;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                        }
                        else if (pce.x == wpawn3.x && pce.y == wpawn3.y)
                        {
                            sqs[x, y].occupied = wpawn3;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                        }
                        else if (pce.x == wpawn4.x && pce.y == wpawn4.y)
                        {
                            sqs[x, y].occupied = wpawn4;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                        }
                        else if (pce.x == wpawn5.x && pce.y == wpawn5.y)
                        {
                            sqs[x, y].occupied = wpawn5;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                        }
                        else if (pce.x == wpawn6.x && pce.y == wpawn6.y)
                        {
                            sqs[x, y].occupied = wpawn6;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                        }
                        else if (pce.x == wpawn7.x && pce.y == wpawn7.y)
                        {
                            sqs[x, y].occupied = wpawn7;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                        }
                        else if (pce.x == wpawn8.x && pce.y == wpawn8.y)
                        {
                            sqs[x, y].occupied = wpawn8;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_pawn;
                        }
                        else if (pce.x == wrook1.x && pce.y == wrook1.y)
                        {
                            sqs[x, y].occupied = wrook1;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_rook;
                        }
                        else if (pce.x == wrook2.x && pce.y == wrook2.y)
                        {
                            sqs[x, y].occupied = wrook2;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_rook;
                        }
                        else if (pce.x == wknight1.x && pce.y == wknight1.y)
                        {
                            sqs[x, y].occupied = wknight1;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_knight;
                        }
                        else if (pce.x == wknight2.x && pce.y == wknight2.y)
                        {
                            sqs[x, y].occupied = wknight2;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_knight;
                        }
                        else if (pce.x == wbishop1.x && pce.y == wbishop1.y)
                        {
                            sqs[x, y].occupied = wbishop1;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_bishop;
                        }
                        else if (pce.x == wbishop2.x && pce.y == wbishop2.y)
                        {
                            sqs[x, y].occupied = wbishop2;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_bishop;
                        }
                        else if (pce.x == wking.x && pce.y == wking.y)
                        {
                            sqs[x, y].occupied = wking;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_king;
                        }
                        else if (pce.x == wqueen.x && pce.y == wqueen.y)
                        {
                            sqs[x, y].occupied = wqueen;
                            CPanels[x, y].BackgroundImage = Properties.Resources.white_queen;
                        }
                    }
                    else
                    {
                        //black
                        if (pce.x == bpawn1.x && pce.y == bpawn1.y)
                        {
                            sqs[x, y].occupied = bpawn1;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                        }
                        else if (pce.x == bpawn2.x && pce.y == bpawn2.y)
                        {
                            sqs[x, y].occupied = bpawn2;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                        }
                        else if (pce.x == bpawn3.x && pce.y == bpawn3.y)
                        {
                            sqs[x, y].occupied = bpawn3;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                        }
                        else if (pce.x == bpawn4.x && pce.y == bpawn4.y)
                        {
                            sqs[x, y].occupied = bpawn4;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                        }
                        else if (pce.x == bpawn5.x && pce.y == bpawn5.y)
                        {
                            sqs[x, y].occupied = bpawn5;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                        }
                        else if (pce.x == bpawn6.x && pce.y == bpawn6.y)
                        {
                            sqs[x, y].occupied = bpawn6;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                        }
                        else if (pce.x == bpawn7.x && pce.y == bpawn7.y)
                        {
                            sqs[x, y].occupied = bpawn7;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                        }
                        else if (pce.x == bpawn8.x && pce.y == bpawn8.y)
                        {
                            sqs[x, y].occupied = bpawn8;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_pawn;
                        }
                        else if (pce.x == brook1.x && pce.y == brook1.y)
                        {
                            sqs[x, y].occupied = brook1;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_rook;
                        }
                        else if (pce.x == brook2.x && pce.y == brook2.y)
                        {
                            sqs[x, y].occupied = brook2;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_rook;
                        }
                        else if (pce.x == bknight1.x && pce.y == bknight1.y)
                        {
                            sqs[x, y].occupied = bknight1;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_knight;
                        }
                        else if (pce.x == bknight2.x && pce.y == bknight2.y)
                        {
                            sqs[x, y].occupied = bknight2;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_knight;
                        }
                        else if (pce.x == bbishop1.x && pce.y == bbishop1.y)
                        {
                            sqs[x, y].occupied = bbishop1;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_bishop;
                        }
                        else if (pce.x == bbishop2.x && pce.y == bbishop2.y)
                        {
                            sqs[x, y].occupied = bbishop2;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_bishop;
                        }
                        else if (pce.x == bking.x && pce.y == bking.y)
                        {
                            sqs[x, y].occupied = bking;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_king;
                        }
                        else if (pce.x == bqueen.x && pce.y == bqueen.y)
                        {
                            sqs[x, y].occupied = bqueen;
                            CPanels[x, y].BackgroundImage = Properties.Resources.black_queen;
                        }
                    }
                }
            }
        }
    }
}
