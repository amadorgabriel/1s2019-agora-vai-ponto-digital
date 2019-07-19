using AgoraVai_PontoDigital.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgoraVai_PontoDigital.Controllers
{
    public class PesquisaController : Controller
    {

        public const string SessionEmail = "Email";
        public PesquisaRepositorio pesquisaRepositorio = new PesquisaRepositorio();
        
        public IActionResult Index(IFormCollection form){
            ViewData["UserLogado"] = HttpContext.Session.GetString(SessionEmail);
            string dado = form["pesquisa"];

            var dadoR =  pesquisaRepositorio.Pesquisar(dado);
            if (!string.IsNullOrEmpty(dadoR) )
            {
                return RedirectToAction("Index", dadoR);
            }else{
            string dadoRExe = pesquisaRepositorio.PesquisarExceção(dado);
                if (dadoRExe != null )
                {
                    return RedirectToAction("Index", dadoRExe);
                }else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        } 
    }
}