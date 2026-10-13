using System.Collections.Generic;
using TallerAutomotriz.Formularios;

namespace TallerAutomotriz.Datos
{
    // Guarda los datos en memoria mientras el programa esta abierto.
    internal class Repository
    {
        // readonly evita reemplazar las colecciones, pero permite agregar y quitar datos.
        public readonly List<ClienteVehiculo> registros = new List<ClienteVehiculo>();
        public readonly List<OrdenServicio> ordenes = new List<OrdenServicio>();
        public readonly Queue<string> vehiculosEnFila = new Queue<string>();
    }
}
