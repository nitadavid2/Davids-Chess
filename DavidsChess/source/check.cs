/*
Logic for checking if a player is in check or checkmate
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

        private string isCheck()
        {
            //clean, check, or checkmate on return
            if (color == pieceCol.white)
            {
                int kx = wking.x;
                int ky = wking.y;

                showValidMoves(kx, ky);
                List<string> kmoves = new List<string>();

                kmoves.Add("center");

                if (kx + 1 < 7)
                {
                    if (CPanels[kx + 1, ky].BackColor == Color.DeepPink || CPanels[kx + 1, ky].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("Right");
                    }
                }
                if (kx + 1 < 7 && ky + 1 < 7)
                {
                    if (CPanels[kx + 1, ky + 1].BackColor == Color.DeepPink || CPanels[kx + 1, ky + 1].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("BottomRight");
                    }
                }
                if (kx + 1 < 7 && ky - 1 > -1)
                {
                    if (CPanels[kx + 1, ky - 1].BackColor == Color.DeepPink || CPanels[kx + 1, ky - 1].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("TopRight");
                    }
                }
                if (ky - 1 > -1)
                {
                    if (CPanels[kx, ky - 1].BackColor == Color.DeepPink || CPanels[kx, ky - 1].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("Top");
                    }
                }
                if (ky + 1 < 7)
                {
                    if (CPanels[kx, ky + 1].BackColor == Color.DeepPink || CPanels[kx, ky + 1].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("Bottom");
                    }
                }
                if (kx - 1 > -1)
                {
                    if (CPanels[kx - 1, ky].BackColor == Color.DeepPink || CPanels[kx + 1, ky].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("Left");
                    }
                }
                if (kx - 1 > -1 && ky + 1 < 7)
                {
                    if (CPanels[kx - 1, ky + 1].BackColor == Color.DeepPink || CPanels[kx - 1, ky + 1].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("BottomLeft");
                    }
                }
                if (kx - 1 > -1 && ky - 1 > -1)
                {
                    if (CPanels[kx - 1, ky - 1].BackColor == Color.DeepPink || CPanels[kx - 1, ky - 1].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("TopLeft");
                    }
                }

                bool[] check = new bool[kmoves.Count()];
                int a = 0;

                foreach (var litem in kmoves)
                {
                    kx = wking.x;
                    ky = wking.y;

                    if (litem == "Right")
                    {
                        kx += 1;
                    }
                    else if (litem == "BottomRight")
                    {
                        kx += 1;
                        ky += 1;
                    }
                    else if (litem == "TopRight")
                    {
                        kx += 1;
                        ky -= 1;
                    }
                    else if (litem == "Top")
                    {
                        ky -= 1;
                    }
                    else if (litem == "Bottom")
                    {
                        ky += 1;
                    }
                    else if (litem == "Left")
                    {
                        kx -= 1;
                    }
                    else if (litem == "TopLeft")
                    {
                        kx -= 1;
                        ky -= 1;
                    }
                    else if (litem == "BottomLeft")
                    {
                        kx -= 1;
                        ky += 1;
                    }

                    showValidMoves(bpawn1.x, bpawn1.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(bpawn2.x, bpawn2.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(bpawn3.x, bpawn3.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(bpawn4.x, bpawn4.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(bpawn5.x, bpawn5.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(bpawn6.x, bpawn6.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(bpawn7.x, bpawn7.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(bpawn8.x, bpawn8.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(brook1.x, brook1.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(brook2.x, brook2.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(bknight1.x, bknight1.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(bknight2.x, bknight2.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(bbishop1.x, bbishop1.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(bbishop2.x, bbishop2.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(bking.x, bking.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(bqueen.x, bqueen.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    color_panel();
                    a++;
                }

                /*string jj = string.Join("  ", check);
                MessageBox.Show(jj);*/

                bool mate = false;
                bool chck = false;

                for (int b = 0; b < check.Length; b++) //is checkmate
                {
                    if (check[b] == true)
                    {
                        mate = true;
                    }
                    else
                    {
                        mate = false;
                        break;
                    }
                }

                for (int c = 0; c < check.Length; c++) //is check
                {
                    if (check[0] == true)
                    {
                        chck = true;
                    }
                }

                if (recur && mate)
                {
                    recur = false;
                    showValidMoves(wpawn1.x, wpawn1.y);
                }

                if (chck)
                {
                    return "check";
                }
                if (mate)
                {
                    return "checkmate";
                }
            }
            else
            {
                int kx = bking.x;
                int ky = bking.y;

                bool mate = false;
                bool chck = false;

                showValidMoves(kx, ky);
                List<string> kmoves = new List<string>();

                kmoves.Add("center");

                if (kx + 1 < 7)
                {
                    if (CPanels[kx + 1, ky].BackColor == Color.DeepPink || CPanels[kx + 1, ky].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("Right");
                    }
                }
                if (kx + 1 < 7 && ky + 1 < 7)
                {
                    if (CPanels[kx + 1, ky + 1].BackColor == Color.DeepPink || CPanels[kx + 1, ky + 1].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("BottomRight");
                    }
                }
                if (kx + 1 < 7 && ky - 1 > -1)
                {
                    if (CPanels[kx + 1, ky - 1].BackColor == Color.DeepPink || CPanels[kx + 1, ky - 1].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("TopRight");
                    }
                }
                if (ky - 1 > -1)
                {
                    if (CPanels[kx, ky - 1].BackColor == Color.DeepPink || CPanels[kx, ky - 1].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("Top");
                    }
                }
                if (ky + 1 < 7)
                {
                    if (CPanels[kx, ky + 1].BackColor == Color.DeepPink || CPanels[kx, ky + 1].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("Bottom");
                    }
                }
                if (kx - 1 > -1)
                {
                    if (CPanels[kx - 1, ky].BackColor == Color.DeepPink || CPanels[kx - 1, ky].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("Left");
                    }
                }
                if (kx - 1 > -1 && ky + 1 < 7)
                {
                    if (CPanels[kx - 1, ky + 1].BackColor == Color.DeepPink || CPanels[kx - 1, ky + 1].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("BottomLeft");
                    }
                }
                if (kx - 1 > -1 && ky - 1 > -1)
                {
                    if (CPanels[kx - 1, ky - 1].BackColor == Color.DeepPink || CPanels[kx - 1, ky - 1].BackColor == Color.LightBlue)
                    {
                        kmoves.Add("TopLeft");
                    }
                }

                bool[] check = new bool[kmoves.Count()];
                int a = 0;

                foreach (var litem in kmoves)
                {
                    kx = bking.x;
                    ky = bking.y;

                    if (litem == "Right")
                    {
                        kx += 1;
                    }
                    else if (litem == "BottomRight")
                    {
                        kx += 1;
                        ky += 1;
                    }
                    else if (litem == "TopRight")
                    {
                        kx += 1;
                        ky -= 1;
                    }
                    else if (litem == "Top")
                    {
                        ky -= 1;
                    }
                    else if (litem == "Bottom")
                    {
                        ky += 1;
                    }
                    else if (litem == "Left")
                    {
                        kx -= 1;
                    }
                    else if (litem == "TopLeft")
                    {
                        kx -= 1;
                        ky -= 1;
                    }
                    else if (litem == "BottomLeft")
                    {
                        kx -= 1;
                        ky += 1;
                    }

                    showValidMoves(wpawn1.x, wpawn1.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(wpawn2.x, wpawn2.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(wpawn3.x, wpawn3.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(wpawn4.x, wpawn4.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(wpawn5.x, wpawn5.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(wpawn6.x, wpawn6.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(wpawn7.x, wpawn7.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(wpawn8.x, wpawn8.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(wrook1.x, wrook1.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(wrook2.x, wrook2.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(wknight1.x, wknight1.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(wknight2.x, wknight2.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(wbishop1.x, wbishop1.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(wbishop2.x, wbishop2.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(wking.x, wking.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    showValidMoves(wqueen.x, wqueen.y, true);
                    if (CPanels[kx, ky].BackColor == Color.DeepPink || CPanels[kx, ky].BackColor == Color.LightBlue)
                    {
                        check[a] = true;
                    }
                    color_panel();
                    a++;
                }

                for (int b = 0; b < check.Length; b++) //is checkmate
                {
                    if (check[b] == true)
                    {
                        mate = true;
                    }
                    else
                    {
                        mate = false;
                        break;
                    }
                }

                for (int c = 0; c < check.Length; c++) //is check
                {
                    if (check[0] == true)
                    {
                        chck = true;
                    }
                }

                if (recur && mate)
                {
                    recur = false;
                    showValidMoves(bpawn1.x, bpawn1.y);
                }

                if (chck)
                {
                    return "check";
                }
                if (mate)
                {
                    return "checkmate";
                }
            }

            return "clean";
        }
    }
}
