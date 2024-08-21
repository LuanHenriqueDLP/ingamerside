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
    public class SolicitacaoRepository
    {
        private const string DadosConexao = "Database = db_aa0d09_hotsite; Datasource = MYSQL8002.site4now.net; User Id = aa0d09_hotsite; Password = luanhots123;";
        public void Cadastrar(Solicitacao s) // MÉTODO PARA CADASTRAR UMA NOVA SOLICITAÇÃO //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
		    conexao.Open();

            string query = "INSERT INTO Solicitacao (Codigo, txtBox, idCadastro) VALUES (@Codigo, @txtBox, @idCadastro)";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@Codigo", s.Codigo);
            comando.Parameters.AddWithValue("@txtBox", s.txtBox);
            comando.Parameters.AddWithValue("@idCadastro", s.idCadastro);

            comando.ExecuteNonQuery();

            conexao.Close();
        }
        
        public List<Solicitacao> Listar() // MÉTODO PARA LISTAR AS SOLICITAÇÕES JÁ CADASTRADAS NA VIEW RESPECTIVA //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);    
            conexao.Open();

            string query = "SELECT * FROM Solicitacao";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            MySqlDataReader reader = comando.ExecuteReader();

            List<Solicitacao> listaSolicitacoes = new List<Solicitacao>(); // CRIAÇÃO DA LISTA QUE RECEBERÁ AS INFORMAÇÕES DA LEITURA FEITA PELO "reader" DENTRO DE "while" //

            while (reader.Read()) // LAÇO DE REPETIÇÃO PARA CRIAR UM NOVO OBJETO PACOTE SEMPRE QUE "reader" LER ALGUMA INFORMAÇÃO //
            {
                Solicitacao s = new Solicitacao();
                s.idSolicitacao = reader.GetInt32("idSolicitacao");

                if(!reader.IsDBNull(reader.GetOrdinal("Codigo"))) // VALIDAÇÃO DE CAMPO NULO: "se 'reader' NÃO ler um valor nulo no campo Codigo, então:" //
                {
                    s.Codigo = reader.GetInt32("Codigo"); // EXECUTA TAIS COMANDOS //
                }

                if(!reader.IsDBNull(reader.GetOrdinal("txtBox")))
                {
                    s.txtBox = reader.GetString("txtBox");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("idCadastro")))
                {
                    s.idCadastro = reader.GetInt32("idCadastro");
                }

                listaSolicitacoes.Add(s); // ADICIONA AS INFORMAÇÕES ADQUIRIDAS DO PACOTE "p" NO "while" DENTRO DA LISTA //
            }

            conexao.Close();

            return listaSolicitacoes;
        }

        public void Excluir(int idSolicitacao) // MÉTODO PARA EXCLUSÃO DE UMA SOLICITAÇÃO, PARA ISSO O PARÂMETRO INFORMADO SERÁ O "idSolicitacao" DA SOLICITAÇÃO EM QUESTÃO //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);    
            conexao.Open();

            string query = "DELETE FROM Solicitacao WHERE idSolicitacao = @idSolicitacao";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@idSolicitacao", idSolicitacao);

            comando.ExecuteNonQuery();

            conexao.Close();
        }

    }
}