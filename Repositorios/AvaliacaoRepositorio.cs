using System;
using System.Collections.Generic;
using System.IO;
using AgoraVai_PontoDigital.Models;

namespace AgoraVai_PontoDigital.Repositorios
{
    public class AvaliacaoRepositorio
    {
        public const string SessionEmail = "Email";
        public const string SessionNomeCliente = "Cliente";
        private const string PATH = "Databases/Avaliacoes.csv";
        private const string PATH_APROVADO = "Databases/AvaliacoesAprovadas.csv";
        private const string PATH_REPROVADO = "Databases/AvaliacoesReprovadas.csv";


        public void AdicionarCSV(AvaliacaoModel ava, string path){

            if (!File.Exists(path))
            {   
                File.Create(path).Close();
                ava.Id = 1;
            }else
            {
                ava.Id = File.ReadAllLines(path).Length + 1;
            }

            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine($"{ava.Id};{ava.ClienteNome};{ava.ClienteEmail};{ava.Mensagem};{ava.DataComentario}");
            sw.Close();
        }

        public void AdicionarCSV(string avaId, string path){

            AvaliacaoModel foundAva = BuscarPorId(avaId);
            //n ta nulo, aceita

    
                if (!File.Exists(path))
                {   
                    File.Create(path).Close();
                    foundAva.Id = 1;
                }else
                {
                    foundAva.Id = File.ReadAllLines(path).Length + 1;
                }

                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine($"{foundAva.Id};{foundAva.ClienteNome};{foundAva.ClienteEmail};{foundAva.Mensagem};{foundAva.DataComentario}");
                sw.Close();
        
        }

        public void RemoverLinhaCSV(int avaId, string path){
            var linhas = File.ReadAllLines(path);
            for (int i = 0; i < linhas.Length; i++) {
                string[] linha = linhas[i].Split (';');
                if (avaId.ToString () == linha[0]) {
                    linhas[i] = "";
                    break;
                }
            }
            System.IO.File.WriteAllLines (path, linhas);
        }

        public AvaliacaoModel BuscarPorId(string Id)
        {
            int IdNumber = int.Parse(Id); 

            List<AvaliacaoModel> listaAvaliacao = ListarAvaliacoes(PATH);
            foreach (AvaliacaoModel item in listaAvaliacao)
            {
                if (item.Id.Equals(IdNumber))
                {
                    return item;   
                    //ctz q não ta nulo, pode confiar
                }
            }
            return null;
        }

        public List<AvaliacaoModel> ListarAvaliacoes ( string Path){
            List<AvaliacaoModel> listaAvaliacao = new List<AvaliacaoModel>();
            string[] Avaliacoes = File.ReadAllLines(Path);
            foreach (var item in Avaliacoes)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                    string[] dadoAva = item.Split(";");
                    AvaliacaoModel avaliacao = new AvaliacaoModel();
                    avaliacao.Id = int.Parse(dadoAva[0]);
                    avaliacao.ClienteNome = dadoAva[1];
                    avaliacao.ClienteEmail = dadoAva[2];
                    avaliacao.Mensagem = dadoAva[3];
                    avaliacao.DataComentario = DateTime.Parse(dadoAva[4]);

                    listaAvaliacao.Add(avaliacao);
            }
            return listaAvaliacao;
        }
    }
}