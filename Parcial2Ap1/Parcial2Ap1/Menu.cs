using Parcial2Ap1.UI.Consultas;
using Parcial2Ap1.UI.Registros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parcial2Ap1
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            IsMdiContainer = true;
        }

        private void CategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rCategorias categorias = new rCategorias();
            categorias.MdiParent = this;
            categorias.Show();

        }


        private void RegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rRegistro registro = new rRegistro();
            registro.MdiParent = this;
            registro.Show();
        }

        private void ConsultaDeRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cRegistro consulta = new cRegistro();
            consulta.MdiParent = this;
            consulta.Show();
        }
    }
}
