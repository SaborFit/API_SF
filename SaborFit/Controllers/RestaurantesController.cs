using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaborFit.DAOs;

namespace SaborFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantesController : ControllerBase
    {
        [HttpGet]
        [Route("listarRestaurantes")]
        public IActionResult ListarRestaurantes()
        {
            var dao = new RestaurantesDAO();
            var restaurantes = dao.ListarRestaurantes();

            return Ok(restaurantes);
        }
    }
}
