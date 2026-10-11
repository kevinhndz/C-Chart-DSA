using System;

namespace TallerAutomotriz.Formularios
{
    internal class ClienteVehiculo
    {
        // Propiedades del cliente.
        public int CodigoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string TelefonoCliente { get; set; }

        // Propiedades del vehiculo.
        public string PlacaVehiculo { get; set; }
        public string MarcaVehiculo { get; set; }
        public string ModeloVehiculo { get; set; }
        public int AnioVehiculo { get; set; }

        // Se utiliza DateTime para manejar correctamente las fechas.
        public DateTime FechaVehiculo { get; set; }

        public ClienteVehiculo(
            int codigoCliente,
            string nombreCliente,
            string telefonoCliente,
            string placaVehiculo,
            string marcaVehiculo,
            string modeloVehiculo,
            int anioVehiculo,
            DateTime fechaVehiculo)
        {
            CodigoCliente = codigoCliente;
            NombreCliente = nombreCliente;
            TelefonoCliente = telefonoCliente;
            PlacaVehiculo = placaVehiculo;
            MarcaVehiculo = marcaVehiculo;
            ModeloVehiculo = modeloVehiculo;
            AnioVehiculo = anioVehiculo;
            FechaVehiculo = fechaVehiculo;
        }
    }
}