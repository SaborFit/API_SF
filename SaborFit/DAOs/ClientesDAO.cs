using Microsoft.AspNetCore.Connections;
using MySql.Data.MySqlClient;
using SaborFit.DTOs;

namespace SaborFit.DAOs
{
    public class ClientesDAO
    {
        public void CadastrarCliente( ClienteDTO cliente)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Clientes (Nome, Sobrenome, Email, Telefone, DataNascimento, CPF, Senha, imagem) VALUES
						(@nome, @sobrenome, @email,@telefone, @nascimento, @CPF, @senha, @imagem)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", cliente.Nome);
            comando.Parameters.AddWithValue("@sobrenome", cliente.Sobrenome);
            comando.Parameters.AddWithValue("@email", cliente.Email);
            comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
            comando.Parameters.AddWithValue("@nascimento", cliente.DataNascimento);
            comando.Parameters.AddWithValue("@CPF", cliente.CPF);
            comando.Parameters.AddWithValue("@senha", cliente.Senha);
            comando.Parameters.AddWithValue("@imagem", cliente.Imagem);


            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public bool VerificarCliente(ClienteDTO cliente)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Clientes WHERE email = @email";

            var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@email", cliente.Email);

            var dataReader = comando.ExecuteReader();

            var clientes = new List<ClienteDTO>();

            while (dataReader.Read())
            {
                cliente = new ClienteDTO();
                cliente.ID = int.Parse(dataReader["ID"].ToString());
                cliente.Nome = dataReader["Nome"].ToString();
                cliente.Sobrenome = dataReader["Sobrenome"].ToString();
                cliente.Email = dataReader["Email"].ToString();
                cliente.Telefone = dataReader["Telefone"].ToString();
                cliente.CPF = dataReader["CPF"].ToString();
                cliente.Senha = dataReader["Senha"].ToString();
                cliente.Imagem = dataReader["Imagem"].ToString();
                cliente.DataNascimento = DateTime.Parse(dataReader["DataNascimento"].ToString());

                clientes.Add(cliente);
            }
            conexao.Close();
            return clientes.Count > 0;
        }

        public List<EnderecoDTO> ListarEnderecos()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT*FROM Enderecos";

            var comando = new MySqlCommand(query, conexao);
            var dataReader = comando.ExecuteReader();

            var enderecos = new List<EnderecoDTO>();

            while (dataReader.Read())
            {
                var endereco = new EnderecoDTO();
                endereco.Id = int.Parse(dataReader["Id"].ToString());
                endereco.Titulo = dataReader["Titulo"].ToString();
                endereco.Endereco = dataReader["Endereco"].ToString();
                endereco.Numero = dataReader["Numero"].ToString();
                endereco.Bairro = dataReader["Bairro"].ToString();
                endereco.Cidade = dataReader["Cidade"].ToString();
                endereco.UF = dataReader["UF"].ToString();
                endereco.CEP = dataReader["CEP"].ToString();
                endereco.Complemento = dataReader["Complemento"].ToString();

                var cliente = new ClienteDTO();
                cliente.ID = int.Parse(dataReader["IdUser"].ToString());

                enderecos.Add(endereco);
            }
            conexao.Close();

            return enderecos;
        }
    }
}
