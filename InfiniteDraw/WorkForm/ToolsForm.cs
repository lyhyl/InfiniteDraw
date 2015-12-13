using InfiniteDraw.Edit.Draw;
using InfiniteDraw.Element.Draw;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace InfiniteDraw.WorkForm
{
    public partial class ToolsForm : DockContent
    {
        private IEditable editable;
        private EditableToolItem[] items;

        public ToolsForm()
        {
            InitializeComponent();
            ElementStorage.Instance.ElementAdded += Elements_ElementAdded;
        }

        private void Elements_ElementAdded(object sender, ElementStorageEventArgs e)
        {
            e.Element.Actived += Element_Actived;
        }

        private void Element_Actived(object sender, EventArgs e)
        {
            IEditable target = sender as IEditable;
            if (target == editable)
                return;
            editable = target;
            ClearItems();
            if (editable != null)
            {
                items = editable.EditMenu;
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
