using System.Collections.Generic;
using System.IO;
using AgoraVai_PontoDigital.Models;

namespace AgoraVai_PontoDigital.Repositorios
{
    public class PlanoRepositorio
    {
        private const string Path = "Databases/Planos.csv";

        public List<FormaPagamento> ListarFormasPagamento ( string Path){
            List<FormaPagamento> listaformas = new List<FormaPagamento>();
            string[] Avaliacoes = File.ReadAllLines(Path);
            foreach (var item in Avaliacoes)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                    string[] dadoAva = item.Split(";");
                    FormaPagamento forma = new FormaPagamento();
                    forma.Id = int.Parse(dadoAva[0]);
                    forma.FormaDePagamento = dadoAva[1];
                    listaformas.Add(forma);
            }
            return listaformas;
        }

        public List<PlanoModel> ListarPlanos (){
            List<PlanoModel> listaPlanos = new List<PlanoModel>();
            string[] Avaliacoes = File.ReadAllLines(Path);
            foreach (var item in Avaliacoes)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                    string[] dadoLs = item.Split(";");
                    PlanoModel plano = new PlanoModel();
                    plano.Id = int.Parse(dadoLs[0]);
                    plano.EmailCliente = dadoLs[1];
                    plano.Preco = float.Parse(dadoLs[2]);
                    plano.FormaDePagamento = dadoLs[3];
                    if (dadoLs[3].Equals("0"))
                    {
                        plano.FormaDePagamento = "A definir..";
                    }
                    listaPlanos.Add(plano);
            }
            return listaPlanos;
        }
        public void AdicionarnoCSV(PlanoModel plano, string path){
             if (!File.Exists(path))
            {   
                File.Create(path).Close();
                plano.Id = 1;
            }else
            {
                plano.Id = File.ReadAllLines(path).Length + 1;
            }

            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine($"{plano.Id};{plano.EmailCliente};{plano.Preco};{plano.FormaDePagamento}");
            sw.Close();
        }


    }
}