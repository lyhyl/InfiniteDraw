using InfiniteDraw.Draw;
using InfiniteDraw.Draw.Base;
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
        private ElementStorage elements;

        public Drawable Drawable { private set; get; }

        public DrawForm(ElementStorage es, Drawable d)
        {
            InitializeComponent();
            InitializeFactor(d);
            elements = es;
        }

        private void InitializeFactor(Drawable d)
        {
            Drawable = d;
            Text = d.ToString();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            elements.Selected(Drawable);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Bitmap bmp = Drawable.Draw(0);
            if (bmp != null)
                e.Graphics.DrawImage(bmp, Point.Empty);
        }
    }
}
