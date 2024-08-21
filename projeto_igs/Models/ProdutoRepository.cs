using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

/*
    SERVIDOR: localhost;
    USER: root;
    BANCO DE DADOS: ingamerside;
*/

/*
    private const string DadosConexao = "Database = ingamerside; Datasource = localhost; User Id = root;";

    MySqlConnection conexao = new MySqlConnection(DadosConexao);    
    conexao.Open();

    string query = "";
    MySqlCommand comando = new MySqlCommand(query, conexao); 

    conexao.Close();
*/

/*
    <<< PARA MÉTODOS DE INSERÇÃO/EDIÇÃO/EXCLUSÃO >>>
    
    > "variávelCommand".ExecuteNonQuery();

    <<< PARA MÉTODOS DE LEITURA/SELECT >>>

    > MySqlDataReader "variávelReader" = "variávelCommand".ExecuteReader();

    <<< PARA MÉTODOS DE LISTAGEM DE REGISTROS >>>
    
    > while ("variávelReader".Read()) // LAÇO DE REPETIÇÃO PARA CRIAR UM NOVO OBJETO SEMPRE QUE "variávelReader" LER ALGUMA INFORMAÇÃO //
    > variávelReader.Get"dataType"("campo"); // conversão das informações recebidas do banco de dados //
    > if(!"variávelReader".IsDBNull("variávelReader".GetOrdinal("campo"))) // VALIDAÇÃO DE CAMPO NULO //
*/

namespace projeto_etapa4.Models
{
    public class ProdutoRepository
    {
        private const string DadosConexao = "Database = db_aa0d09_hotsite; Datasource = MYSQL8002.site4now.net; User Id = aa0d09_hotsite; Password = luanhots123;";
        public void Cadastrar(Produto p) // MÉTODO PARA CADASTRAR UM NOVO PRODUTO //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
		    conexao.Open();

            string query = "INSERT INTO Produto (Codigo, NomePrd, DescPrd, Preco, idCadastro) VALUES (@Codigo, @NomePrd, @DescPrd, @Preco, @idCadastro)";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@Codigo", p.Codigo);
            comando.Parameters.AddWithValue("@NomePrd", p.NomePrd);
            comando.Parameters.AddWithValue("@DescPrd", p.DescPrd);
            comando.Parameters.AddWithValue("@Preco", p.Preco);
            comando.Parameters.AddWithValue("@idCadastro", p.idCadastro);

            comando.ExecuteNonQuery();

            conexao.Close();
        }
        
        public List<Produto> Listar() // MÉTODO PARA LISTAR OS PRODUTOS JÁ CADASTRADOS NA VIEW RESPECTIVA //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);    
            conexao.Open();

            string query = "SELECT * FROM Produto";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            MySqlDataReader reader = comando.ExecuteReader();

            List<Produto> listaProdutos = new List<Produto>(); // CRIAÇÃO DA LISTA QUE RECEBERÁ AS INFORMAÇÕES DA LEITURA FEITA PELO "reader" DENTRO DE "while" //

            while (reader.Read()) // LAÇO DE REPETIÇÃO PARA CRIAR UM NOVO PRODUTO SEMPRE QUE "reader" LER ALGUMA INFORMAÇÃO //
            {
                Produto p = new Produto();
                p.idProduto = reader.GetInt32("idProduto"); // idProduto NÃO PRECISA DESSA VALIDAÇÃO DE NULO POR SER UM CAMPO AUTO INCREMENTO //

                if(!reader.IsDBNull(reader.GetOrdinal("Codigo"))) // VALIDAÇÃO DE CAMPO NULO: "se 'reader' NÃO ler um valor nulo no campo Codigo, então:" //
                {
                    p.Codigo = reader.GetInt32("Codigo"); // EXECUTA TAIS COMANDOS //
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

                listaProdutos.Add(p); // ADICIONA AS INFORMAÇÕES ADQUIRIDAS DO PRODUTO "p" NO "while" DENTRO DA LISTA CRIADA ANTERIORMENTE //
            }

            conexao.Close();

            return listaProdutos;
        }

        public void Editar(Produto p) // MÉTODO PARA EDIÇÃO DE UM PRODUTO //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);    
            conexao.Open();

            string query = "UPDATE Produto SET Codigo = @Codigo, NomePrd = @NomePrd, DescPrd = @DescPrd, Preco = @Preco, idCadastro = @idCadastro WHERE idProduto = @idProduto";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@idProduto", p.idProduto);
            comando.Parameters.AddWithValue("@Codigo", p.Codigo);
            comando.Parameters.AddWithValue("@NomePrd", p.NomePrd);
            comando.Parameters.AddWithValue("@DescPrd", p.DescPrd);
            comando.Parameters.AddWithValue("@Preco", p.Preco);
            comando.Parameters.AddWithValue("@idCadastro", p.idCadastro);

            comando.ExecuteNonQuery();

            conexao.Close();
        }

        public void Excluir(int idProduto) // MÉTODO PARA EXCLUSÃO DE UM PRODUTO, PARA ISSO O PARÂMETRO INFORMADO SERÁ O "idProduto" DO PRODUTO EM QUESTÃO //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);    
            conexao.Open();

            string query = "DELETE FROM Produto WHERE idProduto = @idProduto";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@idProduto", idProduto);

            comando.ExecuteNonQuery();

            conexao.Close();
        }
    
        public Produto BuscarID (int idProduto) // MÉTODO QUE BUSCA O "idProduto" [QUE É IDENTIFICADO AO CLICAR NO LINK DE EDIÇÃO] NO BANCO DE DADOS PARA TRAZER SUAS INFORMAÇÕES NA TELA DE EDIÇÃO //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);    
            conexao.Open();

            string query = "SELECT * FROM Produto WHERE idProduto = @idProduto";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@idProduto", idProduto);

            MySqlDataReader reader = comando.ExecuteReader();

            Produto p = new Produto();

            if (reader.Read()) // CONDIÇAO IF PARA ADQUIRIR AS INFORMAÇÕES REFERENTES AO ID LIDO NA VARIAVEL "reader" //
            {
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
            }

            conexao.Close();

            return p;
        }
    }
}