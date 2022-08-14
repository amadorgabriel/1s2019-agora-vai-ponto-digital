using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace AgoraVai_PontoDigital.Models
{
    public class PesquisaRepositorio
    {
        const string PATH = "Databases/Pesquisa.csv";
        const string PATH_EXEPTION = "Databases/PesquisaExcecao.csv";
        public TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;


        public List<string> Listar(){
            var listaDados = new List<string>();
            var linhas = File.ReadAllLines(PATH);
            foreach (var item in linhas)
            {
                if (item != null)
                {
                    listaDados.Add(item);
                }
            }
            return listaDados;
        }

        public List<string> ListarExecção(){
            var listaDados = new List<string>();
            var linhas = File.ReadAllLines(PATH_EXEPTION);
            foreach (var item in linhas)
            {
                if (item != null)
                {
                    listaDados.Add(item);
                }
            }
            return listaDados;
        }

        
        public string Pesquisar(string dado){
           var listaPesq = Listar();
           var dadoReturn = "";

            foreach (var item in listaPesq)
            {
                if (dado.ToLower().Equals(item)) //se o dado == database
                {
                    return dadoReturn = textInfo.ToTitleCase(dado); 
                }else if( dado.ToLower().Contains(item)){ // se o dado em ToLower == database
                    return dadoReturn = textInfo.ToTitleCase(item); 
                } else if(item.Contains(dado.ToLower())){ // se for escrito no plural
                    return dadoReturn = item;                   
                }else if(dado.ToLower().Contains(item)  ){ // se o dado for singular e houver mais coisas escritas
                    return dadoReturn = item;                   
                }else{
                    continue;
                }
            }
            return null;
        }

        public string PesquisarExceção(string exceçao){

            var dadoReturn = "";
            foreach (var item in ListarExecção())
            {
                if (item.Equals(exceçao.ToLower()) || item.Contains(exceçao.ToLower()) || exceçao.ToLower().Contains(item) )
                {
                    dadoReturn = item;

                    // CADEIA DE ITENS
                    //OBS: PARA UMA BUSCA MAIS EFETIVA O IDEAL SERIA QUE, PARA CADA PAGINA EXISTIR UM CSV DE PALAVRAS RELACIONADAS
                    //     ASSIM SE O "dadoReturn" FOR IGUAL A UM DESSES DADOS RETORNASSE A PÁGINA, E QUANTO MAIOR OS CSVs MAIS
                    //     PRECISAS AS BUSCAS!

                    if (dadoReturn == "cadastro")
                    {
                        return "Login";
                    }else if( dadoReturn == "suporte" || dadoReturn == "reclamar" || dadoReturn == "perguntas frequêntes" || dadoReturn == "contato"){
                        return "Sac";
                    }else if( dadoReturn == "plano" || dadoReturn == "teste gratuito" || dadoReturn == "qualidade" || dadoReturn == "produtos" ){
                        return "Planos";
                    }else if( dadoReturn == "propositos" || dadoReturn == "quem somos" || dadoReturn == "como surgiu" || dadoReturn == "equipe" || dadoReturn == "sede" || dadoReturn == "local" ){
                        return "Historia";
                    } else if( dadoReturn == "ponto digital" || dadoReturn == "agora vai" || dadoReturn == null){
                        return "Home";
                    }

                    //---------------
                }
            }
            return null;
        }



    }
}