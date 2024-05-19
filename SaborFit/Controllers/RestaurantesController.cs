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

        [HttpGet]
        [Route("ListarRestaurantesAbertos")]
        public IActionResult ListarRestaurantesAbertos()
        {
            try
            {
                var dao = new RestaurantesDAO();
                var restaurantesAbertos = dao.ListarRestaurantesAbertos();
                return Ok(restaurantesAbertos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar restaurantes abertos: {ex.Message}");
            }
        }
    }
}
