using System;
using TallerAutomotriz.Oficina;

namespace TallerAutomotriz
{
    internal class MenuPrincipal
    {
        public void MostrarMenu()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();

                Console.WriteLine("=================================");
                Console.WriteLine("        TALLER AUTOMOTRIZ");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Clientes y vehiculos");
                Console.WriteLine("2. Ordenes de servicio");
                Console.WriteLine("3. Cola de atencion");
                Console.WriteLine("4. Historial");
                Console.WriteLine("5. Salir");
                Console.WriteLine("=================================");

                int opcion = LeerEntero("Seleccione una opcion: ");

                switch (opcion)
                {
                    case 1:
                        MenuClientesVehiculos menuClientes =
                            new MenuClientesVehiculos();

                        menuClientes.MostrarMenu();
                        break;

                    case 2:
                        MenuOrdenServicio menuOrdenes =
                            new MenuOrdenServicio();

                        menuOrdenes.MostrarMenu();
                        break;

                    case 3:
                        MenuColaAtencion menuCola =
                            new MenuColaAtencion();

                        menuCola.MostrarMenu();
                        break;

                    case 4:
                        Console.WriteLine(
                            "Modulo de historial pendiente de implementar.");
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.WriteLine("Saliendo del sistema...");
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opcion no valida.");
                        Console.ReadKey();
                        break;
                }
            }
        }

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
                    Console.WriteLine("Debe ingresar un número valido.");
                }
            }
        }
    }
}
