using System;

public class Cliente : Persona
{
    private string correo = "";
    private TipoCliente tipoCliente;
    private int cantidadSolicitudes;

    public Cliente(string codigo, string nombre, string telefono, string correo, TipoCliente tipoCliente)
        : base(codigo, nombre, telefono)
    {
        SetCorreo(correo);
        this.tipoCliente = tipoCliente;
        this.cantidadSolicitudes = 0;
    }

    public Cliente(string codigo, string nombre, string telefono)
        : base(codigo, nombre, telefono)
    {
        this.correo = "Sin correo";
        this.tipoCliente = TipoCliente.Estudiante;
        this.cantidadSolicitudes = 0;
    }

    public string GetCorreo()
    {
        return correo;
    }

    public void SetCorreo(string correo)
    {
        if (string.IsNullOrWhiteSpace(correo))
        {
            throw new ArgumentException("El correo no puede estar vacío.");
        }
        if (!correo.Contains("@"))
        {
            throw new ArgumentException("El correo no es válido.");
        }
        this.correo = correo;
    }

    public TipoCliente GetTipoCliente()
    {
        return tipoCliente;
    }

    public void IncrementarSolicitudes()
    {
        cantidadSolicitudes++;
    }

    public int GetCantidadSolicitudes()
    {
        return cantidadSolicitudes;
    }

    public override void MostrarInformacion()
    {
        base.MostrarInformacion();
        Console.WriteLine("Correo: " + correo);
        Console.WriteLine("Tipo de cliente: " + tipoCliente);
        Console.WriteLine("Solicitudes registradas: " + cantidadSolicitudes);
    }
}