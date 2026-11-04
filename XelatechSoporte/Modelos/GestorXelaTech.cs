
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
        {
            throw new ArgumentException("El cliente no existe.");
        }

        foreach (Cliente actual in clientes)
        {
            if (actual.GetCodigo().Equals(cliente.GetCodigo(),
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Ya existe un cliente con ese código."
                );
            }

            if (actual.GetCorreo() != "Sin correo" &&
                actual.GetCorreo().Equals(cliente.GetCorreo(),
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Ya existe un cliente con ese correo."
                );
            }
        }

        clientes.Add(cliente);
    }

    public void RegistrarTecnico(Tecnico tecnico)
    {
        if (tecnico == null)
        {
            throw new ArgumentException("El técnico no existe.");
        }

        if (tecnicos.Count >= MaximoTecnicos)
        {
            throw new InvalidOperationException(
                "No se pueden registrar más de 6 técnicos."
            );
        }

        foreach (Tecnico actual in tecnicos)
        {
            if (actual.GetCodigo().Equals(tecnico.GetCodigo(),
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Ya existe un técnico con ese código."
                );
            }
        }

        tecnicos.Add(tecnico);
    }

    public void RegistrarEquipo(Equipo equipo)
    {
        if (equipo == null)
        {
            throw new ArgumentException("El equipo no existe.");
        }

        if (!clientes.Contains(equipo.GetPropietario()))
        {
            throw new InvalidOperationException(
                "Primero debe registrarse el propietario del equipo."
            );
        }

        foreach (Equipo actual in equipos)
        {
            if (actual.GetCodigoInterno().Equals(equipo.GetCodigoInterno(),
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Ya existe un equipo con ese código."
                );
            }

            if (actual.GetNumeroSerie().Equals(equipo.GetNumeroSerie(),
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Ya existe un equipo con ese número de serie."
                );
            }
        }

        equipos.Add(equipo);
    }

    public void RegistrarRepuesto(Repuesto repuesto)
    {
        if (repuesto == null)
        {
            throw new ArgumentException("El repuesto no existe.");
        }

        foreach (Repuesto actual in repuestos)
        {
            if (actual.GetCodigo().Equals(repuesto.GetCodigo(),
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Ya existe un repuesto con ese código."
                );
            }
        }

        repuestos.Add(repuesto);
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
