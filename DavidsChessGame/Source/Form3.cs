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
    public partial class Form3 : Form
    {
        WebClient wc = new WebClient();
        string retString = "";
        bool inbound = false;
        int gameid = 0;
        Color gamecol = new Color();

        string name = "";

        public Form3()
        {
            InitializeComponent();
            //Properties.Settings.Default.connect = false;
            retString = wc.DownloadString("http://www.davenwarrior.com/cgi-bin/chessgame.cgi?action=sign_in&user=" + Properties.Settings.Default.nick);
            string[] tString = retString.Split(new string[] { "\n" }, StringSplitOptions.None);
            if (tString[0].Split(new string[] { ",,/-" }, StringSplitOptions.None)[0] != "pending")
            {
                string[] nString = new string[tString.Length - 1];
                int b = 0;
                for (int a = 0; a < tString.Length; a++)
                {
                    if (tString[a].ToString() != Properties.Settings.Default.nick)
                    {
                        nString[b++] = tString[a].ToString();
                    }
                }
                listBox1.Items.AddRange(nString);
            }
            else
            {
                name = tString[0].Split(new string[] { ",,/-" }, StringSplitOptions.None)[1];
                listBox1.Items.Add("Accept request from " + name);
                listBox1.Items.Add("Deny request from " + name);
                inbound = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.connect)
            {
                retString = wc.DownloadString("http://www.davenwarrior.com/cgi-bin/chessgame.cgi?action=connect&user=" + Properties.Settings.Default.nick + "&target=" + Properties.Settings.Default.target);
                if (retString == "null")
                {
                    textBox1.Text += Environment.NewLine + "waiting";
                }
                else if (retString == "reject")
                {
                    //handle reject
                    Properties.Settings.Default.connect = false;
                }
                else
                {
                    /*To Do :
                    *continue here / handle accept
                    *work on receiving invitations too
                     */
                    gameid = Convert.ToInt32(retString);
                    gamecol = Color.White;
                    Properties.Settings.Default.connect = false;
                    Properties.Settings.Default.Save();
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            else
            {
                retString = wc.DownloadString("http://www.davenwarrior.com/cgi-bin/chessgame.cgi?action=report&isbusy=0&user=" + Properties.Settings.Default.nick);
                listBox1.Items.Clear();
                string[] tString = retString.Split(new string[] { "\n" }, StringSplitOptions.None);
                if (tString[0].Split(new string[] { ",,/-" }, StringSplitOptions.None)[0] == "pending")
                {
                    name = tString[0].Split(new string[] { ",,/-" }, StringSplitOptions.None)[1];
                    listBox1.Items.Add("Accept request from " + name);
                    listBox1.Items.Add("Deny request from " + name);
                    inbound = true;
                }
                else
                {
                    string[] nString = new string[tString.Length - 1];
                    int b = 0;
                    for (int a = 0; a < tString.Length; a++)
                    {
                        if (tString[a].ToString() != Properties.Settings.Default.nick)
                        {
                            nString[b++] = tString[a].ToString();
                        }
                    }
                    listBox1.Items.AddRange(nString);
                }
            }
       }
        

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                if (inbound)
                {
                    gamecol = Color.Black;
                    if (listBox1.SelectedIndex == 0)
                    {
                        retString = wc.DownloadString("http://www.davenwarrior.com/cgi-bin/chessgame.cgi?action=accept&user=" + Properties.Settings.Default.nick);
                        gameid = Convert.ToInt32(retString);
                        Properties.Settings.Default.target = name;
                        Properties.Settings.Default.Save();
                        DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                    else if (listBox1.SelectedIndex == 1)
                    {
                        retString = wc.DownloadString("http://www.davenwarrior.com/cgi-bin/chessgame.cgi?action=deny&user=" + Properties.Settings.Default.nick);
                        inbound = false;
                    }
                }
                else
                {
                    textBox1.Text += "sending request to " + listBox1.SelectedItem.ToString();
                    retString = wc.DownloadString("http://www.davenwarrior.com/cgi-bin/chessgame.cgi?action=connect&user=" + Properties.Settings.Default.nick + "&target=" + listBox1.SelectedItem.ToString());
                    Properties.Settings.Default.target = listBox1.SelectedItem.ToString();
                    Properties.Settings.Default.connect = true;
                    Properties.Settings.Default.Save();
                }
            }

            //DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public int id
        {
            get { return gameid; }
        }
        public Color col
        {
            get { return gamecol; }
        }
    }
}
