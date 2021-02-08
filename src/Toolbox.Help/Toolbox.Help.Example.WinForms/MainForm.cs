using System;
using System.Windows.Forms;
using Toolbox.Help.WinForms;

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
            HelpServer = new HelpServer(GetType(), "Help");
            HelpServer.Handlers["info"] = new InfoHandler();
            HelpServer.Enabled = true;
          
            Browser = new WebBrowser
            {                
                Dock = DockStyle.Fill
            };

            tableLayoutPanel.Controls.Add(Browser);
            tableLayoutPanel.SetColumnSpan(Browser, 3);
            tableLayoutPanel.SetCellPosition(Browser, new TableLayoutPanelCellPosition(0, 1));
            
            Browser.DocumentTitleChanged += BrowserDocumentTitleChanged;

            Browser.Navigate(HelpServer.GetUrl("index.html"));
        }

        private void BrowserDocumentTitleChanged(object sender, EventArgs e)
        {
            Text = Prefix + " - " + Browser.DocumentTitle;
        }

        private void HelpProviderHelpRequested(object sender, HelpRequestedEventArgs e)
        {
            Browser.Navigate(HelpServer.GetUrl(e.Url));
        }

        private void ButtonNavigateClick(object sender, EventArgs e)
        {
            var url = textBox.Text.IsEmpty() ? "index.html" : textBox.Text;

            Browser.Navigate(HelpServer.GetUrl(url));
        }
    }
}