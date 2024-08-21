using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projeto_etapa4.Models
{
    public class Produto
    {
        public int idProduto {get; set;}
        public int Codigo {get;set;}
        public string NomePrd {get;set;}
        public string DescPrd {get;set;}
        public double Preco {get;set;}
        public int idCadastro {get; set;}
    }
}