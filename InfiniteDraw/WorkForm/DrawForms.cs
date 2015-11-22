using InfiniteDraw.Draw;
using InfiniteDraw.Draw.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace InfiniteDraw.WorkForm
{
    public class DrawForms
    {
        private Dictionary<Drawable, DrawForm> forms = new Dictionary<Drawable, DrawForm>();
        private ElementStorage elements;
        private DockPanel panel;

        public DrawForms(ElementStorage es, DockPanel dp)
        {
            elements = es;
            panel = dp;
            
            elements.ElementDeleted += ElementStorage_ElementDeleted;
            elements.RequestEditElement += ElementStorage_RequestEditElement;
        }

        private void ElementStorage_RequestEditElement(object sender, ElementEventArgs e)
        {
            if (!forms.ContainsKey(e.Drawable))
            {
                DrawForm drawForm = new DrawForm(elements, e.Drawable);
                drawForm.FormClosed += (s, a) => { forms.Remove(e.Drawable); };
                forms[e.Drawable] = drawForm;
                drawForm.Show(panel);
            }
            else
                forms[e.Drawable].Focus();
        }

        private void ElementStorage_ElementDeleted(object sender, ElementEventArgs e)
        {
            if (forms.ContainsKey(e.Drawable))
            {
                forms[e.Drawable].Close();
                forms.Remove(e.Drawable);
            }
        }
    }
}
