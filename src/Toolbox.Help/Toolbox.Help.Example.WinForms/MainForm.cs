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
        }

        public HelpServer HelpServer { get; set; }

        private void MainFormShown(object sender, EventArgs e)
        {
            HelpServer = new HelpServer(GetType(), "Help");
            HelpServer.Handlers["info"] = new InfoHandler();
          
            SingeltonHelpForm.Server = HelpServer;
            SingeltonHelpForm.OwnerForm = this;
        }

        private void HelpProviderHelpRequested(object sender, HelpRequestedEventArgs e)
        {
            SingeltonHelpForm.Navigate(e.Url);
        }

        private void ButtonNavigateClick(object sender, EventArgs e)
        {
            SingeltonHelpForm.Navigate(textBox.Text);
        }
    }
}