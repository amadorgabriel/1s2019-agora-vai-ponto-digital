
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AgoraVai_PontoDigital.Models;
using System;
using AgoraVai_PontoDigital.Repositorios;

namespace AgoraVai_PontoDigital.Controllers
{
    public class AvaliacaoController : Controller
    {
        public AvaliacaoRepositorio avaliacaoRepositorio = new AvaliacaoRepositorio();
        public const string SessionEmail = "Email";
        public const string SessionNomeCliente = "Cliente";
        private const string PATH = "Databases/Avaliacoes.csv";
        private const string PATH_APROVADO = "Databases/AvaliacoesAprovadas.csv";
        private const string PATH_REPROVADO = "Databases/AvaliacoesReprovadas.csv";

        public IActionResult Index(){
            ViewData["UserLogado"] = HttpContext.Session.GetString(SessionEmail);
            ViewData["UserNomeLogado"] = HttpContext.Session.GetString(SessionNomeCliente);
            ViewData["Titulo"] = "Avaliações";
            return View();
        }

        public IActionResult CadastrarAvaliacao(IFormCollection form){
            AvaliacaoModel avaliacao = new AvaliacaoModel();
            avaliacao.Mensagem = form["avaliacao"];
            avaliacao.ClienteNome = form["nome"];
            avaliacao.ClienteEmail = form["email"];
            avaliacao.DataComentario = DateTime.Now;

            string path = "Databases/Avaliacoes.csv";
            avaliacaoRepositorio.AdicionarCSV(avaliacao, path);
            return RedirectToAction("Index", "Home");
        }

           public IActionResult AprovarComentario(string id){
            //Inserir num csv de aprovados
            //Listar comentários Aprovados 
            string path = "Databases/AvaliacoesAprovadas.csv";
            string PATH = "Databases/Avaliacoes.csv";      
            int intId = int.Parse(id);       

            avaliacaoRepositorio.AdicionarCSV(id, path);
            avaliacaoRepositorio.RemoverLinhaCSV(intId,PATH);
            
            ViewData["UserLogado"] = HttpContext.Session.GetString(SessionEmail);
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult ReprovarComentario(string id){
            
            string path = "Databases/AvaliacoesReprovadas.csv";              
            string PATH = "Databases/Avaliacoes.csv";     
            int intId = int.Parse(id);       
            
            avaliacaoRepositorio.AdicionarCSV(id, path);
            avaliacaoRepositorio.RemoverLinhaCSV(intId,PATH);
            ViewData["UserLogado"] = HttpContext.Session.GetString(SessionEmail);
            return RedirectToAction("Index","Dashboard");
        }

    }
}