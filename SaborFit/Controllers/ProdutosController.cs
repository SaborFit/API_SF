using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaborFit.DAOs;

namespace SaborFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        [HttpGet]
        [Route("ListarCategorias")]
        public IActionResult ListarCategorias()
        {
            var dao = new ProdutosDAO();
            var categorias = dao.ListarCategorias();

            return Ok(categorias);
        }


        [HttpGet]
        [Route("ListarProdutos")]
        public IActionResult ListarProdutos()
        {
            var dao = new ProdutosDAO();
            var produtos = dao.ListarProdutos();

            return Ok(produtos);
        }

        [HttpGet]
        [Route("ListarProdutosCategoria/{idcategoria}")]
        public IActionResult ListarProdutosPorCategoria([FromRoute]int idcategoria)
        {
            var dao = new ProdutosDAO();
            var produtos = dao.ListarProdutosPorCategoria(idcategoria);

            return Ok(produtos);
        }

        [HttpGet]
        [Route("ListarProdutosPorNome")]
        public IActionResult ListarProdutosPorNome([FromQuery] string search)
        {
            var dao = new ProdutosDAO();
            var produtos = dao.ListarProdutosPorNome(search);

            return Ok(produtos);
        }
    }
}
