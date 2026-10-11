using System;
using System.Collections.Generic;

namespace TallerAutomotriz.Oficina
{
    internal class MenuColaAtencion
    {
        // Queue guarda las placas en orden de llegada para atenderlas FIFO.
        private readonly Queue<string> vehiculosEnFila =
            new Queue<string>();

        // Muestra el menu de la fila de atencion.
        public void MostrarMenu()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();

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

                int opcion = LeerEntero("Seleccione una opcion: ");

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
                        Pausar();
                        break;
                }
            }
        }

        // Pide una placa y Enqueue la agrega al final de la cola.
        private void AgregarVehiculo()
        {
            Console.Clear();

            string placa = LeerTexto("Placa del vehiculo: ").ToUpper();
            vehiculosEnFila.Enqueue(placa);

            Console.WriteLine("Vehiculo agregado al final de la fila.");
            Pausar();
        }

        // Atiende y quita de la cola la primera placa que llego.
        private void AtenderProximoVehiculo()
        {
            Console.Clear();

            if (vehiculosEnFila.Count == 0)
            {
                Console.WriteLine("No hay vehiculos esperando.");
            }
            else
            {
                // Dequeue devuelve y quita la primera placa de la cola.
                string placa = vehiculosEnFila.Dequeue();
                Console.WriteLine($"Vehiculo atendido: {placa}");
            }

            Pausar();
        }

        // Peek muestra la primera placa sin quitarla de la cola.
        private void VerProximoVehiculo()
        {
            Console.Clear();

            if (vehiculosEnFila.Count == 0)
            {
                Console.WriteLine("No hay vehiculos esperando.");
            }
            else
            {
                string placa = vehiculosEnFila.Peek();
                Console.WriteLine($"Proximo vehiculo: {placa}");
            }

            Pausar();
        }

        // Recorre la cola y muestra todas las placas que esperan.
        private void MostrarVehiculosEnEspera()
        {
            Console.Clear();

            if (vehiculosEnFila.Count == 0)
            {
                Console.WriteLine("No hay vehiculos esperando.");
                Pausar();
                return;
            }

            Console.WriteLine("VEHICULOS EN ESPERA");

            foreach (string placa in vehiculosEnFila)
            {
                Console.WriteLine($"Placa: {placa}");
            }

            Pausar();
        }

        // Muestra cuantos vehiculos siguen esperando en la cola.
        private void MostrarTotalVehiculos()
        {
            Console.Clear();

            int totalEnEspera = vehiculosEnFila.Count;
            Console.WriteLine($"Vehiculos esperando: {totalEnEspera}");

            Pausar();
        }

        // Lee un entero y repite la solicitud si la entrada no es numerica.
        private int LeerEntero(string mensaje)
        {
            int valor;

            while (true)
            {
                Console.Write(mensaje);
                string? textoIngresado = Console.ReadLine();

                if (int.TryParse(textoIngresado, out valor))
                {
                    return valor;
                }
                else
                {
                    Console.WriteLine("Debe ingresar un numero valido.");
                }
            }
        }

        // Lee texto y repite la solicitud si el campo esta vacio.
        private string LeerTexto(string mensaje)
        {
            while (true)
            {
                Console.Write(mensaje);
                string? valor = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(valor))
                {
                    return valor.Trim();
                }
                else
                {
                    Console.WriteLine("Este campo es obligatorio.");
                }
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
