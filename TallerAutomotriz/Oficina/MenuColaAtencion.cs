using TallerAutomotriz.Datos;
using TallerAutomotriz.Utilidades;
using System;
using System.Collections.Generic;

namespace TallerAutomotriz.Oficina
{
    internal class MenuColaAtencion
    {
        private readonly Repository repository;

        
        public MenuColaAtencion(Repository repository)
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
                Console.WriteLine("         COLA DE ATENCION");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Agregar vehiculo a la fila");
                Console.WriteLine("2. Atender proximo vehiculo");
                Console.WriteLine("3. Ver proximo vehiculo");
                Console.WriteLine("4. Mostrar vehiculos en espera");
                Console.WriteLine("5. Mostrar total de vehiculos en la fila");
                Console.WriteLine("6. Regresar al menu principal");
                Console.WriteLine("=================================");

                int opcion = FuncionesAuxiliares.LeerEntero("Seleccione una opcion: ");

                switch (opcion)
                {
                    case 1:
                        AgregarVehiculo();
                        break;

                    case 2:
                        AtenderProximoVehiculo();
                        break;

                    case 3:
                        VerProximoVehiculo();
                        break;

                    case 4:
                        MostrarVehiculosEnEspera();
                        break;

                    case 5:
                        MostrarTotalVehiculos();
                        break;

                    case 6:
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opcion no valida.");
                        FuncionesAuxiliares.Pausar();
                        break;
                }
            }
        }

        
        private void AgregarVehiculo()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            string placa = FuncionesAuxiliares.LeerTexto("Placa del vehiculo: ").ToUpper();
            repository.vehiculosEnFila.Enqueue(placa);
            
            repository.historial.Push($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - Ingreso a la fila: {placa}");

            Console.WriteLine("Vehiculo agregado al final de la fila.");
            FuncionesAuxiliares.Pausar();
        }

        
        private void AtenderProximoVehiculo()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            if (repository.vehiculosEnFila.Count == 0)
            {
                Console.WriteLine("No hay vehiculos esperando.");
            }
            else
            {
                
                string placa = repository.vehiculosEnFila.Dequeue();
                
                repository.historial.Push($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - Inicio de atencion: {placa}");
                Console.WriteLine($"Vehiculo atendido: {placa}");
            }

            FuncionesAuxiliares.Pausar();
        }

        
        private void VerProximoVehiculo()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            if (repository.vehiculosEnFila.Count == 0)
            {
                Console.WriteLine("No hay vehiculos esperando.");
            }
            else
            {
                string placa = repository.vehiculosEnFila.Peek();
                Console.WriteLine($"Proximo vehiculo: {placa}");
            }

            FuncionesAuxiliares.Pausar();
        }

        
        private void MostrarVehiculosEnEspera()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            if (repository.vehiculosEnFila.Count == 0)
            {
                Console.WriteLine("No hay vehiculos esperando.");
                FuncionesAuxiliares.Pausar();
                return;
            }

            Console.WriteLine("VEHICULOS EN ESPERA");

            string[] placas = repository.vehiculosEnFila.ObtenerElementos();

            foreach (string placa in placas)
            {
                Console.WriteLine($"Placa: {placa}");
            }

            FuncionesAuxiliares.Pausar();
        }

        
        private void MostrarTotalVehiculos()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            int totalEnEspera = repository.vehiculosEnFila.Count;
            Console.WriteLine($"Vehiculos esperando: {totalEnEspera}");

            FuncionesAuxiliares.Pausar();
        }
    }
}
