using Microsoft.AspNetCore.Mvc;
using $safeprojectname$.DAO;

namespace $safeprojectname$.Controllers
{
    [ApiController]
    [Route("api/teste-banco")]
    public class TestarConexaoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Testar()
        {
            try
            {
                var conexao = ConexaoBanco.ObterConexao();
                ConexaoBanco.FecharConexao(conexao);

                return Ok("✅ Conexão com MySQL funcionou perfeitamente!");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "❌ ERRO: " + ex.Message);
            }
        }
    }
}
