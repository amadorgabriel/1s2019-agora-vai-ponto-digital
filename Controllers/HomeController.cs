using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AgoraVai_PontoDigital.Repositorios;

namespace AgoraVai_PontoDigital.Controllers
{
    public class HomeController : Controller
    {
        public const string SessionEmail = "Email";
        public const string SessionNomeCliente = "Cliente";
        private const string PATH_APROVADO = "Databases/AvaliacoesAprovadas.csv";
        public AvaliacaoRepositorio avaliacaoRepositorio = new AvaliacaoRepositorio();
        public IActionResult Index(){
            ViewData["UserLogado"] = HttpContext.Session.GetString(SessionEmail);
            ViewData["listaAprovada"] = avaliacaoRepositorio.ListarAvaliacoes(PATH_APROVADO);
            ViewData["Titulo"] = "Home";
            return View();
        }
      
    }
}
