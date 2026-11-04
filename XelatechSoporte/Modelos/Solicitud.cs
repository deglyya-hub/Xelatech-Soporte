
using System;
using System.Collections.Generic;

public class Solicitud
{
    private string codigo = "";
    private Cliente cliente;
    private Equipo equipo;
    private string descripcionProblema = "";
    private DateTime fechaRecepcion;
    private DateTime horaRecepcion;
    private Prioridad prioridad;
    private EstadoSolicitud estado;
    private Tecnico? tecnicoAsignado;
    private string diagnostico = "";
    private string accionesRealizadas = "";
    private List<RepuestoUtilizado> listaRepuestosUtilizados = new List<RepuestoUtilizado>();
    private double costoRevision;
    private double costoManoObra;
    private double costoRepuestos;
    private double total;
    private int tiempoEstimado;
    private Stack<EstadoSolicitud> historialDeEstados = new Stack<EstadoSolicitud>();

    public Solicitud(string codigo, Cliente cliente, Equipo equipo, string descripcion, Prioridad prioridad)
    {
        if (string.IsNullOrWhiteSpace(codigo))
        {
            throw new ArgumentException("El código no puede estar vacío.");
        }
        if (cliente == null)
        {
            throw new ArgumentException("El cliente no existe.");
        }
        if (equipo == null)
        {
            throw new ArgumentException("El equipo no existe.");
        }
        if (string.IsNullOrWhiteSpace(descripcion))
        {
            throw new ArgumentException("La descripción del problema no puede estar vacía.");
        }

        this.codigo = codigo;
        this.cliente = cliente;
        this.equipo = equipo;
        this.descripcionProblema = descripcion;
        this.prioridad = prioridad;
        this.fechaRecepcion = DateTime.Now;
        this.horaRecepcion = DateTime.Now;
        this.estado = EstadoSolicitud.Recibida;
        this.costoRevision = equipo.CalcularCostoRevision();
    }

    public string GetCodigo()
    {
        return codigo;
    }

    public Cliente GetCliente()
    {
        return cliente;
    }

    public Equipo GetEquipo()
    {
        return equipo;
    }

    public Prioridad GetPrioridad()
    {
        return prioridad;
    }

    public EstadoSolicitud GetEstado()
    {
        return estado;
    }

    public Tecnico? GetTecnicoAsignado()
    {
        return tecnicoAsignado;
    }

    public DateTime GetFechaRecepcion()
    {
        return fechaRecepcion;
    }

    public double GetTotal()
    {
        return total;
    }

    public int GetTiempoEstimado()
    {
        return tiempoEstimado;
    }

    public void SetTiempoEstimado(int horas)
    {
        if (horas < 0)
        {
            throw new ArgumentException("El tiempo estimado no puede ser negativo.");
        }

        tiempoEstimado = horas;
    }

    public Especialidad GetEspecialidadRequerida()
    {
        if (equipo is Laptop)
        {
            return Especialidad.Laptop;
        }

        if (equipo is CompuEscritorio)
        {
            return Especialidad.Escritorio;
        }

        return Especialidad.Impresora;
    }

    public bool EstaActiva()
    {
        return estado != EstadoSolicitud.Entregada &&
               estado != EstadoSolicitud.Cancelada;
    }

    public void CambiarEstado(EstadoSolicitud nuevoEstado)
    {
        if (!EstaActiva())
        {
            throw new InvalidOperationException(
                "La solicitud ya fue entregada o cancelada y no se puede modificar."
            );
        }

        if (nuevoEstado != EstadoSolicitud.Cancelada &&
            (int)nuevoEstado != (int)estado + 1)
        {
            throw new InvalidOperationException(
                "No se puede pasar de " + estado + " a " + nuevoEstado + "."
            );
        }

        if (nuevoEstado == EstadoSolicitud.Asignada && tecnicoAsignado == null)
        {
            throw new InvalidOperationException(
                "Primero se debe asignar un técnico."
            );
        }

        if (nuevoEstado == EstadoSolicitud.ListaParaEntregar &&
            string.IsNullOrWhiteSpace(diagnostico))
        {
            throw new InvalidOperationException(
                "Primero se debe registrar el diagnóstico."
            );
        }

        if (nuevoEstado == EstadoSolicitud.Entregada && tecnicoAsignado != null)
        {
            tecnicoAsignado.FinalizarTrabajo();
        }
        else if (nuevoEstado == EstadoSolicitud.Cancelada && tecnicoAsignado != null)
        {
            tecnicoAsignado.LiberarTrabajo();
        }

        historialDeEstados.Push(estado);
        estado = nuevoEstado;

        if (estado == EstadoSolicitud.Entregada)
        {
            CalcularTotal();
        }
    }

