using System;
using System.Windows.Forms;

namespace Toolbox.Help.WinForms
{
    /// <summary>
    /// Make a singlton help form
    /// </summary>
    public partial class SingeltonHelpForm : Form
    {
        #region static
        private static SingeltonHelpForm Form { get; set; }        

        /// <summary>
        /// Gets or sets the <see cref="HelpServer"/> for the form.
        /// </summary>
        public static HelpServer Server { get; set; }
        /// <summary>
        /// Gets and sets the owner of the help window.
        /// </summary>
        public static Form OwnerForm { get; set; }

        /// <summary>
        /// Navigates the help window to the given url.
        /// Shows the window if it is not already visible.
        /// </summary>
        /// <param name="url"></param>
        public static void Navigate(string url)
        {
            if (Server == null) throw new InvalidOperationException("Missing Server.");

            if (!Server.Enabled) Server.Enabled = true;

            if (Form == null)
            {
                Form = new SingeltonHelpForm();

                if (OwnerForm != null)
                    Form.Text += $" - {OwnerForm.Text}";

                Form.FormClosed += (s, e) => Form = null;
                Form.Show(OwnerForm);
            }
            else
            {
                if (Form.WindowState == FormWindowState.Minimized)
                    Form.WindowState = FormWindowState.Normal;

                Form.BringToFront();
            }

            Form.Browser.Navigate(Server.GetUrl(url));
        }
        #endregion

        private SingeltonHelpForm()
        {
            InitializeComponent();

            Browser = new WebBrowser
            {
                Dock = DockStyle.Fill,
            };

            Controls.Add(Browser);
        }

        private WebBrowser Browser { get; set; } 
    }
}