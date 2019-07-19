using System.Collections.Generic;
using System.IO;
using AgoraVai_PontoDigital.Models;

namespace AgoraVai_PontoDigital.Repositorios
{
    public class ClienteRepositorio
    {

        private const string PATH = "Databases/Clientes.csv";
        public void AdicionarnoCSV(ClienteModel cliente){
            //Criar Id, 
            if (!File.Exists(PATH))
            {   
                File.Create(PATH).Close();
                cliente.Id = 1;
            }else
            {
                cliente.Id = File.ReadAllLines(PATH).Length + 1;
            }
            
            StreamWriter sw = new StreamWriter(PATH, true);
            sw.WriteLine($"{cliente.Id};{cliente.Nome};{cliente.Cpf};{cliente.Email};{cliente.Senha};{cliente.Telefone}");
            sw.Close();
        }

        public List<ClienteModel> ListarClientes (string path){
            List<ClienteModel> listaClientes = new List<ClienteModel>();
            string[] clientes = File.ReadAllLines(path);
            foreach (var item in clientes)
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                    string[] dadosCliente = item.Split(";");
                    ClienteModel cliente = new ClienteModel();
                    cliente.Id = int.Parse(dadosCliente[0]);
                    cliente.Nome = dadosCliente[1];
                    cliente.Cpf = dadosCliente[2];
                    cliente.Email = dadosCliente[3];
                    cliente.Senha = dadosCliente[4];
                    cliente.Telefone = dadosCliente[5];
                    listaClientes.Add(cliente);
            }
            return listaClientes;
        }
  
        public bool VerificarExistencia(ClienteModel cliente){
            var listaClientes = ListarClientes(PATH);

                foreach (var item in listaClientes )
                {
                    if(item == null){
                        continue;
                    }

                    if (item.Email.Equals(cliente.Email) || item.Senha.Equals(cliente.Senha))
                    {
                        return false;
                    }
                }
                return true;
        }
    
    }
}