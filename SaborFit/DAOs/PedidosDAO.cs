﻿using MySql.Data.MySqlClient;
using SaborFit.DTOs;

namespace SaborFit.DAOs
{
    public class PedidosDAO
    {
        internal void Cadastrar(PedidoDTO pedido)
        {
            var conexao = ConnectionFactory.Build();
            conexao.Open();

            var query = @"INSERT INTO Pedidos (idUser, nomeUsuario, cpf, valorTotal, idEndereco, status, idRestaurante, cnpj) 
              VALUES (@idUser, @nomeUsuario, @cpf, @valorTotal, @idEndereco, @status, @idRestaurante, @cnpj);
              SELECT LAST_INSERT_ID();";

            var comando = new MySqlCommand(query, conexao);
            comando.Parameters.AddWithValue("@idUser", pedido.ID);
            comando.Parameters.AddWithValue("@nomeUsuario", pedido.NomeUsuario);
            comando.Parameters.AddWithValue("@nomecpf", pedido.CPF);
            comando.Parameters.AddWithValue("@valorTotal", pedido.ValorTotal);
            comando.Parameters.AddWithValue("@idRestaurante", pedido.restaurante.ID);
            comando.Parameters.AddWithValue("@cnpj", pedido.CNPJ);

            pedido.Id = Convert.ToInt32(comando.ExecuteScalar());

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
                comando.Parameters.AddWithValue("@idPedido", pedido.Id);
                comando.Parameters.AddWithValue("@idProduto", pedidoProduto.Id);
                comando.Parameters.AddWithValue("@quantidade", pedidoProduto.Quantidade);

                comando.ExecuteNonQuery();
            }

            conexao.Close();
        }


    }
}
