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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        Color col = Color.White;
        string difficulty = "";
        string nickname = "";
        bool multi = false;

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox3.SelectedItem != null && textBox1.Text != "")
            {
                if (comboBox1.SelectedItem.ToString() == "Black")
                {
                    col = Color.Black;
                }

                if (comboBox3.SelectedItem.ToString() == "Yes")
                {
                    multi = true;
                }
                difficulty = comboBox2.SelectedItem.ToString();
                nickname = textBox1.Text;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        public Color color
        {
            get { return col; }
        }

        public string diff
        {
            get { return difficulty; }
        }

        public bool multiplayer
        {
            get { return multi; }
        }

        public string nick
        {
            get { return nickname; }
        }
    }
}
