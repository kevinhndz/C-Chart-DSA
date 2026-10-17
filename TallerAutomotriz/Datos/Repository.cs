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
        // PilaManual conserva las operaciones con el orden LIFO.
        public readonly PilaManual<string> historial = new PilaManual<string>();
        // ColaManual conserva las placas con el orden FIFO.
        public readonly ColaManual<string> vehiculosEnFila = new ColaManual<string>();
    }
}






//El menú principal crea el Repository una vez y se lo pasa a cada módulo; el constructor recibe esa referencia para que el módulo pueda usarla.


