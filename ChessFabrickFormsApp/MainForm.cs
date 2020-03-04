using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessFabrickFormsApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnOfflineChess_Click(object sender, EventArgs e)
        {
            new ChessOfflineForm().Show();
        }

        private void btnSockets_Click(object sender, EventArgs e)
        {
            new ConnectionForm().Show();
        }
    }
}
