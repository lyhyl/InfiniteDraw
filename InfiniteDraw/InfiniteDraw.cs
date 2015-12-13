using InfiniteDraw.WorkForm;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace InfiniteDraw
{
    public partial class InfiniteDraw : Form
    {
        private ElementListForm elementListForm = null;
        private PropertyForm propertyForm = null;
        private DrawForms drawForms = null;
        private ToolsForm toolsForm = null;

        public InfiniteDraw()
        {
            InitializeComponent();
            ChangeTheme();
            InitializeForm();
        }

        private void ChangeTheme()
        {
            mainDockPanel.Theme = new VS2012LightTheme();
        }

        private void InitializeForm()
        {
            elementListForm = new ElementListForm();
            propertyForm = new PropertyForm();
            drawForms = new DrawForms(mainDockPanel);
            toolsForm = new ToolsForm();

            elementListForm.Show(mainDockPanel, DockState.DockRight);
            propertyForm.Show(mainDockPanel.ActivePane, DockAlignment.Bottom, .5);
            toolsForm.Show(mainDockPanel, DockState.DockLeft);

            elementListForm.VisibleChanged += (s, e) => { elementListToolStripMenuItem.Checked = elementListForm.Visible; };
            propertyForm.VisibleChanged += (s, e) => { propertyToolStripMenuItem.Checked = propertyForm.Visible; };
            toolsForm.VisibleChanged += (s, e) => { toolboxToolStripMenuItem.Checked = toolsForm.Visible; };

            elementListToolStripMenuItem.Checked = true;
            propertyToolStripMenuItem.Checked = true;
            toolboxToolStripMenuItem.Checked = true;

            elementListToolStripMenuItem.Tag = elementListForm;
            propertyToolStripMenuItem.Tag = propertyForm;
            toolboxToolStripMenuItem.Tag = toolsForm;
        }

        private void formViewVisibleMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                bool state = menuItem.Checked;
                state = !state;
                menuItem.Checked = !state;
                if (state)
                    (menuItem.Tag as DockContent)?.Show();
                else
                    (menuItem.Tag as DockContent)?.Hide();
            }
        }
    }
}
