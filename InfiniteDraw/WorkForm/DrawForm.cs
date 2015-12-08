using InfiniteDraw.Draw;
using InfiniteDraw.Draw.Base;
using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;

namespace InfiniteDraw.WorkForm
{
    public partial class DrawForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private ElementStorage elements;
        private Vector offset = Vector.Zero;
        private int scale = 100;
        private double scalep => scale / 100.0;

        private RectangleF canvas = RectangleF.Empty;
        private Rectangle adjustedCanvas = Rectangle.Empty;

        private Bitmap art = null;
        private bool artValidated = false;
        private bool renderView = false;

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
            KeyDown += DrawForm_KeyDown;
        }

        private void Elements_ElementModified(object sender, ElementEventArgs e)
        {
            Text = Drawable.ToString();
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
                DrawArtImage();
            if (!adjustedCanvas.IsEmpty)
            {
                e.Graphics.TranslateTransform(ClientSize.Width / 2.0f, ClientSize.Height / 2.0f);
                e.Graphics.TranslateTransform(adjustedCanvas.Left, adjustedCanvas.Top);
                e.Graphics.TranslateTransform((float)(offset.X * scalep), (float)(offset.Y * scalep));
                e.Graphics.DrawImage(art, new Point());
            }
        }

        private void DrawArtImage()
        {
            RectangleF size = Drawable.MeasureSize(renderView ? WorkMode.Render : WorkMode.Edit);

            if (size.IsEmpty)
                return;

            CreateCanvas(size);
            CreateImage();
            Graphics g = CreateImageGraphics(art);
            Drawable.Draw(g, renderView ? WorkMode.Render : WorkMode.Edit);

            artValidated = true;
        }

        private void CreateCanvas(RectangleF size)
        {
            const int MaxSize = 2048;
            float s = (float)scalep;
            canvas = new RectangleF(size.Left * s, size.Top * s, size.Width * s, size.Height * s);
            int al = (int)Math.Floor(canvas.Left), ar = (int)Math.Ceiling(canvas.Right);
            int at = (int)Math.Floor(canvas.Top), ab = (int)Math.Ceiling(canvas.Bottom);
            adjustedCanvas = new Rectangle(al, at, Math.Min(ar - al, MaxSize), Math.Min(ab - at, MaxSize));
        }

        private void CreateImage()
        {
            art = new Bitmap(adjustedCanvas.Width, adjustedCanvas.Height, PixelFormat.Format32bppArgb);
        }

        private Graphics CreateImageGraphics(Bitmap image)
        {
            Graphics g = Graphics.FromImage(image);
            g.CompositingMode = CompositingMode.SourceOver;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            Matrix trans = new Matrix();

            float s = (float)scalep;
            trans.Translate(-adjustedCanvas.Left, -adjustedCanvas.Top);
            trans.Scale(s, s);

            g.Transform = trans;

            return g;
        }

        private void InvalidateArt(bool redrawElement)
        {
            artValidated = !redrawElement;
            Invalidate();
            // TODO: invalidate region only
        }

        private void editViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editViewToolStripMenuItem.Checked = !editViewToolStripMenuItem.Checked;
            renderView = !editViewToolStripMenuItem.Checked;
            InvalidateArt(true);
        }
    }
}
