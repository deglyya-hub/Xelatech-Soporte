public class Laptop : Equipo
{
    public Laptop(string codigo, string serie, string marca, string modelo,
                  Cliente propietario, string estadoFisico, string accesorios)
        : base(codigo, serie, marca, modelo, propietario, estadoFisico, accesorios)
    {
    }

    public override string ObtenerTipo()
    {
        return "Laptop";
    }

    public override double CalcularCostoRevision()
    {
        return 75.00;
    }
}