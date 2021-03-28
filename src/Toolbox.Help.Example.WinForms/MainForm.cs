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
          
            SingletonHelpForm.Server = HelpServer;
            SingletonHelpForm.OwnerForm = this;
        }

        private void HelpProviderHelpRequested(object sender, HelpRequestedEventArgs e)
        {
            SingletonHelpForm.Navigate(e.Url);
        }

        private void ButtonNavigateClick(object sender, EventArgs e)
        {
            SingletonHelpForm.Navigate(textBox.Text);
        }
    }
}