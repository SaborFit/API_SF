﻿using Microsoft.AspNetCore.Connections;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
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
                        DataNascimento, CPF, Senha, Imagem) VALUES
						(@nome, @sobrenome, @email,@telefone, @nascimento, @CPF, @senha, @imagemurl);
                        SELECT LAST_INSERT_ID();";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", cliente.Nome);
            comando.Parameters.AddWithValue("@sobrenome", cliente.Sobrenome);
            comando.Parameters.AddWithValue("@email", cliente.Email);
            comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
            comando.Parameters.AddWithValue("@nascimento", cliente.DataNascimento);
            comando.Parameters.AddWithValue("@CPF", cliente.CPF);
            comando.Parameters.AddWithValue("@senha", cliente.Senha);
            comando.Parameters.AddWithValue("@imagemurl", cliente.Imagem);


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

        public List<EnderecoDTO> ListarEnderecosPorId(int idcliente)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT*FROM Enderecos Where IDuser =@idcliente";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@idcliente", idcliente);

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

        public void AtualizarCliente(ClienteDTO cliente)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"UPDATE Clientes 
                  SET Nome = @nome, Sobrenome = @sobrenome, Email = @email, 
                      Telefone = @telefone, DataNascimento = @nascimento,
                      Imagem=@img, Senha = @senha
                  WHERE ID = @id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", cliente.ID);
            comando.Parameters.AddWithValue("@nome", cliente.Nome);
            comando.Parameters.AddWithValue("@sobrenome", cliente.Sobrenome);
            comando.Parameters.AddWithValue("@email", cliente.Email);
            comando.Parameters.AddWithValue("@telefone", cliente.Telefone);
            comando.Parameters.AddWithValue("@nascimento", cliente.DataNascimento);
            comando.Parameters.AddWithValue("@img", cliente.Imagem);
            comando.Parameters.AddWithValue("@senha", cliente.Senha);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public ClienteDTO ObterClientePorId(int id)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Clientes WHERE ID = @id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", id);

            var dataReader = comando.ExecuteReader();

            ClienteDTO cliente = null;

            while (dataReader.Read())
            {
                cliente = new ClienteDTO
                {
                    ID = int.Parse(dataReader["ID"].ToString()),
                    Nome = dataReader["Nome"].ToString(),
                    Sobrenome = dataReader["Sobrenome"].ToString(),
                    Email = dataReader["Email"].ToString(),
                    Telefone = dataReader["Telefone"].ToString(),
                    CPF = dataReader["CPF"].ToString(),
                    Senha = dataReader["Senha"].ToString(),
                    Imagem = dataReader["Imagem"].ToString(),
                    DataNascimento = string.IsNullOrWhiteSpace(dataReader["DataNascimento"].ToString())
                        ? (DateTime?)null
                        : DateTime.Parse(dataReader["DataNascimento"].ToString())
                };
            }

            conexao.Close();

            return cliente;
        }


        public List<ProdutoDTO> ListarFavoritosPorId(int idCliente)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"
                SELECT 
                p.*, 
                r.Nome AS NomeRestaurante,
                r.id AS IdRestaurante
                FROM Produtos p
                INNER JOIN Favoritos f ON p.ID = f.IdProduto
                INNER JOIN Restaurantes r ON p.IdRestaurante = r.id
                WHERE f.IdUser = @idCliente";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@idCliente", idCliente);
            var dataReader = comando.ExecuteReader();

            var produtos = new List<ProdutoDTO>();

            while (dataReader.Read())
            {
                var produto = new ProdutoDTO
                {
                    ID = int.Parse(dataReader["ID"].ToString()),
                    Nome = dataReader["Nome"].ToString(),
                    Tipo = dataReader["Tipo"].ToString(),
                    Descricao = dataReader["Descricao"].ToString(),
                    Ingredientes = dataReader["Ingredientes"].ToString(),
                    Categoria = int.Parse(dataReader["Categoria"].ToString()),
                    Preco = double.Parse(dataReader["Preco"].ToString()),
                    Peso = double.Parse(dataReader["Peso"].ToString()),
                    Quantidade = int.Parse(dataReader["Quantidade"].ToString()),
                    Imagem = dataReader["Imagem"].ToString(),
                    Desconto = double.Parse(dataReader["Desconto"].ToString()),
                    Cnpj = dataReader["Cnpj"].ToString(),
                    Restaurante = new RestauranteDTO
                    {
                        ID = int.Parse(dataReader["IdRestaurante"].ToString()),
                        Nome = dataReader["NomeRestaurante"].ToString()
                    }
                };

                produtos.Add(produto);
            }
            conexao.Close();

            return produtos;
        }

        internal void AdicionarFavorito(int idCliente, int idProduto)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Favoritos (idUser, idProduto) VALUES (@user, @produto)";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@user", idCliente);
            comando.Parameters.AddWithValue("@produto", idProduto);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public void RemoverFavorito(int idCliente, int idProduto)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"DELETE FROM Favoritos WHERE IdUser = @idUser AND IdProduto = @idProduto";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@idUser", idCliente);
            comando.Parameters.AddWithValue("@idProduto", idProduto);

            comando.ExecuteNonQuery();
            conexao.Close();
        }

       /* public string RecuperarSenha(string email)
        {
            string senha = null;

            var conexao = ConnectionFactory.Build();
            conexao.Open();

            try
            {
                string query = @"select Senha from Clientes where Email = @email";

                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    //var comando = new MySqlCommand(query, conexao);
                    comando.Parameters.AddWithValue("@email", email);

                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            senha = reader["Senha"].ToString();
                        }
                    }
                }
                return senha;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexao.Close();
            }
        }
        */
    }


}

