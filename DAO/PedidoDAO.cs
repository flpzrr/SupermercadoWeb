using MySql.Data.MySqlClient;
using $safeprojectname$.Models;
using System;

namespace $safeprojectname$.DAO
{
    public class PedidoDAO
    {
        public static int InserirPedido(Pedido pedido, MySqlConnection conexao, MySqlTransaction transacao)
        {
            string sql = @"
            INSERT INTO pedido 
            (
                status, 
                data_criacao, 
                id_forma_pagamento, 
                valor_frete, 
                valor_desconto, 
                subtotal, 
                total, 
                observacao, 
                endereco_entrega, 
                data_entrega
            ) 
            VALUES 
            (
                @status, 
                @data, 
                @forma, 
                @frete, 
                @desconto, 
                @subtotal, 
                @total, 
                @obs, 
                @endereco, 
                @entrega
            );";

            using (MySqlCommand cmd = new MySqlCommand(sql, conexao, transacao))
            {
                cmd.Parameters.AddWithValue("@status", pedido.Status);
                cmd.Parameters.AddWithValue("@data", pedido.DataCriacao);
                cmd.Parameters.AddWithValue("@forma", pedido.IdFormaPagamento);
                cmd.Parameters.AddWithValue("@frete", pedido.ValorFrete);
                cmd.Parameters.AddWithValue("@desconto", pedido.ValorDesconto);
                cmd.Parameters.AddWithValue("@subtotal", pedido.Subtotal);
                cmd.Parameters.AddWithValue("@total", pedido.Total);
                cmd.Parameters.AddWithValue("@obs", pedido.Observacao ?? "Sem observações");
                cmd.Parameters.AddWithValue("@endereco", pedido.EnderecoEntrega ?? "Retirada no local");

                if (pedido.DataEntrega == DateTime.MinValue)
                    cmd.Parameters.AddWithValue("@entrega", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@entrega", pedido.DataEntrega);

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(cmd.LastInsertedId);
            }
        }
    }
}
