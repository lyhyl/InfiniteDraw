using InfiniteDraw.Element.Draw;
using System.Collections.Generic;
using WeifenLuo.WinFormsUI.Docking;

namespace InfiniteDraw.WorkForm
{
    public class DrawForms
    {
        private Dictionary<IDrawable, DisplayForm> forms = new Dictionary<IDrawable, DisplayForm>();
        private ElementStorage elements = ElementStorage.Instance;
        private DockPanel panel;

        public DrawForms(DockPanel dp)
        {
            panel = dp;
            
            elements.ElementRemoved += ElementStorage_ElementDeleted;
            elements.RequestEditElement += ElementStorage_RequestEditElement;
        }

        private void ElementStorage_RequestEditElement(object sender, ElementStorageEventArgs e)
        {
            if (!forms.ContainsKey(e.Element))
            {
                DisplayForm drawForm = new DisplayForm(e.Element);
                drawForm.FormClosed += (s, a) => { forms.Remove(e.Element); };
                forms[e.Element] = drawForm;
                drawForm.Show(panel);
            }
            else
                forms[e.Element].Focus();
        }

        private void ElementStorage_ElementDeleted(object sender, ElementStorageEventArgs e)
        {
            if (forms.ContainsKey(e.Element))
            {
                forms[e.Element].Close();
                forms.Remove(e.Element);
            }
        }
    }
}
