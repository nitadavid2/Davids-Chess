/*
Logic for CPU Opponent AI
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

        private void compMove()   //computer move
        {
            userMove = false;
            if (color == pieceCol.black)
            {
                color = pieceCol.white;
            }
            else
            {
                color = pieceCol.black;
            }
            int dtstart = DateTime.Now.Millisecond;

            piece tarPiece = analyzeMove(); //finds best move

            if (checkForNone(sqs[tX, tY].occupied))
            {
                moveto(tX, tY, tarPiece);
            }
            else if (color != sqs[tX, tY].occupied.pCol)
            {
                attack(tX, tY, tarPiece);
            }
            //not attacking or moving to

            double elapsed = (DateTime.Now.Millisecond - dtstart);
            listView1.Items.Add("computer move took -> 0." + elapsed.ToString() + " seconds");
            MessageBox.Show(jl);


            if (color == pieceCol.black)
            {
                color = pieceCol.white;
            }
            else
            {
                color = pieceCol.black;
            }
            userMove = true;
        }

        private piece analyzeMove() //finds best move
        {
            iterateOnce = false;
            listView1.Items.Clear();
            moves.Clear();
            curMoves.Clear();
            /*
             values for pieces :
             pawn - 40,  rook - 100, knight - 80, bishop - 100, queen - 180, king - 500
             */
            piece retPiece = new piece();
            string moved = "pawn1";
            string cMove = "pawn1";

            if (color == pieceCol.white)
            {
                if (!red)
                {
                    if (wpawn1.condition == pCond.alive)
                    {
                        moves.Add("pawn1", "0");
                        curMoves.Add("pawn1", "0");
                    }
                    if (wpawn2.condition == pCond.alive)
                    {
                        moves.Add("pawn2", "0");
                        curMoves.Add("pawn2", "0");
                    }
                    if (wpawn3.condition == pCond.alive)
                    {
                        moves.Add("pawn3", "0");
                        curMoves.Add("pawn3", "0");
                    }
                    if (wpawn4.condition == pCond.alive)
                    {
                        moves.Add("pawn4", "0");
                        curMoves.Add("pawn4", "0");
                    }
                    if (wpawn5.condition == pCond.alive)
                    {
                        moves.Add("pawn5", "0");
                        curMoves.Add("pawn5", "0");
                    }
                    if (wpawn6.condition == pCond.alive)
                    {
                        moves.Add("pawn6", "0");
                        curMoves.Add("pawn6", "0");
                    }
                    if (wpawn7.condition == pCond.alive)
                    {
                        moves.Add("pawn7", "0");
                        curMoves.Add("pawn7", "0");
                    }
                    if (wpawn8.condition == pCond.alive)
                    {
                        moves.Add("pawn8", "0");
                        curMoves.Add("pawn8", "0");
                    }
                    if (wrook1.condition == pCond.alive)
                    {
                        moves.Add("rook1", "0");
                        curMoves.Add("rook1", "0");
                    }
                    if (wrook2.condition == pCond.alive)
                    {
                        moves.Add("rook2", "0");
                        curMoves.Add("rook2", "0");
                    }
                    if (wknight1.condition == pCond.alive)
                    {
                        moves.Add("knight1", "0");
                        curMoves.Add("knight1", "0");
                    }
                    if (wknight2.condition == pCond.alive)
                    {
                        moves.Add("knight2", "0");
                        curMoves.Add("knight2", "0");
                    }
                    if (wbishop1.condition == pCond.alive)
                    {
                        moves.Add("bishop1", "0");
                        curMoves.Add("bishop1", "0");
                    }
                    if (wbishop2.condition == pCond.alive)
                    {
                        moves.Add("bishop2", "0");
                        curMoves.Add("bishop2", "0");
                    }
                    if (wqueen.condition == pCond.alive)
                    {
                        moves.Add("queen", "0");
                        curMoves.Add("queen", "0");
                    }
                    if (wking.condition == pCond.alive)
                    {
                        moves.Add("king", "0");
                        curMoves.Add("king", "0");
                    }
                }

                p1 = true;
                p2 = true;
                p3 = true;
                p4 = true;
                p5 = true;
                p6 = true;
                p7 = true;
                p8 = true;
                r1 = true;
                r2 = true;
                kn1 = true;
                kn2 = true;
                b1 = true;
                b2 = true;
                k = true;
                q = true;

                
                pp1 = false; pp2 = false; pp3 = false; pp4 = false; pp5 = false; pp6 = false; pp7 = false; pp8 = false;
                pr1 = false; pr2 = false; pkn1 = false; pkn2 = false; pb1 = false; pb2 = false; pq = false; pk = false;

                opp1 = false; opp2 = false; opp3 = false; opp4 = false; opp5 = false; opp6 = false; opp7 = false; opp8 = false;
                opr1 = false; opr2 = false; opkn1 = false; opkn2 = false; opb1 = false; opb2 = false; opq = false; opk = false;

            star:
                if (wpawn1.condition == pCond.alive)
                {
                    pp1 = true;
                    p1x = wpawn1.x;
                    p1y = wpawn1.y;
                }
                if (wpawn2.condition == pCond.alive)
                {
                    pp2 = true;
                    p2x = wpawn2.x;
                    p2y = wpawn2.y;
                }
                if (wpawn3.condition == pCond.alive)
                {
                    pp3 = true;
                    p3x = wpawn3.x;
                    p3y = wpawn3.y;
                }
                if (wpawn4.condition == pCond.alive)
                {
                    pp4 = true;
                    p4x = wpawn4.x;
                    p4y = wpawn4.y;
                }
                if (wpawn5.condition == pCond.alive)
                {
                    pp5 = true;
                    p5x = wpawn5.x;
                    p5y = wpawn5.y;
                }
                if (wpawn6.condition == pCond.alive)
                {
                    pp6 = true;
                    p6x = wpawn6.x;
                    p6y = wpawn6.y;
                }
                if (wpawn7.condition == pCond.alive)
                {
                    pp7 = true;
                    p7x = wpawn7.x;
                    p7y = wpawn7.y;
                }
                if (wpawn8.condition == pCond.alive)
                {
                    pp8 = true;
                    p8x = wpawn8.x;
                    p8y = wpawn8.y;
                }
                if (wrook1.condition == pCond.alive)
                {
                    pr1 = true;
                    r1x = wrook1.x;
                    r1y = wrook1.y;
                }
                if (wrook2.condition == pCond.alive)
                {
                    pr2 = true;
                    r2x = wrook2.x;
                    r2y = wrook2.y;
                }
                if (wknight1.condition == pCond.alive)
                {
                    pkn1 = true;
                    kn1x = wknight1.x;
                    kn1y = wknight1.y;
                }
                if (wknight2.condition == pCond.alive)
                {
                    pkn2 = true;
                    kn2x = wknight2.x;
                    kn2y = wknight2.y;
                }
                if (wbishop1.condition == pCond.alive)
                {
                    pb1 = true;
                    b1x = wbishop1.x;
                    b1y = wbishop1.y;
                }
                if (wbishop2.condition == pCond.alive)
                {
                    pb2 = true;
                    b2x = wbishop2.x;
                    b2y = wbishop2.y;
                }
                if (wqueen.condition == pCond.alive)
                {
                    pq = true;
                    qx = wqueen.x;
                    qy = wqueen.y;
                }
                if (wking.condition == pCond.alive)
                {
                    pk = true;
                    kx = wking.x;
                    ky = wking.y;
                }

                if (bpawn1.condition == pCond.alive)
                {
                    opp1 = true;
                    op1x = bpawn1.x;
                    op1y = bpawn1.y;
                }
                if (bpawn2.condition == pCond.alive)
                {
                    opp2 = true;
                    op2x = bpawn2.x;
                    op2y = bpawn2.y;
                }
                if (bpawn3.condition == pCond.alive)
                {
                    opp3 = true;
                    op3x = bpawn3.x;
                    op3y = bpawn3.y;
                }
                if (bpawn4.condition == pCond.alive)
                {
                    opp4 = true;
                    op4x = bpawn4.x;
                    op4y = bpawn4.y;
                }
                if (bpawn5.condition == pCond.alive)
                {
                    opp5 = true;
                    op5x = bpawn5.x;
                    op5y = bpawn5.y;
                }
                if (bpawn6.condition == pCond.alive)
                {
                    opp6 = true;
                    op6x = bpawn6.x;
                    op6y = bpawn6.y;
                }
                if (bpawn7.condition == pCond.alive)
                {
                    opp7 = true;
                    op7x = bpawn7.x;
                    op7y = bpawn7.y;
                }
                if (bpawn8.condition == pCond.alive)
                {
                    opp8 = true;
                    op8x = bpawn8.x;
                    op8y = bpawn8.y;
                }
                if (brook1.condition == pCond.alive)
                {
                    opr1 = true;
                    or1x = brook1.x;
                    or1y = brook1.y;
                }
                if (brook2.condition == pCond.alive)
                {
                    opr2 = true;
                    or2x = brook2.x;
                    or2y = brook2.y;
                }
                if (bknight1.condition == pCond.alive)
                {
                    opkn1 = true;
                    okn1x = bknight1.x;
                    okn1y = bknight1.y;
                }
                if (bknight2.condition == pCond.alive)
                {
                    opkn2 = true;
                    okn2x = bknight2.x;
                    okn2y = bknight2.y;
                }
                if (bbishop1.condition == pCond.alive)
                {
                    opb1 = true;
                    ob1x = bbishop1.x;
                    ob1y = bbishop1.y;
                }
                if (bbishop2.condition == pCond.alive)
                {
                    opb2 = true;
                    ob2x = bbishop2.x;
                    ob2y = bbishop2.y;
                }
                if (bqueen.condition == pCond.alive)
                {
                    opq = true;
                    oqx = bqueen.x;
                    oqy = bqueen.y;
                }
                if (bking.condition == pCond.alive)
                {
                    opk = true;
                    okx = bking.x;
                    oky = bking.y;
                }

                for (int a = 0; a < difficulty; a++)
                {
                    if (wpawn1.condition == pCond.alive)
                    {

                        moved = "pawn1";

                        cMove = "pawn1";
                        showValidMoves(wpawn1.x, wpawn1.y);
                        evalMov(moved, cMove);
                    }
                    if (wpawn2.condition == pCond.alive)
                    {

                        moved = "pawn2";

                        cMove = "pawn2";
                        showValidMoves(wpawn2.x, wpawn2.y);
                        evalMov(moved, cMove);
                    }
                    if (wpawn3.condition == pCond.alive)
                    {

                        moved = "pawn3";

                        cMove = "pawn3";
                        showValidMoves(wpawn3.x, wpawn3.y);
                        evalMov(moved, cMove);
                    }
                    if (wpawn4.condition == pCond.alive)
                    {

                        moved = "pawn4";

                        cMove = "pawn4";
                        showValidMoves(wpawn4.x, wpawn4.y);
                        evalMov(moved, cMove);
                    }

                    if (wpawn5.condition == pCond.alive)
                    {

                        moved = "pawn5";

                        cMove = "pawn5";
                        showValidMoves(wpawn5.x, wpawn5.y);
                        evalMov(moved, cMove);
                    }
                    if (wpawn6.condition == pCond.alive)
                    {

                        moved = "pawn6";

                        cMove = "pawn6";
                        showValidMoves(wpawn6.x, wpawn6.y);
                        evalMov(moved, cMove);
                    }
                    if (wpawn7.condition == pCond.alive)
                    {

                        moved = "pawn7";

                        cMove = "pawn7";
                        showValidMoves(wpawn7.x, wpawn7.y);
                        evalMov(moved, cMove);
                    }
                    if (wpawn8.condition == pCond.alive)
                    {

                        moved = "pawn8";

                        cMove = "pawn8";
                        showValidMoves(wpawn8.x, wpawn8.y);
                        evalMov(moved, cMove);
                    }
                    if (wrook1.condition == pCond.alive)
                    {

                        moved = "rook1";

                        cMove = "rook1";
                        showValidMoves(wrook1.x, wrook1.y);
                        evalMov(moved, cMove);
                    }
                    if (wrook2.condition == pCond.alive)
                    {

                        moved = "rook2";

                        cMove = "rook2";
                        showValidMoves(wrook2.x, wrook2.y);
                        evalMov(moved, cMove);
                    }
                    if (wknight1.condition == pCond.alive)
                    {

                        moved = "knight1";

                        cMove = "knight1";
                        showValidMoves(wknight1.x, wknight1.y);
                        evalMov(moved, cMove);
                    }
                    if (wknight2.condition == pCond.alive)
                    {

                        moved = "knight2";

                        cMove = "knight2";
                        showValidMoves(wknight2.x, wknight2.y);
                        evalMov(moved, cMove);
                    }
                    if (wbishop1.condition == pCond.alive)
                    {

                        moved = "bishop1";

                        cMove = "bishop1";
                        showValidMoves(wbishop1.x, wbishop1.y);
                        evalMov(moved, cMove);
                    }
                    if (wbishop2.condition == pCond.alive)
                    {

                        moved = "bishop2";

                        cMove = "bishop2";
                        showValidMoves(wbishop2.x, wbishop2.y);
                        evalMov(moved, cMove);
                    }
                    if (wqueen.condition == pCond.alive)
                    {

                        moved = "queen";

                        cMove = "queen";
                        showValidMoves(wqueen.x, wqueen.y);
                        evalMov(moved, cMove);
                    }
                    if (wking.condition == pCond.alive)
                    {

                        moved = "king";

                        cMove = "king";
                        showValidMoves(wking.x, wking.y);
                        evalMov(moved, cMove);
                    }

                    if (!iterateOnce)
                    {
                        iterateOnce = true;
                        int max = 0;
                        string top = "";
                        foreach (KeyValuePair<string, string> item in moves)
                        {
                            if (Convert.ToInt32(item.Value) > max)
                            {
                                max = Convert.ToInt32(item.Value);
                                top = item.Key;
                            }
                        }

                        if (top == "pawn1")
                        {
                            movToHigh(wpawn1);
                            moved = "pawn1";
                            p1 = false;
                        }
                        else if (top == "pawn2")
                        {
                            movToHigh(wpawn2);
                            moved = "pawn2";
                            p2 = false;
                        }
                        else if (top == "pawn3")
                        {
                            movToHigh(wpawn3);
                            moved = "pawn3";
                            p3 = false;
                        }
                        else if (top == "pawn4")
                        {
                            movToHigh(wpawn4);
                            moved = "pawn4";
                            p4 = false;
                        }
                        else if (top == "pawn5")
                        {
                            movToHigh(wpawn5);
                            moved = "pawn5";
                            p5 = false;
                        }
                        else if (top == "pawn6")
                        {
                            movToHigh(wpawn6);
                            moved = "pawn6";
                            p6 = false;
                        }
                        else if (top == "pawn7")
                        {
                            movToHigh(wpawn7);
                            moved = "pawn7";
                            p7 = false;
                        }
                        else if (top == "pawn8")
                        {
                            movToHigh(wpawn8);
                            moved = "pawn8";
                            p8 = false;
                        }
                        else if (top == "knight1")
                        {
                            movToHigh(wknight1);
                            moved = "knight1";
                            kn1 = false;
                        }
                        else if (top == "knight2")
                        {
                            movToHigh(wknight2);
                            moved = "knight2";
                            kn2 = false;
                        }
                    }
                    else
                    {
                        int max = 0;
                        string top = "";
                        foreach (KeyValuePair<string, string> item in curMoves)
                        {
                            if (Convert.ToInt32(item.Value) > max)
                            {
                                max = Convert.ToInt32(item.Value);
                                top = item.Key;
                            }
                        }

                        if (top == "pawn1")
                        {
                            movToHigh(wpawn1);
                        }
                        else if (top == "pawn2")
                        {
                            movToHigh(wpawn2);
                        }
                        else if (top == "pawn3")
                        {
                            movToHigh(wpawn3);
                        }
                        else if (top == "pawn4")
                        {
                            movToHigh(wpawn4);
                        }
                        else if (top == "pawn5")
                        {
                            movToHigh(wpawn5);
                        }
                        else if (top == "pawn6")
                        {
                            movToHigh(wpawn6);
                        }
                        else if (top == "pawn7")
                        {
                            movToHigh(wpawn7);
                        }
                        else if (top == "pawn8")
                        {
                            movToHigh(wpawn8);
                        }
                        else if (top == "rook1")
                        {
                            movToHigh(wrook1);
                        }
                        else if (top == "rook2")
                        {
                            movToHigh(wrook2);
                        }
                        else if (top == "knight1")
                        {
                            movToHigh(wknight1);
                        }
                        else if (top == "knight2")
                        {
                            movToHigh(wknight2);
                        }
                        else if (top == "bishop1")
                        {
                            movToHigh(wbishop1);
                        }
                        else if (top == "bishop2")
                        {
                            movToHigh(wbishop2);
                        }
                        else if (top == "king")
                        {
                            movToHigh(wking);
                        }
                        else if (top == "queen")
                        {
                            movToHigh(wqueen);
                        }
                    }
                }

                // -- return pieces -- 
                color_panel();
                if (pp1)
                {
                    sqs[p1x, p1y].occupied = wpawn1;
                    wpawn1.x = p1x; wpawn1.y = p1y;
                    CPanels[p1x, p1y].BackgroundImage = Properties.Resources.white_pawn;
                    wpawn1.condition = pCond.alive;
                }
                if (pp2)
                {
                    sqs[p2x, p2y].occupied = wpawn2;
                    wpawn2.x = p2x; wpawn2.y = p2y;
                    CPanels[p2x, p2y].BackgroundImage = Properties.Resources.white_pawn;
                    wpawn2.condition = pCond.alive;
                }
                if (pp3)
                {
                    sqs[p3x, p3y].occupied = wpawn3;
                    wpawn3.x = p3x; wpawn3.y = p3y;
                    CPanels[p3x, p3y].BackgroundImage = Properties.Resources.white_pawn;
                    wpawn3.condition = pCond.alive;
                }
                if (pp4)
                {
                    sqs[p4x, p4y].occupied = wpawn4;
                    wpawn4.x = p4x; wpawn4.y = p4y;
                    CPanels[p4x, p4y].BackgroundImage = Properties.Resources.white_pawn;
                    wpawn4.condition = pCond.alive;
                }
                if (pp5)
                {
                    sqs[p5x, p5y].occupied = wpawn5;
                    wpawn5.x = p5x; wpawn5.y = p5y;
                    CPanels[p5x, p5y].BackgroundImage = Properties.Resources.white_pawn;
                    wpawn5.condition = pCond.alive;
                }
                if (pp6)
                {
                    sqs[p6x, p6y].occupied = wpawn6;
                    wpawn6.x = p6x; wpawn6.y = p6y;
                    CPanels[p6x, p6y].BackgroundImage = Properties.Resources.white_pawn;
                    wpawn6.condition = pCond.alive;
                }
                if (pp7)
                {
                    sqs[p7x, p7y].occupied = wpawn7;
                    wpawn7.x = p7x; wpawn7.y = p7y;
                    CPanels[p7x, p7y].BackgroundImage = Properties.Resources.white_pawn;
                    wpawn7.condition = pCond.alive;
                }
                if (pp8)
                {
                    sqs[p8x, p8y].occupied = wpawn8;
                    wpawn8.x = p8x; wpawn8.y = p8y;
                    CPanels[p8x, p8y].BackgroundImage = Properties.Resources.white_pawn;
                    wpawn8.condition = pCond.alive;
                }
                if (pr1)
                {
                    sqs[r1x, r1y].occupied = wrook1;
                    wrook1.x = r1x; wrook1.y = r1y;
                    CPanels[r1x, r1y].BackgroundImage = Properties.Resources.white_rook;
                    wrook1.condition = pCond.alive;
                }
                if (pr2)
                {
                    sqs[r2x, r2y].occupied = wrook2;
                    wrook2.x = r2x; wrook2.y = r2y;
                    CPanels[r2x, r2y].BackgroundImage = Properties.Resources.white_rook;
                    wrook2.condition = pCond.alive;
                }
                if (pkn1)
                {
                    sqs[kn1x, kn1y].occupied = wknight1;
                    wknight1.x = kn1x; wknight1.y = kn1y;
                    CPanels[kn1x, kn1y].BackgroundImage = Properties.Resources.white_knight;
                    wknight1.condition = pCond.alive;
                }
                if (pkn2)
                {
                    sqs[kn2x, kn2y].occupied = wknight2;
                    wknight2.x = kn2x; wknight2.y = kn2y;
                    CPanels[kn2x, kn2y].BackgroundImage = Properties.Resources.white_knight;
                    wknight2.condition = pCond.alive;
                }
                if (pb1)
                {
                    sqs[b1x, b1y].occupied = wbishop1;
                    wbishop1.x = b1x; wbishop1.y = b1y;
                    CPanels[b1x, b1y].BackgroundImage = Properties.Resources.white_bishop;
                    wbishop1.condition = pCond.alive;
                }
                if (pb2)
                {
                    sqs[b2x, b2y].occupied = wbishop2;
                    wbishop2.x = b2x; wbishop2.y = b2y;
                    CPanels[b2x, b2y].BackgroundImage = Properties.Resources.white_bishop;
                    wbishop2.condition = pCond.alive;
                }
                if (pk)
                {
                    sqs[kx, ky].occupied = wking;
                    wking.x = kx; wking.y = ky;
                    CPanels[kx, ky].BackgroundImage = Properties.Resources.white_king;
                    wking.condition = pCond.alive;
                }
                if (pq)
                {
                    sqs[qx, qy].occupied = wqueen;
                    wqueen.x = qx; wqueen.y = qy;
                    CPanels[qx, qy].BackgroundImage = Properties.Resources.white_queen;
                    wqueen.condition = pCond.alive;
                }
                //other side (user's side)
                if (opp1)
                {
                    sqs[op1x, op1y].occupied = bpawn1;
                    bpawn1.x = op1x; bpawn1.y = op1y;
                    CPanels[op1x, op1y].BackgroundImage = Properties.Resources.black_pawn;
                    bpawn1.condition = pCond.alive;
                }
                if (opp2)
                {
                    sqs[op2x, op2y].occupied = bpawn2;
                    bpawn2.x = op2x; bpawn2.y = op2y;
                    CPanels[op2x, op2y].BackgroundImage = Properties.Resources.black_pawn;
                    bpawn2.condition = pCond.alive;
                }
                if (opp3)
                {
                    sqs[op3x, op3y].occupied = bpawn3;
                    bpawn3.x = op3x; bpawn3.y = op3y;
                    CPanels[op3x, op3y].BackgroundImage = Properties.Resources.black_pawn;
                    bpawn3.condition = pCond.alive;
                }
                if (opp4)
                {
                    sqs[op4x, op4y].occupied = bpawn4;
                    bpawn4.x = op4x; bpawn4.y = op4y;
                    CPanels[op4x, op4y].BackgroundImage = Properties.Resources.black_pawn;
                    bpawn4.condition = pCond.alive;
                }
                if (opp5)
                {
                    sqs[op5x, op5y].occupied = bpawn5;
                    bpawn5.x = op5x; bpawn5.y = op5y;
                    CPanels[op5x, op5y].BackgroundImage = Properties.Resources.black_pawn;
                    bpawn5.condition = pCond.alive;
                }
                if (opp6)
                {
                    sqs[op6x, op6y].occupied = bpawn6;
                    bpawn6.x = op6x; bpawn6.y = op6y;
                    CPanels[op6x, op6y].BackgroundImage = Properties.Resources.black_pawn;
                    bpawn6.condition = pCond.alive;
                }
                if (opp7)
                {
                    sqs[op7x, op7y].occupied = bpawn7;
                    bpawn7.x = op7x; bpawn7.y = op7y;
                    CPanels[op7x, op7y].BackgroundImage = Properties.Resources.black_pawn;
                    bpawn7.condition = pCond.alive;
                }
                if (opp8)
                {
                    sqs[op8x, op8y].occupied = bpawn8;
                    bpawn8.x = op8x; bpawn8.y = op8y;
                    CPanels[op8x, op8y].BackgroundImage = Properties.Resources.black_pawn;
                    bpawn8.condition = pCond.alive;
                }
                if (opr1)
                {
                    sqs[or1x, or1y].occupied = brook1;
                    brook1.x = or1x; brook1.y = or1y;
                    CPanels[or1x, or1y].BackgroundImage = Properties.Resources.black_rook;
                    brook1.condition = pCond.alive;
                }
                if (opr2)
                {
                    sqs[or2x, or2y].occupied = brook2;
                    brook2.x = or2x; brook2.y = or2y;
                    CPanels[or2x, or2y].BackgroundImage = Properties.Resources.black_rook;
                    brook2.condition = pCond.alive;
                }
                if (opkn1)
                {
                    sqs[okn1x, okn1y].occupied = bknight1;
                    bknight1.x = okn1x; bknight1.y = okn1y;
                    CPanels[okn1x, okn1y].BackgroundImage = Properties.Resources.black_knight;
                    bknight1.condition = pCond.alive;
                }
                if (opkn2)
                {
                    sqs[okn2x, okn2y].occupied = bknight2;
                    bknight2.x = okn2x; bknight2.y = okn2y;
                    CPanels[okn2x, okn2y].BackgroundImage = Properties.Resources.black_knight;
                    bknight2.condition = pCond.alive;
                }
                if (opb1)
                {
                    sqs[ob1x, ob1y].occupied = bbishop1;
                    bbishop1.x = ob1x; bbishop1.y = ob1y;
                    //error occurs between here --
                    CPanels[ob1x, ob1y].BackgroundImage = Properties.Resources.black_bishop;
                    //and here --
                    bbishop1.condition = pCond.alive;
                }
                if (opb2)
                {
                    sqs[ob2x, ob2y].occupied = bbishop2;
                    bbishop2.x = ob2x; bbishop2.y = ob2y;
                    CPanels[ob2x, ob2y].BackgroundImage = Properties.Resources.black_bishop;
                    bbishop2.condition = pCond.alive;
                }
                if (opk)
                {
                    sqs[okx, oky].occupied = bking;
                    bking.x = okx; bking.y = oky;
                    CPanels[okx, oky].BackgroundImage = Properties.Resources.black_king;
                    bking.condition = pCond.alive;
                }
                if (opq)
                {
                    sqs[oqx, oqy].occupied = bqueen;
                    bqueen.x = oqx; bqueen.y = oqy;
                    CPanels[oqx, oqy].BackgroundImage = Properties.Resources.black_queen;
                    bqueen.condition = pCond.alive;
                }

                //final move errors
                //not working --  1. poor computer move choices (doesn't look too far into future) 
                //            --  2. re-appear bug after comp attacks and user moves again
                //solution    --  check how far ahead for loop searches
                //            --  check what happens when piece attacks, moves, or is returned
                //            --  continue optimizing

                // -- finalMov here --
                red = false;
                if (p1)
                {
                    moved = "pawn1";
                    p1 = false;
                    goto star;
                }
                else if (p2)
                {
                    moved = "pawn2";
                    p2 = false;
                    goto star;
                }
                else if (p3)
                {
                    moved = "pawn3";
                    p3 = false;
                    goto star;
                }
                else if (p4)
                {
                    moved = "pawn4";
                    p4 = false;
                    goto star;
                }
                else if (p5)
                {
                    moved = "pawn5";
                    p5 = false;
                    goto star;
                }
                else if (p6)
                {
                    moved = "pawn6";
                    p6 = false;
                    goto star;
                }
                else if (p7)
                {
                    moved = "pawn7";
                    p7 = false;
                    goto star;
                }
                else if (p8)
                {
                    moved = "pawn8";
                    p8 = false;
                    goto star;
                }
                else if (r1)
                {
                    moved = "rook1";
                    r1 = false;
                    goto star;
                }
                else if (r2)
                {
                    moved = "rook2";
                    r2 = false;
                    goto star;
                }
                else if (kn1)
                {
                    moved = "knight1";
                    kn1 = false;
                    goto star;
                }
                else if (kn2)
                {
                    moved = "knight2";
                    kn2 = false;
                    goto star;
                }
                else if (b1)
                {
                    moved = "bishop1";
                    b1 = false;
                    goto star;
                }
                else if (b2)
                {
                    moved = "bishop2";
                    b2 = false;
                    goto star;
                }
                else if (k)
                {
                    moved = "king";
                    k = false;
                    goto star;
                }
                else if (q)
                {
                    moved = "queen";
                    q = false;
                    goto star;
                }
                else
                {
                    int max = 0;
                    string top = "";
                    if (!firstTurn)
                    {
                        foreach (KeyValuePair<string, string> item in moves)
                        {
                            listView1.Items.Add(item.Key + " " + item.Value + Environment.NewLine);
                            if (Convert.ToInt32(item.Value) > max)
                            {
                                max = Convert.ToInt32(item.Value);
                                top = item.Key;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("fturn");
                        firstTurn = false;
                        foreach (KeyValuePair<string, string> item in moves)
                        {
                            listView1.Items.Add(item.Key + " " + item.Value + Environment.NewLine);
                            if (Convert.ToInt32(item.Value) > max && item.Key != "rook1" && item.Key != "rook2" && item.Key != "bishop1" && item.Key != "bishop2" && item.Key != "king" && item.Key != "queen")
                            {
                                max = Convert.ToInt32(item.Value);
                                top = item.Key;
                            }
                        }
                    }

                    if (top == "pawn1")
                    {
                        evaltohigh(wpawn1);
                        retPiece = wpawn1;
                    }
                    else if (top == "pawn2")
                    {
                        evaltohigh(wpawn2);
                        retPiece = wpawn2;
                    }
                    else if (top == "pawn3")
                    {
                        evaltohigh(wpawn3);
                        retPiece = wpawn3;
                    }
                    else if (top == "pawn4")
                    {
                        evaltohigh(wpawn4);
                        retPiece = wpawn4;
                    }
                    else if (top == "pawn5")
                    {
                        evaltohigh(wpawn5);
                        retPiece = wpawn5;
                    }
                    else if (top == "pawn6")
                    {
                        evaltohigh(wpawn6);
                        retPiece = wpawn6;
                    }
                    else if (top == "pawn7")
                    {
                        evaltohigh(wpawn7);
                        retPiece = wpawn7;
                    }
                    else if (top == "pawn8")
                    {
                        evaltohigh(wpawn8);
                        retPiece = wpawn8;
                    }
                    else if (top == "rook1")
                    {
                        evaltohigh(wrook1);
                        retPiece = wrook1;
                    }
                    else if (top == "rook2")
                    {
                        evaltohigh(wrook2);
                        retPiece = wrook2;
                    }
                    else if (top == "knight1")
                    {
                        evaltohigh(wknight1);
                        retPiece = wknight1;
                    }
                    else if (top == "knight2")
                    {
                        evaltohigh(wknight2);
                        retPiece = wknight2;
                    }
                    else if (top == "bishop1")
                    {
                        evaltohigh(wbishop1);
                        retPiece = wbishop1;
                    }
                    else if (top == "bishop2")
                    {
                        evaltohigh(wbishop2);
                        retPiece = wbishop2;
                    }
                    else if (top == "king")
                    {
                        evaltohigh(wking);
                        retPiece = wking;
                    }
                    else if (top == "queen")
                    {
                        evaltohigh(wqueen);
                        retPiece = wqueen;
                    }
                }

            }
            else
            {
                if (wpawn1.condition == pCond.alive)
                {
                    moves.Add("pawn1", "0");
                }
                if (wpawn2.condition == pCond.alive)
                {
                    moves.Add("pawn2", "0");
                }
                if (wpawn3.condition == pCond.alive)
                {
                    moves.Add("pawn3", "0");
                }
                if (wpawn4.condition == pCond.alive)
                {
                    moves.Add("pawn4", "0");
                }
                if (wpawn5.condition == pCond.alive)
                {
                    moves.Add("pawn5", "0");
                }
                if (wpawn6.condition == pCond.alive)
                {
                    moves.Add("pawn6", "0");
                }
                if (wpawn7.condition == pCond.alive)
                {
                    moves.Add("pawn7", "0");
                }
                if (wpawn8.condition == pCond.alive)
                {
                    moves.Add("pawn8", "0");
                }
                if (wrook1.condition == pCond.alive)
                {
                    moves.Add("rook1", "0");
                }
                if (wrook2.condition == pCond.alive)
                {
                    moves.Add("rook2", "0");
                }
                if (wknight1.condition == pCond.alive)
                {
                    moves.Add("knight1", "0");
                }
                if (wknight2.condition == pCond.alive)
                {
                    moves.Add("knight2", "0");
                }
                if (wbishop1.condition == pCond.alive)
                {
                    moves.Add("bishop1", "0");
                }
                if (wbishop2.condition == pCond.alive)
                {
                    moves.Add("bishop2", "0");
                }
                if (wqueen.condition == pCond.alive)
                {
                    moves.Add("queen", "0");
                }
                if (wking.condition == pCond.alive)
                {
                    moves.Add("king", "0");
                }

                for (int a = 0; a < difficulty; a++)
                {
                    if (bpawn1.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "pawn1";
                        }
                        p1 = true;
                        p1x = bpawn1.x;
                        p1y = bpawn1.y;
                        showValidMoves(bpawn1.x, bpawn1.y);
                        evalMov(moved, cMove);
                    }
                    if (bpawn2.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "pawn2";
                        }
                        p2 = true;
                        p2x = bpawn2.x;
                        p2y = bpawn2.y;
                        showValidMoves(bpawn2.x, bpawn2.y);
                        evalMov(moved, cMove);
                    }
                    if (bpawn3.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "pawn3";
                        }
                        p3 = true;
                        p3x = bpawn3.x;
                        p3y = bpawn3.y;
                        showValidMoves(bpawn3.x, bpawn3.y);
                        evalMov(moved, cMove);
                    }
                    if (bpawn4.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "pawn4";
                        }
                        p4 = true;
                        p4x = bpawn4.x;
                        p4y = bpawn4.y;
                        showValidMoves(bpawn4.x, bpawn4.y);
                        evalMov(moved, cMove);
                    }

                    if (bpawn5.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "pawn5";
                        }
                        p5 = true;
                        p5x = bpawn5.x;
                        p5y = bpawn5.y;
                        showValidMoves(bpawn5.x, bpawn5.y);
                        evalMov(moved, cMove);
                    }
                    if (bpawn6.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "pawn6";
                        }
                        p6 = true;
                        p6x = bpawn6.x;
                        p6y = bpawn6.y;
                        showValidMoves(bpawn6.x, bpawn6.y);
                        evalMov(moved, cMove);
                    }
                    if (bpawn7.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "pawn7";
                        }
                        p7 = true;
                        p7x = bpawn7.x;
                        p7y = bpawn7.y;
                        showValidMoves(bpawn7.x, bpawn7.y);
                        evalMov(moved, cMove);
                    }
                    if (bpawn8.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "pawn8";
                        }
                        p8 = true;
                        p8x = bpawn8.x;
                        p8y = bpawn8.y;
                        showValidMoves(bpawn8.x, bpawn8.y);
                        evalMov(moved, cMove);
                    }
                    if (brook1.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "rook1";
                        }
                        r1 = true;
                        r1x = brook1.x;
                        r1y = brook1.y;
                        showValidMoves(brook1.x, brook1.y);
                        evalMov(moved, cMove);
                    }
                    if (brook2.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "rook2";
                        }
                        r2 = true;
                        r2x = brook2.x;
                        r2y = brook2.y;
                        showValidMoves(brook2.x, brook2.y);
                        evalMov(moved, cMove);
                    }
                    if (bknight1.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "knight1";
                        }
                        kn1 = true;
                        kn1x = bknight1.x;
                        kn1y = bknight1.y;
                        showValidMoves(bknight1.x, bknight1.y);
                        evalMov(moved, cMove);
                    }
                    if (bknight2.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "knight2";
                        }
                        kn2 = true;
                        kn2x = bknight2.x;
                        kn2y = bknight2.y;
                        showValidMoves(bknight2.x, bknight2.y);
                        evalMov(moved, cMove);
                    }
                    if (bbishop1.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "bishop1";
                        }
                        b1 = true;
                        b1x = bbishop1.x;
                        b1y = bbishop1.y;
                        showValidMoves(bbishop1.x, bbishop1.y);
                        evalMov(moved, cMove);
                    }
                    if (bbishop2.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "bishop2";
                        }
                        b2 = true;
                        b2x = bbishop2.x;
                        b2y = bbishop2.y;
                        showValidMoves(bbishop2.x, bbishop2.y);
                        evalMov(moved, cMove);
                    }
                    if (bqueen.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "queen";
                        }
                        q = true;
                        qx = bqueen.x;
                        qy = bqueen.y;
                        showValidMoves(bqueen.x, bqueen.y);
                        evalMov(moved, cMove);
                    }
                    if (bking.condition == pCond.alive)
                    {
                        if (!red)
                        {
                            moved = "king";
                        }
                        k = true;
                        kx = bpawn1.x;
                        ky = bpawn1.y;
                        showValidMoves(bking.x, bking.y);
                        evalMov(moved, cMove);
                    }

                    //decide move (not final)
                    if (!red)
                    {
                        red = true;

                    }
                    else
                    {

                    }
                }
            }

            jl = "LL" + tX.ToString() + ", " + tY.ToString() + "  " + retPiece.pType.ToString() + " \n " + sqs[0, 1].occupied.pType + "  " + answer + "  " + bbishop1.condition.ToString() + "\n ";
            color_panel();
            return retPiece;
        }

        private void evaltohigh(piece pce)
        {
            List<int> points = new List<int>();
            List<int> xs = new List<int>();
            List<int> ys = new List<int>();

            showValidMoves(pce.x, pce.y);
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (CPanels[x, y].BackColor == Color.DeepPink)
                    {
                        if (sqs[x, y].occupied.pType == pieceTypes.pawn)
                        {
                            points.Add(40);
                            xs.Add(x);
                            ys.Add(y);
                        }
                        else if (sqs[x, y].occupied.pType == pieceTypes.rook)
                        {
                            points.Add(100);
                            xs.Add(x);
                            ys.Add(y);
                        }
                        else if (sqs[x, y].occupied.pType == pieceTypes.knight)
                        {
                            points.Add(80);
                            xs.Add(x);
                            ys.Add(y);
                        }
                        else if (sqs[x, y].occupied.pType == pieceTypes.bishop)
                        {
                            points.Add(100);
                            xs.Add(x);
                            ys.Add(y);
                        }
                        else if (sqs[x, y].occupied.pType == pieceTypes.queen)
                        {
                            points.Add(180);
                            xs.Add(x);
                            ys.Add(y);
                        }
                        else if (sqs[x, y].occupied.pType == pieceTypes.king)
                        {
                            points.Add(500);
                            xs.Add(x);
                            ys.Add(y);
                        }
                    }
                    else if (CPanels[x, y].BackColor == Color.LightBlue)
                    {
                        points.Add(10);
                        xs.Add(x);
                        ys.Add(y);
                    }
                }

                int max = 0;
                int hx = 0;
                int hy = 0;
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i] > max)
                    {
                        max = points[i];
                        hx = xs[i];
                        hy = ys[i];
                    }
                }

                tX = hx;
                tY = hy;
            }
        }

        private void evalMov(string moved, string cMove)
        {
            int xx = Convert.ToInt32(moves[moved]) + 10;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (CPanels[x, y].BackColor == Color.DeepPink)
                    {
                        piece pce = sqs[x, y].occupied;
                        int val = Convert.ToInt32(moves[moved]);
                        int cVal = Convert.ToInt32(curMoves[cMove]);
                        if (pce.pType == pieceTypes.pawn)
                        {
                            moves[moved] = (val + 40).ToString();
                            curMoves[cMove] = (cVal + 40).ToString();
                        }
                        else if (pce.pType == pieceTypes.rook)
                        {
                            moves[moved] = (val + 100).ToString();
                            curMoves[cMove] = (cVal + 100).ToString();
                        }
                        else if (pce.pType == pieceTypes.knight)
                        {
                            moves[moved] = (val + 80).ToString();
                            curMoves[cMove] = (cVal + 80).ToString();
                        }
                        else if (pce.pType == pieceTypes.bishop)
                        {
                            moves[moved] = (val + 100).ToString();
                            curMoves[cMove] = (cVal + 100).ToString();
                        }
                        else if (pce.pType == pieceTypes.queen)
                        {
                            moves[moved] = (val + 180).ToString();
                            curMoves[cMove] = (cVal + 180).ToString();
                        }
                        else if (pce.pType == pieceTypes.king)
                        {
                            moves[moved] = (val + 500).ToString();
                            curMoves[cMove] = (cVal + 500).ToString();
                        }
                    }
                    else if (CPanels[x, y].BackColor == Color.LightBlue)
                    {
                        //check for blue spaces
                        moves[moved] = xx.ToString();
                        curMoves[cMove] = xx.ToString();
                        xx++;
                    }
                }
            }
        }

        private void movToHigh(piece pce)
        {
            List<int> points = new List<int>();
            List<int> xs = new List<int>();
            List<int> ys = new List<int>();

            showValidMoves(pce.x, pce.y);
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (sqs[x, y].occupied.pType == pieceTypes.pawn)
                    {
                        points.Add(40);
                        xs.Add(x);
                        ys.Add(y);
                    }
                    else if (sqs[x, y].occupied.pType == pieceTypes.rook)
                    {
                        points.Add(100);
                        xs.Add(x);
                        ys.Add(y);
                    }
                    else if (sqs[x, y].occupied.pType == pieceTypes.knight)
                    {
                        points.Add(80);
                        xs.Add(x);
                        ys.Add(y);
                    }
                    else if (sqs[x, y].occupied.pType == pieceTypes.bishop)
                    {
                        points.Add(100);
                        xs.Add(x);
                        ys.Add(y);
                    }
                    else if (sqs[x, y].occupied.pType == pieceTypes.queen)
                    {
                        points.Add(180);
                        xs.Add(x);
                        ys.Add(y);
                    }
                    else if (sqs[x, y].occupied.pType == pieceTypes.king)
                    {
                        points.Add(500);
                        xs.Add(x);
                        ys.Add(y);
                    }
                    else
                    {
                        points.Add(10);
                        xs.Add(x);
                        ys.Add(y);
                    }
                }

                int max = 0;
                int hx = 0;
                int hy = 0;
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i] > max)
                    {
                        max = points[i];
                        hx = xs[i];
                        hy = ys[i];
                    }
                }

                if (checkForNone(sqs[hx, hy].occupied) && sqs[hx, hy].occupied.pCol != color)
                {
                    moveto(hx, hy, pce);
                }
                else if (sqs[hx, hy].occupied.pCol != color)
                {
                    attack(hx, hy, pce, true);
                }
            }
        }

    }
}
