using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Випускна
{
    public partial class Theory : Form
    {
        public Theory()
        {
            InitializeComponent();
        }

        private void Theory_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
        private void Theory_Load(object sender, EventArgs e)
        {
            string path = Application.StartupPath;
            webBrowser1.Navigate(path);
            webBrowser1.Navigate(path + "\\Документация.htm");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
