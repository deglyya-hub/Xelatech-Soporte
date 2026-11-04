
using System;
using System.Collections.Generic;

public static class Busquedas
{
    public static List<Cliente> BuscarClientesPorNombre(
        List<Cliente> clientes,
        string nombre)
    {
        if (clientes == null)
            throw new ArgumentNullException(nameof(clientes));

        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("Ingrese un nombre para buscar.");

        List<Cliente> resultados = new List<Cliente>();

        foreach (Cliente cliente in clientes)
        {
            if (cliente.GetNombre().IndexOf(
                nombre.Trim(),
                StringComparison.OrdinalIgnoreCase) >= 0)
            {
                resultados.Add(cliente);
            }
        }

        return resultados;
    }

    public static List<Solicitud> BuscarSolicitudesPorEstado(
        List<Solicitud> solicitudes,
        EstadoSolicitud estado)
    {
        if (solicitudes == null)
            throw new ArgumentNullException(nameof(solicitudes));

        List<Solicitud> resultados = new List<Solicitud>();

        foreach (Solicitud solicitud in solicitudes)
        {
            if (solicitud.GetEstado() == estado)
            {
                resultados.Add(solicitud);
            }
        }

        return resultados;
    }

    public static List<Equipo> BuscarEquiposPorMarca(
        List<Equipo> equipos,
        string marca)
    {
        if (equipos == null)
            throw new ArgumentNullException(nameof(equipos));

        if (string.IsNullOrWhiteSpace(marca))
            throw new ArgumentException("Ingrese una marca para buscar.");

        List<Equipo> resultados = new List<Equipo>();

        foreach (Equipo equipo in equipos)
        {
            if (equipo.GetMarca().IndexOf(
                marca.Trim(),
                StringComparison.OrdinalIgnoreCase) >= 0)
            {
                resultados.Add(equipo);
            }
        }

        return resultados;
    }

    public static Solicitud? BusquedaBinariaSolicitudPorCodigo(
        List<Solicitud> solicitudes,
        string codigo)
    {
        if (solicitudes == null)
            throw new ArgumentNullException(nameof(solicitudes));

        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("Ingrese un código para buscar.");

        List<Solicitud> ordenadas =
            new List<Solicitud>(solicitudes);

        OrdenarSolicitudesPorCodigo(ordenadas);

        int izquierda = 0;
        int derecha = ordenadas.Count - 1;

        while (izquierda <= derecha)
        {
            int medio = izquierda + (derecha - izquierda) / 2;

            int comparacion = string.Compare(
                ordenadas[medio].GetCodigo(),
                codigo.Trim(),
                StringComparison.OrdinalIgnoreCase
            );

            if (comparacion == 0)
                return ordenadas[medio];

            if (comparacion < 0)
                izquierda = medio + 1;
            else
                derecha = medio - 1;
        }

        return null;
    }

    private static void OrdenarSolicitudesPorCodigo(
        List<Solicitud> solicitudes)
    {
        for (int i = 1; i < solicitudes.Count; i++)
        {
            Solicitud actual = solicitudes[i];
            int j = i - 1;

            while (j >= 0 &&
                   string.Compare(
                       solicitudes[j].GetCodigo(),
                       actual.GetCodigo(),
                       StringComparison.OrdinalIgnoreCase) > 0)
            {
                solicitudes[j + 1] = solicitudes[j];
                j--;
            }

            solicitudes[j + 1] = actual;
        }
    }

    public static void MostrarClientesPorNombre(
        List<Cliente> clientes,
        string nombre)
    {
        List<Cliente> resultados =
            BuscarClientesPorNombre(clientes, nombre);

        Console.WriteLine("=== CLIENTES ENCONTRADOS ===");

        if (resultados.Count == 0)
        {
            Console.WriteLine("No se encontraron clientes.");
            return;
        }

        foreach (Cliente cliente in resultados)
        {
            Console.WriteLine(
                cliente.GetCodigo() + " | " +
                cliente.GetNombre()
            );
        }

        Console.WriteLine("Total encontrados: " + resultados.Count);
    }

    public static void MostrarSolicitudesPorEstado(
        List<Solicitud> solicitudes,
        EstadoSolicitud estado)
    {
        List<Solicitud> resultados =
            BuscarSolicitudesPorEstado(solicitudes, estado);

        Console.WriteLine("=== SOLICITUDES EN ESTADO: " + estado + " ===");

        if (resultados.Count == 0)
        {
            Console.WriteLine("No se encontraron solicitudes.");
            return;
        }

        foreach (Solicitud solicitud in resultados)
        {
            Console.WriteLine(
                solicitud.GetCodigo() + " | " +
                solicitud.GetCliente().GetNombre() + " | " +
                solicitud.GetEstado()
            );
        }

        Console.WriteLine("Total encontrados: " + resultados.Count);
    }

    public static void MostrarEquiposPorMarca(
        List<Equipo> equipos,
        string marca)
    {
        List<Equipo> resultados =
            BuscarEquiposPorMarca(equipos, marca);

        Console.WriteLine("=== EQUIPOS ENCONTRADOS ===");

        if (resultados.Count == 0)
        {
            Console.WriteLine("No se encontraron equipos.");
            return;
        }

        foreach (Equipo equipo in resultados)
        {
            Console.WriteLine(
                equipo.GetCodigoInterno() + " | " +
                equipo.ObtenerTipo() + " | " +
                equipo.GetMarca()
            );
        }

        Console.WriteLine("Total encontrados: " + resultados.Count);
    }

    public static void MostrarBusquedaBinaria(
        List<Solicitud> solicitudes,
        string codigo)
    {
        Solicitud? resultado =
            BusquedaBinariaSolicitudPorCodigo(solicitudes, codigo);

        Console.WriteLine("=== BÚSQUEDA BINARIA POR CÓDIGO ===");

        if (resultado == null)
        {
            Console.WriteLine("No se encontró la solicitud.");
            return;
        }

        resultado.MostrarInformacion();
    }
}