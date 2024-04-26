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

                produtos.Add(produto);
            }
            conexao.Close();

            return produtos;
        }

        public List<ProdutoDTO> ListarProdutosPorCategoria( int idcategoria)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = "SELECT * FROM Produtos WHERE categoria = @idCategoria"; 

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

                var restaurante = new RestauranteDTO();
                restaurante.ID = int.Parse(dataReader["IdRestaurante"].ToString());

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

    }
}
