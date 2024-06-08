using MySql.Data.MySqlClient;
using SaborFit.DTOs;
using System.Runtime.Intrinsics.X86;
using System;

namespace SaborFit.DAOs
{
    public class ProdutosDAO
    {
        public List<CategoriaDTO> ListarCategorias()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT*FROM Categorias";

            var comando = new MySqlCommand(query, conexao);
            var dataReader = comando.ExecuteReader();

            var categorias = new List<CategoriaDTO>();

            while (dataReader.Read())
            {
                var categoria = new CategoriaDTO();
                categoria.ID = int.Parse(dataReader["ID"].ToString());
                categoria.Nome = dataReader["Nome"].ToString();
                categoria.Imagem = dataReader["Imagem"].ToString();


                categorias.Add(categoria);
            }
            conexao.Close();

            return categorias;
        }

        public List<ProdutoDTO> ListarProdutos()
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT*FROM Produtos";

            var comando = new MySqlCommand(query, conexao);
            var dataReader = comando.ExecuteReader();

            var produtos = new List<ProdutoDTO>();

            while (dataReader.Read())
            {
                var produto = new ProdutoDTO();

                produto.ID = int.Parse(dataReader["ID"].ToString());
                produto.Nome = dataReader["Nome"].ToString();
                produto.Tipo = dataReader["Tipo"].ToString();
                produto.Descricao = dataReader["Descricao"].ToString();
                produto.Ingredientes = dataReader["Ingredientes"].ToString();
                produto.Categoria = int.Parse(dataReader["Categoria"].ToString());
                produto.Preco = double.Parse(dataReader["Preco"].ToString());
                produto.Peso = double.Parse(dataReader["Peso"].ToString());
                produto.Quantidade = int.Parse(dataReader["Quantidade"].ToString());
                produto.Imagem = dataReader["Imagem"].ToString();
                produto.Desconto = double.Parse(dataReader["Desconto"].ToString());
                produto.Cnpj = dataReader["Cnpj"].ToString();

                var restaurante = new RestauranteDTO();
                restaurante.ID = int.Parse(dataReader["IdRestaurante"].ToString());


                produto.Restaurante = restaurante;

                produtos.Add(produto);
            }
            conexao.Close();

