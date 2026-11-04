
using System;
using System.Collections.Generic;

public static class Reportes
{
    public static void MostrarMenuReportes()
    {
        Console.WriteLine("=== REPORTES XELATECH ===");
        Console.WriteLine("1. Solicitudes pendientes por prioridad");
        Console.WriteLine("2. Solicitudes agrupadas por estado");
        Console.WriteLine("3. Técnicos disponibles y con carga completa");
        Console.WriteLine("4. Técnico con más servicios finalizados");
        Console.WriteLine("5. Equipos por tipo");
        Console.WriteLine("6. Solicitudes ordenadas por costo");
        Console.WriteLine("7. Solicitudes ordenadas por fecha");
        Console.WriteLine("8. Repuestos ordenados por existencia");
        Console.WriteLine("9. Repuestos agotados o con existencia mínima");
        Console.WriteLine("10. Ingresos por servicios entregados");
        Console.WriteLine("11. Resultados de búsquedas");
        Console.WriteLine("12. Matriz de carga semanal");
    }

    public static void PendientesPorPrioridad(GestorXelaTech gestor)
    {
        Console.WriteLine("=== PENDIENTES POR PRIORIDAD ===");

        gestor.MostrarColas();

        Console.WriteLine();
        Console.WriteLine("Urgentes: " + gestor.GetCantidadUrgentes());
        Console.WriteLine("Normales: " + gestor.GetCantidadNormales());
        Console.WriteLine(
            "Total pendientes: " +
            (gestor.GetCantidadUrgentes() + gestor.GetCantidadNormales()));
    }

    public static void SolicitudesPorEstado(GestorXelaTech gestor)
    {
        Console.WriteLine("=== SOLICITUDES POR ESTADO ===");

        List<Solicitud> solicitudes = gestor.GetSolicitudes();

        foreach (EstadoSolicitud estado in Enum.GetValues<EstadoSolicitud>())
        {
            int cantidad = 0;

            Console.WriteLine();
            Console.WriteLine("Estado: " + estado);

            foreach (Solicitud solicitud in solicitudes)
            {
                if (solicitud.GetEstado() == estado)
                {
                    Console.WriteLine(
                        "  " + solicitud.GetCodigo() +
                        " - " + solicitud.GetCliente().GetNombre()
                    );

                    cantidad++;
                }
            }

            Console.WriteLine("Cantidad: " + cantidad);
        }

        Console.WriteLine();
        Console.WriteLine("Total de solicitudes: " + solicitudes.Count);
    }

    public static void DisponibilidadTecnicos(GestorXelaTech gestor)
    {
        Console.WriteLine("=== DISPONIBILIDAD DE TÉCNICOS ===");

        List<Tecnico> tecnicos = gestor.GetTecnicos();

        int disponibles = 0;
        int completos = 0;
        int noDisponibles = 0;

        foreach (Tecnico tecnico in tecnicos)
        {
            string estado;

            if (!tecnico.EstaDisponible())
            {
                estado = "No disponible";
                noDisponibles++;
            }
            else if (tecnico.TieneCargaCompleta())
            {
                estado = "Carga completa";
                completos++;
            }
            else
            {
                estado = "Disponible";
                disponibles++;
            }

            Console.WriteLine(
                tecnico.GetCodigo() + " - " +
                tecnico.GetNombre() +
                " | Especialidad: " + tecnico.GetEspecialidad() +
                " | Trabajos activos: " + tecnico.GetTrabajosActivos() +
                " | " + estado
            );
        }

        Console.WriteLine();
        Console.WriteLine("Disponibles: " + disponibles);
        Console.WriteLine("Con carga completa: " + completos);
        Console.WriteLine("No disponibles: " + noDisponibles);
    }

    public static void TecnicoConMasServicios(GestorXelaTech gestor)
    {
        Console.WriteLine("=== TÉCNICO CON MÁS SERVICIOS ===");

        List<Tecnico> ordenados =
            Ordenamientos.SeleccionTecnicosPorServicios(
                gestor.GetTecnicos());

        if (ordenados.Count == 0)
        {
            Console.WriteLine("No hay técnicos registrados.");
            return;
        }

        int mayor = ordenados[0].GetServiciosFinalizados();

        Console.WriteLine("Mayor cantidad de servicios: " + mayor);

        foreach (Tecnico tecnico in ordenados)
        {
            if (tecnico.GetServiciosFinalizados() == mayor)
            {
                Console.WriteLine(
                    tecnico.GetCodigo() + " - " +
                    tecnico.GetNombre());
            }
        }
    }

