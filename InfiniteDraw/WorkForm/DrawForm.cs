﻿using InfiniteDraw.Draw;
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
using WeifenLuo.WinFormsUI.Docking;

namespace InfiniteDraw.WorkForm
{
    public partial class DrawForm : DockContent
    {
        private ElementStorage elements = ElementStorage.Instance;
        private Vector offset = Vector.Zero;
        private int scale = 100;
        private double scalep => scale / 100.0;

        private RectangleF canvas = RectangleF.Empty;
        private Rectangle adjustedCanvas = Rectangle.Empty;

        private Bitmap art = null;
        private bool artValidated = false;
        private bool renderView = false;

        public Drawable Drawable { private set; get; }

        private Matrix DrawingSpaceMatrix
        {
            get
            {
                Matrix m = new Matrix();
                m.Translate(ClientSize.Width / 2.0f, ClientSize.Height / 2.0f);
                m.Scale(1, -1);
                m.Translate((float)(offset.X * scalep), (float)(offset.Y * scalep));
                return m;
            }
        }

        public DrawForm(Drawable d)
        {
            InitializeComponent();
            InitializeElement(d);
            RegisterEvents();
        }

        private void InitializeElement(Drawable d)
        {
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
                e.Graphics.Transform = DrawingSpaceMatrix;
                DrawAxes(e.Graphics);
                e.Graphics.DrawImage(art, new Point(adjustedCanvas.Left, adjustedCanvas.Top));
            }
        }

        private void DrawAxes(Graphics g)
        {
            g.DrawLine(Pens.Red, new Point(0, 0), new Point(0, ClientSize.Height));
            g.DrawLine(Pens.Green, new Point(0, 0), new Point(ClientSize.Width, 0));
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
            trans.Translate(-adjustedCanvas.Left, -adjustedCanvas.Top);
            trans.Scale((float)scalep, (float)scalep);
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

        private void resetViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            offset = Vector.Zero;
            scale = 100;
            InvalidateArt(true);
        }
    }
}
