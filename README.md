# Taller Automotriz

Aplicacion de consola en C# para la actividad IS-142. Los cuatro modulos trabajan con datos temporales en memoria.

## Ejecutar

Desde la carpeta del proyecto:

```powershell
dotnet build TallerAutomotriz/TallerAutomotriz.csproj -c Release
dotnet run --project TallerAutomotriz/TallerAutomotriz.csproj -c Release
```

El proyecto utiliza .NET 8.

## Responsabilidad de cada carpeta

| Carpeta | Responsabilidad |
| --- | --- |
| Puerta | Program crea y muestra el menu principal. |
| Formularios | Define las propiedades y constructores de ClienteVehiculo y OrdenServicio. |
| Recepcion | MenuPrincipal crea un Repository y lo entrega a cada menu. |
| Oficina | Contiene las operaciones de clientes, ordenes, cola e historial. |
| Datos | Repository contiene las listas y las estructuras manuales compartidas. |
| Utilidades | FuncionesAuxiliares reune lectura, validacion, limpieza y pausa de consola. |

## Como se comparten los datos

MenuPrincipal crea una sola instancia de Repository. Al abrir un modulo, se la pasa a su constructor:

```csharp
MenuClientesVehiculos menuClientes = new MenuClientesVehiculos(repository);
```

El constructor guarda esa referencia en el campo de la clase:

```csharp
public MenuClientesVehiculos(Repository repository)
{
    this.repository = repository;
}
```

El parametro y el campo se llaman igual. `this.repository` identifica el campo del menu. Esta asignacion permite acceder al mismo almacen, sin copiar los datos ni crear otra lista.

Por ejemplo, `repository.registros.Add(nuevoRegistro)` agrega el objeto a la lista compartida. Aunque el menu se cree otra vez, recibe el Repository que ya existia y puede mostrar los registros anteriores.

`readonly` impide reemplazar una coleccion por otra. Su contenido puede cambiar con las operaciones de las listas y de las estructuras manuales.

Los datos duran hasta cerrar el programa. No se guardan en un archivo ni en una base de datos.

## Funciones auxiliares

Se invocan por el nombre de la clase, por ejemplo `FuncionesAuxiliares.LeerEntero(mensaje)`. Son static porque no necesitan guardar informacion de un cliente o de una orden.

- LeerTexto: solicita texto obligatorio y quita espacios de los extremos.
- LeerEntero: intenta convertir la entrada con TryParse y vuelve a pedirla si no es valida.
- LeerDecimal: acepta cantidades decimales mayores o iguales a cero.
- LeerFecha: valida la fecha con DateTime.TryParse, conservando el comportamiento previo y la configuracion regional del equipo.
- LimpiarPantalla: limpia la consola antes de mostrar otra operacion.
- Pausar: espera una tecla para que se pueda leer el resultado.

Los mensajes particulares de cada operacion permanecen en su menu.

## Modulos completos

1. Clientes y vehiculos: registrar, mostrar, buscar por codigo o placa, modificar, eliminar y contar.
2. Ordenes: registrar, mostrar, buscar por cliente, placa o fecha, modificar, cancelar y consultar pendientes.
3. Cola: agregar, atender, consultar el siguiente sin retirarlo, mostrar y contar.
4. Historial: mostrar todas las operaciones desde la mas reciente.

En clientes, las opciones 4, 5 y 6 permiten modificar, eliminar y contar; la opcion 7 regresa.

Para finalizar una orden, entrar a Ordenes, elegir Modificar orden (5) y seleccionar Finalizada (3) al cambiar el estado. La finalizacion queda registrada automaticamente en la pila. La cancelacion tambien se registra si se realiza desde Modificar orden.

## Cola e historial

Repository contiene dos listas y dos estructuras creadas para este proyecto: `ColaManual` y `PilaManual`. Ambas guardan textos (`string`). No se usan los tipos prediseñados `Queue` ni `Stack`.

La cola guarda placas en un arreglo circular de cuatro espacios. Enqueue agrega al final logico, Dequeue devuelve y retira la primera placa, y Peek consulta esa primera placa sin retirarla. Si el arreglo se llena, crea otro con el doble de espacios y copia las placas en su orden FIFO. La vuelta al inicio se calcula con if, sin usar el operador modulo. Antes de atender o consultar se comprueba Count para evitar operar sobre una cola vacia. Es FIFO: primero en entrar, primero en salir.

La pila guarda descripciones con fecha y hora en un arreglo que crece cuando se llena. Push guarda la operacion en el siguiente espacio libre. Pop retira la operacion superior. ObtenerElementos recorre las posiciones ocupadas desde la ultima hasta la primera; consultar el historial conserva todos los elementos. Es el orden LIFO: la ultima operacion aparece primero.

Se registran las altas de clientes y ordenes, modificaciones y eliminaciones, ingresos y atenciones en la cola, cancelaciones y finalizaciones. Las busquedas, consultas y operaciones rechazadas no crean eventos. Cancelar nuevamente una orden ya cancelada tampoco agrega otra cancelacion.

## Verificacion

La compilacion Release termino sin errores ni advertencias. Se ejecutaron 53 comprobaciones en una consola interactiva real, incluyendo dos ejecuciones completas del programa.

El detalle y los datos de prueba estan en [docs/VERIFICACION.md](docs/VERIFICACION.md).

