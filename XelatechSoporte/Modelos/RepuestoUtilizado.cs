using System;
public class RepuestoUtilizado
{
    private Repuesto repuesto;
    private int cantidad;

    public RepuestoUtilizado(Repuesto repuesto, int cantidad)
    {
        if (repuesto == null)
        {
            throw new ArgumentException("El repuesto no existe.");
        }
        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad debe ser mayor a cero.");
        }
        this.repuesto = repuesto;
        this.cantidad = cantidad;
    }

    public Repuesto GetRepuesto()
    {
        return repuesto;
    }

    public int GetCantidad()
    {
        return cantidad;
    }

    public double CalcularSubtotal()
    {
        return cantidad * repuesto.GetPrecioVenta();
    }
}