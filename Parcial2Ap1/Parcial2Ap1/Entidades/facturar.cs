using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2Ap1.Entidades
{
    public class facturar
    {
        [Key]
        public int FacturaId { get; set; }
        public int CategoriaId { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Importe { get; set; }
        public decimal Total { get; set; }

        public virtual List<DetalleFactura>Detalles{ get; set; }

        public facturar(int FacturaId, DateTime Fecha, int Cantidad, decimal Precio, decimal Importe, decimal Total)
        {
            this.Importe = Importe;
            this.FacturaId = FacturaId;
            this.Fecha = Fecha;
            this.Cantidad = Cantidad;
            this.Precio = Precio;
            this.Total = Total;
            this.Detalles = Detalles;


        }
        public facturar()
        {

        }



}
}