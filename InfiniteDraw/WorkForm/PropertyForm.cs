﻿using InfiniteDraw.Draw.Base;
using InfiniteDraw.Edit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace InfiniteDraw.WorkForm
{
    public partial class PropertyForm : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private ElementStorage elements;
        private IPropertyEditable editableProperties;

        public PropertyForm(ElementStorage es)
        {
            InitializeComponent();

            elements = es;
            elements.ElementSelected += Elements_ElementSelected;
        }

        private void Elements_ElementSelected(object sender, ElementEventArgs e)
        {
            IPropertyEditable newValue = e.Drawable as IPropertyEditable;
            if (newValue == editableProperties)
                return;
            editableProperties = newValue;
            Console.WriteLine(editableProperties);
            RecreateTable();
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
                Label left = new Label();
                left.Dock = DockStyle.Top;
                left.Text = p.Name;
                left.TextAlign = ContentAlignment.MiddleLeft;
                propertyNamePanel.Controls.Add(left);

                Control right;
                CreateTerm(p, out right);
                propertyValuePanel.Controls.Add(right);
            }
        }

        private void CreateTerm(ElementProperty p, out Control right)
        {
            if (p.Type == typeof(string))
            {
                TextBox ri = new TextBox();
                ri.Dock = DockStyle.Top;
                ri.Text = p.Getter() as string;
                ri.TextChanged += (s, e) => {
                    if (!p.Setter(ri.Text))
                        p.Setter(p.Default);
                };
                right = ri;
            }
            else if (p.Type == typeof(int))
            {
                NumericUpDown ri = new NumericUpDown();
                ri.Dock = DockStyle.Top;
                ri.Value = (int)p.Getter();
                ri.ValueChanged += (s, e) => {
                    if (!p.Setter((int)ri.Value))
                        p.Setter(p.Default);
                };
                right = ri;
            }
            else if(p.Type.IsEnum)
            {
                ComboBox ri = new ComboBox();
                ri.Dock = DockStyle.Top;
                ri.FormattingEnabled = true;
                /// TODO
                ri.SelectedValueChanged += (s, e) => {
                    if (!p.Setter(ri.SelectedValue))
                        p.Setter(p.Default);
                };
                right = ri;
            }
            else
            {
                Label ri = new Label();
                ri.Dock = DockStyle.Top;
                ri.Text = "Unsupported Type";
                ri.TextAlign = ContentAlignment.MiddleLeft;
                right = ri;
            }
        }
    }
}
