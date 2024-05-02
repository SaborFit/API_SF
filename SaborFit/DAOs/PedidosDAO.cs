using MySql.Data.MySqlClient;
using SaborFit.DTOs;

namespace SaborFit.DAOs
{
    public class PedidosDAO
    {
        internal void Cadastrar(PedidoDTO pedido)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Pedidos (idUser, nomeUsuario, cpf, valorTotal,
                        idEndereco, idStatus, idRestaurante, cnpj)
                        VALUES (@idUser, @nomeUsuario, @cpf, @valorTotal, @idEndereco,
                        @idstatus, @idRestaurante, @cnpj);
                        SELECT LAST_INSERT_ID();";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@idUser", pedido.ID);
            comando.Parameters.AddWithValue("@nomeUsuario", pedido.NomeUsuario);
            comando.Parameters.AddWithValue("@cpf", pedido.CPF);
            comando.Parameters.AddWithValue("@valorTotal", pedido.ValorTotal);
            comando.Parameters.AddWithValue("@idEndereco", pedido.endereco.ID);
            comando.Parameters.AddWithValue("@idStatus", pedido.IdStatus);
            comando.Parameters.AddWithValue("@idRestaurante", pedido.restaurante.ID);
            comando.Parameters.AddWithValue("@cnpj", pedido.CNPJ);

            pedido.ID = Convert.ToInt32(comando.ExecuteScalar());

            conexao.Close();
            

            CadastrarProdutosPedido(pedido);

        }

        private void CadastrarProdutosPedido(PedidoDTO pedido)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO PedidoProduto (idPedido, idProduto, quantidade) 
                        VALUES (@idPedido, @idProduto, @quantidade)";

            foreach (var pedidoProduto in pedido.Produtos)
            {
                var comando = new MySqlCommand(query, conexao);
                comando.Parameters.AddWithValue("@idPedido", pedido.ID);
                comando.Parameters.AddWithValue("@idProduto", pedidoProduto.ID);
                comando.Parameters.AddWithValue("@quantidade", pedidoProduto.Quantidade);
                
                comando.ExecuteNonQuery();
            }

            conexao.Close();
        }

        public List<PedidoDTO> ListarPedidosEmPreparo(int idUsuario)
        {
            var pedidosEmPreparo = new List<PedidoDTO>();

            using (var conexao = ConnectionFactory.Build())
            {
                conexao.Open();

                var query = @"SELECT * FROM Pedidos WHERE idStatus = @idStatus AND idUser = @idUsuario;";
                var comando = new MySqlCommand(query, conexao);
                comando.Parameters.AddWithValue("@idStatus", 2);
                comando.Parameters.AddWithValue("@idUsuario", idUsuario);

                using (var reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pedido = new PedidoDTO
                        {
                            ID = reader.GetInt32("id"),
                            NomeUsuario = reader.GetString("nomeUsuario"),
                            CPF = reader.GetString("cpf"),
                            ValorTotal = reader.GetDouble("valorTotal"),
                            // Se necessário, preencha outros campos do pedido aqui
                        };

                        // Adicione o pedido à lista de pedidos em preparo
                        pedidosEmPreparo.Add(pedido);
                    }
                }
            }

            return pedidosEmPreparo;
        }

        internal object ListarPedidosEmPreparo()
        {
            throw new NotImplementedException();
        }

    }
}
