using Microsoft.AspNetCore.Mvc;
using $safeprojectname$.DAO;
using $safeprojectname$.Models;

namespace $safeprojectname$.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

        // ✅ Exibir todas as formas de pagamento
        public IActionResult Pagamentos()
        {
            var lista = FormaPagamentoDAO.Listar();
            return View(lista);
        }

        // ✅ Cadastrar nova forma de pagamento
        [HttpPost]
        public IActionResult CadastrarFormaPagamento(string nome)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                FormaPagamentoDAO.Inserir(nome);
            }

            return RedirectToAction("Pagamentos");
        }

        // (Opcional) Excluir
        public IActionResult ExcluirFormaPagamento(int id)
        {
            FormaPagamentoDAO.Excluir(id);
            return RedirectToAction("Pagamentos");
        }
    }
}
