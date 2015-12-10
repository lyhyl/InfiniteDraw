using InfiniteDraw.Draw;
using InfiniteDraw.Edit.Draw;
using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace InfiniteDraw.WorkForm
{
    public partial class DrawForm
    {
        private bool draggingCavans = false;
        private PointF prvMousePosition = Point.Empty;

        private IEnumerable<IDraggableComponent> componentRegions;
        private IDraggableComponent draggingComponent;

        private IEditable Editable => Drawable as IEditable;
        private EditState editState = EditState.Ended;

        private void DrawForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (!ClientRectangle.Contains(e.Location))
                return;
            prvMousePosition = GetImageSpaceLocation(e.Location);
            editState = Editable.EditComponentMouseDown(prvMousePosition.ToVector(), e.Button);
            SwitchCursor();
            if (editState == EditState.Ended)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        LeftMouseDown();
                        break;
                    case MouseButtons.Right:
                        break;
                    case MouseButtons.Middle:
                        break;
                    default:
                        break;
                }
                ContextMenuStrip = contextMenu;
            }
            else
            {
                ContextMenuStrip = null;
            }
        }

        private void DrawForm_MouseMove(object sender, MouseEventArgs e)
        {
            PointF curMousePosition = GetImageSpaceLocation(e.Location);
            float dx = curMousePosition.X - prvMousePosition.X;
            float dy = curMousePosition.Y - prvMousePosition.Y;
            if (draggingCavans)
            {
                InvalidateArt(false);
                offset.X += dx;
                offset.Y += dy;
            }
            if (draggingComponent != null)
            {
                InvalidateArt(true);
                draggingComponent.Drag(new Vector(dx, dy));
                elements.Modified(draggingComponent as Drawable);
            }
            switch (editState)
            {
                case EditState.Editing:
                    editState = Editable.EditComponentMouseMove(curMousePosition.ToVector());
                    InvalidateArt(true);
                    break;
            }
            SwitchCursor();
            prvMousePosition = GetImageSpaceLocation(e.Location);
        }

        private void DrawForm_MouseUp(object sender, MouseEventArgs e)
        {
            draggingCavans = false;
            draggingComponent = null;
            switch (editState)
            {
                case EditState.Editing:
                    editState = Editable.EditComponentMouseUp(prvMousePosition.ToVector(), e.Button);
                    InvalidateArt(true);
                    break;
            }
            SwitchCursor();
        }

        private void DrawForm_MouseWheel(object sender, MouseEventArgs e)
        {
            const int WHEEL_DELTA = 120;
            const int UNIT_SIZE = 10;
            scale += Math.Min(e.Delta / WHEEL_DELTA * UNIT_SIZE, scale);
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

        private void LeftMouseDown()
        {
            draggingComponent = ClickComponent(prvMousePosition);
            if (draggingComponent == null)
            {
                draggingCavans = true;
                elements.Selected(Drawable);
            }
            else
                elements.Selected(draggingComponent as Drawable);
        }

        private IDraggableComponent ClickComponent(PointF location)
        {
            if (componentRegions == null)
                componentRegions = (Drawable as IEditable)?.Components;
            if (componentRegions != null)
                foreach (var comp in componentRegions)
                    if (comp.Region.Contains(location))
                        return comp;
            return null;
        }

        private PointF GetImageSpaceLocation(Point location)
        {
            Matrix m = DrawingSpaceMatrix;
            m.Invert();
            PointF[] loc = new PointF[] { location };
            m.TransformPoints(loc);
            loc[0].X /= (float)scalep;
            loc[0].Y /= (float)scalep;
            return loc[0];
        }

        private void SwitchCursor()
        {
            switch (editState)
            {
                case EditState.Editing:
                    Cursor = Cursors.Cross;
                    break;
                case EditState.Ended:
                    Cursor = Cursors.Default;
                    break;
                default:
                    break;
            }
        }
    }
}
