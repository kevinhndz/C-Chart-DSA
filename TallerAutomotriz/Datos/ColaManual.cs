namespace TallerAutomotriz.Datos
{
    
    internal class ColaManual
    {
        private string[] elementos = new string[4];
        private int inicio = 0;
        private int cantidad = 0;

        public int Count
        {
            get { return cantidad; }
        }

        public void Enqueue(string elemento)
        {
            if (cantidad == elementos.Length)
            {
                AumentarEspacio();
            }

            int indiceFinal = inicio + cantidad;

            if (indiceFinal >= elementos.Length)
            {
                indiceFinal = indiceFinal - elementos.Length;
            }

            elementos[indiceFinal] = elemento;
            cantidad++;
        }

        public string Dequeue()
        {
            string elemento = elementos[inicio];
            elementos[inicio] = "";

            inicio++;

            if (inicio == elementos.Length)
            {
                inicio = 0;
            }

            cantidad--;

            if (cantidad == 0)
            {
                inicio = 0;
            }

            return elemento;
        }

        public string Peek()
        {
            return elementos[inicio];
        }

        public string[] ObtenerElementos()
        {
            string[] resultado = new string[cantidad];
            int indice = inicio;

            for (int i = 0; i < cantidad; i++)
            {
                resultado[i] = elementos[indice];

                indice++;

                if (indice == elementos.Length)
                {
                    indice = 0;
                }
            }

            return resultado;
        }

        private void AumentarEspacio()
        {
            string[] cajaVieja = elementos;
            string[] cajaNueva = new string[elementos.Length * 2];
            int indice = inicio;

            for (int i = 0; i < cantidad; i++)
            {
                cajaNueva[i] = cajaVieja[indice];

                indice++;

                if (indice == cajaVieja.Length)
                {
                    indice = 0;
                }
            }

            elementos = cajaNueva;
            inicio = 0;
        }
    }
}
