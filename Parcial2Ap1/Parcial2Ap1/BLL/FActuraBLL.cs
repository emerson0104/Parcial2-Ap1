using Parcial2Ap1.DAL;
using Parcial2Ap1.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial2Ap1.BLL
{
    public class FActuraBLL
    {
        public static bool Guardar(facturar factura)
        {
            Contexto db = new Contexto();
            bool paso = false;
            try
            {
                if (db.Facturar.Add(factura) != null)
                {
                    paso = db.SaveChanges() > 0;

                        }
            }
            catch (Exception)
            {

                throw;
            
            
            }
            finally
            {
                db.Dispose();

            }
            return paso;
        }
        public static bool Modificar(facturar factura)
        { Contexto db = new Contexto();
            bool paso = false;
            try
            {
                var anterior = db.Facturar.Find(factura.FacturaId);
                foreach (var item in anterior.Detalles)
                {
                    if (factura.Detalles.Exists(d=>d.DetalleId == item.DetalleId))
                    {
                        db.Entry(factura).State = EntityState.Deleted;
                    }
                    db.Entry(factura).State = EntityState.Modified;
                    paso = db.SaveChanges() > 0;
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }
        public static facturar Buscar(int id)
        {
            Contexto db = new Contexto();
            facturar factu = new facturar();
            try
            {
               factu = db.Facturar.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }
            return factu;


        }
        public static bool Eliminar(int id)
        {
            Contexto db = new Contexto();
            bool paso = false;
            facturar fac = new facturar();
            try
            {
                var anterior = db.Facturar.Find(id);
                db.Entry(fac).State = EntityState.Deleted;
                paso= db.SaveChanges() > 0;


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();

            }
            return paso;


        }

        public static decimal CalcularImporte(decimal precio, int cantidad)
        {
            return Convert.ToDecimal(precio) * Convert.ToInt32(cantidad);
        }

    }
}
