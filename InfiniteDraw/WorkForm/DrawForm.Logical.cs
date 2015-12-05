using InfiniteDraw.Draw;
using InfiniteDraw.Edit;
using InfiniteDraw.Edit.Draw;
using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfiniteDraw.WorkForm
{
    public partial class DrawForm
    {
        private bool draggingCavans = false;
        private Point lastMousePos = Point.Empty;

        private IEnumerable<IDraggableComponent> componentRegions;
        private IDraggableComponent draggingComponent;

        private void DrawForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (!ClientRectangle.Contains(e.Location))
                return;
            lastMousePos = e.Location;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    draggingComponent = ClickComponent(e.Location);
                    if (draggingComponent == null)
                        draggingCavans = true;
                    else
                        elements.Selected(draggingComponent as Drawable);
                    break;
                case MouseButtons.Right:
                    break;
                case MouseButtons.Middle:
                    break;
                default:
                    break;
            }
        }

        private IDraggableComponent ClickComponent(Point location)
        {
            PointF loc = location;
            loc.X -= offset.X;
            loc.Y -= offset.Y;
            loc.X -= ClientSize.Width / 2;
            loc.Y -= ClientSize.Height / 2;
            IEditable editable = Drawable as IEditable;
            if (componentRegions == null)
                componentRegions = editable?.Components;
            if (componentRegions != null)
                foreach (var comp in componentRegions)
                    if (comp.Region.Contains(loc))
                        return comp;
            return null;
        }

        private void DrawForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggingCavans)
            {
                InvalidateArt(false);
                offset.X += e.X - lastMousePos.X;
                offset.Y += e.Y - lastMousePos.Y;
            }
            if (draggingComponent != null)
            {
                InvalidateArt(true);
                Invalidate();
                draggingComponent.Drag(new Vector(e.X - lastMousePos.X, e.Y - lastMousePos.Y));
            }
            lastMousePos = e.Location;
        }

        private void DrawForm_MouseUp(object sender, MouseEventArgs e)
        {
            draggingCavans = false;
            draggingComponent = null;
        }

        private void DrawForm_MouseWheel(object sender, MouseEventArgs e)
        {
            const int WHEEL_DELTA = 120;
            scale += Math.Min(e.Delta / WHEEL_DELTA * 10, scale);
            scale = Math.Max(1, scale);
            /// TODO : Must improve it...
            Invalidate();
            InvalidateArt(true);
        }
    }
}
