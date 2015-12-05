using InfiniteDraw.Draw;
using InfiniteDraw.Draw.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

namespace InfiniteDraw.WorkForm
{
    public partial class DrawForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private ElementStorage elements;
        private Point offset = new Point(0, 0);
        private int scale = 100;

        private RectangleF canvas = RectangleF.Empty;

        private Bitmap art = null;
        private bool artValidated = false;
        private bool renderView = false;

        public Drawable Drawable { private set; get; }

        private Matrix WorldScaleMatrix
        {
            get
            {
                return new Matrix(scale / 100.0f, 0, 0, scale / 100.0f, 0, 0);
            }
        }

        private Point WorldTranslateOffset
        {
            get
            {
                Point p = new Point();
                p += new Size(ClientSize.Width / 2, ClientSize.Height / 2);
                p += new Size(offset.X, offset.Y);
                if (artValidated)
                    p += new Size(-art.Width / 2, -art.Height / 2);
                return p;
            }
        }

        public DrawForm(ElementStorage es, Drawable d)
        {
            InitializeComponent();
            InitializeElement(es, d);
            RegisterEvents();
        }

        private void InitializeElement(ElementStorage es, Drawable d)
        {
            elements = es;
            Drawable = d;
            Text = d.ToString();
        }

        private void RegisterEvents()
        {
            elements.ElementModified += Elements_ElementModified;
            MouseDown += DrawForm_MouseDown;
            MouseMove += DrawForm_MouseMove;
            MouseUp += DrawForm_MouseUp;
            MouseWheel += DrawForm_MouseWheel;
        }

        private void Elements_ElementModified(object sender, ElementEventArgs e)
        {
            Text = Drawable.ToString();
            /// TODO : Must improve it
            Invalidate();

            InvalidateArt(true);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            elements.Selected(Drawable);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!artValidated)
                DrawArt();
            if (!canvas.IsEmpty)
            {
                Point offset = WorldTranslateOffset;
                e.Graphics.TranslateTransform(offset.X, offset.Y);
                e.Graphics.DrawImage(art, new Point());
            }
        }

        private void DrawArt()
        {
            if (renderView)
                canvas = Drawable.MeasureSize(WorldScaleMatrix);
            else
                canvas = Drawable.MeasureEditSize(WorldScaleMatrix);

            if (canvas.IsEmpty)
                return;

            if (renderView)
                Drawable.Draw(CreateImageGraphics(canvas, out art));
            else
                Drawable.EditDraw(CreateImageGraphics(canvas, out art));

            artValidated = true;
        }

        private Graphics CreateImageGraphics(RectangleF size, out Bitmap image)
        {
            int w = (int)Math.Min(Math.Ceiling(size.Right) - Math.Floor(size.Left), 2048);
            int h = (int)Math.Min(Math.Ceiling(size.Bottom) - Math.Floor(size.Top), 2048);
            image = new Bitmap(w, h, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(image);
            g.CompositingMode = CompositingMode.SourceOver;
            g.InterpolationMode = InterpolationMode.High;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;

            Matrix trans = new Matrix();

            trans.Translate((float)-Math.Floor(size.Left), (float)-Math.Floor(size.Top));
            trans.Scale(scale / 100.0f, scale / 100.0f);

            g.Transform = trans;

            return g;
        }

        private void InvalidateArt(bool redrawElement)
        {
            artValidated = !redrawElement;
            Invalidate(new Rectangle(WorldTranslateOffset, art.Size));
        }

        private void editViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editViewToolStripMenuItem.Checked = !editViewToolStripMenuItem.Checked;
            renderView = !editViewToolStripMenuItem.Checked;
            InvalidateArt(true);
            Invalidate();
        }
    }
}
