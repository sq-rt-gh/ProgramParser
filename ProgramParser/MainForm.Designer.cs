
namespace ProgramParser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            MenuStrip = new MenuStrip();
            RunToolStripMenuItem = new ToolStripMenuItem();
            TextBoxCode = new RichTextBox();
            SplitContainerInner = new SplitContainer();
            splitContainerOuter = new SplitContainer();
            textBoxInfo = new TextBox();
            TextBoxOutput = new TextBox();
            MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainerInner).BeginInit();
            SplitContainerInner.Panel1.SuspendLayout();
            SplitContainerInner.Panel2.SuspendLayout();
            SplitContainerInner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerOuter).BeginInit();
            splitContainerOuter.Panel1.SuspendLayout();
            splitContainerOuter.Panel2.SuspendLayout();
            splitContainerOuter.SuspendLayout();
            SuspendLayout();
            // 
            // MenuStrip
            // 
            MenuStrip.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MenuStrip.ImageScalingSize = new Size(20, 20);
            MenuStrip.Items.AddRange(new ToolStripItem[] { RunToolStripMenuItem });
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Padding = new Padding(8, 2, 0, 2);
            MenuStrip.Size = new Size(1002, 33);
            MenuStrip.TabIndex = 0;
            MenuStrip.Text = "menuStrip1";
            // 
            // RunToolStripMenuItem
            // 
            RunToolStripMenuItem.AccessibleRole = AccessibleRole.PushButton;
            RunToolStripMenuItem.Name = "RunToolStripMenuItem";
            RunToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            RunToolStripMenuItem.Size = new Size(105, 29);
            RunToolStripMenuItem.Text = "Запустить";
            RunToolStripMenuItem.Click += RunToolStripMenuItem_Click;
            // 
            // TextBoxCode
            // 
            TextBoxCode.BackColor = SystemColors.Control;
            TextBoxCode.Dock = DockStyle.Fill;
            TextBoxCode.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TextBoxCode.Location = new Point(0, 0);
            TextBoxCode.Margin = new Padding(0);
            TextBoxCode.Name = "TextBoxCode";
            TextBoxCode.Size = new Size(604, 502);
            TextBoxCode.TabIndex = 1;
            TextBoxCode.Text = "";
            // 
            // SplitContainerInner
            // 
            SplitContainerInner.BackColor = SystemColors.ControlDark;
            SplitContainerInner.BorderStyle = BorderStyle.FixedSingle;
            SplitContainerInner.Dock = DockStyle.Fill;
            SplitContainerInner.FixedPanel = FixedPanel.Panel2;
            SplitContainerInner.Location = new Point(0, 33);
            SplitContainerInner.Margin = new Padding(0);
            SplitContainerInner.Name = "SplitContainerInner";
            SplitContainerInner.Orientation = Orientation.Horizontal;
            // 
            // SplitContainerInner.Panel1
            // 
            SplitContainerInner.Panel1.BackColor = SystemColors.Control;
            SplitContainerInner.Panel1.Controls.Add(splitContainerOuter);
            SplitContainerInner.Panel1MinSize = 150;
            // 
            // SplitContainerInner.Panel2
            // 
            SplitContainerInner.Panel2.BackColor = SystemColors.Control;
            SplitContainerInner.Panel2.Controls.Add(TextBoxOutput);
            SplitContainerInner.Panel2.Padding = new Padding(1);
            SplitContainerInner.Panel2MinSize = 50;
            SplitContainerInner.Size = new Size(1002, 640);
            SplitContainerInner.SplitterDistance = 504;
            SplitContainerInner.SplitterIncrement = 5;
            SplitContainerInner.SplitterWidth = 12;
            SplitContainerInner.TabIndex = 2;
            SplitContainerInner.TabStop = false;
            // 
            // splitContainerOuter
            // 
            splitContainerOuter.BackColor = SystemColors.ControlDark;
            splitContainerOuter.Dock = DockStyle.Fill;
            splitContainerOuter.FixedPanel = FixedPanel.Panel2;
            splitContainerOuter.Location = new Point(0, 0);
            splitContainerOuter.Name = "splitContainerOuter";
            // 
            // splitContainerOuter.Panel1
            // 
            splitContainerOuter.Panel1.Controls.Add(TextBoxCode);
            splitContainerOuter.Panel1MinSize = 150;
            // 
            // splitContainerOuter.Panel2
            // 
            splitContainerOuter.Panel2.Controls.Add(textBoxInfo);
            splitContainerOuter.Panel2MinSize = 50;
            splitContainerOuter.Size = new Size(1000, 502);
            splitContainerOuter.SplitterDistance = 604;
            splitContainerOuter.SplitterIncrement = 5;
            splitContainerOuter.SplitterWidth = 10;
            splitContainerOuter.TabIndex = 3;
            splitContainerOuter.TabStop = false;
            // 
            // textBoxInfo
            // 
            textBoxInfo.BackColor = SystemColors.Control;
            textBoxInfo.BorderStyle = BorderStyle.None;
            textBoxInfo.Dock = DockStyle.Fill;
            textBoxInfo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxInfo.Location = new Point(0, 0);
            textBoxInfo.Margin = new Padding(4);
            textBoxInfo.Multiline = true;
            textBoxInfo.Name = "textBoxInfo";
            textBoxInfo.ReadOnly = true;
            textBoxInfo.ScrollBars = ScrollBars.Vertical;
            textBoxInfo.Size = new Size(386, 502);
            textBoxInfo.TabIndex = 2;
            textBoxInfo.TabStop = false;
            textBoxInfo.Text = resources.GetString("textBoxInfo.Text");
            textBoxInfo.WordWrap = false;
            // 
            // TextBoxOutput
            // 
            TextBoxOutput.BorderStyle = BorderStyle.None;
            TextBoxOutput.Dock = DockStyle.Fill;
            TextBoxOutput.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TextBoxOutput.Location = new Point(1, 1);
            TextBoxOutput.Margin = new Padding(4);
            TextBoxOutput.Multiline = true;
            TextBoxOutput.Name = "TextBoxOutput";
            TextBoxOutput.ReadOnly = true;
            TextBoxOutput.ScrollBars = ScrollBars.Vertical;
            TextBoxOutput.Size = new Size(998, 120);
            TextBoxOutput.TabIndex = 1;
            TextBoxOutput.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1002, 673);
            Controls.Add(SplitContainerInner);
            Controls.Add(MenuStrip);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainMenuStrip = MenuStrip;
            Margin = new Padding(4);
            MinimumSize = new Size(300, 300);
            Name = "MainForm";
            Text = "Parser";
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            SplitContainerInner.Panel1.ResumeLayout(false);
            SplitContainerInner.Panel2.ResumeLayout(false);
            SplitContainerInner.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SplitContainerInner).EndInit();
            SplitContainerInner.ResumeLayout(false);
            splitContainerOuter.Panel1.ResumeLayout(false);
            splitContainerOuter.Panel2.ResumeLayout(false);
            splitContainerOuter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerOuter).EndInit();
            splitContainerOuter.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MenuStrip;
        private ToolStripMenuItem RunToolStripMenuItem;
        private RichTextBox TextBoxCode;
        private SplitContainer SplitContainerInner;
        private TextBox TextBoxOutput;
        private SplitContainer splitContainerOuter;
        private TextBox textBoxInfo;
    }
}
