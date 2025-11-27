using MySql.Data.MySqlClient;
using $safeprojectname$.Models;
using System.Collections.Generic;

namespace $safeprojectname$.DAO
{
    public class ProdutoDAO
    {
        public static List<Produto> ListarTodos()
        {
            List<Produto> lista = new List<Produto>();

            using (var conexao = ConexaoBanco.ObterConexao())
            {
                string sql = "SELECT * FROM produto";

                using (var cmd = new MySqlCommand(sql, conexao))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Produto p = new Produto
                        {
                            Id = dr.GetInt32("id"),
                            Nome = dr.GetString("nome"),
                            Preco = dr.GetDecimal("preco"),
                            Quantidade = dr.GetInt32("quantidade")
                        };

                        lista.Add(p);
                    }
                }
            }

            return lista;
        }

        public static void Inserir(Produto produto)
        {
            using (var conexao = ConexaoBanco.ObterConexao())
            {
                string sql = @"
                    INSERT INTO produto (nome, preco, quantidade)
                    VALUES (@nome, @preco, @quantidade);
                ";

                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", produto.Nome);
                    cmd.Parameters.AddWithValue("@preco", produto.Preco);
                    cmd.Parameters.AddWithValue("@quantidade", produto.Quantidade);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
