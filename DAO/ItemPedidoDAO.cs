using MySql.Data.MySqlClient;
using $safeprojectname$.Models;

namespace $safeprojectname$.DAO
{
    public class ItemPedidoDAO
    {
        public static void InserirItem(ItemPedido item, MySqlConnection conexao, MySqlTransaction transacao)
        {
            string sql = @"
                INSERT INTO item_pedido 
                (id_pedido, id_produto, quantidade, valor_unitario) 
                VALUES 
                (@pedido, @produto, @qtd, @unit)
            ";

            using (MySqlCommand cmd = new MySqlCommand(sql, conexao, transacao))
            {
                cmd.Parameters.AddWithValue("@pedido", item.IdPedido);
                cmd.Parameters.AddWithValue("@produto", item.IdProduto);
                cmd.Parameters.AddWithValue("@qtd", item.Quantidade);
                cmd.Parameters.AddWithValue("@unit", item.ValorUnitario);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
