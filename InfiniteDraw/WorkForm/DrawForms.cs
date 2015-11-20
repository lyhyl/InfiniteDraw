using InfiniteDraw.Draw;
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
        private Dictionary<IDrawable, DrawForm> forms = new Dictionary<IDrawable, DrawForm>();
        private ElementStorage elements;
        private DockPanel panel;

        public DrawForms(ElementStorage es, DockPanel dp)
        {
            elements = es;
            panel = dp;
            
            elements.ElementRemoved += Factors_FactorRemoved;
            elements.RequestEditElement += Factors_FactorActived;
        }

        private void Factors_FactorActived(object sender, ElementEventArgs e)
        {
            if (!forms.ContainsKey(e.Drawable))
            {
                DrawForm drawForm = new DrawForm(e.Drawable);
                drawForm.FormClosed += (s, a) => { forms.Remove(e.Drawable); };
                forms[e.Drawable] = drawForm;
                drawForm.Show(panel);
            }
            else
                forms[e.Drawable].Focus();
        }

        private void Factors_FactorRemoved(object sender, ElementEventArgs e)
        {
            if (forms.ContainsKey(e.Drawable))
            {
                forms[e.Drawable].Close();
                forms.Remove(e.Drawable);
            }
        }
    }
}
