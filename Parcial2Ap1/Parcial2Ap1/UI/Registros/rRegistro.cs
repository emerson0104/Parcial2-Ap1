using Parcial2Ap1.BLL;
using Parcial2Ap1.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial2Ap1.UI.Registros
{
    public partial class rRegistro : Form
    {
        decimal Total = 0;
        public List<DetalleFactura> Detalle { get; set; }
        public rRegistro()
        {
            this.Detalle = new List<DetalleFactura>();
            InitializeComponent();
            LlenaComboBox();


        }

        private void LlenaComboBox()
        {
            RepositorioBase<facturar> repositorio = new RepositorioBase<facturar>();
            List<facturar> lista = new List<facturar>();
            lista = repositorio.GetList(p => true);

            CategoriacomboBox.DataSource = lista;
            CategoriacomboBox.ValueMember = "CategoriaId";
            CategoriacomboBox.DisplayMember = "Descripcion";
        }


        public void limpiar()
        {
            EstudiantetextBox1.Text = string.Empty;
            CategoriacomboBox.Text = string.Empty;
            ImportetextBox.Text = string.Empty;
            PreciotextBox.Text = string.Empty;
            numericUpDownCantida.Value = 0;


        }
        private facturar LLenaclase()
        {

            facturar fact = new facturar();

            fact.FacturaId = Convert.ToInt32(IDnumericUpDown.Value);
            fact.CategoriaId = (int)CategoriacomboBox.SelectedValue;
            fact.Fecha = FechadateTimePicker.Value;
            fact.Cantidad = Convert.ToInt32(numericUpDownCantida.Text);

            fact.Detalles = this.Detalle;

            return fact;
        }
        private void LLenaCampo(facturar f)
        {
            IDnumericUpDown.Value = Convert.ToInt32(f.FacturaId);
            CategoriacomboBox.SelectedValue = Convert.ToInt32(f.CategoriaId);
            FechadateTimePicker.Value = f.Fecha;
            TotaltextBox.Text = f.Total.ToString();
            DetalledataGridView.DataSource = f.Detalles;


        }

        private bool Existe()
        {
            facturar f = FActuraBLL.Buscar((int)IDnumericUpDown.Value);
            return (f != null);
        }
        private bool Validar(int error)
        {
            bool paso = false;

            if (error == 1 && IDnumericUpDown.Value == 0)
            {
                errorProvider1.SetError(IDnumericUpDown,
                  "Favor Introduzca Un Id");
                paso = true;
            }
            if (error == 2 && string.IsNullOrWhiteSpace(TotaltextBox.Text))
            {
                errorProvider1.SetError(TotaltextBox,
                   "Favor Introducir SubTotal!");
                paso = true;
            }


            if (error == 2 && DetalledataGridView.RowCount == 0)
            {
                errorProvider1.SetError(DetalledataGridView,
                    "Es obligatorio Agregar una categoria");
                paso = true;
            }

            if (error == 3 && string.IsNullOrEmpty(ImportetextBox.Text))
            {
                errorProvider1.SetError(ImportetextBox,
                    "Es obligatorio Agregar una Categoria");
                paso = true;
            }

            return paso;
        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            if (Validar(2))
            {
                MessageBox.Show("Debe Agregar Algun Producto al Grid", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                facturar fa = LLenaclase();
                bool Paso = false;

                if (IDnumericUpDown.Value == 0)
                {
                    Paso = FActuraBLL.Guardar(fa);
                    errorProvider1.Clear();
                }
                else
                {
                    int id = Convert.ToInt32(IDnumericUpDown.Value);
                    var mantenimientos = FActuraBLL.Buscar(id);
                    if (mantenimientos != null)
                    {
                        Paso = FActuraBLL.Modificar(fa);
                    }
                    errorProvider1.Clear();
                }

                if (Paso)
                {
                    limpiar();
                    MessageBox.Show("Guardado!!", "Exito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("No se pudo guardar!!", "Fallo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Agregarbutton_Click(object sender, EventArgs e)
        {
            List<DetalleFactura> Detalle = new List<DetalleFactura>();

            if (DetalledataGridView.DataSource != null)
            {
                Detalle = (List<DetalleFactura>)DetalledataGridView.DataSource;
            }
            if (string.IsNullOrEmpty(ImportetextBox.Text))
            {
                MessageBox.Show("EL Importe Se Encuentra vacio",
                    "Verificacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.Detalle.Add(new DetalleFactura(
                    id: 0,
                   facturaID: Convert.ToInt32(IDnumericUpDown.Value),
                    Combo: (int)CategoriacomboBox.SelectedValue,
                    cantidad: Convert.ToInt32(numericUpDownCantida.Value),
                    precio: Convert.ToDecimal(PreciotextBox.Text),
                    importe: Convert.ToDecimal(ImportetextBox)
                 )
             );
                
                foreach (var item in Detalle)
                {
                    Total += item.Importe;
                }





                TotaltextBox.Text = Total.ToString();

                // Cargar el detalle al Grid
                DetalledataGridView.DataSource = null;
                DetalledataGridView.DataSource = Detalle;
            
        }
        }
        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(IDnumericUpDown.Value);
            facturar fa = FActuraBLL.Buscar(id);

            if (fa != null)
            {
                LLenaCampo(fa);

            }
            else
                MessageBox.Show("No se encontro!", "Fallo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            if (Validar(1))
            {
                MessageBox.Show("Favor Llenar Casilla!", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int id = Convert.ToInt32(IDnumericUpDown.Value);
                if (FActuraBLL.Eliminar(id))
                {
                    MessageBox.Show("Eliminado!!", "Exito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                }
                else
                    MessageBox.Show("No se pudo eliminar!!", "Fallo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Removerbutton_Click(object sender, EventArgs e)
        {
            if (DetalledataGridView.Rows.Count > 0 && DetalledataGridView.CurrentRow != null)
            {
                List<DetalleFactura> detalle = (List<DetalleFactura>)DetalledataGridView.DataSource;

                //remover la fila
                detalle.RemoveAt(DetalledataGridView.CurrentRow.Index);


                foreach (var item in detalle)
                {
                    Total += item.Importe;
                }





                TotaltextBox.Text = Total.ToString();

                // Cargar el detalle al Grid
                DetalledataGridView.DataSource = null;
                DetalledataGridView.DataSource = detalle;
            }
        }

        private void NumericUpDownCantida_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void PreciotextBox_TextChanged(object sender, EventArgs e)
        {
            ImportetextBox.Text = FActuraBLL.CalcularImporte(Convert.ToDecimal(PreciotextBox.Text),
               Convert.ToInt32(numericUpDownCantida.Value)).ToString();
        }
    }
    }


