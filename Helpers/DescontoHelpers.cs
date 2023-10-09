namespace CompanyAPI.Helpers;
public class DescontoHelpers
{
    public static double CalculaIR(double valor)
    {
        switch(valor)
        {
            case <= 1903.98:
                return 0;

            case <= 2826.65:
                return (valor * 0.075) - 142.80;
            
            case <= 3751.05:
                return (valor * 0.15) - 354.80;

            case <= 4664.68:
                return (valor * 0.225) - 636.13;

            default:
                return (valor * 0.275) - 869.36;
        }
    }

    public static double CalculaFgts(double valor)
    {
        return valor * 0.08;
    }

    public static double CalculaInss(double valor)
    {
        switch(valor)
        {
            case <= 1693.72:
                return valor * 0.08;

            case <= 2822.90:
                return valor * 0.09;
            
            case <= 5645.80:
                return valor * 0.11;

            default:
                return valor - 621.03;
        }
    }
}
