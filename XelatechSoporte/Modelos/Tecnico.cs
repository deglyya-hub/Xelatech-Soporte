using System;

public class Tecnico : Persona
{
    private const int LimiteTrabajos = 3;

    private Especialidad especialidad;
    private bool disponible;
    private int trabajosActivos;
    private int serviciosFinalizados;
    private int[] arregloCalificaciones = new int[5];

    public Tecnico(string codigo, string nombre, string telefono, Especialidad especialidad)
        : base(codigo, nombre, telefono)
    {
        this.especialidad = especialidad;
        this.disponible = true;
        this.trabajosActivos = 0;
        this.serviciosFinalizados = 0;
    }

    public Especialidad GetEspecialidad()
    {
        return especialidad;
    }

    public int GetTrabajosActivos()
    {
        return trabajosActivos;
    }

    public int GetServiciosFinalizados()
    {
        return serviciosFinalizados;
    }

    public bool EstaDisponible()
    {
        return disponible;
    }

    public bool TieneCargaCompleta()
    {
        return trabajosActivos >= LimiteTrabajos;
    }

    public bool PuedeRecibir(Especialidad especialidadRequerida)
    {
        return disponible && especialidad == especialidadRequerida && !TieneCargaCompleta();
    }

    public void AsignarTrabajo()
    {
        if (TieneCargaCompleta())
        {
            throw new InvalidOperationException("El técnico ya tiene la carga completa.");
        }
        trabajosActivos++;
    }

    public void FinalizarTrabajo()
    {
        if (trabajosActivos == 0)
        {
            throw new InvalidOperationException("El técnico no tiene trabajos activos.");
        }
        trabajosActivos--;
        serviciosFinalizados++;
    }

    public void RegistrarCalificacion(int nota)
    {
        if (nota < 1 || nota > 5)
        {
            throw new ArgumentException("La calificación debe estar entre 1 y 5.");
        }
        arregloCalificaciones[nota - 1]++;
    }

    public double CalcularPromedio()
    {
        int total = 0;
        int suma = 0;
        for (int i = 0; i < arregloCalificaciones.Length; i++)
        {
            total += arregloCalificaciones[i];
            suma += arregloCalificaciones[i] * (i + 1);
        }
        if (total == 0)
        {
            return 0;
        }
        return (double)suma / total;
    }

    public override void MostrarInformacion()
    {
        base.MostrarInformacion();
        Console.WriteLine("Especialidad: " + especialidad);
        Console.WriteLine("Trabajos activos: " + trabajosActivos + " de " + LimiteTrabajos);
        Console.WriteLine("Servicios finalizados: " + serviciosFinalizados);
        Console.WriteLine("Calificación promedio: " + CalcularPromedio().ToString("0.0"));
    }
}
