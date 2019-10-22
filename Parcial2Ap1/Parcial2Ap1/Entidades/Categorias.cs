using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2Ap1.Entidades
{
    public class Categorias
    {
        
            [Key]
            public int CategoriaId { get; set; }
            public string Descripcion { get; set; }
            /* public Categorias(int CategoriaId, string Descripcion)
             {
                 CategoriaId = 0;
                 Descripcion = string.Empty;
             }*/
            public Categorias(int categoriaID, string nombre)
            {
                CategoriaId = categoriaID;
                Descripcion = nombre;
            }

            public Categorias()
            {

            }
        }



    }
    

