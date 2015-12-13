using InfiniteDraw.Edit.Property;
using InfiniteDraw.Element.Draw;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace InfiniteDraw.WorkForm
{
    public partial class PropertyForm : DockContent
    {
        private IPropertyEditable editableProperties;

        public PropertyForm()
        {
            InitializeComponent();

            ElementStorage.Instance.ElementAdded += Instance_ElementAdded;
        }

        private void Instance_ElementAdded(object sender, ElementStorageEventArgs e)
        {
            e.Element.Actived += Element_Actived;
            e.Element.Modified += Element_Modified;
        }
        
        private void Element_Actived(object sender, EventArgs e)
        {
            IPropertyEditable target = sender as IPropertyEditable;
            if (target == editableProperties)
                return;
            editableProperties = target;
            RecreateTable();
        }

        private void Element_Modified(object sender, EventArgs e)
        {
            IPropertyEditable target = sender as IPropertyEditable;
            if (target == editableProperties)
                RefreshTable();
        }
        
        private void RecreateTable()
        {
            SuspendLayout();

            propertyNamePanel.Controls.Clear();
            propertyValuePanel.Controls.Clear();
            CreateTable();

            ResumeLayout(false);
        }

        private void CreateTable()
        {
            if (editableProperties == null)
                return;

            foreach (var p in editableProperties.EditableProperties)
            {
                Label nameLabel = new Label();
                nameLabel.Dock = DockStyle.Top;
                nameLabel.Text = p.Name;
                nameLabel.TextAlign = ContentAlignment.MiddleLeft;
                propertyNamePanel.Controls.Add(nameLabel);

                Control ctrl = CreateTerm(p);
                ctrl.Tag = p;
                propertyValuePanel.Controls.Add(ctrl);
            }
        }

        private void RefreshTable()
        {
            foreach (Control ctrl in propertyValuePanel.Controls)
                RefreshTerm(ctrl);
        }

        private void RefreshTerm(Control ctrl)
        {
            ElementProperty p = ctrl.Tag as ElementProperty;
            if (p.Type == typeof(string))
            {
                TextBox ri = ctrl as TextBox;
                ri.Text = Convert.ToString(p.Getter());
            }
            else if (p.Type == typeof(int) || p.Type == typeof(double) || p.Type == typeof(float))
            {
                NumericUpDown ri = ctrl as NumericUpDown;
                ri.Value = Convert.ToDecimal(p.Getter());
            }
            else if (p.Type.IsEnum)
            {
                ComboBox ri = ctrl as ComboBox;
                ri.SelectedValue = p.Getter();
            }
            else
            {
            }
        }

        private Control CreateTerm(ElementProperty p)
        {
            if (p.Type == typeof(string))
            {
                TextBox ri = new TextBox()
                {
                    Dock = DockStyle.Top,
                    Text = Convert.ToString(p.Getter())
                };
                ri.TextChanged += (s, e) =>
                {
                    if (!p.Setter(ri.Text))
                    {
                        p.Setter(p.Default);
                        ri.Text = Convert.ToString(p.Default);
                    }
                    //elements.Modified(editableProperties as IElement);
                };
                return ri;
            }
            else if (p.Type == typeof(int) || p.Type == typeof(double) || p.Type == typeof(float))
            {
                NumericUpDown ri = new NumericUpDown()
                {
                    Dock = DockStyle.Top,
                    DecimalPlaces = p.Type == typeof(int) ? 0 : 3,
                    Minimum = decimal.MinValue,
                    Maximum = decimal.MaxValue,
                    Value = Convert.ToDecimal(p.Getter())
                };
                ri.ValueChanged += (s, e) =>
                {
                    if (!p.Setter(ri.Value))
                    {
                        p.Setter(p.Default);
                        ri.Value = Convert.ToDecimal(p.Default);
                    }
                    //elements.Modified(editableProperties as IElement);
                };
                return ri;
            }
            else if (p.Type.IsEnum)
            {
                ComboBox ri = new ComboBox()
                {
                    Dock = DockStyle.Top,
                    FormattingEnabled = true
                };
                /// TODO
                ri.SelectedValueChanged += (s, e) =>
                {
                    if (!p.Setter(ri.SelectedValue))
                    {
                        p.Setter(p.Default);
                        ri.SelectedValue = p.Default;
                    }
                    //elements.Modified(editableProperties as IElement);
                };
                return ri;
            }
            else
            {
                return new Label()
                {
                    Dock = DockStyle.Top,
                    Text = "Unsupported Type",
                    TextAlign = ContentAlignment.MiddleLeft
                };
            }
        }
    }
}
