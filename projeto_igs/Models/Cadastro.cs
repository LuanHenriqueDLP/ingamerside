using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projeto_etapa4.Models
{
    public class Cadastro
    {
        public int idCadastro {get; set;}
        public string Nome {get; set;}
        public string Senha {get; set;}
        public string Email {get; set;}
        public string Endereco {get; set;}
        public int Numero {get; set;}
        public string Complemento {get; set;}
        public string Bairro {get; set;}
        public string CEP {get; set;}
        public string Cidade {get; set;}
        public string Estado {get; set;}
        public string Tipo {get; set;}
    }
}