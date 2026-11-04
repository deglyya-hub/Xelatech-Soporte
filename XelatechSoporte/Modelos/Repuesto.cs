using System;

public class Repuesto
{
    private string codigo = "";
    private string nombre = "";
    private string categoria = "";
    private int existencia;
    private int existenciaMinima;
    private double costoUnitario;
    private double precioVenta;
    private int cantidadUtilizada;

    public Repuesto(string codigo, string nombre, string categoria, int existencia,
                    int existenciaMinima, double costoUnitario, double precioVenta)
    {
        if (string.IsNullOrWhiteSpace(codigo))
        {
            throw new ArgumentException("El código no puede estar vacío.");
        }
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre no puede estar vacío.");
        }
        if (string.IsNullOrWhiteSpace(categoria))
        {
            throw new ArgumentException("La categoría no puede estar vacía.");
        }
        if (existencia < 0 || existenciaMinima < 0)
        {
            throw new ArgumentException("La existencia no puede ser negativa.");
        }
        if (costoUnitario < 0 || precioVenta < 0)
        {
            throw new ArgumentException("Los costos no pueden ser negativos.");
        }
        if (precioVenta < costoUnitario)
        {
            throw new ArgumentException("El precio de venta no puede ser menor al costo unitario.");
        }

        this.codigo = codigo;
        this.nombre = nombre;
        this.categoria = categoria;
        this.existencia = existencia;
        this.existenciaMinima = existenciaMinima;
        this.costoUnitario = costoUnitario;
        this.precioVenta = precioVenta;
        this.cantidadUtilizada = 0;
    }

    public string GetCodigo()
    {
        return codigo;
    }

    public string GetNombre()
    {
        return nombre;
    }

    public string GetCategoria()
    {
        return categoria;
    }

    public int GetExistencia()
    {
        return existencia;
    }

    public int GetExistenciaMinima()
    {
        return existenciaMinima;
    }

    public double GetPrecioVenta()
    {
        return precioVenta;
    }

    public int GetCantidadUtilizada()
    {
        return cantidadUtilizada;
    }

    public bool HayExistencia(int cantidad)
    {
        return cantidad <= existencia;
    }

    public void Utilizar(int cantidad)
    {
        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad debe ser mayor a cero.");
        }
        if (!HayExistencia(cantidad))
        {
            throw new InvalidOperationException("No hay existencia suficiente de " + nombre + ".");
        }
        existencia = existencia - cantidad;
        cantidadUtilizada = cantidadUtilizada + cantidad;
    }

    public bool EstaEnMinimo()
    {
        return existencia > 0 && existencia <= existenciaMinima;
    }

    public bool EstaAgotado()
    {
        return existencia == 0;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine("Código: " + codigo);
        Console.WriteLine("Nombre: " + nombre);
        Console.WriteLine("Categoría: " + categoria);
        Console.WriteLine("Existencia: " + existencia + " (mínima: " + existenciaMinima + ")");
        Console.WriteLine("Costo unitario: Q" + costoUnitario.ToString("0.00"));
        Console.WriteLine("Precio de venta: Q" + precioVenta.ToString("0.00"));
        Console.WriteLine("Cantidad utilizada: " + cantidadUtilizada);
        if (EstaAgotado())
        {
            Console.WriteLine("ALERTA: repuesto agotado.");
        }
        else if (EstaEnMinimo())
        {
            Console.WriteLine("ALERTA: repuesto en existencia mínima.");
        }
    }
}