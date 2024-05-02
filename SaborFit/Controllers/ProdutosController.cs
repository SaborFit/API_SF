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
        [Route("listarCategorias")]
        public IActionResult ListarCategorias()
        {
            var dao = new ProdutosDAO();
            var categorias = dao.ListarCategorias();

            return Ok(categorias);
        }


        [HttpGet]
        [Route("listarProdutos")]
        public IActionResult ListarProdutos()
        {
            var dao = new ProdutosDAO();
            var produtos = dao.ListarProdutos();

            return Ok(produtos);
        }

        [HttpGet]
        [Route("listarProdutosCategoria")]
        public IActionResult ListarProdutosPorCategoria(int idcategoria)
        {
            var dao = new ProdutosDAO();
            var produtos = dao.ListarProdutosPorCategoria(idcategoria);

            return Ok(produtos);
        }

    }
}
