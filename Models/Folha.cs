namespace CompanyAPI.Model;
public class Folha
{
    public int FolhaId { get; set; }
    public double ValorHora { get; set; }
    public int QuantidadeHoras { get; set; }
    public double SalarioBruto { get; set; }
    public double SalarioLiquido { get; set; }
    public double DescontoIr { get; set; }
    public double DescontoInss { get; set; }
    public double DescontoFgts { get; set; }
    public int Mes { get; set; }
    public int Ano { get; set; }
    public Funcionario? Funcionario { get; set; }
    public int FuncionarioId { get; set; }
    public DateTime CriadoEm { get; set; }

    public Folha () => CriadoEm = DateTime.Now;
}
