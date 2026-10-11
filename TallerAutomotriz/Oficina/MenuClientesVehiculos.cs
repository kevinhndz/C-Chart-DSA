using System;
using System.Collections.Generic;
using System.Linq;
using TallerAutomotriz.Formularios;

namespace TallerAutomotriz.Oficina
{
    internal class MenuClientesVehiculos
    {
        // Lista que almacena temporalmente los clientes y vehículos registrados.
        private readonly List<ClienteVehiculo> registros =
            new List<ClienteVehiculo>();

        public void MostrarMenu()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();

                Console.WriteLine("=================================");
                Console.WriteLine("      CLIENTES Y VEHÍCULOS");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Registrar cliente y vehículo");
                Console.WriteLine("2. Mostrar registros");
                Console.WriteLine("3. Buscar registro");
                Console.WriteLine("4. Regresar al menú principal");
                Console.WriteLine("=================================");

                int opcion = LeerEntero("Seleccione una opción: ");

                switch (opcion)
                {
                    case 1:
                        Registrar();
                        break;

                    case 2:
                        MostrarRegistros();
                        break;

                    case 3:
                        MostrarMenuBusqueda();
                        break;

                    case 4:
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        Pausar();
                        break;
                }
            }
        }

        private void Registrar()
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("   REGISTRAR CLIENTE Y VEHÍCULO");
            Console.WriteLine("=================================");

            int codigoCliente = LeerEntero("Código del cliente: ");

            // Evita registrar dos clientes con el mismo codigo.
            if (registros.Any(r => r.CodigoCliente == codigoCliente))
            {
                Console.WriteLine("Ya existe un cliente con ese código.");
                Pausar();
                return;
            }

            string nombreCliente = LeerTexto("Nombre del cliente: ");
            string telefonoCliente = LeerTexto("Teléfono del cliente: ");
            string placaVehiculo = LeerTexto("Placa del vehículo: ").ToUpper();
            string marcaVehiculo = LeerTexto("Marca del vehículo: ");
            string modeloVehiculo = LeerTexto("Modelo del vehículo: ");
            int anioVehiculo = LeerEntero("Año del vehículo: ");

            DateTime fechaVehiculo = LeerFecha(
                "Fecha de registro (dd/MM/yyyy): ");

            ClienteVehiculo nuevoRegistro = new ClienteVehiculo(
                codigoCliente,
                nombreCliente,
                telefonoCliente,
                placaVehiculo,
                marcaVehiculo,
                modeloVehiculo,
                anioVehiculo,
                fechaVehiculo
            );

            registros.Add(nuevoRegistro);

            Console.WriteLine();
            Console.WriteLine("Registro agregado exitosamente.");
            Pausar();
        }

        private void MostrarRegistros()
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("      REGISTROS DE CLIENTES");
            Console.WriteLine("=================================");

            if (registros.Count == 0)
            {
                Console.WriteLine("No hay registros actualmente.");
                Pausar();
                return;
            }

            foreach (ClienteVehiculo registro in registros)
            {
                MostrarRegistro(registro);
                Console.WriteLine("---------------------------------");
            }

            Pausar();
        }

        private void MostrarMenuBusqueda()
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("        BUSCAR REGISTRO");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Buscar por código de cliente");
            Console.WriteLine("2. Buscar por placa");
            Console.WriteLine("3. Regresar");

            int opcion = LeerEntero("Seleccione una opción: ");

            switch (opcion)
            {
                case 1:
                    int codigo = LeerEntero("Ingrese el código del cliente: ");

                    ClienteVehiculo? registroPorCodigo = null;

                    foreach (ClienteVehiculo registro in registros)
                    {
                        if (registro.CodigoCliente == codigo)
                        {
                            registroPorCodigo = registro;
                        }
                    }

                    MostrarResultadoBusqueda(registroPorCodigo);
                    break;

                case 2:
                    string placa = LeerTexto("Ingrese la placa del vehículo: ").ToUpper();

                    ClienteVehiculo? registroPorPlaca = null;

                    foreach (ClienteVehiculo registro in registros)
                    {
                        if (registro.PlacaVehiculo == placa)
                        {
                            registroPorPlaca = registro;
                        }
                    }

                    MostrarResultadoBusqueda(registroPorPlaca);
                    break;

                case 3:
                    break;

                default:
                    Console.WriteLine("Opcion no valida.");
                    Pausar();
                    break;
            }
        }

        private void MostrarResultadoBusqueda(ClienteVehiculo? registro)
        {
            Console.Clear();

            if (registro == null)
            {
                Console.WriteLine("No se encontro ningun registro.");
            }
            else
            {
                MostrarRegistro(registro);
            }

            Pausar();
        }

        private void MostrarRegistro(ClienteVehiculo registro)
        {
            Console.WriteLine($"Código del cliente: {registro.CodigoCliente}");
            Console.WriteLine($"Nombre: {registro.NombreCliente}");
            Console.WriteLine($"Teléfono: {registro.TelefonoCliente}");
            Console.WriteLine($"Placa: {registro.PlacaVehiculo}");
            Console.WriteLine($"Marca: {registro.MarcaVehiculo}");
            Console.WriteLine($"Modelo: {registro.ModeloVehiculo}");
            Console.WriteLine($"Año: {registro.AnioVehiculo}");
            Console.WriteLine(
                $"Fecha de registro: " +
                $"{registro.FechaVehiculo:dd/MM/yyyy}");
        }

        // Lee un número entero y repite la petición si el texto no es válido.
        private int LeerEntero(string mensaje)
        {
            int valor;

            while (true)
            {
                Console.Write(mensaje);

                if (int.TryParse(Console.ReadLine(), out valor))
                {
                    return valor;
                }

                Console.WriteLine("Debe ingresar un número válido.");
            }
        }

        // Lee un texto y no acepta vacíos ni solo espacios.
        private string LeerTexto(string mensaje)
        {
            string? valor;

            while (true)
            {
                Console.Write(mensaje);
                valor = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(valor))
                {
                    return valor.Trim();
                }

                Console.WriteLine("Este campo es obligatorio.");
            }
        }

        // Lee una fecha y repite la petición si el formato no es válido.
        private DateTime LeerFecha(string mensaje)
        {
            DateTime fecha;

            while (true)
            {
                Console.Write(mensaje);

                if (DateTime.TryParse(Console.ReadLine(), out fecha))
                {
                    return fecha;
                }

                Console.WriteLine("Formato inválido. Ejemplo: 25/09/2026");
            }
        }

        private void Pausar()
        {
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
