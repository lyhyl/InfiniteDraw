using InfiniteDraw.Draw;
using InfiniteDraw.Draw.Base;
using InfiniteDraw.Edit.Draw;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace InfiniteDraw.WorkForm
{
    public partial class ToolsForm : DockContent
    {
        private ElementStorage elements = ElementStorage.Instance;
        private IEditable editable;
        private EditableToolItem[] items;

        public ToolsForm()
        {
            InitializeComponent();
            elements.ElementSelected += Elements_ElementSelected;
        }

        private void Elements_ElementSelected(object sender, ElementEventArgs e)
        {
            IEditable edit = e.Drawable as IEditable;
            if (edit == editable)
                return;
            editable = edit;
            ClearItems();
            if (editable != null)
            {
                items = edit.EditMenu;
                RecreateItems();
            }
        }

        private void ClearItems()
        {
            Controls.Clear();
        }

        private void RecreateItems()
        {
            foreach (var item in items)
            {
                Button btn = new Button();
                btn.Text = item.Name;
                btn.Click += (s, e) => item.Callback();
                btn.Dock = DockStyle.Top;
                Controls.Add(btn);
            }
        }
    }
}
