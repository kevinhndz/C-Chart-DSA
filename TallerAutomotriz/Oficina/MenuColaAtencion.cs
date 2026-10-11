using System;
using System.Collections.Generic;

namespace TallerAutomotriz.Oficina
{
    internal class MenuColaAtencion
    {
        // Guarda las placas en el orden en que llegaron.
        private readonly List<string> vehiculosEnFila =
            new List<string>();

        // Indica la posicion del primer vehiculo que sigue esperando.
        private int inicioFila = 0;

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

        // Pide una placa y la agrega despues de las que ya estan esperando.
        private void AgregarVehiculo()
        {
            Console.Clear();

            string placa = LeerTexto("Placa del vehiculo: ").ToUpper();
            vehiculosEnFila.Add(placa);

            Console.WriteLine("Vehiculo agregado al final de la fila.");
            Pausar();
        }

        // Atiende la placa al inicio y avanza la posicion de la fila.
        private void AtenderProximoVehiculo()
        {
            Console.Clear();

            if (inicioFila >= vehiculosEnFila.Count)
            {
                Console.WriteLine("No hay vehiculos esperando.");
                Pausar();
                return;
            }

            string placa = vehiculosEnFila[inicioFila];
            Console.WriteLine($"Vehiculo atendido: {placa}");

            // Avanza al siguiente elemento para retirar el atendido de la fila.
            inicioFila = inicioFila + 1;

            Pausar();
        }

        // Muestra la placa al inicio sin cambiar la posicion de la fila.
        private void VerProximoVehiculo()
        {
            Console.Clear();

            if (inicioFila >= vehiculosEnFila.Count)
            {
                Console.WriteLine("No hay vehiculos esperando.");
            }
            else
            {
                string placa = vehiculosEnFila[inicioFila];
                Console.WriteLine($"Proximo vehiculo: {placa}");
            }

            Pausar();
        }

        // Recorre la lista y muestra las placas que todavia esperan.
        private void MostrarVehiculosEnEspera()
        {
            Console.Clear();

            if (inicioFila >= vehiculosEnFila.Count)
            {
                Console.WriteLine("No hay vehiculos esperando.");
                Pausar();
                return;
            }

            Console.WriteLine("VEHICULOS EN ESPERA");

            int posicion = 0;

            foreach (string placa in vehiculosEnFila)
            {
                if (posicion >= inicioFila)
                {
                    Console.WriteLine($"Placa: {placa}");
                }

                // Aumenta el numero de posicion para revisar el siguiente elemento.
                posicion = posicion + 1;
            }

            Pausar();
        }

        // Resta las posiciones atendidas del total de posiciones guardadas.
        private void MostrarTotalVehiculos()
        {
            Console.Clear();

            int totalEnEspera = vehiculosEnFila.Count - inicioFila;
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
