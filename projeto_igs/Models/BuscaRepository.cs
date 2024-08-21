using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace projeto_etapa4.Models
{
    public class BuscaRepository
    {
        private const string DadosConexao = "Database = db_aa0d09_hotsite; Datasource = MYSQL8002.site4now.net; User Id = aa0d09_hotsite; Password = luanhots123;";
        public List<Produto> Pesquisar (string Busca) // MÉTODO QUE BUSCA NO BANCO DE DADOS O EMAIL E SENHA RECEBIDOS PARA CRIAR UMA SESSAO DE LOGIN //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);    
            conexao.Open();

            string query = "SELECT * FROM Produto WHERE Codigo LIKE @Busca OR NomePrd LIKE @Busca OR DescPrd LIKE @Busca";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@Busca", "%"+Busca+"%");

            MySqlDataReader reader = comando.ExecuteReader();

            List<Produto> produtosEncontrados = new List<Produto>();

            if (!reader.HasRows)
            {
                produtosEncontrados = null;
            }
            else
            {
                while (reader.Read())
                {
                    Produto p = new Produto();
                    p.idProduto = reader.GetInt32("idProduto");

                    if(!reader.IsDBNull(reader.GetOrdinal("Codigo")))
                    {
                        p.Codigo = reader.GetInt32("Codigo");
                    }

                    if(!reader.IsDBNull(reader.GetOrdinal("NomePrd")))
                    {
                        p.NomePrd = reader.GetString("NomePrd");
                    }

                    if(!reader.IsDBNull(reader.GetOrdinal("DescPrd")))
                    {
                        p.DescPrd = reader.GetString("DescPrd");
                    }

                    if(!reader.IsDBNull(reader.GetOrdinal("Preco")))
                    {
                        p.Preco = reader.GetDouble("Preco");
                    }

                    if(!reader.IsDBNull(reader.GetOrdinal("idCadastro")))
                    {
                        p.idCadastro = reader.GetInt32("idCadastro");
                    }

                    produtosEncontrados.Add(p);
                }            
            }
            
            conexao.Close();

            return produtosEncontrados;
        }
    }
}