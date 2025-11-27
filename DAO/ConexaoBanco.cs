using MySql.Data.MySqlClient;
using System;

namespace $safeprojectname$.DAO
{
    public class ConexaoBanco
    {
        private static string servidor = "localhost";
        private static string bancoDados = "supermercado";
        private static string usuario = "root";
        private static string senha = "2005";

        private static string conexaoString =
            $"Server={servidor};" +
            $"Database={bancoDados};" +
            $"Uid={usuario};" +
            $"Pwd={senha};" +
            $"Port=3306;" +
            $"SslMode=Disabled;" +
            $"AllowPublicKeyRetrieval=True;";

        public static MySqlConnection ObterConexao()
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection(conexaoString);
                conexao.Open();

                Console.WriteLine("✅ Conectado ao MySQL com sucesso!");
                return conexao;
            }
            catch (Exception erro)
            {
                Console.WriteLine("❌ ERRO AO CONECTAR NO BANCO:");
                Console.WriteLine(erro.Message);
                throw;
            }
        }

        public static void FecharConexao(MySqlConnection conexao)
        {
            if (conexao != null && conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
                Console.WriteLine("🔌 Conexão encerrada");
            }
        }
    }
}
