﻿using FacturacionAplicado.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacturacionAplicado.UI.Registros
{
    public partial class rProducto : Form
    {
        public rProducto()
        {
            InitializeComponent();
            
            AgregarAComboBox();
        }

        public void LLenarCombo()
        {
            DepartamentocomboBox.Items.Clear();
            foreach (var tel in BLL.DepartamentoBLL.Buscar())
            {
                DepartamentocomboBox.Items.Add(tel.Nombre);

            }
        }
        private bool SetError(int error)
        {
            bool paso = false;
            if (error == 1 && IDcomboBox.Text == string.Empty)
            {
                IderrorProvider.SetError(IDcomboBox, "Llenar Id");
                paso = true;
            }
            if (error == 2 && PrecionumericUpDown.Value == 0)
            {
                DemasCamposerrorProvider.SetError(PrecionumericUpDown, "LLenar Precio");
                paso = true;
            }
            if (error == 2 && DepartamentocomboBox.Text == string.Empty)
            {
                DemasCamposerrorProvider.SetError(DepartamentocomboBox, "LLenar Departamento");
                paso = true;
            }
            if (error == 2 && CantidadnumericUpDown.Value == 0)
            {
                DemasCamposerrorProvider.SetError(CantidadnumericUpDown, "LLenar Cantidad");
                paso = true;
            }
            if (error == 2 && DescripciontextBox.Text == string.Empty)
            {
                DemasCamposerrorProvider.SetError(DescripciontextBox, "LLenar Descripcion");
                paso = true;
            }


            return paso;
        }

        private void LimpiarProvider()
        {
            IderrorProvider.Clear();
            DemasCamposerrorProvider.Clear();
        }

        private Producto LlenaClase()
        {
            Producto producto = new Producto();
            if (IDcomboBox.Text == string.Empty)
            {
                producto.id = 0;
            }
            else
            {
                producto.id = Convert.ToInt32(IDcomboBox.Text);
            }
            producto.Descripcion = DescripciontextBox.Text;
            producto.Cantidad = Convert.ToInt32(CantidadnumericUpDown.Value);
            producto.Precio = PrecionumericUpDown.Value;
            producto.DepartamentoId = DepartamentocomboBox.SelectedIndex + 1;
            return producto;

        }

        public void AgregarAComboBox()
        {
            IDcomboBox.Items.Clear();
            DepartamentocomboBox.Items.Clear();
            foreach (var tel in BLL.DepartamentoBLL.Buscar())
            {
                DepartamentocomboBox.Items.Add(tel.Nombre);

            }

            foreach (var item in BLL.ProductoBLL.Buscar())
            {
                IDcomboBox.Items.Add(item.id);
            }

        }

        private void Clear()
        {
            LimpiarProvider();
            IDcomboBox.Text = string.Empty;
            PrecionumericUpDown.Value = 0;
            DepartamentocomboBox.Text = string.Empty;
            CantidadnumericUpDown.Value = 0;
            DescripciontextBox.Clear();
        }

        private void IDcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarProvider();

            foreach (var producto in BLL.ProductoBLL.Buscar(IDcomboBox.Text))
            {

                PrecionumericUpDown.Value = producto.Precio;
                CantidadnumericUpDown.Value = producto.Cantidad;
                foreach (var departamento in BLL.DepartamentoBLL.Buscar(producto.DepartamentoId.ToString()))
                {
                    DepartamentocomboBox.Text = departamento.Nombre;
                }

                DescripciontextBox.Text = producto.Descripcion;
            }
        }

        private void GuardarButton_Click(object sender, EventArgs e)
        {
            LimpiarProvider();
            if (SetError(2))
            {
                MessageBox.Show("LLenar Los Campos Vacios");
                return;
            }
            Producto product = LlenaClase();
            if (IDcomboBox.Text == string.Empty)
            {
                if (BLL.ProductoBLL.Guardar(product))
                {
                    MessageBox.Show("Guardado!!");
                    Clear();
                    AgregarAComboBox();
                    
                    return;
                }
                else
                {
                    MessageBox.Show("No se Guardo!!");
                    return;
                }
            }
            else
            {
                if (BLL.ProductoBLL.Modificar(LlenaClase()))
                {
                    MessageBox.Show("Modificado!!");
                    return;
                }
                else
                {
                    MessageBox.Show("No se pudo modificar!!");
                    return;
                }
            }
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            LimpiarProvider();
            if (SetError(1))
            {
                MessageBox.Show("Llenar campo Id");
                return;
            }

            if (BLL.ProductoBLL.Eliminar(IDcomboBox.Text))
            {
                MessageBox.Show("Eliminado!!");
                Clear();
                AgregarAComboBox();
                return;
            }
            else
            {
                MessageBox.Show("No se pudo eliminar!!");
                return;
            }
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Clear();
        }

       

        
    }
}
