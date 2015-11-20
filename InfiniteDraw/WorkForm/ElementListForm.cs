using InfiniteDraw.Draw;
using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InfiniteDraw.WorkForm
{
    public partial class ElementListForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private ElementStorage elements;

        public ElementListForm(ElementStorage es)
        {
            InitializeComponent();

            elements = es;
            elements.ElementAdded += Factors_FactorCreated;
            elements.ElementRemoved += Factors_FactorRemoved;
        }

        private void Factors_FactorRemoved(object sender, ElementEventArgs e)
        {
            listBox.Items.Remove(e.Drawable);
        }

        private void Factors_FactorCreated(object sender, ElementEventArgs e)
        {
            listBox.Items.Add(e.Drawable);
        }

        public void Reset()
        {
            listBox.Items.Clear();
        }

        private void newFactorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Factor f = elements[elements.CreateFactor()] as Factor;
            f.AddElement(new RefElement(elements, elements.CreateBezier()));
            elements.Add(f);
        }

        private void deleteElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            elements.Remove(listBox.SelectedItem as IDrawable);
        }

        private void listBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
                elements.RequestEdit(listBox.SelectedItem as IDrawable);
        }

        private void listBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBox.IndexFromPoint(e.Location);
                deleteElementToolStripMenuItem.Enabled = index != ListBox.NoMatches;
                listBox.SelectedIndex = index;
            }
            else
                deleteElementToolStripMenuItem.Enabled = false;
        }
    }
}
