public class Impresora : Equipo
{
    public Impresora(string codigo, string serie, string marca, string modelo,
                     Cliente propietario, string estadoFisico, string accesorios)
        : base(codigo, serie, marca, modelo, propietario, estadoFisico, accesorios)
    {
    }

    public override string ObtenerTipo()
    {
        return "Impresora";
    }

    public override double CalcularCostoRevision()
    {
        return 50.00;
    }
}
