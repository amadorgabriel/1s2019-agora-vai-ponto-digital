using System.Collections.Generic;
using System.Threading;
using AgoraVai_PontoDigital.Models;
using AgoraVai_PontoDigital.Repositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgoraVai_PontoDigital.Controllers
{
    public class LoginController : Controller
    {
        private const string PATH = "Databases/Clientes.csv";

        public static ClienteRepositorio clienteRepositorio = new ClienteRepositorio();
        public const string SessionEmail = "Email";
        public const string SessionNomeCliente = "Cliente";

        public IActionResult Index(){
            ViewData["UserLogado"] = HttpContext.Session.GetString(SessionEmail);
            ViewData["Titulo"] = "Login ou Cadastro";
            return View();
        }

        public IActionResult Cadastrar(IFormCollection form){
            

            ClienteModel cliente = new ClienteModel();
            cliente.Nome = form["nome"];
            cliente.Cpf = form["cpf"];
            cliente.Email = form["email"];
            cliente.Senha = form["senha"];
            cliente.Telefone = form["telefone"];
           
            var clienteExists = clienteRepositorio.VerificarExistencia(cliente);
            if (clienteExists.Equals(true))
            {
                clienteRepositorio.AdicionarnoCSV(cliente);

                ViewData["Titulo"] = "Cadastro Bem Sucedido";
                ViewData["nomeCadastrado"] = cliente.Nome;
                return View("Cadastrado");
            }else{
                ViewData["Titulo"] = "Erro";
                return View("Index", "Login");
            }


        }

        public IActionResult Logar(IFormCollection form){
            var email = form["email"];
            var senha = form["senha"];

            foreach (var item in clienteRepositorio.ListarClientes(PATH))
            {

                // if(item == null){
                //    System.Console.WriteLine("ta nulo CARAMBA");
                // }

                if ( item != null && item.Email.Equals(email) && item.Senha.Equals(senha))
                {
                    //Criar Sessions                    
                    HttpContext.Session.SetString(SessionEmail, item.Email);
                    HttpContext.Session.SetString(SessionNomeCliente, item.Nome);

                    if (email.Equals("admin@agoravai.com") && senha.Equals("admin"))
                    {
                        return RedirectToAction("Index","Dashboard");
                    }
                    
                    return RedirectToAction("Index","Home");
                }
                
            }
            ViewData["ErroData"] = "ErroLogin";
            return View("Index");
        }

        public IActionResult Deslogar(){
        
            HttpContext.Session.Remove(SessionEmail);
            HttpContext.Session.Remove(SessionNomeCliente);
           
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}