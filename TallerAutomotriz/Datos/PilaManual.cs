namespace TallerAutomotriz.Datos
{
    
    internal class PilaManual
    {
        private string[] elementos = new string[4];
        private int cantidad = 0;

        public int Count
        {
            get { return cantidad; }
        }

        public void Push(string elemento)
        {
            if (cantidad == elementos.Length)
            {
                AumentarEspacio();
            }

            elementos[cantidad] = elemento;
            cantidad++;
        }

        public string Pop()
        {
            int posicionSuperior = cantidad - 1;
            string elemento = elementos[posicionSuperior];
            elementos[posicionSuperior] = "";
            cantidad--;
            return elemento;
        }

        public string[] ObtenerElementos()
        {
            string[] resultado = new string[cantidad];
            int posicionResultado = 0;

            for (int i = cantidad - 1; i >= 0; i--)
            {
                resultado[posicionResultado] = elementos[i];
                posicionResultado++;
            }

            return resultado;
        }

        private void AumentarEspacio()
        {
            string[] elementosNuevos = new string[elementos.Length * 2];

            for (int i = 0; i < cantidad; i++)
            {
                elementosNuevos[i] = elementos[i];
            }

            elementos = elementosNuevos;
        }
    }
}
