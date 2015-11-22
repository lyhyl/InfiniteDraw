using InfiniteDraw.Draw;
using InfiniteDraw.WorkForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace InfiniteDraw
{
    public partial class InfiniteDraw : Form
    {
        private ElementListForm elementListForm = null;
        private PropertyForm propertyForm = null;
        private DrawForms drawForms = null;

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
            elementListForm = new ElementListForm(elements);
            propertyForm = new PropertyForm(elements);
            drawForms = new DrawForms(elements, mainDockPanel);

            elementListForm.Show(mainDockPanel, DockState.DockRight);
            propertyForm.Show(mainDockPanel.ActivePane, DockAlignment.Bottom, .5);

            elementListForm.VisibleChanged += (s, e) => { elementListToolStripMenuItem.Checked = elementListForm.Visible; };
            propertyForm.VisibleChanged += (s, e) => { propertyToolStripMenuItem.Checked = propertyForm.Visible; };

            elementListToolStripMenuItem.Checked = true;
            propertyToolStripMenuItem.Checked = true;
        }

        private void elementListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool state = elementListToolStripMenuItem.Checked;
            state = !state;
            elementListToolStripMenuItem.Checked = state;
            if (state)
                elementListForm.Show();
            else
                elementListForm.Hide();
        }

        private void propertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool state = propertyToolStripMenuItem.Checked;
            state = !state;
            propertyToolStripMenuItem.Checked = state;
            if (state)
                propertyForm.Show();
            else
                propertyForm.Hide();
        }
    }
}
