namespace TallerAutomotriz.Datos
{
    // Guarda elementos en orden LIFO y permite ver primero el mas reciente.
    internal class PilaManual<T>
    {
        private T[] elementos = new T[4];
        private int cantidad = 0;

        public int Count
        {
            get { return cantidad; }
        }

        public void Push(T elemento)
        {
            if (cantidad == elementos.Length)
            {
                AumentarEspacio();
            }

            elementos[cantidad] = elemento;
            cantidad++;
        }

        public T Pop()
        {
            int indiceSuperior = cantidad - 1;
            T elemento = elementos[indiceSuperior];
            elementos[indiceSuperior] = default!;
            cantidad--;
            return elemento;
        }

        public T[] ObtenerElementos()
        {
            T[] resultado = new T[cantidad];
            int indiceResultado = 0;

            for (int i = cantidad - 1; i >= 0; i--)
            {
                resultado[indiceResultado] = elementos[i];
                indiceResultado++;
            }

            return resultado;
        }

        private void AumentarEspacio()
        {
            T[] elementosNuevos = new T[elementos.Length * 2];

            for (int i = 0; i < cantidad; i++)
            {
                elementosNuevos[i] = elementos[i];
            }

            elementos = elementosNuevos;
        }
    }
}
