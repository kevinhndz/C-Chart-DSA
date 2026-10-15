using TallerAutomotriz.Datos;
using TallerAutomotriz.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using TallerAutomotriz.Formularios;

namespace TallerAutomotriz.Oficina
{
    internal class MenuClientesVehiculos
    {
        private readonly Repository repository;

        // Recibe el mismo almacen que usan los otros menus.
        public MenuClientesVehiculos(Repository repository)
        {
            this.repository = repository;
        }

        public void MostrarMenu()
        {
            bool continuar = true;

            while (continuar)
            {
                FuncionesAuxiliares.LimpiarPantalla();

                Console.WriteLine("=================================");
                Console.WriteLine("      CLIENTES Y VEHÍCULOS");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Registrar cliente y vehículo");
                Console.WriteLine("2. Mostrar registros");
                Console.WriteLine("3. Buscar registro");
                Console.WriteLine("4. Modificar registro");
                Console.WriteLine("5. Eliminar registro");
                Console.WriteLine("6. Mostrar total de registros");
                Console.WriteLine("7. Regresar al menu principal");
                Console.WriteLine("=================================");

                int opcion = FuncionesAuxiliares.LeerEntero("Seleccione una opción: ");

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
                        ModificarRegistro();
                        break;

                    case 5:
                        EliminarRegistro();
                        break;

                    case 6:
                        MostrarTotalRegistros();
                        break;

                    case 7:
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        FuncionesAuxiliares.Pausar();
                        break;
                }
            }
        }

        private void Registrar()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            Console.WriteLine("=================================");
            Console.WriteLine("   REGISTRAR CLIENTE Y VEHÍCULO");
            Console.WriteLine("=================================");

            int codigoCliente = FuncionesAuxiliares.LeerEntero("Código del cliente: ");

            // Evita registrar dos clientes con el mismo codigo.
            if (repository.registros.Any(r => r.CodigoCliente == codigoCliente))
            {
                Console.WriteLine("Ya existe un cliente con ese código.");
                FuncionesAuxiliares.Pausar();
                return;
            }

            string nombreCliente = FuncionesAuxiliares.LeerNombre("Nombre del cliente: ");
            string telefonoCliente = FuncionesAuxiliares.LeerTelefono("Teléfono del cliente: ");
            string placaVehiculo = FuncionesAuxiliares.LeerTexto("Placa del vehículo: ").ToUpper();
            string marcaVehiculo = FuncionesAuxiliares.LeerTexto("Marca del vehículo: ");
            string modeloVehiculo = FuncionesAuxiliares.LeerTexto("Modelo del vehículo: ");
            int anioVehiculo = FuncionesAuxiliares.LeerEntero("Año del vehículo: ");

            DateTime fechaVehiculo = FuncionesAuxiliares.LeerFecha(
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

            repository.registros.Add(nuevoRegistro);
            // Push agrega esta operacion encima de las anteriores.
            repository.historial.Push($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - Registro de cliente y vehiculo: {codigoCliente}, placa {placaVehiculo}");

            Console.WriteLine();
            Console.WriteLine("Registro agregado exitosamente.");
            FuncionesAuxiliares.Pausar();
        }

        private void MostrarRegistros()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            Console.WriteLine("=================================");
            Console.WriteLine("      REGISTROS DE CLIENTES");
            Console.WriteLine("=================================");

            if (repository.registros.Count == 0)
            {
                Console.WriteLine("No hay registros actualmente.");
                FuncionesAuxiliares.Pausar();
                return;
            }

            foreach (ClienteVehiculo registro in repository.registros)
            {
                MostrarRegistro(registro);
                Console.WriteLine("---------------------------------");
            }

            FuncionesAuxiliares.Pausar();
        }

        private void MostrarMenuBusqueda()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            Console.WriteLine("=================================");
            Console.WriteLine("        BUSCAR REGISTRO");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Buscar por código de cliente");
            Console.WriteLine("2. Buscar por placa");
            Console.WriteLine("3. Regresar");

            int opcion = FuncionesAuxiliares.LeerEntero("Seleccione una opción: ");

