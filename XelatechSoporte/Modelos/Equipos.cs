using System;

public abstract class Equipo
{
    protected string codigoInterno = "";
    protected string numeroSerie = "";
    protected string marca = "";
    protected string modelo = "";
    protected Cliente propietario;
    protected string estadoFisicoRecibido = "";
    protected string accesoriosEntregados = "";

    public Equipo(string codigo, string serie, string marca, string modelo,
                  Cliente propietario, string estadoFisico, string accesorios)
    {
        if (propietario == null)
        {
            throw new ArgumentException("El equipo debe tener un propietario.");
        }
        this.codigoInterno = ValidarTexto(codigo, "código interno");
        this.numeroSerie = ValidarTexto(serie, "número de serie");
        this.marca = ValidarTexto(marca, "marca");
        this.modelo = ValidarTexto(modelo, "modelo");
        this.propietario = propietario;
        this.estadoFisicoRecibido = ValidarTexto(estadoFisico, "estado físico");
        this.accesoriosEntregados = ValidarTexto(accesorios, "accesorios");
    }

    private string ValidarTexto(string valor, string campo)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            throw new ArgumentException("El campo " + campo + " no puede estar vacío.");
        }
        return valor;
    }

    public string GetCodigoInterno()
    {
        return codigoInterno;
    }

    public string GetNumeroSerie()
    {
        return numeroSerie;
    }

    public string GetMarca()
    {
        return marca;
    }

    public Cliente GetPropietario()
    {
        return propietario;
    }

    public abstract string ObtenerTipo();

    public abstract double CalcularCostoRevision();

    public virtual void MostrarInformacion()
    {
        Console.WriteLine("Código: " + codigoInterno);
        Console.WriteLine("Tipo: " + ObtenerTipo());
        Console.WriteLine("Serie: " + numeroSerie);
        Console.WriteLine("Marca: " + marca);
        Console.WriteLine("Modelo: " + modelo);
        Console.WriteLine("Propietario: " + propietario.GetNombre());
        Console.WriteLine("Estado físico: " + estadoFisicoRecibido);
        Console.WriteLine("Accesorios: " + accesoriosEntregados);
        Console.WriteLine("Costo de revisión: Q" + CalcularCostoRevision().ToString("0.00"));
    }
}