using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgoraVai_PontoDigital.Controllers
{
    public class HistoriaController : Controller
    {
        public const string SessionEmail = "Email";
        public const string SessionNomeCliente = "Cliente";
        public IActionResult Index(){
            ViewData["UserLogado"] = HttpContext.Session.GetString(SessionEmail);
            ViewData["Titulo"] = "Historia e Sede";
            return View();
        }
    }
}