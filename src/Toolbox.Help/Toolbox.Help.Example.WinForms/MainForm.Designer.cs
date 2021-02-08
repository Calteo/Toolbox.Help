
namespace Toolbox.Help.Example.WinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.helpProvider = new Toolbox.Help.WinForms.HelpProvider(this.components);
            this.textBox = new System.Windows.Forms.TextBox();
            this.buttonNavigate = new System.Windows.Forms.Button();
            this.labelLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // helpProvider
            // 
            this.helpProvider.HelpRequested += new System.EventHandler<Toolbox.Help.WinForms.HelpRequestedEventArgs>(this.HelpProviderHelpRequested);
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpProvider.SetHelpUrl(this.textBox, "edit.html");
            this.textBox.Location = new System.Drawing.Point(163, 3);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(474, 23);
            this.textBox.TabIndex = 1;
            // 
            // buttonNavigate
            // 
            this.buttonNavigate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.helpProvider.SetHelpUrl(this.buttonNavigate, "button.html");
            this.buttonNavigate.Location = new System.Drawing.Point(643, 3);
            this.buttonNavigate.Name = "buttonNavigate";
            this.buttonNavigate.Size = new System.Drawing.Size(154, 28);
            this.buttonNavigate.TabIndex = 2;
            this.buttonNavigate.Text = "&Navigate";
            this.buttonNavigate.UseVisualStyleBackColor = true;
            this.buttonNavigate.Click += new System.EventHandler(this.ButtonNavigateClick);
            // 
            // labelLabel
            // 
            this.labelLabel.AutoSize = true;
            this.labelLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLabel.Location = new System.Drawing.Point(3, 0);
            this.labelLabel.Name = "labelLabel";
            this.labelLabel.Size = new System.Drawing.Size(154, 34);
            this.labelLabel.TabIndex = 0;
            this.labelLabel.Text = "Label";
            this.labelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanel.Controls.Add(this.labelLabel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.textBox, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.buttonNavigate, 2, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel);
            this.helpProvider.SetHelpUrl(this, "index.html");
            this.Name = "MainForm";
            this.Text = "Help Example - Win Forms (.NET)";
            this.Shown += new System.EventHandler(this.MainFormShown);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Help.WinForms.HelpProvider helpProvider;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelLabel;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button buttonNavigate;
    }
}

