using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaborFit.DAOs;
using SaborFit.DTOs;
using System.Security.Claims;

namespace SaborFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PedidosController : ControllerBase
    {
        [HttpPost]
        [Route("CadastrarPedido")]
        public IActionResult CadastrarPedido([FromBody] PedidoDTO pedido)
        {
            var idCliente = int.Parse(HttpContext.User.FindFirstValue("id"));
            pedido.cliente = new ClienteDTO() { ID = idCliente };

            var dao = new PedidosDAO();
            dao.Cadastrar(pedido);

            return Ok();
        }

        [HttpPut]
        [Route("status")]
        public IActionResult AlterarStatusPedido(int status, int pedido)
        {
            var dao = new ProdutosDAO();
            dao.AtualizarPedido(status, pedido);

            return Ok();
        }
    }
}