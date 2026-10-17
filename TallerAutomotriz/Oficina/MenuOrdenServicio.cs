using TallerAutomotriz.Datos;
using TallerAutomotriz.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using TallerAutomotriz.Formularios;

namespace TallerAutomotriz.Oficina
{
    internal class MenuOrdenServicio
    {
        private readonly Repository repository;

        
        public MenuOrdenServicio(Repository repository)
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

                int opcion = FuncionesAuxiliares.LeerEntero("Seleccione una opcion: ");

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
                        FuncionesAuxiliares.Pausar();
                        break;
                }
            }
        }

        
        private void RegistrarOrden()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            Console.WriteLine("=================================");
            Console.WriteLine("   REGISTRAR ORDEN DE SERVICIO");
            Console.WriteLine("=================================");

            string numeroOrden = FuncionesAuxiliares.LeerTexto("Numero de orden: ");

            if (repository.ordenes.Any(o => o.numeroOrden == numeroOrden))
            {
                Console.WriteLine("Ya existe una orden con ese numero.");
                FuncionesAuxiliares.Pausar();
                return;
            }

            string codigoCliente = FuncionesAuxiliares.LeerTexto("Codigo del cliente: ");
            string placaVehiculo = FuncionesAuxiliares.LeerTexto("Placa del vehiculo: ").ToUpper();
            DateTime fechaIngreso = FuncionesAuxiliares.LeerFecha("Fecha de ingreso (dd/MM/yyyy): ");
            string tipoServicio = FuncionesAuxiliares.LeerTexto("Tipo de servicio: ");
            string descripcion = FuncionesAuxiliares.LeerTexto("Descripcion del problema: ");
            string mecanicoAsignado = FuncionesAuxiliares.LeerTexto("Mecanico asignado: ");
            decimal costoEstimado = FuncionesAuxiliares.LeerDecimal("Costo estimado: ");

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

            repository.ordenes.Add(nuevaOrden);
            
            repository.historial.Push($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - Registro de orden: {numeroOrden}, placa {placaVehiculo}");

            Console.WriteLine("Orden agregada exitosamente.");
            FuncionesAuxiliares.Pausar();
        }

        
        private void MostrarOrdenes()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            Console.WriteLine("=================================");
            Console.WriteLine("      ORDENES REGISTRADAS");
            Console.WriteLine("=================================");

            if (repository.ordenes.Count == 0)
            {
                Console.WriteLine("No hay ordenes actualmente.");
                FuncionesAuxiliares.Pausar();
                return;
            }

            foreach (OrdenServicio orden in repository.ordenes)
            {
                MostrarOrden(orden);
                Console.WriteLine("---------------------------------");
            }

            FuncionesAuxiliares.Pausar();
        }

        
        private void MostrarMenuBusqueda()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            Console.WriteLine("=================================");
            Console.WriteLine("        BUSCAR ORDEN");
            Console.WriteLine("=================================");
            Console.WriteLine("1. Buscar por codigo de cliente");
            Console.WriteLine("2. Buscar por placa");
            Console.WriteLine("3. Regresar");

            int opcion = FuncionesAuxiliares.LeerEntero("Seleccione una opcion: ");

            switch (opcion)
            {
                case 1:
                    string codigo = FuncionesAuxiliares.LeerTexto("Ingrese el codigo del cliente: ");
                    bool encontroCodigo = false;

                    foreach (OrdenServicio orden in repository.ordenes)
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
                    FuncionesAuxiliares.Pausar();
                    break;

                case 2:
                    string placa = FuncionesAuxiliares.LeerTexto("Ingrese la placa del vehiculo: ").ToUpper();
                    bool encontroPlaca = false;

                    foreach (OrdenServicio orden in repository.ordenes)
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
                    FuncionesAuxiliares.Pausar();
                    break;

                case 3:
                    break;

                default:
                    Console.WriteLine("Opcion no valida.");
                    FuncionesAuxiliares.Pausar();
                    break;
            }
        }

        
        private void BuscarPorFecha()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            DateTime fecha = FuncionesAuxiliares.LeerFecha("Ingrese la fecha que desea buscar: ");
            bool encontro = false;

            foreach (OrdenServicio orden in repository.ordenes)
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
            FuncionesAuxiliares.Pausar();
        }

        
        private void ModificarOrden()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            string numeroOrden = FuncionesAuxiliares.LeerTexto("Numero de la orden que desea modificar: ");
            OrdenServicio? orden = null;

            foreach (OrdenServicio ordenActual in repository.ordenes)
            {
                if (ordenActual.numeroOrden == numeroOrden)
                {
                    orden = ordenActual;
                }
            }

            if (orden == null)
            {
                Console.WriteLine("No se encontro ninguna orden.");
                FuncionesAuxiliares.Pausar();
                return;
            }

            
            string estadoAnterior = orden.estadoOrden;
            Console.WriteLine("Ingrese los nuevos datos de la orden.");
            orden.codigoCliente = FuncionesAuxiliares.LeerTexto("Codigo del cliente: ");
            orden.placaVehiculo = FuncionesAuxiliares.LeerTexto("Placa del vehiculo: ").ToUpper();
            orden.fechaIngreso = FuncionesAuxiliares.LeerFecha("Fecha de ingreso (dd/MM/yyyy): ");
            orden.tipoServicio = FuncionesAuxiliares.LeerTexto("Tipo de servicio: ");
            orden.descripcion = FuncionesAuxiliares.LeerTexto("Descripcion del problema: ");
            orden.mecanicoAsignado = FuncionesAuxiliares.LeerTexto("Mecanico asignado: ");
            orden.costoEstimado = FuncionesAuxiliares.LeerDecimal("Costo estimado: ");
            Console.WriteLine("1. Pendiente");
            Console.WriteLine("2. En proceso");
            Console.WriteLine("3. Finalizada");
            Console.WriteLine("4. Cancelada");

            int opcionEstado = FuncionesAuxiliares.LeerEntero("Seleccione el estado: ");

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

            repository.historial.Push($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - Orden modificada: {orden.numeroOrden}");

            
            if (estadoAnterior != orden.estadoOrden)
            {
                if (orden.estadoOrden == "Finalizada")
                {
                    repository.historial.Push($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - Orden finalizada: {orden.numeroOrden}");
                }
                else if (orden.estadoOrden == "Cancelada")
                {
                    repository.historial.Push($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - Orden cancelada: {orden.numeroOrden}");
                }
            }

            Console.WriteLine("Orden modificada exitosamente.");
            FuncionesAuxiliares.Pausar();
        }

        
        private void CancelarOrden()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            string numeroOrden = FuncionesAuxiliares.LeerTexto("Numero de la orden que desea cancelar: ");
            OrdenServicio? orden = null;

            foreach (OrdenServicio ordenActual in repository.ordenes)
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
                if (orden.estadoOrden != "Cancelada")
                {
                    orden.estadoOrden = "Cancelada";
                    repository.historial.Push($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - Orden cancelada: {orden.numeroOrden}");
                }
                Console.WriteLine("Orden cancelada exitosamente.");
            }

            FuncionesAuxiliares.Pausar();
        }

        
        private void MostrarPendientes()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            Console.WriteLine("=================================");
            Console.WriteLine("      ORDENES PENDIENTES");
            Console.WriteLine("=================================");

            bool encontro = false;

            foreach (OrdenServicio orden in repository.ordenes)
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
            FuncionesAuxiliares.Pausar();
        }

        
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
    }
}
