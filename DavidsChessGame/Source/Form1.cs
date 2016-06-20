using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace DavidsChessGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if multiplayer, wait untill oponent moves
            if (multiplayer && !userTurn)
            {
                WebClient wc = new WebClient();
                int turn = Convert.ToInt32(wc.DownloadString("http://www.davenwarrior.com/cgi-bin/chessgame.cgi?action=move&move=wait&user=" + Properties.Settings.Default.nick));

                textBox1.Text += Environment.NewLine + "tick...";

                if (turn == Properties.Settings.Default.movenum + 1)
                {
                    textBox1.Text += Environment.NewLine + "Recieved signal, making move : ";
                    //if ready to make move
                    Properties.Settings.Default.movenum = turn;
                    Properties.Settings.Default.Save();

                    string[] components = wc.DownloadString("http://www.davenwarrior.com/cgi-bin/chessgame.cgi?action=move&move=get&user=" + Properties.Settings.Default.nick).Split(new string[] {"[[]]"}, StringSplitOptions.None);
                    int cSelection = 0;

                    textBox1.Text += components[0] + components[1] + components[2];

                    //find game piece
                    for (int a = 0; a < game.opponentPieces.Count(); a++)
                    {
                        if (game.opponentPieces[a].name == components[0])
                        {
                            cSelection = a;
                            break;
                        }
                    }

                    int x = Convert.ToInt32(components[1]);
                    int y = 7 - Convert.ToInt32(components[2]);
                    bool delPieces = Convert.ToBoolean(components[3]);

                    bool isCheck = false;

                    //actually move piece here
                    game.opponentPieces[cSelection] = move(game.opponentPieces[cSelection], x, y, delPieces, ref game.squares, out isCheck);
                    userTurn = true;
                    textBox1.Text += Environment.NewLine + "made move";

                    //cleanup. clean squares and call render.
                    render();

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
                }
            }
        }

    }
}
