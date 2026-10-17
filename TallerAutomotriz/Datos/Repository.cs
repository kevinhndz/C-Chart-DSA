using System.Collections.Generic;
using TallerAutomotriz.Formularios;

namespace TallerAutomotriz.Datos
{
    
    internal class Repository
    {
        
        public readonly List<ClienteVehiculo> registros = new List<ClienteVehiculo>();
        public readonly List<OrdenServicio> ordenes = new List<OrdenServicio>();
        
        public readonly PilaManual historial = new PilaManual();
        
        public readonly ColaManual vehiculosEnFila = new ColaManual();
    }
}









