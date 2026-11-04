
using System;
using System.Collections.Generic;

public class GestorXelaTech
{
    private const int MaximoTecnicos = 6;

    private List<Cliente> clientes = new List<Cliente>();
    private List<Tecnico> tecnicos = new List<Tecnico>();
    private List<Equipo> equipos = new List<Equipo>();
    private List<Solicitud> solicitudes = new List<Solicitud>();
    private List<Repuesto> repuestos = new List<Repuesto>();

    private Queue<Solicitud> colaUrgentes = new Queue<Solicitud>();
    private Queue<Solicitud> colaNormales = new Queue<Solicitud>();

    private int[,] cargaSemanal = new int[6, 5];

    public void RegistrarCliente(Cliente cliente)
    {
        if (cliente == null)
            throw new ArgumentException("El cliente no existe.");

        foreach (Cliente actual in clientes)
        {
            if (actual.GetCodigo().Equals(cliente.GetCodigo(),
                StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Código de cliente duplicado.");

            if (actual.GetCorreo() != "Sin correo" &&
                actual.GetCorreo().Equals(cliente.GetCorreo(),
                StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Correo de cliente duplicado.");
        }

        clientes.Add(cliente);
    }

    public void RegistrarTecnico(Tecnico tecnico)
    {
        if (tecnico == null)
            throw new ArgumentException("El técnico no existe.");

        if (tecnicos.Count >= MaximoTecnicos)
            throw new InvalidOperationException("Máximo de 6 técnicos alcanzado.");

        foreach (Tecnico actual in tecnicos)
        {
            if (actual.GetCodigo().Equals(tecnico.GetCodigo(),
                StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Código de técnico duplicado.");
        }

        tecnicos.Add(tecnico);
    }

    public void RegistrarEquipo(Equipo equipo)
    {
        if (equipo == null)
            throw new ArgumentException("El equipo no existe.");

        if (!clientes.Contains(equipo.GetPropietario()))
            throw new InvalidOperationException("El propietario no está registrado.");

        foreach (Equipo actual in equipos)
        {
            if (actual.GetCodigoInterno().Equals(equipo.GetCodigoInterno(),
                StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Código de equipo duplicado.");

            if (actual.GetNumeroSerie().Equals(equipo.GetNumeroSerie(),
                StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Número de serie duplicado.");
        }

        equipos.Add(equipo);
    }

    public void RegistrarRepuesto(Repuesto repuesto)
    {
        if (repuesto == null)
            throw new ArgumentException("El repuesto no existe.");

        foreach (Repuesto actual in repuestos)
        {
            if (actual.GetCodigo().Equals(repuesto.GetCodigo(),
                StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Código de repuesto duplicado.");
        }

        repuestos.Add(repuesto);
    }

    public void RegistrarSolicitud(Solicitud solicitud)
    {
        if (solicitud == null)
            throw new ArgumentException("La solicitud no existe.");

        if (!clientes.Contains(solicitud.GetCliente()))
            throw new InvalidOperationException("El cliente no está registrado.");

        if (!equipos.Contains(solicitud.GetEquipo()))
            throw new InvalidOperationException("El equipo no está registrado.");

        if (solicitud.GetEstado() != EstadoSolicitud.Recibida)
            throw new InvalidOperationException("La solicitud debe estar en estado Recibida.");

        foreach (Solicitud actual in solicitudes)
        {
            if (actual.GetCodigo().Equals(solicitud.GetCodigo(),
                StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Código de solicitud duplicado.");

            if (actual.GetEquipo() == solicitud.GetEquipo() && actual.EstaActiva())
                throw new InvalidOperationException("El equipo ya tiene una solicitud activa.");
        }

        solicitud.CambiarEstado(EstadoSolicitud.EnEspera);
        solicitudes.Add(solicitud);
        solicitud.GetCliente().IncrementarSolicitudes();

        if (solicitud.GetPrioridad() == Prioridad.Urgente)
            colaUrgentes.Enqueue(solicitud);
        else
            colaNormales.Enqueue(solicitud);
    }

    public Cliente? BuscarCliente(string codigo)
    {
        foreach (Cliente cliente in clientes)
        {
            if (cliente.GetCodigo().Equals(codigo, StringComparison.OrdinalIgnoreCase))
                return cliente;
        }

        return null;
    }

    public Tecnico? BuscarTecnico(string codigo)
    {
        foreach (Tecnico tecnico in tecnicos)
        {
            if (tecnico.GetCodigo().Equals(codigo, StringComparison.OrdinalIgnoreCase))
                return tecnico;
        }

        return null;
    }

    public Equipo? BuscarEquipo(string codigo)
    {
        foreach (Equipo equipo in equipos)
        {
            if (equipo.GetCodigoInterno().Equals(codigo, StringComparison.OrdinalIgnoreCase))
                return equipo;
        }

        return null;
    }

    public Solicitud? BuscarSolicitud(string codigo)
    {
        foreach (Solicitud solicitud in solicitudes)
        {
            if (solicitud.GetCodigo().Equals(codigo, StringComparison.OrdinalIgnoreCase))
                return solicitud;
        }

        return null;
    }

    public Repuesto? BuscarRepuesto(string codigo)
    {
        foreach (Repuesto repuesto in repuestos)
        {
            if (repuesto.GetCodigo().Equals(codigo, StringComparison.OrdinalIgnoreCase))
                return repuesto;
        }

        return null;
    }

    private Solicitud ObtenerSolicitudExistente(string codigo)
    {
        Solicitud? solicitud = BuscarSolicitud(codigo);

        if (solicitud == null)
            throw new InvalidOperationException("La solicitud no existe.");

        return solicitud;
    }

    private void LimpiarCola(Queue<Solicitud> cola)
    {
        while (cola.Count > 0 &&
               cola.Peek().GetEstado() != EstadoSolicitud.EnEspera)
        {
            cola.Dequeue();
        }
    }

    public Solicitud? VerSiguienteSolicitud()
    {
        LimpiarCola(colaUrgentes);
        LimpiarCola(colaNormales);

        if (colaUrgentes.Count > 0)
            return colaUrgentes.Peek();

        if (colaNormales.Count > 0)
            return colaNormales.Peek();

        return null;
    }

    public Tecnico? BuscarTecnicoDisponible(Especialidad especialidad)
    {
        Tecnico? seleccionado = null;

        foreach (Tecnico tecnico in tecnicos)
        {
            if (tecnico.PuedeRecibir(especialidad))
            {
                if (seleccionado == null ||
                    tecnico.GetTrabajosActivos() < seleccionado.GetTrabajosActivos())
                {
                    seleccionado = tecnico;
                }
            }
        }

        return seleccionado;
    }

    public Solicitud AtenderSiguiente(int diaSemana)
    {
        if (diaSemana < 0 || diaSemana > 4)
            throw new ArgumentException("El día debe estar entre 0 y 4.");

        Solicitud? solicitud = VerSiguienteSolicitud();

        if (solicitud == null)
            throw new InvalidOperationException("No hay solicitudes pendientes.");

        Tecnico? tecnico = BuscarTecnicoDisponible(
            solicitud.GetEspecialidadRequerida()
        );

        if (tecnico == null)
            throw new InvalidOperationException(
                "No hay técnicos disponibles con la especialidad requerida."
            );

        solicitud.AsignarTecnico(tecnico);

        if (solicitud.GetPrioridad() == Prioridad.Urgente)
            colaUrgentes.Dequeue();
        else
            colaNormales.Dequeue();

        int indiceTecnico = tecnicos.IndexOf(tecnico);
        cargaSemanal[indiceTecnico, diaSemana]++;

        return solicitud;
    }

    public void IniciarDiagnostico(string codigoSolicitud)
    {
        Solicitud solicitud = ObtenerSolicitudExistente(codigoSolicitud);

        if (solicitud.GetEstado() != EstadoSolicitud.Asignada)
            throw new InvalidOperationException(
                "La solicitud debe estar asignada."
            );

        solicitud.CambiarEstado(EstadoSolicitud.EnDiagnostico);
    }

    public void RegistrarDiagnostico(
        string codigoSolicitud,
        string diagnostico,
        string acciones,
        double costoManoObra)
    {
        Solicitud solicitud = ObtenerSolicitudExistente(codigoSolicitud);

        solicitud.RegistrarDiagnostico(
            diagnostico,
            acciones,
            costoManoObra
        );
    }

    public void IniciarReparacion(string codigoSolicitud)
    {
        Solicitud solicitud = ObtenerSolicitudExistente(codigoSolicitud);

        if (solicitud.GetEstado() != EstadoSolicitud.EnDiagnostico)
            throw new InvalidOperationException(
                "La solicitud debe estar en diagnóstico."
            );

        solicitud.CambiarEstado(EstadoSolicitud.EnReparacion);
    }

    public void AgregarRepuestoSolicitud(
        string codigoSolicitud,
        string codigoRepuesto,
        int cantidad)
    {
        Solicitud solicitud = ObtenerSolicitudExistente(codigoSolicitud);
        Repuesto? repuesto = BuscarRepuesto(codigoRepuesto);

        if (repuesto == null)
            throw new InvalidOperationException("El repuesto no existe.");

        solicitud.AgregarRepuesto(repuesto, cantidad);
    }

    public void MarcarListaParaEntregar(string codigoSolicitud)
    {
        Solicitud solicitud = ObtenerSolicitudExistente(codigoSolicitud);

        if (solicitud.GetEstado() != EstadoSolicitud.EnReparacion)
            throw new InvalidOperationException(
                "La solicitud debe estar en reparación."
            );

        solicitud.CambiarEstado(EstadoSolicitud.ListaParaEntregar);
    }

    public double EntregarSolicitud(string codigoSolicitud)
    {
        Solicitud solicitud = ObtenerSolicitudExistente(codigoSolicitud);

        if (solicitud.GetEstado() != EstadoSolicitud.ListaParaEntregar)
            throw new InvalidOperationException(
                "La solicitud todavía no está lista para entregar."
            );

        solicitud.CambiarEstado(EstadoSolicitud.Entregada);

        return solicitud.GetTotal();
    }

    public void CancelarSolicitud(string codigoSolicitud)
    {
        Solicitud solicitud = ObtenerSolicitudExistente(codigoSolicitud);

        if (!solicitud.EstaActiva())
            throw new InvalidOperationException(
                "La solicitud ya está entregada o cancelada."
            );

        solicitud.CambiarEstado(EstadoSolicitud.Cancelada);
    }

    public void MostrarColas()
    {
        Console.WriteLine("=== SOLICITUDES URGENTES ===");

        int urgentes = 0;

        foreach (Solicitud solicitud in colaUrgentes)
        {
            if (solicitud.GetEstado() == EstadoSolicitud.EnEspera)
            {
                Console.WriteLine(
                    solicitud.GetCodigo() + " - " +
                    solicitud.GetEquipo().ObtenerTipo()
                );
                urgentes++;
            }
        }

        if (urgentes == 0)
            Console.WriteLine("No hay solicitudes urgentes.");

        Console.WriteLine();
        Console.WriteLine("=== SOLICITUDES NORMALES ===");

        int normales = 0;

        foreach (Solicitud solicitud in colaNormales)
        {
            if (solicitud.GetEstado() == EstadoSolicitud.EnEspera)
            {
                Console.WriteLine(
                    solicitud.GetCodigo() + " - " +
                    solicitud.GetEquipo().ObtenerTipo()
                );
                normales++;
            }
        }

        if (normales == 0)
            Console.WriteLine("No hay solicitudes normales.");
    }

    public void MostrarCargaSemanal()
    {
        Console.WriteLine("=== CARGA SEMANAL DE TÉCNICOS ===");
        Console.WriteLine("Técnico\tLun\tMar\tMié\tJue\tVie\tTotal");

        for (int i = 0; i < tecnicos.Count; i++)
        {
            int totalTecnico = 0;

            Console.Write(tecnicos[i].GetCodigo() + "\t");

            for (int j = 0; j < 5; j++)
            {
                Console.Write(cargaSemanal[i, j] + "\t");
                totalTecnico += cargaSemanal[i, j];
            }

            Console.WriteLine(totalTecnico);
        }
    }

    public Tecnico? TecnicoConMasAsignacionesSemanales()
    {
        Tecnico? seleccionado = null;
        int mayor = 0;

        for (int i = 0; i < tecnicos.Count; i++)
        {
            int suma = 0;

            for (int j = 0; j < 5; j++)
                suma += cargaSemanal[i, j];

            if (seleccionado == null || suma > mayor)
            {
                mayor = suma;
                seleccionado = tecnicos[i];
            }
        }

        return seleccionado;
    }

    public int GetCantidadUrgentes()
    {
        int cantidad = 0;

        foreach (Solicitud solicitud in colaUrgentes)
        {
            if (solicitud.GetEstado() == EstadoSolicitud.EnEspera)
                cantidad++;
        }

        return cantidad;
    }

    public int GetCantidadNormales()
    {
        int cantidad = 0;

        foreach (Solicitud solicitud in colaNormales)
        {
            if (solicitud.GetEstado() == EstadoSolicitud.EnEspera)
                cantidad++;
        }

        return cantidad;
    }

    public List<Cliente> GetClientes()
    {
        return new List<Cliente>(clientes);
    }

    public List<Tecnico> GetTecnicos()
    {
        return new List<Tecnico>(tecnicos);
    }

    public List<Equipo> GetEquipos()
    {
        return new List<Equipo>(equipos);
    }

    public List<Solicitud> GetSolicitudes()
    {
        return new List<Solicitud>(solicitudes);
    }

    public List<Repuesto> GetRepuestos()
    {
        return new List<Repuesto>(repuestos);
    }

    public int[,] GetCargaSemanal()
    {
        return (int[,])cargaSemanal.Clone();
    }
}

