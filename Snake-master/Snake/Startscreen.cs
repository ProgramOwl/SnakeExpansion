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
        SnakeForm s;
        public Startscreen()
        {
            InitializeComponent();
        }

        private void Startscreen_Load(object sender, EventArgs e)
        {
            
        }

        private void btn1Player_Click(object sender, EventArgs e)
        {
            s = new SnakeForm(this, false);
            s.Show();
            this.Hide();
        }

        private void btn2Player_Click(object sender, EventArgs e)
        {
            s = new SnakeForm(this, true);
            s.Show();
            this.Hide();
        }

        public void subExit()
        {
            this.Close();
        }

        //public event EventHandler Closed;
        //private void SnakeForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    // Determine if text has changed in the textbox by comparing to original text.
        //    //if (textBox1.Text != strMyOriginalText)
        //    //{
        //    // Display a MsgBox asking the user to save changes or abort.
        //    if (MessageBox.Show("Do you want to return to menu?", "Snake Game",
        //       MessageBoxButtons.YesNo) == DialogResult.Yes)
        //    {
        //        // Cancel the Closing event from closing the form.
        //        e.Cancel = true;
        //        // Call method to save file...
        //    }
        //    //}
        //}
    }
}
