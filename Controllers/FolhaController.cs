using CompanyAPI.Data;
using CompanyAPI.Helpers;
using CompanyAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class FolhaController : ControllerBase
{
    private readonly AppDataContext _ctx;
    public FolhaController(AppDataContext ctx)
    {
        _ctx = ctx;
    }


    [HttpPost]
    [Route("cadastrar")]
    public IActionResult CadastrarFolha([FromBody] Folha folha)
    {
        try
        {
            Funcionario? funcionario = _ctx.Funcionarios.Find(folha.FuncionarioId);
            if(funcionario == null) return NotFound();
            folha.Funcionario = funcionario;

            folha.SalarioBruto = folha.ValorHora * folha.QuantidadeHoras;

            folha.DescontoIr = DescontoHelpers.CalculaIR(folha.SalarioBruto);
            folha.DescontoFgts = DescontoHelpers.CalculaFgts(folha.SalarioBruto);
            folha.DescontoInss = DescontoHelpers.CalculaInss(folha.SalarioBruto);

            folha.SalarioLiquido = folha.SalarioBruto - folha.DescontoIr - folha.DescontoInss;
            _ctx.Add(folha);
            _ctx.SaveChanges();
            return Created("", folha);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("listar")]
    public IActionResult ListarFolhas()
    {
        try
        {
            List<Folha> folhas = _ctx.Folhas
                                    .Include(x => x.Funcionario)
                                    .ToList();
            return folhas.Count == 0 ? NotFound() : Ok(folhas);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("buscar/{cpf}/{mes}/{ano}")]
    public IActionResult BuscarFolha([FromRoute] string cpf, int mes, int ano)
    {
        try
        {
            Folha folha = _ctx.Folhas.Include(x => x.Funcionario).
                                      FirstOrDefault(x => x.Ano == ano && x.Mes == mes && x.Funcionario.Cpf == cpf);

            if(folha == null) return NotFound();
            
            return Ok(folha);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
