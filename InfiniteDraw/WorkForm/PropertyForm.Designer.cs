namespace InfiniteDraw.WorkForm
{
    partial class PropertyForm
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
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.propertiesTable = new System.Windows.Forms.SplitContainer();
            this.propertyNamePanel = new System.Windows.Forms.Panel();
            this.propertyValuePanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.propertiesTable)).BeginInit();
            this.propertiesTable.Panel1.SuspendLayout();
            this.propertiesTable.Panel2.SuspendLayout();
            this.propertiesTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // vScrollBar
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.Location = new System.Drawing.Point(267, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 261);
            this.vScrollBar.TabIndex = 0;
            // 
            // propertiesTable
            // 
            this.propertiesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesTable.Location = new System.Drawing.Point(0, 0);
            this.propertiesTable.Name = "propertiesTable";
            // 
            // propertiesTable.Panel1
            // 
            this.propertiesTable.Panel1.Controls.Add(this.propertyNamePanel);
            // 
            // propertiesTable.Panel2
            // 
            this.propertiesTable.Panel2.Controls.Add(this.propertyValuePanel);
            this.propertiesTable.Size = new System.Drawing.Size(267, 261);
            this.propertiesTable.SplitterDistance = 120;
            this.propertiesTable.TabIndex = 1;
            // 
            // propertyNamePanel
            // 
            this.propertyNamePanel.Location = new System.Drawing.Point(3, 3);
            this.propertyNamePanel.Name = "propertyNamePanel";
            this.propertyNamePanel.Size = new System.Drawing.Size(114, 255);
            this.propertyNamePanel.TabIndex = 0;
            // 
            // propertyValuePanel
            // 
            this.propertyValuePanel.Location = new System.Drawing.Point(3, 3);
            this.propertyValuePanel.Name = "propertyValuePanel";
            this.propertyValuePanel.Size = new System.Drawing.Size(137, 255);
            this.propertyValuePanel.TabIndex = 0;
            // 
            // PropertyForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.propertiesTable);
            this.Controls.Add(this.vScrollBar);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideOnClose = true;
            this.Name = "PropertyForm";
            this.Text = "Property";
            this.propertiesTable.Panel1.ResumeLayout(false);
            this.propertiesTable.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertiesTable)).EndInit();
            this.propertiesTable.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar vScrollBar;
        private System.Windows.Forms.SplitContainer propertiesTable;
        private System.Windows.Forms.Panel propertyNamePanel;
        private System.Windows.Forms.Panel propertyValuePanel;
    }
}
