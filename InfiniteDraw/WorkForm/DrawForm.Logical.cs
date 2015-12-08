using InfiniteDraw.Draw;
using InfiniteDraw.Edit.Draw;
using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                    {
                        draggingCavans = true;
                        elements.Selected(Drawable);
                    }
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
        
        private void DrawForm_MouseMove(object sender, MouseEventArgs e)
        {
            int dx = e.X - lastMousePos.X;
            int dy = e.Y - lastMousePos.Y;
            if (draggingCavans)
            {
                InvalidateArt(false);
                offset.X += dx / scalep;
                offset.Y += dy / scalep;
            }
            if (draggingComponent != null)
            {
                InvalidateArt(true);
                draggingComponent.Drag(new Vector(dx / scalep, dy / scalep));
                elements.Modified(draggingComponent as Drawable);
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
            InvalidateArt(true);
        }

        private void DrawForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up: draggingComponent?.Drag(Vector.YAxis); break;
                case Keys.Down: draggingComponent?.Drag(-Vector.YAxis); break;
                case Keys.Left: draggingComponent?.Drag(-Vector.XAxis); break;
                case Keys.Right: draggingComponent?.Drag(Vector.XAxis); break;
            }
        }

        private IDraggableComponent ClickComponent(Point location)
        {
            PointF loc = GetImageSpaceLocation(location);
            if (componentRegions == null)
                componentRegions = (Drawable as IEditable)?.Components;
            if (componentRegions != null)
                foreach (var comp in componentRegions)
                    if (comp.Region.Contains(loc))
                        return comp;
            return null;
        }

        private PointF GetImageSpaceLocation(Point location)
        {
            PointF loc = location;
            loc.X -= ClientSize.Width / 2.0f;
            loc.Y -= ClientSize.Height / 2.0f;
            loc.X -= (float)offset.X * (float)scalep;
            loc.Y -= (float)offset.Y * (float)scalep;
            loc.X /= (float)scalep;
            loc.Y /= (float)scalep;
            return loc;
        }
    }
}
