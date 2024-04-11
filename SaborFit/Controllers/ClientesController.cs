using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SaborFit.DAOs;
using SaborFit.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SaborFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ClientesController : ControllerBase
    {
        [HttpPost]
 [Route("Login")]
 [AllowAnonymous]
 public IActionResult Login([FromForm] ClienteDTO cliente)
 {
     try
     {
         var dao = new ClientesDAO();
         var clientelogado = dao.Login(cliente);

         if (clientelogado.ID == null)
         {
             return Unauthorized("Senha inválida.");
         }

         var token = GenerateJwtToken(clientelogado);

         return Ok(new { token });
     }
     catch (Exception ex)
     {
         return StatusCode(500, "Erro interno do servidor.");
     }
 }

        [HttpGet]
        [Route("listarEndereços")]
        public IActionResult ListarEnderecos()
        {
            var dao = new ClientesDAO();
            var enderecos = dao.ListarEnderecos();

            return Ok(enderecos);
        }

        [HttpPost]
        [Route("Login")]
        
        public IActionResult Login([FromForm] ClienteDTO cliente)
        {
            var dao = new ClientesDAO();
            var clientelogado = dao.Login(cliente);

            if (clientelogado.ID == 0)
            {
                return Unauthorized();
            }
            var token = GenerateJwtToken(clientelogado);

            return Ok(new { token });
        }


        private string GenerateJwtToken(ClienteDTO cliente)
        {
            var secretKey = "PU8a9W4sv2opkqlOwmgsn3w3Innlc4D5";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new Claim("ID", cliente.ID.ToString()),
                    new Claim("Email", cliente.Email),
                };

            var token = new JwtSecurityToken(
                "SaborFit", //Nome da sua api
                "SaborFit", //Nome da sua api
                claims, //Lista de claims
                expires: DateTime.UtcNow.AddDays(1), //Tempo de expiração do Token, nesse caso o Token expira em um dia
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
     }
}
