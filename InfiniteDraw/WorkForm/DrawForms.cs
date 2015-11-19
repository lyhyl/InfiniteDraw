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
        private Dictionary<Factor, DrawForm> forms = new Dictionary<Factor, DrawForm>();
        private FactorStorage factors;
        private DockPanel panel;

        public DrawForms(FactorStorage fs, DockPanel dp)
        {
            factors = fs;
            panel = dp;
            
            factors.FactorRemoved += Factors_FactorRemoved;
            factors.FactorActived += Factors_FactorActived;
        }

        private void Factors_FactorActived(object sender, FactorEventArgs e)
        {
            if (!forms.ContainsKey(e.Factor))
            {
                DrawForm drawForm = new DrawForm(e.Factor);
                drawForm.FormClosed += (s, a) => { forms.Remove(e.Factor); };
                forms[e.Factor] = drawForm;
                drawForm.Show(panel);
            }
            else
                forms[e.Factor].Focus();
        }

        private void Factors_FactorRemoved(object sender, FactorEventArgs e)
        {
            if (forms.ContainsKey(e.Factor))
            {
                forms[e.Factor].Close();
                forms.Remove(e.Factor);
            }
        }
    }
}
