public class CompuEscritorio : Equipo
{
    public CompuEscritorio(string codigo, string serie, string marca, string modelo,
                           Cliente propietario, string estadoFisico, string accesorios)
        : base(codigo, serie, marca, modelo, propietario, estadoFisico, accesorios)
    {
    }

    public override string ObtenerTipo()
    {
        return "Computadora de escritorio";
    }

    public override double CalcularCostoRevision()
    {
        return 60.00;
    }
}