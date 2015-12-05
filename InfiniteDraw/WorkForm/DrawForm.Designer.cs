namespace InfiniteDraw.WorkForm
{
    partial class DrawForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editViewToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(153, 48);
            // 
            // editViewToolStripMenuItem
            // 
            this.editViewToolStripMenuItem.Checked = true;
            this.editViewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.editViewToolStripMenuItem.Name = "editViewToolStripMenuItem";
            this.editViewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editViewToolStripMenuItem.Text = "&Edit View";
            this.editViewToolStripMenuItem.Click += new System.EventHandler(this.editViewToolStripMenuItem_Click);
            // 
            // DrawForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ContextMenuStrip = this.contextMenu;
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DrawForm";
            this.Text = "Element";
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem editViewToolStripMenuItem;
    }
}
