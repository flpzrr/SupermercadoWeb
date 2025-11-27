using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using $safeprojectname$.DAO;
using $safeprojectname$.Models;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class ProdutoController : ControllerBase
{
    // ===========================
    // LISTAR PRODUTOS
    // ===========================
    [HttpGet]
    public IActionResult GetProdutos()
    {
        try
        {
            List<object> produtos = new List<object>();

            using (var conexao = ConexaoBanco.ObterConexao())
            {
                string sql = "SELECT id, nome, quantidade, preco FROM produto";

                using (var cmd = new MySqlCommand(sql, conexao))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        produtos.Add(new
                        {
                            id = reader.GetInt32("id"),
                            nome = reader.GetString("nome"),
                            quantidade = reader.GetInt32("quantidade"),
                            preco = reader.GetDecimal("preco")
                        });
                    }
                }
            }

            return Ok(produtos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // ===========================
    // CADASTRAR PRODUTO (NOVO)
    // ===========================
    [HttpPost]
    public IActionResult CadastrarProduto([FromBody] Produto produto)
    {
        try
        {
            using (var conexao = ConexaoBanco.ObterConexao())
            {
                string sql = @"INSERT INTO produto (nome, quantidade, preco) 
                               VALUES (@nome, @quantidade, @preco)";

                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@nome", produto.Nome);
                    cmd.Parameters.AddWithValue("@quantidade", produto.Quantidade);
                    cmd.Parameters.AddWithValue("@preco", produto.Preco);

                    cmd.ExecuteNonQuery();
                }
            }

            return Ok(new { mensagem = "Produto cadastrado com sucesso!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // ===========================
    // REMOVER PRODUTO
    // ===========================
    [HttpDelete("{id}")]
    public IActionResult RemoverProduto(int id)
    {
        try
        {
            using (var conexao = ConexaoBanco.ObterConexao())
            {
                string sql = "DELETE FROM produto WHERE id = @id";
                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }

            return Ok(new { mensagem = "Produto removido com sucesso!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // ===========================
    // BAIXAR ESTOQUE (VENDA)
    // ===========================
    [HttpPut("baixar")]
    public IActionResult BaixarEstoque(int produtoId, int quantidade)
    {
        try
        {
            using (var conexao = ConexaoBanco.ObterConexao())
            {
                string sql = @"UPDATE produto 
                               SET quantidade = quantidade - @quantidade
                               WHERE id = @produtoId
                               AND quantidade >= @quantidade";

                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@produtoId", produtoId);
                    cmd.Parameters.AddWithValue("@quantidade", quantidade);

                    int linhas = cmd.ExecuteNonQuery();

                    if (linhas == 0)
                        return BadRequest("Estoque insuficiente ou produto inexistente.");
                }
            }

            return Ok(new { mensagem = "Estoque atualizado!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // ===========================
    // RESETAR ESTOQUE PARA 100
    // ===========================
    [HttpPut("resetar/{id}")]
    public IActionResult ResetarEstoque(int id)
    {
        try
        {
            using (var conexao = ConexaoBanco.ObterConexao())
            {
                string sql = "UPDATE produto SET quantidade = 100 WHERE id = @id";

                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }

            return Ok(new { mensagem = "Estoque redefinido para 100" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
