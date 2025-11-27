using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using $safeprojectname$.DAO;
using $safeprojectname$.Models;
using System;
using System.Collections.Generic;

namespace $safeprojectname$.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] PedidoRequest request)
        {
            if (request == null || request.Pedido == null)
                return BadRequest("Pedido não foi enviado corretamente.");

            if (request.Itens == null || request.Itens.Count == 0)
                return BadRequest("O pedido precisa ter pelo menos um item.");

            using (var conexao = ConexaoBanco.ObterConexao())
            {
                if (conexao == null)
                    return StatusCode(500, "Erro de conexão com o banco!");

                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        request.Pedido.Status = "Finalizado";
                        request.Pedido.DataCriacao = DateTime.Now;
                        request.Pedido.Observacao ??= "Sem observações";
                        request.Pedido.EnderecoEntrega ??= "Retirada no local";

                        if (request.Pedido.IdFormaPagamento <= 0)
                            request.Pedido.IdFormaPagamento = 1;

                        // ✅ Inserir pedido
                        int idPedido = PedidoDAO.InserirPedido(request.Pedido, conexao, transacao);

                        if (idPedido <= 0)
                            throw new Exception("Erro ao gerar ID do pedido.");

                        Console.WriteLine($"✅ Pedido criado: {idPedido}");

                        // ✅ Processar itens
                        foreach (var item in request.Itens)
                        {
                            if (item.IdProduto <= 0 || item.Quantidade <= 0)
                                throw new Exception("Item inválido detectado.");

                            item.IdPedido = idPedido;

                            // ✅ Busca estoque atual
                            int estoqueAtual = ObterEstoque(item.IdProduto, conexao, transacao);

                            Console.WriteLine($"📦 Estoque atual produto {item.IdProduto}: {estoqueAtual}");

                            if (estoqueAtual < item.Quantidade)
                                throw new Exception($"Estoque insuficiente para o produto ID {item.IdProduto}");

                            // ✅ Insere item
                            ItemPedidoDAO.InserirItem(item, conexao, transacao);

                            // ✅ Atualiza estoque
                            AtualizarEstoque(item.IdProduto, item.Quantidade, conexao, transacao);
                        }

                        transacao.Commit();

                        return Ok(new
                        {
                            mensagem = "✅ Pedido concluído e estoque atualizado com sucesso!",
                            pedido_id = idPedido
                        });
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();

                        Console.WriteLine("❌ ERRO NO PEDIDO:");
                        Console.WriteLine(ex.Message);

                        return StatusCode(500, ex.Message);
                    }
                }
            }
        }

        // ✅ OBTÉM ESTOQUE ATUAL
        private int ObterEstoque(int idProduto, MySqlConnection conexao, MySqlTransaction transacao)
        {
            string sql = "SELECT quantidade FROM produto WHERE id = @id";

            using (var cmd = new MySqlCommand(sql, conexao, transacao))
            {
                cmd.Parameters.AddWithValue("@id", idProduto);

                object result = cmd.ExecuteScalar();

                if (result == null)
                    throw new Exception($"Produto {idProduto} não encontrado no banco.");

                return Convert.ToInt32(result);
            }
        }

        // ✅ ATUALIZA ESTOQUE
        private void AtualizarEstoque(int idProduto, int quantidadeVendida,
    MySqlConnection conexao, MySqlTransaction transacao)
        {
            string sql = @"
                UPDATE produto
                SET quantidade = quantidade - @quantidade
                WHERE id = @id
            ";

            using (var cmd = new MySqlCommand(sql, conexao, transacao))
            {
                cmd.Parameters.AddWithValue("@quantidade", quantidadeVendida);
                cmd.Parameters.AddWithValue("@id", idProduto);

                int linhas = cmd.ExecuteNonQuery();

                if (linhas == 0)
                    throw new Exception("Nenhuma linha foi atualizada no estoque!");

                Console.WriteLine($"✅ Estoque do produto {idProduto} atualizado (-{quantidadeVendida}).");
            }
        }
    }

    public class PedidoRequest
    {
        public Pedido Pedido { get; set; }
        public List<ItemPedido> Itens { get; set; }
    }
}
