
using AgoraVai_PontoDigital.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgoraVai_PontoDigital.Controllers
{
    public class DashboardController : Controller
    {
        public const string SessionEmail = "Email";
        public const string SessionNomeCliente = "Cliente";
        private const string PATHAvaliacao = "Databases/Avaliacoes.csv";
        private const string PATHCliente = "Databases/Clientes.csv";

        private const string PATH_APROVADO = "Databases/AvaliacoesAprovadas.csv";
        private const string PATH_REPROVADO = "Databases/AvaliacoesReprovadas.csv";

        public AvaliacaoRepositorio avaliacaoRepositorio = new AvaliacaoRepositorio();
        public ClienteRepositorio clienteRepositorio = new ClienteRepositorio();
        public PlanoRepositorio planoRepositorio = new PlanoRepositorio();
        public IActionResult Index(){
            ViewData["Titulo"] = "Dasboard Administrador";
            ViewData["UserLogado"] = HttpContext.Session.GetString(SessionEmail);
    
            ViewData["listaGeral"] = avaliacaoRepositorio.ListarAvaliacoes(PATHAvaliacao);
            ViewData["listaAprovada"] = avaliacaoRepositorio.ListarAvaliacoes(PATH_APROVADO);
            ViewData["listaReprovada"] = avaliacaoRepositorio.ListarAvaliacoes(PATH_REPROVADO);
            ViewData["clienteGeral"] = clienteRepositorio.ListarClientes(PATHCliente);
            ViewData["listaPlanos"] = planoRepositorio.ListarPlanos();
            return View();
        }

     


    }
}