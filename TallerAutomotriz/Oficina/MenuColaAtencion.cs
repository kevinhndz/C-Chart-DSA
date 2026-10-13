using TallerAutomotriz.Datos;
using TallerAutomotriz.Utilidades;
using System;
using System.Collections.Generic;

namespace TallerAutomotriz.Oficina
{
    internal class MenuColaAtencion
    {
        private readonly Repository repository;

        // Recibe el mismo almacen que usan los otros menus.
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

        // Pide una placa y Enqueue la agrega al final de la cola.
        private void AgregarVehiculo()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            string placa = FuncionesAuxiliares.LeerTexto("Placa del vehiculo: ").ToUpper();
            repository.vehiculosEnFila.Enqueue(placa);

            Console.WriteLine("Vehiculo agregado al final de la fila.");
            FuncionesAuxiliares.Pausar();
        }

        // Atiende y quita de la cola la primera placa que llego.
        private void AtenderProximoVehiculo()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            if (repository.vehiculosEnFila.Count == 0)
            {
                Console.WriteLine("No hay vehiculos esperando.");
            }
            else
            {
                // Dequeue devuelve y quita la primera placa de la cola.
                string placa = repository.vehiculosEnFila.Dequeue();
                Console.WriteLine($"Vehiculo atendido: {placa}");
            }

            FuncionesAuxiliares.Pausar();
        }

        // Peek muestra la primera placa sin quitarla de la cola.
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

        // Recorre la cola y muestra todas las placas que esperan.
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

            foreach (string placa in repository.vehiculosEnFila)
            {
                Console.WriteLine($"Placa: {placa}");
            }

            FuncionesAuxiliares.Pausar();
        }

        // Muestra cuantos vehiculos siguen esperando en la cola.
        private void MostrarTotalVehiculos()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            int totalEnEspera = repository.vehiculosEnFila.Count;
            Console.WriteLine($"Vehiculos esperando: {totalEnEspera}");

            FuncionesAuxiliares.Pausar();
        }
    }
}
