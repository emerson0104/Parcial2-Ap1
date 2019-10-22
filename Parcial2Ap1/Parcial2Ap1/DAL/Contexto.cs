using Parcial2Ap1.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2Ap1.DAL
{
    public class Contexto:DbContext
    {
       
            public DbSet<Categorias> Categorias { get; set; }
        public DbSet<facturar> Facturar { get; set; }
        public Contexto() : base("ConStr")
            {

            }
        }
}
