using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerAutomotriz.Formularios
{
    internal class OrdenServicio
    {

        public string numeroOrden { get; set; }
        public string codigoCliente { get; set; }
        public string placaVehiculo { get; set; }
        public DateTime fechaIngreso { get; set; }
        public string tipoServicio { get; set; }
        public string descripcion { get; set; }
        public string mecanicoAsignado { get; set; }
        public decimal costoEstimado { get; set; }
        public string estadoOrden { get; set; }

        public OrdenServicio(string numeroOrden, string codigoCliente,
            string placaVehiculo, DateTime fechaIngreso, string tipoServicio,
            string descripcion, string mecanicoAsignado, decimal costoEstimado,
            string estadoOrden)
        {
            this.numeroOrden = numeroOrden;
            this.codigoCliente = codigoCliente;
            this.placaVehiculo = placaVehiculo;
            this.fechaIngreso = fechaIngreso;
            this.tipoServicio = tipoServicio;
            this.descripcion = descripcion;
            this.mecanicoAsignado = mecanicoAsignado;
            this.costoEstimado = costoEstimado;
            this.estadoOrden = estadoOrden;
        }




    }
}
