
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
            bool huboIntercambio = false;

            for (int j = 0; j < ordenadas.Count - 1 - i; j++)
            {
                double costoActual = ordenadas[j].CalcularTotal();
                double costoSiguiente = ordenadas[j + 1].CalcularTotal();

                if (costoActual < costoSiguiente)
                {
                    Solicitud temporal = ordenadas[j];
                    ordenadas[j] = ordenadas[j + 1];
                    ordenadas[j + 1] = temporal;

                    huboIntercambio = true;
                }
            }

            if (!huboIntercambio)
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

    public static List<Repuesto> ShellRepuestosPorExistencia(
        List<Repuesto> repuestos)
    {
        if (repuestos == null)
            throw new ArgumentNullException(nameof(repuestos));

        List<Repuesto> ordenados = new List<Repuesto>(repuestos);

        int cantidad = ordenados.Count;

        for (int salto = cantidad / 2; salto > 0; salto /= 2)
        {
            for (int i = salto; i < cantidad; i++)
            {
                Repuesto temporal = ordenados[i];
                int j = i;

                while (j >= salto &&
                       ordenados[j - salto].GetExistencia() >
                       temporal.GetExistencia())
                {
                    ordenados[j] = ordenados[j - salto];
                    j -= salto;
                }

                ordenados[j] = temporal;
            }
        }

        return ordenados;
    }

    public static List<Solicitud> QuickSortSolicitudesPorFecha(
        List<Solicitud> solicitudes)
    {
        if (solicitudes == null)
            throw new ArgumentNullException(nameof(solicitudes));

        List<Solicitud> ordenadas = new List<Solicitud>(solicitudes);

        if (ordenadas.Count > 1)
            QuickSortFechas(ordenadas, 0, ordenadas.Count - 1);

        return ordenadas;
    }

    private static void QuickSortFechas(
        List<Solicitud> solicitudes,
        int izquierda,
        int derecha)
    {
        int i = izquierda;
        int j = derecha;

        DateTime pivote =
            solicitudes[izquierda + (derecha - izquierda) / 2]
            .GetFechaRecepcion();

        while (i <= j)
        {
            while (solicitudes[i].GetFechaRecepcion() < pivote)
                i++;

            while (solicitudes[j].GetFechaRecepcion() > pivote)
                j--;

            if (i <= j)
            {
                Solicitud temporal = solicitudes[i];
                solicitudes[i] = solicitudes[j];
                solicitudes[j] = temporal;

                i++;
                j--;
            }
        }

        if (izquierda < j)
            QuickSortFechas(solicitudes, izquierda, j);

        if (i < derecha)
            QuickSortFechas(solicitudes, i, derecha);
    }

    public static void MostrarSolicitudesPorCosto(
        List<Solicitud> solicitudes)
    {
        List<Solicitud> ordenadas =
            BurbujaSolicitudesPorCosto(solicitudes);

        Console.WriteLine("=== SOLICITUDES POR COSTO ===");

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
            ShellRepuestosPorExistencia(repuestos);

        Console.WriteLine("=== REPUESTOS POR EXISTENCIA ===");

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
            QuickSortSolicitudesPorFecha(solicitudes);

        Console.WriteLine("=== SOLICITUDES POR FECHA ===");

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
