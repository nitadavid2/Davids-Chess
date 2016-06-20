/*
Piece change bug appears to be in AI code
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
        public ChessHost()
        {
            InitializeComponent();
        }

        private Panel[,] CPanels;
        List<string> deletedPieces = new List<string>();
        string winner = "";
        bool userMove = true;

        private void ChessHost_Load(object sender, EventArgs e)//on load init board pieces
        {
            int sqSize = 80;
            int bAcross = 8;
            int[] padd = {300, 25};

            CPanels = new Panel[bAcross, bAcross]; //8 * 8 grid

            for (int y = 0; y < bAcross; y++)
            {
                for (int x = 0; x < bAcross; x++)
                {
                    var newPan = new Panel
                    {
                        Size = new Size(sqSize, sqSize),
                        Location = new Point(x * sqSize + padd[0], y * sqSize + padd[1])
                    };

                    Controls.Add(newPan);
                    CPanels[x, y] = newPan; //add to correct location on board and to index of CPanels
                    CPanels[x, y].Click += Pan_Click;

                    var colW = Color.White;
                    var colB = Color.DarkGray;
                    //color piece
                    if (y % 2 == 0)
                    {
                        newPan.BackColor = x % 2 != 0 ? colB : colW;
                    }
                    else
                    {
                        newPan.BackColor = x % 2 != 0 ? colW : colB;
                    }
                }
            }
            /* CPanels[2 - 1, 3 - 1] is method to access a panel in CPanels array 
               CPanels[1, 2].BackgroundImage.ToString(); */
        }

        //--variables
        int tX;
        int tY;
        int difficulty;
        string jl; //message box test data for comp moves
        //values needed to return pieces back to pre-computer eval positions
        int p1x, p2x, p3x, p4x, p5x, p6x, p7x, p8x, r1x, r2x, kn1x, kn2x, b1x, b2x, kx, qx;
        int p1y, p2y, p3y, p4y, p5y, p6y, p7y, p8y, r1y, r2y, kn1y, kn2y, b1y, b2y, ky, qy;
        int op1x, op2x, op3x, op4x, op5x, op6x, op7x, op8x, or1x, or2x, okn1x, okn2x, ob1x, ob2x, okx, oqx;
        int op1y, op2y, op3y, op4y, op5y, op6y, op7y, op8y, or1y, or2y, okn1y, okn2y, ob1y, ob2y, oky, oqy;
        bool p1, p2, p3, p4, p5, p6, p7, p8, r1, r2, kn1, kn2, b1, b2, k, q;
        bool pp1, pp2, pp3, pp4, pp5, pp6, pp7, pp8, pr1, pr2, pkn1, pkn2, pb1, pb2, pk, pq;
        bool opp1, opp2, opp3, opp4, opp5, opp6, opp7, opp8, opr1, opr2, opkn1, opkn2, opb1, opb2, opk, opq;
        bool red = false;
        bool firstTurn = true;
		bool iterateOnce = false;

        //--initialize dictionary for lookup
        Dictionary<string, string> moves = new Dictionary<string, string>();
        Dictionary<string, string> curMoves = new Dictionary<string,string>();

        //-- implement necessary classes, structs, and enums here --
        public enum pieceTypes
        {
            none, pawn, knight, bishop, rook, queen, king
        }

        public enum pieceCol
        {
            white, black
        }

        public enum pCond
        {
            alive, dead
        }

        public struct piece
        {
            public pieceTypes pType { get; set; }
            public pieceCol pCol { get; set; }
            public int x { get; set; }
            public int y { get; set; }
            public pCond condition { get; set; }
        }

        public struct squares
        {
            public piece occupied { get; set; }
        }

        public struct game
        {
            public pieceCol col { get; set; }
            public int difficulty { get; set; }
        }

    }
}
