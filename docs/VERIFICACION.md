# Verificacion del proyecto

Fecha: 12 de octubre de 2026.

Se compilo el proyecto en Release con `dotnet build TallerAutomotriz/TallerAutomotriz.csproj -c Release`: cero errores y cero advertencias.

Se ejecuto la aplicacion real mediante `dotnet run --project TallerAutomotriz/TallerAutomotriz.csproj -c Release --no-build` en una terminal interactiva. Se ingresaron las opciones y los datos por teclado y se compararon los mensajes esperados con la salida de cada paso. Las pausas con ReadKey y el regreso a los menus se probaron durante estos recorridos.

## Datos de prueba

- Cliente 101, Kevin Prueba, placa HAA1234. Se modifico a Kevin Modificado y luego se elimino.
- Orden OS10: se registro pendiente y se finalizo desde Modificar orden.
- Orden OS20: se registro y luego se cancelo.
- Cola: HAA1234 seguida de HBB5678.
- Busquedas inexistentes: cliente 999 y orden NOEXISTE.
- Entradas incorrectas: texto obligatorio vacio, entero abc, fecha incorrecta, costo abc y costo negativo.
- Al cerrar la primera ejecucion se abrio otra para comprobar el historial inicialmente vacio.

## Comprobaciones

| N. | Caso | Resultado |
| --- | --- | --- |
| 1 | Registrar con texto vacio, entero y fecha invalidos | Correcto |
| 2 | Conservar cliente al salir y volver | Correcto |
| 3 | Buscar cliente por codigo | Correcto |
| 4 | Buscar cliente por placa | Correcto |
| 5 | Rechazar codigo duplicado | Correcto |
| 6 | Buscar cliente inexistente | Correcto |
| 7 | Modificar cliente | Correcto |
| 8 | Consultar cliente modificado | Correcto |
| 9 | Total clientes | Correcto |
| 10 | Ordenes vacias | Correcto |
| 11 | Registrar orden con costo invalido y negativo | Correcto |
| 12 | Conservar orden al salir y volver | Correcto |
| 13 | Rechazar orden duplicada | Correcto |
| 14 | Buscar orden por cliente | Correcto |
| 15 | Buscar orden por placa | Correcto |
| 16 | Buscar orden por fecha | Correcto |
| 17 | Buscar orden inexistente | Correcto |
| 18 | Mostrar pendientes | Correcto |
| 19 | Finalizar orden mediante modificacion | Correcto |
| 20 | Orden finalizada fuera de pendientes | Correcto |
| 21 | Cancelar orden inexistente | Correcto |
| 22 | Registrar segunda orden | Correcto |
| 23 | Cancelar segunda orden | Correcto |
| 24 | Consultar estados finales | Correcto |
| 25 | Atender cola vacia | Correcto |
| 26 | Consultar cola vacia | Correcto |
| 27 | Mostrar cola vacia | Correcto |
| 28 | Agregar primer vehiculo | Correcto |
| 29 | Agregar segundo vehiculo | Correcto |
| 30 | Conservar cola al salir y volver | Correcto |
| 31 | Consultar inicio sin retirar | Correcto |
| 32 | Consulta conserva total | Correcto |
| 33 | Atender FIFO | Correcto |
| 34 | Consultar siguiente | Correcto |
| 35 | Historial de todos los modulos | Correcto |
| 36 | Consultar historial por segunda vez | Correcto |
| 37 | Modificar cliente inexistente | Correcto |
| 38 | Eliminar cliente inexistente | Correcto |
| 39 | Eliminar cliente existente | Correcto |
| 40 | Total despues de eliminar | Correcto |
| 41 | Lista vacia despues de eliminar | Correcto |
| 42 | Modificar orden inexistente | Correcto |
| 43 | Cancelar nuevamente sin duplicar evento | Correcto |
| 44 | Historial despues de eliminar y cancelar otra vez | Correcto |
| 45 | Regresar a cola conservada | Correcto |
| 46 | Atender ultimo vehiculo | Correcto |
| 47 | Cola vacia despues de atender | Correcto |
| 48 | No atender una cola agotada | Correcto |
| 49 | Opcion principal invalida | Correcto |
| 50 | Salida normal | Correcto |
| 51 | Historial inicialmente vacio | Correcto |
| 52 | Opcion invalida en historial | Correcto |
| 53 | Cerrar segunda ejecucion | Correcto |

## Observaciones verificadas

- Los clientes, las ordenes y la cola conservaron su contenido al salir y volver a entrar en cada modulo.
- Consultar el proximo vehiculo con Peek mantuvo el total de dos vehiculos.
- Se atendio HAA1234 antes de HBB5678, respetando FIFO.
- El historial mostro Inicio de atencion: HAA1234 antes de Ingreso a la fila: HBB5678, y este antes de Ingreso a la fila: HAA1234.
- Una segunda consulta del historial mantuvo las operaciones anteriores.
- La cancelacion repetida de OS20 dejo una sola entrada de cancelacion.
- Los intentos con 999 y NOEXISTE no aparecieron como operaciones exitosas.
- Se observaron los mensajes de validacion para entero, texto vacio, fecha y costo.
- Ambas ejecuciones terminaron con Programa finalizado.

Estas comprobaciones cubren los recorridos indicados. El programa conserva el almacenamiento temporal en RAM y la interpretacion de fechas y decimales segun la configuracion regional del equipo.

## Verificacion de las estructuras manuales

Despues de reemplazar los tipos Queue y Stack por ColaManual y PilaManual, se compilo el proyecto en Release sin errores ni advertencias. Una comprobacion aislada ejercito la cola circular con A, B, C y D; retiro A, agrega E, retira B y agrega F y G, verificando que el orden final fuera C, D, E, F, G. Esto tambien verifico el crecimiento de capacidad, Count y Peek.

La misma comprobacion agrego cinco elementos a la pila para forzar su crecimiento, verifico que la consulta devolviera E, D, C, B, A y comprobo que Pop retirara E y redujera Count. Todas las comprobaciones terminaron correctamente.

