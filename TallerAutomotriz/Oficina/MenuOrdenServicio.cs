using System;
using System.Collections.Generic;
using System.Linq;
using TallerAutomotriz.Formularios;

namespace TallerAutomotriz.Oficina
{
    internal class MenuOrdenServicio
    {
        // Lista que guarda las ordenes de servicio mientras el programa esta abierto.
        private readonly List<OrdenServicio> ordenes =
            new List<OrdenServicio>();

        public void MostrarMenu()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();

                Console.WriteLine("=================================");
                Console.WriteLine("      ORDENES DE SERVICIO");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Registrar orden de servicio");
                Console.WriteLine("2. Mostrar ordenes");
                Console.WriteLine("3. Buscar orden por cliente o placa");
                Console.WriteLine("4. Buscar orden por fecha");
                Console.WriteLine("5. Modificar orden");
                Console.WriteLine("6. Cancelar orden");
                Console.WriteLine("7. Mostrar ordenes pendientes");
                Console.WriteLine("8. Regresar al menu principal");
                Console.WriteLine("=================================");

                int opcion = LeerEntero("Seleccione una opcion: ");

                switch (opcion)
                {
                    case 1:
                        RegistrarOrden();
                        break;

                    case 2:
                        MostrarOrdenes();
                        break;

                    case 3:
                        MostrarMenuBusqueda();
                        break;

                    case 4:
                        BuscarPorFecha();
                        break;

                    case 5:
                        ModificarOrden();
                        break;

                    case 6:
                        CancelarOrden();
                        break;

                    case 7:
                        MostrarPendientes();
                        break;

                    case 8:
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opcion no valida.");
                        Pausar();
                        break;
                }
            }
        }

        // Pide los datos de una orden y la agrega a la lista.
        private void RegistrarOrden()
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("   REGISTRAR ORDEN DE SERVICIO");
            Console.WriteLine("=================================");

            string numeroOrden = LeerTexto("Numero de orden: ");

            if (ordenes.Any(o => o.numeroOrden == numeroOrden))
            {
                Console.WriteLine("Ya existe una orden con ese numero.");
                Pausar();
                return;
            }

            string codigoCliente = LeerTexto("Codigo del cliente: ");
            string placaVehiculo = LeerTexto("Placa del vehiculo: ").ToUpper();
            DateTime fechaIngreso = LeerFecha("Fecha de ingreso (dd/MM/yyyy): ");
            string tipoServicio = LeerTexto("Tipo de servicio: ");
            string descripcion = LeerTexto("Descripcion del problema: ");
            string mecanicoAsignado = LeerTexto("Mecanico asignado: ");
            decimal costoEstimado = LeerDecimal("Costo estimado: ");

            OrdenServicio nuevaOrden = new OrdenServicio(
                numeroOrden,
                codigoCliente,
                placaVehiculo,
                fechaIngreso,
                tipoServicio,
                descripcion,
                mecanicoAsignado,
                costoEstimado,
                "Pendiente");

            ordenes.Add(nuevaOrden);

            Console.WriteLine("Orden agregada exitosamente.");
            Pausar();
        }

        // Recorre la lista y muestra todas las ordenes registradas.
        private void MostrarOrdenes()
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("      ORDENES REGISTRADAS");
            Console.WriteLine("=================================");

            if (ordenes.Count == 0)
            {
                Console.WriteLine("No hay ordenes actualmente.");
                Pausar();
                return;
            }

            foreach (OrdenServicio orden in ordenes)
            {
                MostrarOrden(orden);
                Console.WriteLine("---------------------------------");
            }

            Pausar();
        }

        // Presenta las opciones para buscar por codigo de cliente o por placa.
        private void MostrarMenuBusqueda()
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("        BUSCAR ORDEN");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Buscar por codigo de cliente");
            Console.WriteLine("2. Buscar por placa");
            Console.WriteLine("3. Regresar");

            int opcion = LeerEntero("Seleccione una opcion: ");

            switch (opcion)
            {
                case 1:
                    string codigo = LeerTexto("Ingrese el codigo del cliente: ");
                    bool encontroCodigo = false;

                    foreach (OrdenServicio orden in ordenes)
                    {
                        if (orden.codigoCliente == codigo)
                        {
                            MostrarOrden(orden);
                            encontroCodigo = true;
                        }
                    }

                    if (!encontroCodigo)
                    {
                        Console.WriteLine("No se encontro ninguna orden.");
                    }
                    Pausar();
                    break;

                case 2:
                    string placa = LeerTexto("Ingrese la placa del vehiculo: ").ToUpper();
                    bool encontroPlaca = false;

                    foreach (OrdenServicio orden in ordenes)
                    {
                        if (orden.placaVehiculo == placa)
                        {
                            MostrarOrden(orden);
                            encontroPlaca = true;
                        }
                    }

                    if (!encontroPlaca)
                    {
                        Console.WriteLine("No se encontro ninguna orden.");
                    }
                    Pausar();
                    break;

                case 3:
                    break;

                default:
                    Console.WriteLine("Opcion no valida.");
                    Pausar();
                    break;
            }
        }

        // Busca las ordenes que tienen la fecha de ingreso indicada.
        private void BuscarPorFecha()
        {
            Console.Clear();

            DateTime fecha = LeerFecha("Ingrese la fecha que desea buscar: ");
            bool encontro = false;

            foreach (OrdenServicio orden in ordenes)
            {
                if (orden.fechaIngreso.Date == fecha.Date)
                {
                    MostrarOrden(orden);
                    encontro = true;
                }
            }

            if (!encontro)
            {
                Console.WriteLine("No se encontro ninguna orden.");
            }
            Pausar();
        }

        // Busca una orden por su numero y permite cambiar sus datos.
        private void ModificarOrden()
        {
            Console.Clear();

            string numeroOrden = LeerTexto("Numero de la orden que desea modificar: ");
            OrdenServicio? orden = null;

            foreach (OrdenServicio ordenActual in ordenes)
            {
                if (ordenActual.numeroOrden == numeroOrden)
                {
                    orden = ordenActual;
                }
            }

            if (orden == null)
            {
                Console.WriteLine("No se encontro ninguna orden.");
                Pausar();
                return;
            }

            Console.WriteLine("Ingrese los nuevos datos de la orden.");
            orden.codigoCliente = LeerTexto("Codigo del cliente: ");
            orden.placaVehiculo = LeerTexto("Placa del vehiculo: ").ToUpper();
            orden.fechaIngreso = LeerFecha("Fecha de ingreso (dd/MM/yyyy): ");
            orden.tipoServicio = LeerTexto("Tipo de servicio: ");
            orden.descripcion = LeerTexto("Descripcion del problema: ");
            orden.mecanicoAsignado = LeerTexto("Mecanico asignado: ");
            orden.costoEstimado = LeerDecimal("Costo estimado: ");
            Console.WriteLine("1. Pendiente");
            Console.WriteLine("2. En proceso");
            Console.WriteLine("3. Finalizada");
            Console.WriteLine("4. Cancelada");

            int opcionEstado = LeerEntero("Seleccione el estado: ");

            switch (opcionEstado)
            {
                case 1:
                    orden.estadoOrden = "Pendiente";
                    break;
                case 2:
                    orden.estadoOrden = "En proceso";
                    break;
                case 3:
                    orden.estadoOrden = "Finalizada";
                    break;
                case 4:
                    orden.estadoOrden = "Cancelada";
                    break;
                default:
                    orden.estadoOrden = "Pendiente";
                    Console.WriteLine("Opcion no valida. Se asigno el estado Pendiente.");
                    break;
            }

            Console.WriteLine("Orden modificada exitosamente.");
            Pausar();
        }

        // Cambia el estado de una orden a Cancelada.
        private void CancelarOrden()
        {
            Console.Clear();

            string numeroOrden = LeerTexto("Numero de la orden que desea cancelar: ");
            OrdenServicio? orden = null;

            foreach (OrdenServicio ordenActual in ordenes)
            {
                if (ordenActual.numeroOrden == numeroOrden)
                {
                    orden = ordenActual;
                }
            }

            if (orden == null)
            {
                Console.WriteLine("No se encontro ninguna orden.");
            }
            else
            {
                orden.estadoOrden = "Cancelada";
                Console.WriteLine("Orden cancelada exitosamente.");
            }

            Pausar();
        }

        // Recorre la lista y muestra las ordenes con estado Pendiente.
        private void MostrarPendientes()
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("      ORDENES PENDIENTES");
            Console.WriteLine("=================================");

            bool encontro = false;

            foreach (OrdenServicio orden in ordenes)
            {
                if (orden.estadoOrden == "Pendiente")
                {
                    MostrarOrden(orden);
                    Console.WriteLine("---------------------------------");
                    encontro = true;
                }
            }

            if (!encontro)
            {
                Console.WriteLine("No se encontro ninguna orden.");
            }
            Pausar();
        }

        // Imprime las propiedades de una orden.
        private void MostrarOrden(OrdenServicio orden)
        {
            Console.WriteLine($"Numero de orden: {orden.numeroOrden}");
            Console.WriteLine($"Codigo del cliente: {orden.codigoCliente}");
            Console.WriteLine($"Placa del vehiculo: {orden.placaVehiculo}");
            Console.WriteLine($"Fecha de ingreso: {orden.fechaIngreso:dd/MM/yyyy}");
            Console.WriteLine($"Tipo de servicio: {orden.tipoServicio}");
            Console.WriteLine($"Descripcion: {orden.descripcion}");
            Console.WriteLine($"Mecanico asignado: {orden.mecanicoAsignado}");
            Console.WriteLine($"Costo estimado: {orden.costoEstimado}");
            Console.WriteLine($"Estado: {orden.estadoOrden}");
        }

        // Lee un numero entero y vuelve a pedirlo si no es valido.
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

                Console.WriteLine("Debe ingresar un numero valido.");
            }
        }

        // Lee un texto obligatorio y quita los espacios de sus extremos.
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

        // Lee una fecha y vuelve a pedirla si no es valida.
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

                Console.WriteLine("Fecha no valida. Ejemplo: 25/09/2026");
            }
        }

        // Lee el costo como decimal y no acepta valores negativos.
        private decimal LeerDecimal(string mensaje)
        {
            decimal valor;

            while (true)
            {
                Console.Write(mensaje);

                if (decimal.TryParse(Console.ReadLine(), out valor) && valor >= 0)
                {
                    return valor;
                }

                Console.WriteLine("Ingrese un costo valido mayor o igual a cero.");
            }
        }

        // Pausa la pantalla para que el usuario pueda leer el resultado.
        private void Pausar()
        {
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
