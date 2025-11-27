using Microsoft.AspNetCore.Mvc;
using $safeprojectname$.DAO;
using System;

namespace $safeprojectname$.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormaPagamentoController : ControllerBase
    {
        // GET: api/FormaPagamento
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var lista = FormaPagamentoDAO.Listar();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao listar formas de pagamento: " + ex.Message);
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        // POST: api/FormaPagamento
        [HttpPost]
        public IActionResult Create([FromBody] FormaRequest request)
        {
            if (string.IsNullOrEmpty(request.Nome))
                return BadRequest(new { erro = "Nome inválido" });

            try
            {
                FormaPagamentoDAO.Inserir(request.Nome);
                return Ok(new { mensagem = "Inserido com sucesso" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao inserir forma de pagamento: " + ex.Message);
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        // DELETE: api/FormaPagamento/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                FormaPagamentoDAO.Excluir(id);
                return Ok(new { mensagem = "Removido com sucesso" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao remover forma de pagamento: " + ex.Message);
                return StatusCode(500, new { erro = ex.Message });
            }
        }

        // GET: api/FormaPagamento/TesteConexao
        [HttpGet("TesteConexao")]
        public IActionResult TesteConexao()
        {
            try
            {
                using (var conexao = ConexaoBanco.ObterConexao())
                {
                    var cmd = new MySql.Data.MySqlClient.MySqlCommand(
                        "INSERT INTO forma_pagamento (nome) VALUES ('TesteCSharp')", conexao);
                    cmd.ExecuteNonQuery();
                }
                return Ok(new { mensagem = "Conexão e insert funcionando!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro no teste de conexão: " + ex.Message);
                return StatusCode(500, new { erro = ex.Message });
            }
        }
    }

    public class FormaRequest
    {
        public string Nome { get; set; }
    }
}
