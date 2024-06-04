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
        [AllowAnonymous]
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
            endereco.cliente = new ClienteDTO() { ID = idCliente };

            var dao = new ClientesDAO();
            dao.CadastrarEndereco(endereco);

            return Ok("Endereço cadastrado com sucesso!");
        }


        [HttpGet]
        [Route("ListarEnderecosPorID")]
        public IActionResult ListarEnderecosPorId()
        {
            try
            {
                // Obter o ID do cliente a partir do token JWT
                var idClienteClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "ID");

                if (idClienteClaim == null || !int.TryParse(idClienteClaim.Value, out int idCliente))
                {
                    return BadRequest("ID do cliente não encontrado no token.");
                }

                // Usar o ID do cliente para buscar os endereços no banco de dados
                var dao = new ClientesDAO();
                var enderecos = dao.ListarEnderecosPorId(idCliente);

                return Ok(enderecos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor.");
            }

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

                if (clientelogado.ID == 0)
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


        [HttpPut]
        [Route("AtualizarCliente")]
          public IActionResult AtualizarCliente([FromBody] ClienteDTO cliente)
        {
            try
            {
                var idClienteClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "ID");

                if (idClienteClaim == null || !int.TryParse(idClienteClaim.Value, out int idCliente))
                {
                    return BadRequest("ID do cliente não encontrado no token.");
                }

                cliente.ID = idCliente;
                var dao = new ClientesDAO();
                var clienteExistente = dao.ObterClientePorId(idCliente);

                if (clienteExistente == null)
                {
                    return NotFound("Cliente não encontrado.");
                }

                dao.AtualizarCliente(cliente);

                return Ok("Dados atualizados com sucesso!");
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
                    new Claim("Nome",cliente.Nome),
                     new Claim("DataNascimento", cliente.DataNascimento?.ToString("yyyy-MM-dd") ?? string.Empty), // Converte o DateTime? para string
                    new Claim("Telefone",cliente.Telefone),

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
