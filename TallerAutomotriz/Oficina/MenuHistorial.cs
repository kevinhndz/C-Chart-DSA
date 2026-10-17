using System;
using TallerAutomotriz.Datos;
using TallerAutomotriz.Utilidades;

namespace TallerAutomotriz.Oficina
{
    internal class MenuHistorial
    {
        private readonly Repository repository;

        
        public MenuHistorial(Repository repository)
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
                Console.WriteLine("      HISTORIAL DEL SISTEMA");
                Console.WriteLine("=================================");
                Console.WriteLine("1. Mostrar historial completo");
                Console.WriteLine("2. Regresar al menu principal");

                int opcion = FuncionesAuxiliares.LeerEntero("Seleccione una opcion: ");

                switch (opcion)
                {
                    case 1:
                        MostrarHistorial();
                        break;
                    case 2:
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opcion no valida.");
                        FuncionesAuxiliares.Pausar();
                        break;
                }
            }
        }

        
        private void MostrarHistorial()
        {
            FuncionesAuxiliares.LimpiarPantalla();

            if (repository.historial.Count == 0)
            {
                Console.WriteLine("No hay operaciones registradas.");
            }
            else
            {
                Console.WriteLine("HISTORIAL: DE MAS RECIENTE A MAS ANTIGUO");

                
                string[] operaciones = repository.historial.ObtenerElementos();

                foreach (string operacion in operaciones)
                {
                    Console.WriteLine(operacion);
                    Console.WriteLine("---------------------------------");
                }
            }

            FuncionesAuxiliares.Pausar();
        }
    }
}
