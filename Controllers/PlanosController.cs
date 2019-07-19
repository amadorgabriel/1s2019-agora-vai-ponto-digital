using AgoraVai_PontoDigital.Models;
using AgoraVai_PontoDigital.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgoraVai_PontoDigital.Controllers
{
    public class PlanosController : Controller
    {
        public const string SessionEmail = "Email";
        public const string SessionNomeCliente = "Cliente";
        private const string PATH = "Databases/FormasPagamento.csv";
        private const string Path = "Databases/Planos.csv";


        PlanoRepositorio planoRepositorio = new PlanoRepositorio();
        public IActionResult Index(){
            ViewData["listaPlanos"] = planoRepositorio.ListarFormasPagamento(PATH);
            ViewData["UserLogado"] = HttpContext.Session.GetString(SessionEmail);
            ViewData["Titulo"] = "Planos e Produtos";
            
            return View();
        }

        public IActionResult Comprar(IFormCollection form){
            string preco = form["preco"];
            string formaP = form["formaP"];
            float precoFloat = 0;
            var userLogado = HttpContext.Session.GetString(SessionEmail);

            if (string.IsNullOrEmpty(userLogado))
            {
                //soltar um script
                return RedirectToAction("Index", "Login");
            }

            if (preco.Length == 9)
            {
                preco = "29,90";
            }else if(preco.Length == 10){
                preco = "149,90";
            } else if(preco.Length == 11){
                preco = "299,90";
            }else{
                preco = "0";
            }

            if (preco != null){
                precoFloat = float.Parse(preco);
            }

            PlanoModel plano = new PlanoModel();
            plano.Preco = precoFloat;
            plano.FormaDePagamento = formaP;
            plano.EmailCliente = userLogado;

            planoRepositorio.AdicionarnoCSV(plano, Path);
            //soltar javascript de compra efetuada
            return RedirectToAction("Index","Home");
        }



        
    }
}