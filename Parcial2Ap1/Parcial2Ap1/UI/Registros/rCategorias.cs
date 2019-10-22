using Parcial2Ap1.BLL;
using Parcial2Ap1.DAL;
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
using Parcial2Ap1.UI.Registros;
namespace Parcial2Ap1.UI.Registros
{
    public partial class rCategorias : Form
    {
        RepositorioBase<Categorias> repositorio;
        public rCategorias()
        {
          repositorio = new RepositorioBase<Categorias>();
            InitializeComponent();
        }
        private void Limpiar()
        {
            IDnumericUpDown.Value = 0;
            DescripciontextBox.Text = string.Empty;
        }
        private bool Existe()
        {
            Categorias categoria = repositorio.Buscar((int)IDnumericUpDown.Value);

            return (categoria != null);
        }
        private bool Validar()
        {
            bool realizado = true;
           errorProvider1.Clear();

            if (string.IsNullOrWhiteSpace(DescripciontextBox.Text))
            {
                errorProvider1.SetError(DescripciontextBox , "EL CAMPO NOMBRE NO PUEDE ESTAR VACIO");
                DescripciontextBox.Focus();
                realizado = false;
            }

            return realizado;
        }
        private Categorias LlenaClase()
        {
           Categorias categoria = new Categorias();

            categoria.CategoriaId = Convert.ToInt32(IDnumericUpDown.Value);
            categoria.Descripcion  = DescripciontextBox.Text;

            return categoria;
        }
        private void LlenaCampos(Categorias categoria)
        {
            IDnumericUpDown.Value = categoria.CategoriaId;
            DescripciontextBox.Text = categoria.Descripcion;
        }
       
      
        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Categorias categoria = new Categorias();
            bool Paso = false;

            if (!Validar())
                return;

            categoria = LlenaClase();


            if (IDnumericUpDown.Value == 0)
          Paso= repositorio.Guardar(categoria);
            else
            {
                if (!Existe())
                {
                    MessageBox.Show("NO SE PUEDE MODIFICAR UNA CATEGORIA INEXISTENTE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
             Paso= repositorio.Modificar(categoria);
            }

            if (Paso)
            {
                Limpiar();
                MessageBox.Show("GUARDADA EXITOSAMENTE", "GUARDADA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("NO SE PUDO GUARDAR", "NO GUARDADA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
           
            int id;
            int.TryParse(IDnumericUpDown.Text, out id);
            Contexto db = new Contexto();

            
            if (!Existe())
            {
                MessageBox.Show("No se puede eliminar un dato que no exista");
                return;
            }Limpiar();
            if (repositorio.Eliminar(id))
            {
                MessageBox.Show("Eliminada correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                errorProvider1.SetError(IDnumericUpDown, "No se puede eliminar una categoria inexistente");
            }
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id;
            Categorias categoria = new Categorias();

            repositorio = new RepositorioBase<Categorias>();
            int.TryParse(IDnumericUpDown.Text, out id);

            Limpiar();

            categoria = repositorio.Buscar(id);

            if (categoria != null)
            {
                LlenaCampos(categoria);
            }
            else
            {
                MessageBox.Show("Categoria no encontrada");
            }
        }
    }
}

