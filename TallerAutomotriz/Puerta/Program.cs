using System;

namespace TallerAutomotriz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuPrincipal menu = new MenuPrincipal();

            
            menu.MostrarMenu();

            Console.WriteLine("Programa finalizado.");
        }
    }
}