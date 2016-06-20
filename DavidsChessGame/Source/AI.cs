/*
AI logic
 Notice:
  1. when using move() with AI tests, note that it will only edit squares
  2. to use showValidMoves(), first paramater is from opponentPieces. Just do : if (square[].name == opponentPieces[a].name){
                                                                                       Piece = opponemtPieces[a];
                                                                                }
 For chess AI we could create and maintain a list of calidmoves for each opponent piece instead of
 multiple nested loops. Also, may have to examine user pieces to see if any can attack the opponent piece and subtract
 from the profitability of moving that piece.
 
 * Here is a basic logic tree of the engine :
                                                ***LOGIC TREE***
                                          First Move (cycle through every move)
                                         Second Move (Cycle through all pieces again)
                              Profitable(make second move)          Not Profitable(Stop tree)
                             Third Move(cycle through all)
 *                    Profitable(continue)    Not Profitable()
                                          etc.
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
        private void compMove()
        {
            //snapshot and worker square[8, 8] arrays
            board brd = new board();
            Array.Copy(game.squares, brd.snapshot, game.squares.Length);

            Array.Copy(game.squares, brd.worker, game.squares.Length);

            //blank moveBoard
            MoveBoard[,] movboard = new MoveBoard[8, 8];

            //blank panels
            Panel[,] pans = new Panel[8, 8];

            //Piece being evaled
            Piece currEval = new Piece();

            //dictionary that stores points that each piece has in algorithm
            Dictionary<Piece, int> hits = new Dictionary<Piece, int>();

            //dictionary that temporarily stores the max points a Piece has, used for deciding which move a piece will make
            Dictionary<Piece, int> tempHits = new Dictionary<Piece, int>();

            //dictionary that temporarily stores the max points a Piece has, used for deciding best move to make after initial piece move
            Dictionary<Piece, int> tempHits_2 = new Dictionary<Piece, int>();

            //populate dictionary
            foreach (Piece pce in game.opponentPieces)
            {
                if (pce.alive)
                {
                    hits.Add(pce, 0);
                    tempHits.Add(pce, 0);
                }
            }

            //how far to look ahead, based on difficulty
            int limiter = 0;
            if (game.difficulty == "easy")
            {
                limiter = 20;
            }else if (game.difficulty == "medium")
            {
                limiter = 30;
            }
            else 
            {
                limiter = 40;
            }

            foreach (Piece pce in game.opponentPieces)
            {
                /*Here we start by making every piece have a shot at moving first, the one that allows the best future moves,
                will be selected and moved. I know it's primitive, but we're not trying to outdo HAL! */

                Array.Copy(brd.snapshot, brd.worker, brd.snapshot.Length);
                Array.Clear(movboard, 0, movboard.Length);

                currEval = pce;

                bool isCheck = false;

                if (pce.alive)
                {
                    //first move
                    showValidMoves(pce, brd.worker, ref movboard, ref pans);

                    //now cycle through each potetial move of each potential piece we could move
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            if (movboard[x, y].moveable)
                            {
                                //now move to each movable slot
                                move(pce, x, y, false, ref brd.worker, out isCheck);
                                if (!isCheck)
                                {
                                    //add points to piece since it was able to be moved, helps AI break check situation
                                    hits[pce] += 10;

                                    //now we dig deeper
                                    for (int a = 0; a < limiter; a++)
                                    {
                                        //run through pieces, the one that has most move options is chosen
                                        //________________________________________________________________
                                        // NOTE : IN FUTURE, OPTIMIZE THIS SECTION FOR BETTER CRITERIA!

                                        foreach (Piece nextPce in game.opponentPieces)
                                        {
                                            showValidMoves(nextPce, brd.worker, ref movboard, ref pans);
                                            for (int p = 0; p < 8; p++)
                                            {
                                                for (int q = 0; q < 8; q++)
                                                {
                                                    if (movboard[p, q].moveable)
                                                    {
                                                        if (brd.worker[p, q].name != null)
                                                        {
                                                            hits[pce] += 40;
                                                            tempHits_2[nextPce] += 40;
                                                        }
                                                        hits[pce] += 10;
                                                        tempHits_2[nextPce] += 10;
                                                    }
                                                }
                                            }
                                        }

                                        //make move
                                        Piece holdr = new Piece();
                                        int max = 0;
                                        foreach (KeyValuePair<Piece, int> temp in tempHits_2)
                                        {
                                            if (temp.Value > max)
                                            {
                                                holdr = temp.Key;
                                                max = temp.Value;
                                            }
                                        }

                                        //continue here, we made first move, now we found most profitable next move
                                        //finding moves that have holdr above average could be a better way of identing profitability
                                        //challenge : pick where holdr (most profitable piece moves to)

                                        foreach (Piece pice in game.opponentPieces)
                                        {
                                            if (pice.name == holdr.name)
                                            {
                                                //move most profitable second piece

                                            }
                                        }

                                    }

                                }
                            }
                        }

                    }

                }
            }
        }

    }
}
