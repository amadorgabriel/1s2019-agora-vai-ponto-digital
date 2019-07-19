using AgoraVai_PontoDigital.Models;
using System;


namespace AgoraVai_PontoDigital.Models
{
    public class AvaliacaoModel
    {
        public int Id {get;set;}
        public string Mensagem {get;set;}
        public string ClienteNome {get;set;}
        public string ClienteEmail {get;set;}
        public DateTime DataComentario {get;set;}
    }
}