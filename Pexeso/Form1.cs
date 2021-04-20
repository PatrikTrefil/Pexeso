using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexeso
{
    public partial class StartScreen : Form
    {
        GameForm gameForm;
        public StartScreen()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            gameForm = new GameForm(this);
            gameForm.Show();
            Hide();
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
