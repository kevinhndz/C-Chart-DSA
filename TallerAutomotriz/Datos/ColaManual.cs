namespace TallerAutomotriz.Datos
{
    // Guarda elementos en orden FIFO usando un arreglo circular.
    internal class ColaManual<T>
    {
        private T[] elementos = new T[4];
        private int inicio = 0;
        private int cantidad = 0;

        public int Count
        {
            get { return cantidad; }
        }

        public void Enqueue(T elemento)
        {
            if (cantidad == elementos.Length)
            {
                AumentarEspacio();
            }

            int indiceFinal = (inicio + cantidad) % elementos.Length;
            elementos[indiceFinal] = elemento;
            cantidad++;
        }

        public T Dequeue()
        {
            T elemento = elementos[inicio];
            elementos[inicio] = default!;
            inicio = (inicio + 1) % elementos.Length;
            cantidad--;

            if (cantidad == 0)
            {
                inicio = 0;
            }

            return elemento;
        }

        public T Peek()
        {
            return elementos[inicio];
        }

        public T[] ObtenerElementos()
        {
            T[] resultado = new T[cantidad];

            for (int i = 0; i < cantidad; i++)
            {
                int indice = (inicio + i) % elementos.Length;
                resultado[i] = elementos[indice];
            }

            return resultado;
        }

        private void AumentarEspacio()
        {
            T[] elementosNuevos = new T[elementos.Length * 2];

            for (int i = 0; i < cantidad; i++)
            {
                int indice = (inicio + i) % elementos.Length;
                elementosNuevos[i] = elementos[indice];
            }

            elementos = elementosNuevos;
            inicio = 0;
        }
    }
}
