using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Startscreen : Form
    {
        public Startscreen()
        {
            InitializeComponent();
        }

        private void Startscreen_Load(object sender, EventArgs e)
        {

        }

        private void btn1Player_Click(object sender, EventArgs e)
        {
            SnakeForm s = new SnakeForm(false);
            s.Show();
            this.Hide();
        }

        private void btn2Player_Click(object sender, EventArgs e)
        {
            SnakeForm s = new SnakeForm(true);
            s.Show();
            this.Hide();
        }
    }
}
