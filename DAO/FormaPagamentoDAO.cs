using MySql.Data.MySqlClient;
using $safeprojectname$.Models;
using System.Collections.Generic;

namespace $safeprojectname$.DAO
{
    public class FormaPagamentoDAO
    {
        // Retorna o nome da forma de pagamento pelo ID
        public static string ObterNomePorId(int id)
        {
            string nome = "Desconhecido";

            using (var conexao = ConexaoBanco.ObterConexao())
            {
                string sql = "SELECT nome FROM forma_pagamento WHERE id = @id";

                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                            nome = dr.GetString("nome");
                    }
                }
            }
            return nome;
        }

        // Lista todas as formas de pagamento (retorna LISTA de objetos)
        public static List<FormaPagamento> Listar()
        {
            List<FormaPagamento> lista = new List<FormaPagamento>();

            using (var conexao = ConexaoBanco.ObterConexao())
            {
                string sql = "SELECT id, nome FROM forma_pagamento";

                using (var cmd = new MySqlCommand(sql, conexao))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new FormaPagamento
                        {
                            Id = dr.GetInt32("id"),
                            Nome = dr.GetString("nome")
                        });
                    }
                }
            }

            return lista;
        }

        // Insere nova forma de pagamento
        public static void Inserir(string nome)
        {
            using (var conexao = ConexaoBanco.ObterConexao())
            {
                string sql = "INSERT INTO forma_pagamento (nome) VALUES (@nome)";

                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Exclui por ID
        public static void Excluir(int id)
        {
            using (var conexao = ConexaoBanco.ObterConexao())
            {
                string sql = "DELETE FROM forma_pagamento WHERE id = @id";

                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
