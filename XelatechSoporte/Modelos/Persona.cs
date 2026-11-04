using System;
public class Persona
{
    protected string codigo = "";
    protected string nombreCompleto = "";
    protected string telefono = "";

    public Persona(string codigo, string nombre, string telefono)
    {
        SetCodigo(codigo);
        SetNombre(nombre);
        SetTelefono(telefono);
    }
    
    public string GetCodigo()
    {
        return codigo;
    }

    public void SetCodigo(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
        {
            throw new ArgumentException("El código no puede estar vacío.");
        }
        this.codigo = codigo;
    }

    public string GetNombre()
    {
        return nombreCompleto;
    }

    public void SetNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre no puede estar vacío.");
        }
        this.nombreCompleto = nombre;
    }

    public string GetTelefono()
    {
        return telefono;
    }

    public void SetTelefono(string telefono)
    {
        if (string.IsNullOrWhiteSpace(telefono))
        {
            throw new ArgumentException("El teléfono no puede estar vacío.");
        }
        if (telefono.Length != 8)
        {
            throw new ArgumentException("El teléfono debe tener 8 dígitos.");
        }
        foreach (char c in telefono)
        {
            if (!char.IsDigit(c))
            {
                throw new ArgumentException("El teléfono solo puede tener números.");
            }
        }
        this.telefono = telefono;
    }

    public virtual void MostrarInformacion()
    {
        Console.WriteLine("Código: " + codigo);
        Console.WriteLine("Nombre: " + nombreCompleto);
        Console.WriteLine("Teléfono: " + telefono);
    }
}