using InfiniteDraw.Draw;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InfiniteDraw.WorkForm
{
    public partial class DrawForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public Factor Factor { private set; get; }

        public DrawForm(Factor f)
        {
            InitializeComponent();
            InitializeFactor(f);
        }

        private void InitializeFactor(Factor f)
        {
            Factor = f;
            Text = f.Name;
        }
    }
}
