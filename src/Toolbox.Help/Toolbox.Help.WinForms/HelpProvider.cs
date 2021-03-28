using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Toolbox.Help.WinForms
{
    /// <summary>
    /// Help provider component to attach help through the <see cref="HelpServer"/>.
    /// </summary>
    [ToolboxBitmap(typeof(System.Windows.Forms.HelpProvider))]
    [ProvideProperty("HelpUrl", typeof(Control))]
    public partial class HelpProvider : Component, IExtenderProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpProvider"/> class.
        /// </summary>
        public HelpProvider()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpProvider"/> class.
        /// </summary>
        public HelpProvider(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private Dictionary<Control, string> Urls { get; } = new Dictionary<Control, string>();

        bool IExtenderProvider.CanExtend(object extendee)
        {
            return extendee is Control && !(extendee is HelpProvider);
        }

        /// <summary>
        /// Return the help url for a given control.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        [Category("Help"), DefaultValue(""), Description("Url inside the help site.")]
        public string GetHelpUrl(Control control)
        {
            if (!Urls.TryGetValue(control, out var url)) return "";
            return url;
        }

        /// <summary>
        /// Sets the help url for a control.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="url"></param>
        public void SetHelpUrl(Control control, string url)
        {    
            if (url.IsEmpty())
            {
                Urls.Remove(control); 
                control.HelpRequested -= ControlHelpRequested;
            }
            else
            {
                if (!Urls.ContainsKey(control))
                {
                    control.HelpRequested += ControlHelpRequested;
                }
                Urls[control] = url;                
            }
        }

        private void ControlHelpRequested(object sender, HelpEventArgs args)
        {
            var control = (Control)sender;

            if (HelpRequested != null)
                HelpRequested(this, new HelpRequestedEventArgs(control, Urls[control]));

            args.Handled = args.Handled;
        }

        /// <summary>
        /// Gets raised if the user hits F1 on a control that has an url registered.
        /// </summary>
        [Category("Help"), Description("Gets raised if the user requests help for a control.")]
        public event EventHandler<HelpRequestedEventArgs> HelpRequested;
    }
}