    public static void EquiposPorTipo(GestorXelaTech gestor)
    {
        Console.WriteLine("=== EQUIPOS POR TIPO ===");

        int laptops = 0;
        int escritorios = 0;
        int impresoras = 0;

        foreach (Equipo equipo in gestor.GetEquipos())
        {
            if (equipo is Laptop)
                laptops++;
            else if (equipo is CompuEscritorio)
                escritorios++;
            else if (equipo is Impresora)
                impresoras++;
        }

        Console.WriteLine("Laptops: " + laptops);
        Console.WriteLine("Computadoras de escritorio: " + escritorios);
        Console.WriteLine("Impresoras: " + impresoras);
        Console.WriteLine(
            "Total: " + (laptops + escritorios + impresoras));
    }

    public static void SolicitudesPorCosto(GestorXelaTech gestor)
    {
        Ordenamientos.MostrarSolicitudesPorCosto(
            gestor.GetSolicitudes());
    }

    public static void SolicitudesPorFecha(GestorXelaTech gestor)
    {
        Ordenamientos.MostrarSolicitudesPorFecha(
            gestor.GetSolicitudes());
    }

    public static void RepuestosPorExistencia(GestorXelaTech gestor)
    {
        Ordenamientos.MostrarRepuestosPorExistencia(
            gestor.GetRepuestos());
    }

    public static void AlertasDeInventario(GestorXelaTech gestor)
    {
        Console.WriteLine("=== ALERTAS DE INVENTARIO ===");

        int agotados = 0;
        int minimos = 0;

        foreach (Repuesto repuesto in gestor.GetRepuestos())
        {
            if (repuesto.EstaAgotado())
            {
                Console.WriteLine(
                    "AGOTADO: " + repuesto.GetNombre() +
                    " | Código: " + repuesto.GetCodigo()
                );

                agotados++;
            }
            else if (repuesto.EstaEnMinimo())
            {
                Console.WriteLine(
                    "EXISTENCIA MÍNIMA: " + repuesto.GetNombre() +
                    " | Actual: " + repuesto.GetExistencia() +
                    " | Mínima: " + repuesto.GetExistenciaMinima()
                );

                minimos++;
            }
        }

        Console.WriteLine();
        Console.WriteLine("Repuestos agotados: " + agotados);
        Console.WriteLine("Repuestos en mínimo: " + minimos);

        if (agotados == 0 && minimos == 0)
            Console.WriteLine("No hay alertas de inventario.");
    }

    public static void IngresosPorServicios(GestorXelaTech gestor)
    {
        Console.WriteLine("=== INGRESOS POR SERVICIOS ===");

        double ingresos = 0;
        int entregadas = 0;

        foreach (Solicitud solicitud in gestor.GetSolicitudes())
        {
            if (solicitud.GetEstado() == EstadoSolicitud.Entregada)
            {
                double total = solicitud.CalcularTotal();

                Console.WriteLine(
                    solicitud.GetCodigo() +
                    " | Q" + total.ToString("0.00"));

                ingresos += total;
                entregadas++;
            }
        }

        Console.WriteLine();
        Console.WriteLine("Servicios entregados: " + entregadas);
        Console.WriteLine(
            "Ingresos totales: Q" + ingresos.ToString("0.00")
        );
    }

    public static void ResultadosDeBusqueda(
        GestorXelaTech gestor,
        int tipo,
        string criterio)
    {
        Console.WriteLine("=== RESULTADOS DE BÚSQUEDA ===");

        switch (tipo)
        {
            case 1:
                Busquedas.MostrarClientesPorNombre(
                    gestor.GetClientes(), criterio
                );
                break;

            case 2:
                if (Enum.TryParse<EstadoSolicitud>(
                    criterio, true, out EstadoSolicitud estado) &&
                    Enum.IsDefined(estado))
                {
                    Busquedas.MostrarSolicitudesPorEstado(
                        gestor.GetSolicitudes(), estado
                    );
                }
                else
                {
                    Console.WriteLine("Estado no válido.");
                }
                break;

            case 3:
                Busquedas.MostrarEquiposPorMarca(
                    gestor.GetEquipos(), criterio
                );
                break;

            case 4:
                Busquedas.MostrarBusquedaBinaria(
                    gestor.GetSolicitudes(), criterio
                );
                break;

            default:
                Console.WriteLine("Tipo de búsqueda no válido.");
                break;
        }
    }

    public static void MatrizCargaSemanal(GestorXelaTech gestor)
    {
        gestor.MostrarCargaSemanal();
    }
}
