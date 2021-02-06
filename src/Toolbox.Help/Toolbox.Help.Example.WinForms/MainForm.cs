using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toolbox.Help.Example.WinForms
{
    internal partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Prefix = Text;
        }

        public HelpServer HelpServer { get; set; }

        public WebBrowser Browser { get; set; }
        private string Prefix { get; set; }

        private void MainFormShown(object sender, EventArgs e)
        {
            HelpServer = new HelpServer(GetType().Assembly, GetType().Namespace + ".Help");
            HelpServer.Handlers["info"] = new InfoHandler();
            HelpServer.Enabled = true;
          
            Browser = new WebBrowser
            {                
                Dock = DockStyle.Fill
            };
            Controls.Add(Browser);

            Browser.DocumentTitleChanged += BrowserDocumentTitleChanged;

            Browser.Navigate(HelpServer.GetUrl("index.html"));
        }

        private void BrowserDocumentTitleChanged(object sender, EventArgs e)
        {
            Text = Prefix + " - " + Browser.DocumentTitle;
        }
    }
}
