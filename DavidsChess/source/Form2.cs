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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new string[] { "White", "Black" });
            comboBox2.Items.AddRange(new string[] { "Easy", "Normal", "Hard" });
        }

        string difficult = "";
        string color = "";
        private void button1_Click(object sender, EventArgs e)//submit
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                difficult = comboBox2.SelectedItem.ToString();
                color = comboBox1.SelectedItem.ToString();
                DialogResult = DialogResult.OK;
                this.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)//cancel
        {
            DialogResult = DialogResult.No;
            this.Close();
        }

        public string retDiff
        {
            get { return difficult; }
        }

        public string retCol
        {
            get { return color; }
        }

    }
}