            switch (opcion)
            {
                case 1:
                    int codigo = FuncionesAuxiliares.LeerEntero("Ingrese el código del cliente: ");

                    ClienteVehiculo? registroPorCodigo = null;

                    foreach (ClienteVehiculo registro in repository.registros)
                    {
                        if (registro.CodigoCliente == codigo)
                        {
                            registroPorCodigo = registro;
                        }
                    }

                    MostrarRegistroEncontrado(registroPorCodigo);
                    break;

                case 2:
                    string placa = FuncionesAuxiliares.LeerTexto("Ingrese la placa del vehículo: ").ToUpper();

                    ClienteVehiculo? registroPorPlaca = null;

                    foreach (ClienteVehiculo registro in repository.registros)
                    {
                        if (registro.PlacaVehiculo == placa)
                        {
                            registroPorPlaca = registro;
                        }
                    }

                    MostrarRegistroEncontrado(registroPorPlaca);
                    break;

                case 3:
                    break;

                default:
                    Console.WriteLine("Opcion no valida.");
                    FuncionesAuxiliares.Pausar();
                    break;
            }
        }

        // Busca por codigo y cambia los datos del objeto encontrado en la lista.
        private void ModificarRegistro()
        {
            FuncionesAuxiliares.LimpiarPantalla();
            int codigo = FuncionesAuxiliares.LeerEntero("Codigo del registro que desea modificar: ");
            ClienteVehiculo? registroEncontrado = null;

            foreach (ClienteVehiculo registro in repository.registros)
            {
                if (registro.CodigoCliente == codigo)
                {
                    registroEncontrado = registro;
                }
            }

            if (registroEncontrado == null)
            {
                Console.WriteLine("No se encontro ningun registro.");
            }
            else
            {
                // El codigo conserva la identidad del registro que se esta modificando.
                registroEncontrado.NombreCliente = FuncionesAuxiliares.LeerNombre("Nombre del cliente: ");
                registroEncontrado.TelefonoCliente = FuncionesAuxiliares.LeerTelefono("Telefono del cliente: ");
                registroEncontrado.PlacaVehiculo = FuncionesAuxiliares.LeerTexto("Placa del vehiculo: ").ToUpper();
                registroEncontrado.MarcaVehiculo = FuncionesAuxiliares.LeerTexto("Marca del vehiculo: ");
                registroEncontrado.ModeloVehiculo = FuncionesAuxiliares.LeerTexto("Modelo del vehiculo: ");
                registroEncontrado.AnioVehiculo = FuncionesAuxiliares.LeerEntero("Anio del vehiculo: ");
                registroEncontrado.FechaVehiculo = FuncionesAuxiliares.LeerFecha("Fecha de registro (dd/MM/yyyy): ");
                // Push agrega esta operacion encima de las anteriores.
                repository.historial.Push($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - Registro modificado: {codigo}");
                Console.WriteLine("Registro modificado exitosamente.");
            }

            FuncionesAuxiliares.Pausar();
        }

        // Encuentra el objeto y lo retira despues de terminar el recorrido.
        private void EliminarRegistro()
        {
            FuncionesAuxiliares.LimpiarPantalla();
            int codigo = FuncionesAuxiliares.LeerEntero("Codigo del registro que desea eliminar: ");
            ClienteVehiculo? registroEncontrado = null;

            foreach (ClienteVehiculo registro in repository.registros)
            {
                if (registro.CodigoCliente == codigo)
                {
                    registroEncontrado = registro;
                }
            }

            if (registroEncontrado == null)
            {
                Console.WriteLine("No se encontro ningun registro.");
            }
            else
            {
                repository.registros.Remove(registroEncontrado);
                // Push agrega esta operacion encima de las anteriores.
                repository.historial.Push($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - Registro eliminado: {codigo}");
                Console.WriteLine("Registro eliminado exitosamente.");
            }

            FuncionesAuxiliares.Pausar();
        }

        // Count devuelve cuantos registros de clientes y vehiculos hay en la lista.
        private void MostrarTotalRegistros()
        {
            FuncionesAuxiliares.LimpiarPantalla();
            Console.WriteLine($"Total de registros: {repository.registros.Count}");
            FuncionesAuxiliares.Pausar();
        }

        private void MostrarRegistroEncontrado(ClienteVehiculo? registro)
        {
            FuncionesAuxiliares.LimpiarPantalla();

            if (registro == null)
            {
                Console.WriteLine("No se encontro ningun registro.");
            }
            else
            {
                MostrarRegistro(registro);
            }

            FuncionesAuxiliares.Pausar();
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
    }
}
