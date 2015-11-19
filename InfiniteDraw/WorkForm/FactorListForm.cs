using InfiniteDraw.Draw;
using InfiniteDraw.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InfiniteDraw.WorkForm
{
    public partial class FactorListForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private FactorStorage factors;

        public FactorListForm(FactorStorage fs)
        {
            InitializeComponent();

            factors = fs;
            factors.FactorCreated += Factors_FactorCreated;
            factors.FactorRemoved += Factors_FactorRemoved;
        }

        private void Factors_FactorRemoved(object sender, FactorEventArgs e)
        {
            listBox.Items.Remove(e.Factor);
        }

        private void Factors_FactorCreated(object sender, FactorEventArgs e)
        {
            listBox.Items.Add(e.Factor);
        }

        public void Reset()
        {
            listBox.Items.Clear();
        }

        private void newFactorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            factors.Create();
        }

        private void deleteFactorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            factors.Remove(listBox.SelectedItem as Factor);
        }

        private void listBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
                factors.Active(listBox.SelectedItem as Factor);
        }

        private void listBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBox.IndexFromPoint(e.Location);
                deleteFactorToolStripMenuItem.Enabled = index != ListBox.NoMatches;
                listBox.SelectedIndex = index;
            }
            else
                deleteFactorToolStripMenuItem.Enabled = false;
        }
    }
}