            return produtos;
        }

        public List<ProdutoDTO> ListarProdutosPorID(int ID)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT Produtos.*, Restaurantes.telefone FROM Produtos INNER JOIN Restaurantes " +
                "ON Produtos.restaurante_id = Restaurantes.id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@ID", ID);
            var dataReader = comando.ExecuteReader();

            var produtos = new List<ProdutoDTO>();

            while (dataReader.Read())
            {
                var produto = new ProdutoDTO();

                produto.ID = int.Parse(dataReader["ID"].ToString());
                produto.Nome = dataReader["Nome"].ToString();
                produto.Tipo = dataReader["Tipo"].ToString();
                produto.Descricao = dataReader["Descricao"].ToString();
                produto.Ingredientes = dataReader["Ingredientes"].ToString();
                produto.Categoria = int.Parse(dataReader["Categoria"].ToString());
                produto.Preco = double.Parse(dataReader["Preco"].ToString());
                produto.Peso = double.Parse(dataReader["Peso"].ToString());
                produto.Quantidade = int.Parse(dataReader["Quantidade"].ToString());
                produto.Imagem = dataReader["Imagem"].ToString();
                produto.Desconto = double.Parse(dataReader["Desconto"].ToString());
                produto.Cnpj = dataReader["Cnpj"].ToString();

                var restaurante = new RestauranteDTO();
                restaurante.ID = int.Parse(dataReader["IdRestaurante"].ToString());


                produto.Restaurante = restaurante;

                produtos.Add(produto);
            }
            conexao.Close();

            return produtos;
        }

        public List<ProdutoDTO> ListarProdutosPorCategoria(int idcategoria)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"
        SELECT 
            p.*, 
            GROUP_CONCAT(m.id) AS MarcadoresIDs,
            GROUP_CONCAT(m.nome) AS MarcadoresNomes
        FROM 
            Produtos p
        LEFT JOIN 
            MarcadorProduto mp ON p.id = mp.idProduto
        LEFT JOIN 
            Marcadores m ON mp.idMarcador = m.id
        WHERE 
            p.categoria = @idCategoria
        GROUP BY 
            p.id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@idCategoria", idcategoria);
            var dataReader = comando.ExecuteReader();

            var produtos = new List<ProdutoDTO>();

            while (dataReader.Read())
            {
                var produto = new ProdutoDTO();
                produto.ID = int.Parse(dataReader["ID"].ToString());
                produto.Nome = dataReader["Nome"].ToString();
                produto.Tipo = dataReader["Tipo"].ToString();
                produto.Descricao = dataReader["Descricao"].ToString();
                produto.Ingredientes = dataReader["Ingredientes"].ToString();
                produto.Categoria = int.Parse(dataReader["Categoria"].ToString());
                produto.Preco = double.Parse(dataReader["Preco"].ToString());
                produto.Peso = double.Parse(dataReader["Peso"].ToString());
                produto.Quantidade = int.Parse(dataReader["Quantidade"].ToString());
                produto.Imagem = dataReader["Imagem"].ToString();
                produto.Desconto = double.Parse(dataReader["Desconto"].ToString());
                produto.Cnpj = dataReader["Cnpj"].ToString();

                // Verifica se há marcadores associados ao produto
                if (!dataReader.IsDBNull(dataReader.GetOrdinal("MarcadoresIDs")))
                {
                    var marcadorIDs = dataReader["MarcadoresIDs"].ToString().Split(',');
                    var marcadorNomes = dataReader["MarcadoresNomes"].ToString().Split(',');

                    for (int i = 0; i < marcadorIDs.Length; i++)
                    {
                        var marcador = new MarcadorDTO
                        {
                            ID = int.Parse(marcadorIDs[i]),
                            Nome = marcadorNomes[i]
                        };

                        produto.Marcadores.Add(marcador);
                    }
                }

                produtos.Add(produto);
            }

            conexao.Close();

            return produtos;
        }


        internal void AtualizarPedido(int status, int pedido)
        {

            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"UPDATE Pedidos SET status = @status WHERE ID = @id;";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@id", pedido);
            comando.Parameters.AddWithValue("@status", status);

            conexao.Close();
        }
          public List<ProdutoDTO> ListarProdutosPorNome(string Nome)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @" SELECT p.*, GROUP_CONCAT(m.id) AS MarcadoresIDs,GROUP_CONCAT(m.nome) 
            AS NomesMarcadores
            FROM Produtos p 
            LEFT JOIN MarcadorProduto mp ON p.id = mp.idProduto
            LEFT JOIN Marcadores m ON mp.idMarcador = m.id
            WHERE p.nome LIKE @nome
            GROUP BY p.id";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@nome", $"%{Nome}%");
            var dataReader = comando.ExecuteReader();

            var produtos = new List<ProdutoDTO>();

            while (dataReader.Read())
            {
                var produtoId = int.Parse(dataReader["ID"].ToString());
                var produto = produtos.FirstOrDefault(p => p.ID == produtoId);

                if (produto == null)
                {
                    produto = new ProdutoDTO
                    {
                        ID = produtoId,
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
                        Marcadores = new List<MarcadorDTO>() // Inicializa a lista de marcadores
                    };

                    produtos.Add(produto);
                }

                if (!dataReader.IsDBNull(dataReader.GetOrdinal("MarcadoresIDs")))
                {
                    var marcadoresIDs = dataReader["MarcadoresIDs"].ToString().Split(',');
                    var nomesMarcadores = dataReader["NomesMarcadores"].ToString().Split(',');

                    for (int i = 0; i < marcadoresIDs.Length; i++)
                    {
                        var marcador = new MarcadorDTO
                        {
                            ID = int.Parse(marcadoresIDs[i]),
                            Nome = nomesMarcadores[i]
                        };
                        produto.Marcadores.Add(marcador);
                    }
                }
            }

            conexao.Close();
            return produtos;
        }




        public List<ProdutoDTO> ListarProdutosPorRestaurante(int idrestaurante)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Produtos WHERE IdRestaurante = @idRestaurante";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@idRestaurante", idrestaurante);
            var dataReader = comando.ExecuteReader();

            var produtos = new List<ProdutoDTO>();

            while (dataReader.Read())
            {
                var produto = new ProdutoDTO();
                produto.ID = int.Parse(dataReader["ID"].ToString());
                produto.Nome = dataReader["Nome"].ToString();
                produto.Tipo = dataReader["Tipo"].ToString();
                produto.Descricao = dataReader["Descricao"].ToString();
                produto.Ingredientes = dataReader["Ingredientes"].ToString();
                produto.Categoria = int.Parse(dataReader["Categoria"].ToString());
                produto.Preco = double.Parse(dataReader["Preco"].ToString());
                produto.Peso = double.Parse(dataReader["Peso"].ToString());
                produto.Quantidade = int.Parse(dataReader["Quantidade"].ToString());
                produto.Imagem = dataReader["Imagem"].ToString();
                produto.Desconto = double.Parse(dataReader["Desconto"].ToString());
                produto.Cnpj = dataReader["Cnpj"].ToString();

                var restaurante = new RestauranteDTO();
                restaurante.ID = int.Parse(dataReader["IdRestaurante"].ToString());

                produto.Restaurante = restaurante;

                produtos.Add(produto);
            }
            conexao.Close();

            return produtos;
        }
    }
}