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

            var query = @"INSERT INTO Clientes (Nome, Sobrenome, Email, Telefone,
                        DataNascimento, CPF, Senha, imagem) VALUES
						(@nome, @sobrenome, @email,@telefone, @nascimento, @CPF, @senha, @imagem);
                        SELECT LAST_INSERT_ID();";

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

        public void CadastrarEndereco(EnderecoDTO endereco)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Enderecos (titulo, endereco, numero, bairro, cidade, uf, cep, complemento, idUser)
                          VALUES (@titulo, @endereco, @numero, @bairro, @cidade, @uf, @cep, @complemento, @idUser)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@titulo", endereco.Titulo);
            comando.Parameters.AddWithValue("@endereco", endereco.Endereco);
            comando.Parameters.AddWithValue("@numero", endereco.Numero);
            comando.Parameters.AddWithValue("@bairro", endereco.Bairro);
            comando.Parameters.AddWithValue("@cidade", endereco.Cidade);
            comando.Parameters.AddWithValue("@uf", endereco.UF);
            comando.Parameters.AddWithValue("@cep", endereco.CEP);
            comando.Parameters.AddWithValue("@complemento", endereco.Complemento);
            comando.Parameters.AddWithValue("@idUser", endereco.cliente.ID);


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
                var dataNascimento = dataReader["DataNascimento"].ToString();

                if (string.IsNullOrWhiteSpace(dataNascimento) is false)
                {
                    cliente.DataNascimento = DateTime.Parse(dataNascimento);
                }

                clientes.Add(cliente);
            }
            conexao.Close();
            return clientes.Count > 0;
        }

        public List<EnderecoDTO> ListarEnderecosPorId(int ID)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT*FROM Enderecos Where ID =@id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", ID);

            var dataReader = comando.ExecuteReader();

            var enderecos = new List<EnderecoDTO>();

            while (dataReader.Read())
            {
                var endereco = new EnderecoDTO();

                endereco.ID = int.Parse(dataReader["ID"].ToString());
                endereco.ID = int.Parse(dataReader["Id"].ToString());
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

                // Atribuindo o objeto cliente ao endereço
                endereco.cliente = cliente;

                enderecos.Add(endereco);
            }
            conexao.Close();

            return enderecos;
        }


        public ClienteDTO Login(ClienteDTO cliente)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Clientes WHERE email = @email and senha = @senha";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@email", cliente.Email);
            comando.Parameters.AddWithValue("@senha", cliente.Senha);

            var dataReader = comando.ExecuteReader();

            cliente = new ClienteDTO();

            while (dataReader.Read())
            {
                cliente.ID = int.Parse(dataReader["ID"].ToString());
                cliente.Nome = dataReader["Nome"].ToString();
                cliente.Sobrenome = dataReader["Sobrenome"].ToString();
                cliente.Email = dataReader["Email"].ToString();
                cliente.Telefone = dataReader["Telefone"].ToString();
                cliente.CPF = dataReader["CPF"].ToString();
                cliente.Senha = dataReader["Senha"].ToString();
                cliente.Imagem = dataReader["Imagem"].ToString();
                var dataNascimento = dataReader["DataNascimento"].ToString();

                if (string.IsNullOrWhiteSpace(dataNascimento) is false)
                {
                    cliente.DataNascimento = DateTime.Parse(dataNascimento);
                }
            }
            conexao.Close();

            return cliente;
        }
    }
}
