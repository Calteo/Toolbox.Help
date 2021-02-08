using System;
using System.Windows.Forms;

namespace Toolbox.Help.WinForms
{
    public class HelpRequestedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpRequestedEventArgs"/> class.
        /// </summary>
        internal HelpRequestedEventArgs(Control control, string url)
        {
            Control = control;
            Url = url;
        }

        /// <summary>
        /// Gets the <see cref="Control"/> that is requesting the help.
        /// </summary>
        public Control Control { get; }

        /// <summary>
        /// Gets the url associated with the control.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Gets or sets the state if the request has been handled.
        /// </summary>
        public bool Handled { get; set; }
    }
}
