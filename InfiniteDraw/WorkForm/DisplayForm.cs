using InfiniteDraw.Element.Draw;
using InfiniteDraw.Utilities;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace InfiniteDraw.WorkForm
{
    public partial class DisplayForm : DockContent
    {
        private Vector offset = Vector.Zero;
        private int scale = 100;
        private double scalep => scale / 100.0;

        private RectangleF canvas = RectangleF.Empty;
        private Rectangle adjustedCanvas = Rectangle.Empty;

        private Bitmap art = null;
        private bool artValidated = false;
        private bool renderView = false;

        public IDrawable Element { private set; get; }

        public DisplayForm(IDrawable d)
        {
            InitializeComponent();
            InitializeElement(d);
            RegisterEvents();
        }

        private void InitializeElement(IDrawable d)
        {
            Element = d;
            Text = d.ToString();
        }

        private void RegisterEvents()
        {
            Element.Modified += Element_Modified;
            MouseDown += DrawForm_MouseDown;
            MouseMove += DrawForm_MouseMove;
            MouseUp += DrawForm_MouseUp;
            MouseWheel += DrawForm_MouseWheel;
            KeyDown += DrawForm_KeyDown;
        }

        private void Element_Modified(object sender, EventArgs e)
        {
            // Do not check if is the same target
            // because it may refer other element
            // if other element modified, it may also modified
            Text = Element.ToString();
            InvalidateArt(true);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            Element.Active();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!artValidated)
                DrawArtImage();
            if (!adjustedCanvas.IsEmpty)
            {
                e.Graphics.Transform = Helper.ImageToDrawing(offset * scalep, ClientSize);
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
            WorkMode workMode = renderView ? WorkMode.Render : WorkMode.Edit;
            RectangleF size = Element.MeasureSize(0, new Matrix(), workMode);

            if (size.IsEmpty)
                return;

            CreateCanvas(size);
            CreateImage();
            Graphics g = CreateImageGraphics(art);
            Element.Draw(g, 0, new Matrix(), workMode);

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
