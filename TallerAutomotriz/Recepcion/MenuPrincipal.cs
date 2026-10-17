using TallerAutomotriz.Datos;
using TallerAutomotriz.Utilidades;
using System;
using TallerAutomotriz.Oficina;

namespace TallerAutomotriz
{
    internal class MenuPrincipal
    {
        
        private readonly Repository repository = new Repository();

        public void MostrarMenu()
        {
            bool continuar = true;

            while (continuar)
            {
                FuncionesAuxiliares.LimpiarPantalla();

                Console.WriteLine("=================================");
                Console.WriteLine("        TALLER AUTOMOTRIZ");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Clientes y vehiculos");
                Console.WriteLine("2. Ordenes de servicio");
                Console.WriteLine("3. Cola de atencion");
                Console.WriteLine("4. Historial");
                Console.WriteLine("5. Salir");
                Console.WriteLine("=================================");

                int opcion = FuncionesAuxiliares.LeerEntero("Seleccione una opcion: ");

                switch (opcion)
                {
                    case 1:
                        MenuClientesVehiculos menuClientes =
                            new MenuClientesVehiculos(repository);

                        menuClientes.MostrarMenu();
                        break;

                    case 2:
                        MenuOrdenServicio menuOrdenes =
                            new MenuOrdenServicio(repository);

                        menuOrdenes.MostrarMenu();
                        break;

                    case 3:
                        MenuColaAtencion menuCola =
                            new MenuColaAtencion(repository);

                        menuCola.MostrarMenu();
                        break;

                    case 4:
                        MenuHistorial menuHistorial = new MenuHistorial(repository);
                        menuHistorial.MostrarMenu();
                        break;

                    case 5:
                        Console.WriteLine("Saliendo del sistema...");
                        continuar = false;
                        break;

                    default:
                        Console.WriteLine("Opcion no valida.");
                        FuncionesAuxiliares.Pausar();
                        break;
                }
            }
        }
    }
}
