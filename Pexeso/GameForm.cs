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
    public partial class GameForm : Form
    {
        private StartScreen startScreen;
        public GameForm(StartScreen startScreen)
        {
            this.startScreen = startScreen;
            InitializeComponent();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            startScreen.Show();
        }
    }
}
