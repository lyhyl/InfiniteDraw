using InfiniteDraw.Draw;
using InfiniteDraw.Draw.Base;
using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace InfiniteDraw.WorkForm
{
    public partial class ElementListForm : DockContent
    {
        private ElementStorage elements = ElementStorage.Instance;

        public ElementListForm()
        {
            InitializeComponent();
            
            elements.ElementCreated += ElementStorage_ElementCreated;
            elements.ElementDeleted += ElementStorage_ElementDeleted;
        }

        private void ElementStorage_ElementDeleted(object sender, ElementEventArgs e)
        {
            if (!(e.Drawable is RefElement))
                listBox.Items.Remove(e.Drawable);
        }

        private void ElementStorage_ElementCreated(object sender, ElementEventArgs e)
        {
            if (!(e.Drawable is RefElement))
                listBox.Items.Add(e.Drawable);
        }

        public void Reset()
        {
            listBox.Items.Clear();
        }

        private void newFactorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prototype f = elements[elements.CreateFactor()] as Prototype;

            RefElement re2 = elements[elements.CreateRefElement(f.GID)] as RefElement;
            Vector v = new Vector(2, 1);
            v.Normalize();
            re2.BaseTransform = v * .6;
            f.AddElement(re2);

            int bezier = elements.CreateBezier();
            RefElement re = elements[elements.CreateRefElement(bezier)] as RefElement;
            f.AddElement(re);
        }

        private void deleteElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            elements.Delete(listBox.SelectedItem as Drawable);
        }

        private void listBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                listBox.SelectedIndex = index;
                elements.RequestEdit(listBox.SelectedItem as Drawable);
            }
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
