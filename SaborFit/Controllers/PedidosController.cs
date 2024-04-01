using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaborFit.DAOs;
using SaborFit.DTOs;

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
            var dao = new PedidosDAO();
            dao.Cadastrar(pedido);

            return Ok();
        }
    }
}
