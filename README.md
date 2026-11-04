# XelaTech Soporte

## Descripción del proyecto

XelaTech Soporte es un sistema de consola desarrollado en C# para administrar las solicitudes de mantenimiento y reparación de equipos de cómputo.

El sistema permite registrar clientes, técnicos, equipos, repuestos y solicitudes de servicio. También facilita la asignación de trabajos, el seguimiento de reparaciones, el control del inventario y la generación de reportes.

## Información del estudiante

- **Nombre:** Degly Xhunlol Mateo Juan
- **Carné:** 1610826
- **Lenguaje de programación:** C#

## Funcionalidades principales

### 1. Gestión de registros

- Registro y consulta de clientes.
- Registro de técnicos según su especialidad.
- Registro de laptops, computadoras de escritorio e impresoras.
- Registro y control de repuestos.
- Registro de solicitudes de servicio.

### 2. Gestión de solicitudes

- Clasificación de solicitudes con prioridad normal o urgente.
- Atención de solicitudes mediante colas FIFO.
- Asignación de técnicos según su especialidad y disponibilidad.
- Seguimiento de los estados de diagnóstico, reparación y entrega.
- Registro de diagnósticos, acciones realizadas y costos.
- Utilización de repuestos del inventario.
- Historial de estados mediante una pila.
- Cancelación y entrega de solicitudes.

### 3. Algoritmos de ordenamiento

Se implementaron dos algoritmos de ordenamiento:

**Burbuja (Bubble Sort):**

- Ordenamiento de solicitudes por costo total, de mayor a menor.
- Ordenamiento de solicitudes por fecha de recepción, de la más antigua a la más reciente.

**Selección (Selection Sort):**

- Ordenamiento de técnicos por servicios finalizados, de mayor a menor.
- Ordenamiento de repuestos por existencia, de menor a mayor.

### 4. Algoritmos de búsqueda

- Búsqueda secuencial de clientes por nombre.
- Búsqueda secuencial de solicitudes por estado.
- Búsqueda secuencial de equipos por marca.
- Búsqueda binaria de solicitudes por código.

### 5. Reportes

El sistema cuenta con 12 reportes:

1. Solicitudes pendientes por prioridad.
2. Solicitudes agrupadas por estado.
3. Técnicos disponibles y con carga completa.
4. Técnico con más servicios finalizados.
5. Equipos por tipo.
6. Solicitudes ordenadas por costo.
7. Solicitudes ordenadas por fecha.
8. Repuestos ordenados por existencia.
9. Repuestos agotados o con existencia mínima.
10. Ingresos por servicios entregados.
11. Resultados de búsquedas.
12. Matriz de carga semanal de técnicos.

### 6. Datos de prueba

El sistema incluye un generador de datos de prueba que permite cargar registros de ejemplo para comprobar las funcionalidades sin ingresarlos manualmente.

Los datos de prueba deben cargarse cuando el gestor se encuentra vacío.

## Requisitos para ejecutar el proyecto

- Sistema operativo compatible con .NET.
- SDK de .NET correspondiente a la versión del proyecto.
- Visual Studio con soporte para desarrollo en C#, o una terminal con .NET SDK.

## Instrucciones de ejecución

### Desde Visual Studio

1. Descargar o clonar el repositorio.
2. Abrir la solución o el proyecto en Visual Studio.
3. Compilar el proyecto.
4. Seleccionar una opción del menú principal.

## Uso del sistema

Al iniciar la aplicación se muestra un menú principal con las opciones para registrar información, gestionar solicitudes, realizar búsquedas, ordenar registros y generar reportes.

Para probar el funcionamiento del sistema sin registrar todos los datos manualmente, se puede seleccionar la opción **11. Cargar datos de prueba**.

## Estructuras de datos utilizadas

- **Listas:** almacenamiento de clientes, técnicos, equipos, repuestos y solicitudes.
- **Colas:** atención de solicitudes urgentes y normales.
- **Pilas:** almacenamiento del historial de estados.
- **Matriz bidimensional:** registro de la carga semanal de los técnicos.

## Programación orientada a objetos

El proyecto utiliza conceptos de programación orientada a objetos, como clases, objetos, herencia, encapsulamiento, abstracción y polimorfismo, para organizar las entidades y funcionalidades del sistema.

## Repositorio

[https://github.com/deglyya-hub/Xelatech-Soporte](https://github.com/deglyya-hub/Xelatech-Soporte)
