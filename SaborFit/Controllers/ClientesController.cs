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
    [Authorize]
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


        [HttpPost]
        [Route("CadastrarEndereco")]
        public IActionResult CadastrarEndereco([FromBody] EnderecoDTO endereco)
        {
            var idCliente = int.Parse(HttpContext.User.FindFirstValue("id"));
            var dao = new ClientesDAO();
            dao.CadastrarEndereco(endereco);

            return Ok("Endereço cadastrado com sucesso!");
        }
    

        [HttpGet]
        [Route("listarEndereçosPorID")]
        public IActionResult ListarEnderecosPorId(int ID )
        {

            var dao = new ClientesDAO();
            var enderecos = dao.ListarEnderecosPorId(ID);

            return Ok(enderecos);
        }
      
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
                            return Unauthorized("Email ou senha inválidos.");
                        }

                        var token = GenerateJwtToken(clientelogado);

                        return Ok(new { token });
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, "Erro interno do servidor.");
                    }
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
