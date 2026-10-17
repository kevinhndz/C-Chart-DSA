using System;

namespace TallerAutomotriz.Utilidades
{
    
    internal static class FuncionesAuxiliares
    {
        
        public static int LeerEntero(string mensaje)
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

        
        public static string LeerTexto(string mensaje)
        {
            string? valor;

            while (true)
            {
                Console.Write(mensaje);
                valor = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(valor))
                {
                    return valor.Trim();
                }

                Console.WriteLine("Este campo es obligatorio.");
            }
        }

        
        public static string LeerNombre(string mensaje)
        {
            while (true)
            {
                string nombre = LeerTexto(mensaje);
                bool tieneNumero = false;

                foreach (char caracter in nombre)
                {
                    if (caracter >= '0' && caracter <= '9')
                    {
                        tieneNumero = true;
                    }
                }

                if (tieneNumero == false)
                {
                    return nombre;
                }
                else
                {
                    Console.WriteLine("El nombre no debe contener numeros.");
                }
            }
        }

        
        public static string LeerTelefono(string mensaje)
        {
            while (true)
            {
                string telefono = LeerTexto(mensaje);
                bool tieneCaracterNoNumerico = false;

                foreach (char caracter in telefono)
                {
                    if (caracter < '0' || caracter > '9')
                    {
                        tieneCaracterNoNumerico = true;
                    }
                }

                if (tieneCaracterNoNumerico == false)
                {
                    return telefono;
                }
                else
                {
                    Console.WriteLine("El telefono solo debe contener numeros.");
                }
            }
        }

        
        public static DateTime LeerFecha(string mensaje)
        {
            DateTime fecha;

            while (true)
            {
                Console.Write(mensaje);

                if (DateTime.TryParse(Console.ReadLine(), out fecha))
                {
                    return fecha;
                }

                Console.WriteLine("Fecha no valida. Ejemplo: 25/09/2026");
            }
        }

        
        public static decimal LeerDecimal(string mensaje)
        {
            decimal valor;

            while (true)
            {
                Console.Write(mensaje);

                string? textoIngresado = Console.ReadLine();

                if (decimal.TryParse(textoIngresado, out valor) && valor >= 0)
                {
                    return valor;
                }
                else
                {
                    Console.WriteLine("Ingrese un costo valido mayor o igual a cero.");
                }
            }
        }

        
        public static void Pausar()
        {
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
        
        public static void LimpiarPantalla()
        {
            Console.Clear();
        }
    }
}
