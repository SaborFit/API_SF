using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaborFit.DAOs;
using SaborFit.DTOs;

namespace SaborFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpPost]
        [Route("CadastrarCliente")]
        public IActionResult CadastrarCliente([FromBody] ClienteDTO cliente)
        {
            var dao = new ClientesDAO();

            var ClienteExiste = dao.VerificarCliente(cliente);
            if (ClienteExiste)
            {
                var mensagem = "Email já existe na base de dados";
                return Conflict(mensagem);
            }
            dao.CadastrarCliente(cliente);

            return Ok();
        }

        [HttpGet]
        [Route("listarEndereços")]
        public IActionResult ListarEnderecos()
        {
            var dao = new ClientesDAO();
            var enderecos = dao.ListarEnderecos();

            return Ok(enderecos);
        }
    }
}
