using System;

namespace TallerAutomotriz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuPrincipal menu = new MenuPrincipal();

            // Inicia el menu principal del sistema.
            menu.MostrarMenu();

            Console.WriteLine("Programa finalizado.");
        }
    }
}