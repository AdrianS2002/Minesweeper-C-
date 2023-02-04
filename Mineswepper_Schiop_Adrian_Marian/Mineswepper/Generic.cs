using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mineswepper
{
    public partial class Generic : Form
    {
        public Generic()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Visible = false;
            f.ShowDialog();
            Application.Exit();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            Instructiuni f = new Instructiuni();
            this.Visible = false;
            f.ShowDialog();
            this.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Generic_Load(object sender, EventArgs e)
        {

        }
    }
}
