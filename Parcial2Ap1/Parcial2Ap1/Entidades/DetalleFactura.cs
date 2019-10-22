using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2Ap1.Entidades
{
   public  class DetalleFactura
    {
        public int DetalleId { get; set; }
        public int FacturaId {get; set; }
        public int CategoriaId { get; set; }
        public decimal  Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Importe { get; set; }

        
        public DetalleFactura()
        {
            DetalleId = 0;
            CategoriaId = 0;
            FacturaId = 0;
            Precio = 0;
            Cantidad = 0;
            Importe = 0;


        }
        

        

        public DetalleFactura(int id, int facturaID, int Combo, int cantidad, decimal precio, decimal importe)
        {
           DetalleId = id;
            FacturaId = facturaID;
            CategoriaId = Combo;
            Cantidad = cantidad;
            Precio = precio;
            Importe = importe;
        }
    }
}