    public void DeshacerUltimoCambio()
    {
        if (!EstaActiva())
        {
            throw new InvalidOperationException(
                "La solicitud ya fue entregada o cancelada y no se puede modificar."
            );
        }

        if (estado == EstadoSolicitud.EnEspera ||
            estado == EstadoSolicitud.Asignada)
        {
            throw new InvalidOperationException(
                "No se puede deshacer desde EnEspera o Asignada para proteger las colas."
            );
        }

        if (historialDeEstados.Count == 0)
        {
            throw new InvalidOperationException(
                "El historial de estados está vacío."
            );
        }

        EstadoSolicitud estadoAnterior = historialDeEstados.Peek();

        if (estadoAnterior == EstadoSolicitud.Asignada ||
            estadoAnterior == EstadoSolicitud.EnEspera)
        {
            throw new InvalidOperationException(
                "No se puede deshacer una asignación desde esta operación."
            );
        }

        estado = historialDeEstados.Pop();
    }


    public void AsignarTecnico(Tecnico tecnico)
    {
        if (tecnico == null)
        {
            throw new ArgumentException("El técnico no existe.");
        }

        if (estado != EstadoSolicitud.EnEspera)
        {
            throw new InvalidOperationException(
                "La solicitud debe estar en espera para asignar un técnico."
            );
        }

        if (!tecnico.PuedeRecibir(GetEspecialidadRequerida()))
        {
            throw new InvalidOperationException(
                "El técnico no tiene la especialidad requerida o tiene la carga completa."
            );
        }

        tecnico.AsignarTrabajo();
        tecnicoAsignado = tecnico;
        CambiarEstado(EstadoSolicitud.Asignada);
    }

    public void RegistrarDiagnostico(string diagnostico, string acciones, double costoManoObra)
    {
        if (estado != EstadoSolicitud.EnDiagnostico)
        {
            throw new InvalidOperationException(
                "La solicitud debe estar en diagnóstico."
            );
        }

        if (string.IsNullOrWhiteSpace(diagnostico) ||
            string.IsNullOrWhiteSpace(acciones))
        {
            throw new ArgumentException(
                "El diagnóstico y las acciones no pueden estar vacíos."
            );
        }

        if (costoManoObra < 0)
        {
            throw new ArgumentException(
                "El costo de mano de obra no puede ser negativo."
            );
        }

        this.diagnostico = diagnostico;
        this.accionesRealizadas = acciones;
        this.costoManoObra = costoManoObra;
    }

    public void AgregarRepuesto(Repuesto repuesto, int cantidad)
    {
        if (estado != EstadoSolicitud.EnDiagnostico &&
            estado != EstadoSolicitud.EnReparacion)
        {
            throw new InvalidOperationException(
                "Solo se pueden agregar repuestos en diagnóstico o reparación."
            );
        }

        RepuestoUtilizado usado = new RepuestoUtilizado(repuesto, cantidad);
        repuesto.Utilizar(cantidad);
        listaRepuestosUtilizados.Add(usado);
        CalcularCostoRepuestos();
    }

    public double CalcularCostoRepuestos()
    {
        double suma = 0;

        foreach (RepuestoUtilizado usado in listaRepuestosUtilizados)
        {
            suma = suma + usado.CalcularSubtotal();
        }

        costoRepuestos = suma;
        return costoRepuestos;
    }

    public double CalcularTotal()
    {
        CalcularCostoRepuestos();
        total = costoRevision + costoManoObra + costoRepuestos;
        return total;
    }

    public void MostrarHistorial()
    {
        Console.WriteLine("Historial de estados (del más reciente al más antiguo):");
        Console.WriteLine("- Estado actual: " + estado);

        if (historialDeEstados.Count == 0)
        {
            Console.WriteLine("No hay estados anteriores.");
            return;
        }

        foreach (EstadoSolicitud anterior in historialDeEstados)
        {
            Console.WriteLine("- " + anterior);
        }
    }

    public void MostrarInformacion()
    {
        Console.WriteLine("Solicitud: " + codigo);
        Console.WriteLine("Cliente: " + cliente.GetNombre());
        Console.WriteLine("Equipo: " + equipo.ObtenerTipo() + " " + equipo.GetNumeroSerie());
        Console.WriteLine("Problema: " + descripcionProblema);
        Console.WriteLine("Recibida: " + fechaRecepcion.ToString("dd/MM/yyyy") + " " + horaRecepcion.ToString("HH:mm"));
        Console.WriteLine("Prioridad: " + prioridad);
        Console.WriteLine("Estado: " + estado);

        if (tecnicoAsignado != null)
        {
            Console.WriteLine("Técnico: " + tecnicoAsignado.GetNombre());
        }

        if (diagnostico != "")
        {
            Console.WriteLine("Diagnóstico: " + diagnostico);
            Console.WriteLine("Acciones: " + accionesRealizadas);
        }

        Console.WriteLine("Costo de revisión: Q" + costoRevision.ToString("0.00"));
        Console.WriteLine("Mano de obra: Q" + costoManoObra.ToString("0.00"));
        Console.WriteLine("Repuestos: Q" + costoRepuestos.ToString("0.00"));
        Console.WriteLine("Total: Q" + total.ToString("0.00"));
    }
}
