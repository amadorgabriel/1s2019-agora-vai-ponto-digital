using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgoraVai_PontoDigital.Controllers
{
    public class SacController : Controller
    {
        public const string SessionEmail = "Email";
        public const string SessionNomeCliente = "Cliente";
        public IActionResult Index(){
            ViewData["UserLogado"] = HttpContext.Session.GetString(SessionEmail);
            ViewData["Titulo"] = "Sac";
            return View();
        }

        public IActionResult Contatar(IFormCollection form){
            var msg = form["problem"];
            ViewData["problem"] = msg;

            return RedirectToAction("Index","Sac");

        }
    }
}