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
            var produtos = new List<ProdutoDTO>();

            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();

                var query = @"
                SELECT 
                p.id AS ProdutoID, 
                p.nome AS NomeProduto, 
                p.tipo AS TipoProduto, 
                p.descricao AS DescricaoProduto, 
                p.ingredientes AS IngredientesProduto, 
                p.categoria AS CategoriaID, 
                p.preco AS PrecoProduto, 
                p.peso AS PesoProduto, 
                p.quantidade AS QuantidadeProduto, 
                p.imagem AS ImagemProduto, 
                p.desconto AS DescontoProduto, 
                p.cnpj AS CnpjProduto, 
                r.id AS RestauranteID, 
                r.nome AS NomeRestaurante, 
                r.telefone AS TelefoneRestaurante
                FROM Produtos p
                INNER JOIN Restaurantes r ON p.idRestaurante = r.id
                WHERE p.id = @ProdutoID";

                using (var comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@ProdutoID", ID);

                    using (var dataReader = comando.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var produto = new ProdutoDTO
                            {
                                ID = Convert.ToInt32(dataReader["ProdutoID"]),
                                Nome = dataReader["NomeProduto"].ToString(),
                                Tipo = dataReader["TipoProduto"].ToString(),
                                Descricao = dataReader["DescricaoProduto"].ToString(),
                                Ingredientes = dataReader["IngredientesProduto"].ToString(),
                                Categoria = Convert.ToInt32(dataReader["CategoriaID"]),
                                Preco = Convert.ToDouble(dataReader["PrecoProduto"]),
                                Peso = Convert.ToDouble(dataReader["PesoProduto"]),
                                Quantidade = Convert.ToInt32(dataReader["QuantidadeProduto"]),
                                Imagem = dataReader["ImagemProduto"].ToString(),
                                Desconto = Convert.ToDouble(dataReader["DescontoProduto"]),
                                Cnpj = dataReader["CnpjProduto"].ToString(),
                                Restaurante = new RestauranteDTO
                                {
                                    ID = Convert.ToInt32(dataReader["RestauranteID"]),
                                    Nome = dataReader["NomeRestaurante"].ToString(),
                                    Telefone = dataReader["TelefoneRestaurante"].ToString()
                                }
                            };

                            produtos.Add(produto);
                        }
                    }
                }
            }

            return produtos;
        }


        public List<ProdutoDTO> ListarProdutosPorCategoria(int idcategoria)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"
                SELECT p.*, r.nome AS NomeRestaurante,
                GROUP_CONCAT(m.id) AS MarcadoresIDs,
                GROUP_CONCAT(m.nome) AS MarcadoresNomes
                FROM Produtos p
                LEFT JOIN MarcadorProduto mp ON p.id = mp.idProduto
                LEFT JOIN Marcadores m ON mp.idMarcador = m.id
                LEFT JOIN Restaurantes r ON p.idRestaurante = r.id
                WHERE p.categoria = @idCategoria
                GROUP BY p.id";

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

                // Adiciona o nome do restaurante
                produto.Restaurante = new RestauranteDTO
                {
                    Nome = dataReader["NomeRestaurante"].ToString()
                };

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

            var query = @"
                SELECT 
                p.*, 
                r.id AS RestauranteID,
                r.Nome AS NomeRestaurante,
                GROUP_CONCAT(m.id) AS MarcadoresIDs,
                GROUP_CONCAT(m.nome) AS NomesMarcadores
                FROM Produtos p 
                LEFT JOIN MarcadorProduto mp ON p.id = mp.idProduto
                LEFT JOIN Marcadores m ON mp.idMarcador = m.id
                LEFT JOIN Restaurantes r ON p.idRestaurante = r.id
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
                        Restaurante = new RestauranteDTO
                        {
                            ID = int.Parse(dataReader["RestauranteID"].ToString()),
                            Nome = dataReader["NomeRestaurante"].ToString()
                        },
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