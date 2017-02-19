using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class fmMenu : Form
    {
        public fmMenu()
        {
            InitializeComponent();
        }

        private void btnPvC_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            fmMain _fm = new fmMain(1);
            Hide();
            this.Cursor = Cursors.Default;
            _fm.ShowDialog();
            Show();
        }

        private void btnPvP_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            fmMain _fm = new fmMain(2);
            Hide();
            this.Cursor = Cursors.Default;
            _fm.ShowDialog();
            Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
