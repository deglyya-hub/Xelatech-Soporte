
using System;
using System.Collections.Generic;

public static class Ordenamientos
{
    public static List<Solicitud> BurbujaSolicitudesPorCosto(
        List<Solicitud> solicitudes)
    {
        if (solicitudes == null)
            throw new ArgumentNullException(nameof(solicitudes));

        List<Solicitud> ordenadas = new List<Solicitud>(solicitudes);

        for (int i = 0; i < ordenadas.Count - 1; i++)
        {
            bool intercambio = false;

            for (int j = 0; j < ordenadas.Count - 1 - i; j++)
            {
                if (ordenadas[j].CalcularTotal() <
                    ordenadas[j + 1].CalcularTotal())
                {
                    Solicitud temporal = ordenadas[j];
                    ordenadas[j] = ordenadas[j + 1];
                    ordenadas[j + 1] = temporal;
                    intercambio = true;
                }
            }

            if (!intercambio)
                break;
        }

        return ordenadas;
    }

    public static List<Solicitud> BurbujaSolicitudesPorFecha(
        List<Solicitud> solicitudes)
    {
        if (solicitudes == null)
            throw new ArgumentNullException(nameof(solicitudes));

        List<Solicitud> ordenadas = new List<Solicitud>(solicitudes);

        for (int i = 0; i < ordenadas.Count - 1; i++)
        {
            bool intercambio = false;

            for (int j = 0; j < ordenadas.Count - 1 - i; j++)
            {
                if (ordenadas[j].GetFechaRecepcion() >
                    ordenadas[j + 1].GetFechaRecepcion())
                {
                    Solicitud temporal = ordenadas[j];
                    ordenadas[j] = ordenadas[j + 1];
                    ordenadas[j + 1] = temporal;
                    intercambio = true;
                }
            }

            if (!intercambio)
                break;
        }

        return ordenadas;
    }

    public static List<Tecnico> SeleccionTecnicosPorServicios(
        List<Tecnico> tecnicos)
    {
        if (tecnicos == null)
            throw new ArgumentNullException(nameof(tecnicos));

        List<Tecnico> ordenados = new List<Tecnico>(tecnicos);

        for (int i = 0; i < ordenados.Count - 1; i++)
        {
            int indiceMayor = i;

            for (int j = i + 1; j < ordenados.Count; j++)
            {
                if (ordenados[j].GetServiciosFinalizados() >
                    ordenados[indiceMayor].GetServiciosFinalizados())
                {
                    indiceMayor = j;
                }
            }

            if (indiceMayor != i)
            {
                Tecnico temporal = ordenados[i];
                ordenados[i] = ordenados[indiceMayor];
                ordenados[indiceMayor] = temporal;
            }
        }

        return ordenados;
    }

    public static List<Repuesto> SeleccionRepuestosPorExistencia(
        List<Repuesto> repuestos)
    {
        if (repuestos == null)
            throw new ArgumentNullException(nameof(repuestos));

        List<Repuesto> ordenados = new List<Repuesto>(repuestos);

        for (int i = 0; i < ordenados.Count - 1; i++)
        {
            int indiceMenor = i;

            for (int j = i + 1; j < ordenados.Count; j++)
            {
                if (ordenados[j].GetExistencia() <
                    ordenados[indiceMenor].GetExistencia())
                {
                    indiceMenor = j;
                }
            }

            if (indiceMenor != i)
            {
                Repuesto temporal = ordenados[i];
                ordenados[i] = ordenados[indiceMenor];
                ordenados[indiceMenor] = temporal;
            }
        }

        return ordenados;
    }

    public static void MostrarSolicitudesPorCosto(
        List<Solicitud> solicitudes)
    {
        List<Solicitud> ordenadas =
            BurbujaSolicitudesPorCosto(solicitudes);

        Console.WriteLine("=== SOLICITUDES POR COSTO ===");
        Console.WriteLine(
            "Algoritmo: Burbuja | Criterio: costo total | Orden: descendente"
        );

        if (ordenadas.Count == 0)
        {
            Console.WriteLine("No hay solicitudes registradas.");
            return;
        }

        foreach (Solicitud solicitud in ordenadas)
        {
            Console.WriteLine(
                solicitud.GetCodigo() +
                " | Estado: " + solicitud.GetEstado() +
                " | Total: Q" +
                solicitud.CalcularTotal().ToString("0.00")
            );
        }
    }

    public static void MostrarTecnicosPorServicios(
        List<Tecnico> tecnicos)
    {
        List<Tecnico> ordenados =
            SeleccionTecnicosPorServicios(tecnicos);

        Console.WriteLine("=== TÉCNICOS POR SERVICIOS FINALIZADOS ===");
        Console.WriteLine(
            "Algoritmo: Selección | Criterio: servicios finalizados | Orden: descendente"
        );

        if (ordenados.Count == 0)
        {
            Console.WriteLine("No hay técnicos registrados.");
            return;
        }

        foreach (Tecnico tecnico in ordenados)
        {
            Console.WriteLine(
                tecnico.GetCodigo() +
                " | " + tecnico.GetNombre() +
                " | Servicios finalizados: " +
                tecnico.GetServiciosFinalizados()
            );
        }
    }

    public static void MostrarRepuestosPorExistencia(
        List<Repuesto> repuestos)
    {
        List<Repuesto> ordenados =
            SeleccionRepuestosPorExistencia(repuestos);

        Console.WriteLine("=== REPUESTOS POR EXISTENCIA ===");
        Console.WriteLine(
            "Algoritmo: Selección | Criterio: existencia | Orden: ascendente"
        );

        if (ordenados.Count == 0)
        {
            Console.WriteLine("No hay repuestos registrados.");
            return;
        }

        foreach (Repuesto repuesto in ordenados)
        {
            Console.WriteLine(
                repuesto.GetCodigo() +
                " | " + repuesto.GetNombre() +
                " | Existencia: " +
                repuesto.GetExistencia()
            );
        }
    }

    public static void MostrarSolicitudesPorFecha(
        List<Solicitud> solicitudes)
    {
        List<Solicitud> ordenadas =
            BurbujaSolicitudesPorFecha(solicitudes);

        Console.WriteLine("=== SOLICITUDES POR FECHA ===");
        Console.WriteLine(
            "Algoritmo: Burbuja | Criterio: fecha de recepción | Orden: ascendente"
        );

        if (ordenadas.Count == 0)
        {
            Console.WriteLine("No hay solicitudes registradas.");
            return;
        }

        foreach (Solicitud solicitud in ordenadas)
        {
            Console.WriteLine(
                solicitud.GetCodigo() +
                " | " +
                solicitud.GetFechaRecepcion().ToString("dd/MM/yyyy HH:mm") +
                " | " + solicitud.GetEstado()
            );
        }
    }
}
