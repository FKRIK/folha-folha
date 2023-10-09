using CompanyAPI.Data;
using CompanyAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class FuncionarioController : ControllerBase
{
    private readonly AppDataContext _ctx;

    public FuncionarioController(AppDataContext ctx)
    {
        _ctx = ctx;
    }

    [HttpPost]
    [Route("cadastrar")]
    public IActionResult CadastrarFuncionario([FromBody] Funcionario funcionario)
    {
        try
        {
            _ctx.Add(funcionario);
            _ctx.SaveChanges();
            return Created("", funcionario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("listar")]
    public IActionResult ListarFuncionarios()
    {
        try
        {
            List<Funcionario> funcionarios = _ctx.Funcionarios.ToList();
            return funcionarios.Count == 0 ? NotFound() : Ok(funcionarios);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}