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
        private RectangleF canvas = RectangleF.Empty;
        private Point offset = new Point(0, 0);
        private int scale = 100;
        private Bitmap art = null;
        private bool artValidated = false;

        private bool Dragging = false;
        private Point lastMousePos = Point.Empty;

        public Drawable Drawable { private set; get; }

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

        private void DrawForm_MouseWheel(object sender, MouseEventArgs e)
        {
            Invalidate(new Rectangle(ArtPosition(), art.Size));
            artValidated = false;
            scale += e.Delta / 10;
        }

        private void DrawForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ClientRectangle.Contains(e.Location))
            {
                lastMousePos = e.Location;
                Dragging = true;
            }
        }

        private void DrawForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging)
            {
                Invalidate(new Rectangle(ArtPosition(), art.Size));
                offset.X += e.X - lastMousePos.X;
                offset.Y += e.Y - lastMousePos.Y;
                lastMousePos = e.Location;
            }
        }

        private void DrawForm_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void Elements_ElementModified(object sender, ElementEventArgs e)
        {
            Text = Drawable.ToString();
            Invalidate();
            artValidated = false;
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
            {
                Matrix m = new Matrix();
                m.Scale(scale / 100.0f, scale / 100.0f);
                canvas = Drawable.MeasureSize(0, m);
                if (canvas == RectangleF.Empty)
                    return;

                /// TODO : Limit Size
                int artW = (int)Math.Min(Math.Ceiling(canvas.Right) - Math.Floor(canvas.Left), 2048);
                int artH = (int)Math.Min(Math.Ceiling(canvas.Bottom) - Math.Floor(canvas.Top), 2048);
                art = new Bitmap(artW, artH, PixelFormat.Format32bppArgb);

                Graphics g = Graphics.FromImage(art);
                g.CompositingMode = CompositingMode.SourceOver;
                g.InterpolationMode = InterpolationMode.High;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.TranslateTransform((float)-Math.Floor(canvas.Left), (float)-Math.Floor(canvas.Top));
                g.ScaleTransform(scale / 100.0f, scale / 100.0f);
                Drawable.Draw(0, g);

                artValidated = true;
            }

            e.Graphics.DrawImage(art, ArtPosition());
        }

        private Point ArtPosition()
        {
            Point pos = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
            pos.X += offset.X;
            pos.Y += offset.Y;
            if (artValidated)
            {
                pos.X -= art.Width / 2;
                pos.Y -= art.Height / 2;
            }
            return pos;
        }
    }
}
