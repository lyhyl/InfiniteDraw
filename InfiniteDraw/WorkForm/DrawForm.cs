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
        public IDrawable Drawable { private set; get; }

        public DrawForm(IDrawable d)
        {
            InitializeComponent();
            InitializeFactor(d);
        }

        private void InitializeFactor(IDrawable d)
        {
            Drawable = d;
            Text = d.Name;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);


        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Bitmap bmp = Drawable.Draw(0);

            e.Graphics.DrawImage(bmp, Point.Empty);
        }
    }
}
