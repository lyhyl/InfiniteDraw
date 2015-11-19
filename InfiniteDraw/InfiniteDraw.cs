﻿using InfiniteDraw.Draw;
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
        private FactorListForm factorListForm = null;
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
            factorListForm = new FactorListForm(factors);
            propertyForm = new PropertyForm();
            drawForms = new DrawForms(factors, mainDockPanel);

            factorListForm.Show(mainDockPanel, DockState.DockRight);
            propertyForm.Show(mainDockPanel.ActivePane, DockAlignment.Bottom, .5);

            factorListToolStripMenuItem.Checked = true;
            propertyToolStripMenuItem.Checked = true;
        }

        private void factorListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool state = factorListToolStripMenuItem.Checked;
            state = !state;
            factorListToolStripMenuItem.Checked = state;
            if (state)
                factorListForm.Show();
            else
                factorListForm.Hide();
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