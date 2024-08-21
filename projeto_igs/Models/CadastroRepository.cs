using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using Microsoft.AspNetCore.Http;

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
    public class CadastroRepository
    {
        private const string DadosConexao = "Database = db_aa0d09_hotsite; Datasource = MYSQL8002.site4now.net; User Id = aa0d09_hotsite; Password = luanhots123;";
        public void CadastrarUsuario(Cadastro c) // MÉTODO PARA CADASTRAR UM NOVO USUARIO NÃO-ADMIN //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
		    conexao.Open();

            string query = "INSERT INTO Cadastro (Nome, Senha, Email, Endereco, Numero, Complemento, Bairro, CEP, Cidade, Estado, Tipo) VALUES (@Nome, @Senha, @Email, @Endereco, @Numero, @Complemento, @Bairro, @CEP, @Cidade, @Estado, @Tipo)";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@Nome", c.Nome);
            comando.Parameters.AddWithValue("@Senha", c.Senha);
            comando.Parameters.AddWithValue("@Email", c.Email);
            comando.Parameters.AddWithValue("@Endereco", c.Endereco);
            comando.Parameters.AddWithValue("@Numero", c.Numero);
            comando.Parameters.AddWithValue("@Complemento", c.Complemento);
            comando.Parameters.AddWithValue("@Bairro", c.Bairro);
            comando.Parameters.AddWithValue("@CEP", c.CEP);
            comando.Parameters.AddWithValue("@Cidade", c.Cidade);
            comando.Parameters.AddWithValue("@Estado", c.Estado);
            comando.Parameters.AddWithValue("@Tipo", c.Tipo);

            comando.ExecuteNonQuery();

            conexao.Close();
        }
        
        public void CadastrarAdmin(Cadastro c) // MÉTODO PARA CADASTRAR UM NOVO ADMIN //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
		    conexao.Open();

            string query = "INSERT INTO Cadastro (Nome, Senha, Email, Tipo) VALUES (@Nome, @Senha, @Email, @Tipo)";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@Nome", c.Nome);
            comando.Parameters.AddWithValue("@Senha", c.Senha);
            comando.Parameters.AddWithValue("@Email", c.Email);
            comando.Parameters.AddWithValue("@Tipo", c.Tipo);

            comando.ExecuteNonQuery();

            conexao.Close();
        }

        public string ChecarEmail (Cadastro c) // MÉTODO QUE BUSCA NO BANCO DE DADOS O EMAIL E SENHA RECEBIDOS PARA CRIAR UMA SESSAO DE LOGIN //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);    
            conexao.Open();

            string query = "SELECT Email FROM Cadastro WHERE Email = @Email";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@Email", c.Email);

            MySqlDataReader reader = comando.ExecuteReader();

            string emailVerify;

            if (reader.HasRows)
            {
                emailVerify = null;
            }
            else
            {
                emailVerify = "y";
            }

            conexao.Close();

            return emailVerify;
        }

        public List<Cadastro> Listar() // MÉTODO PARA LISTAR OS CADASTROS EXISTENTES NA VIEW RESPECTIVA //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);    
            conexao.Open();

            string query = "SELECT * FROM Cadastro";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            MySqlDataReader reader = comando.ExecuteReader();

            List<Cadastro> listaCadastros = new List<Cadastro>(); // CRIAÇÃO DA LISTA QUE RECEBERÁ AS INFORMAÇÕES DA LEITURA FEITA PELO "reader" DENTRO DE "while" //

            while (reader.Read()) // LAÇO DE REPETIÇÃO PARA CRIAR UM NOVO CADASTRO SEMPRE QUE "reader" LER ALGUMA INFORMAÇÃO //
            {
                Cadastro c = new Cadastro();
                c.idCadastro = reader.GetInt32("idCadastro"); // idCadastro NÃO PRECISA DESSA VALIDAÇÃO DE NULO POR SER UM CAMPO AUTO INCREMENTO //

                if(!reader.IsDBNull(reader.GetOrdinal("Nome"))) // VALIDAÇÃO DE CAMPO NULO: "se 'reader' NÃO ler um valor nulo no campo Nome, então:" //
                {
                    c.Nome = reader.GetString("Nome"); // EXECUTA TAIS COMANDOS //
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Senha")))
                {
                    c.Senha = reader.GetString("Senha");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Email")))
                {
                    c.Email = reader.GetString("Email");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Endereco")))
                {
                    c.Endereco = reader.GetString("Endereco");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Numero")))
                {
                    c.Numero = reader.GetInt32("Numero");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Complemento")))
                {
                    c.Complemento = reader.GetString("Complemento");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Bairro")))
                {
                    c.Bairro = reader.GetString("Bairro");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("CEP")))
                {
                    c.CEP = reader.GetString("CEP");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Cidade")))
                {
                    c.Cidade = reader.GetString("Cidade");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Estado")))
                {
                    c.Estado = reader.GetString("Estado");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Tipo")))
                {
                    c.Tipo = reader.GetString("Tipo");
                }

                listaCadastros.Add(c); // ADICIONA AS INFORMAÇÕES ADQUIRIDAS DO CADASTRO "c" NO "while" DENTRO DA LISTA //
            }

            conexao.Close();

            return listaCadastros;
        }

        public List<Cadastro> ListarAtual(int idCadastro) // MÉTODO PARA LISTAR O CADASTRO LOGADO NA VIEW RESPECTIVA //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);
            conexao.Open();

            string query = "SELECT * FROM Cadastro WHERE idCadastro = @idCadastro";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@idCadastro", idCadastro);

            MySqlDataReader reader = comando.ExecuteReader();

            List<Cadastro> listaCadastros = new List<Cadastro>();

            while (reader.Read())
            {
                Cadastro c = new Cadastro();
                c.idCadastro = reader.GetInt32("idCadastro");

                if(!reader.IsDBNull(reader.GetOrdinal("Nome")))
                {
                    c.Nome = reader.GetString("Nome");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Senha")))
                {
                    c.Senha = reader.GetString("Senha");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Email")))
                {
                    c.Email = reader.GetString("Email");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Endereco")))
                {
                    c.Endereco = reader.GetString("Endereco");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Numero")))
                {
                    c.Numero = reader.GetInt32("Numero");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Complemento")))
                {
                    c.Complemento = reader.GetString("Complemento");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Bairro")))
                {
                    c.Bairro = reader.GetString("Bairro");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("CEP")))
                {
                    c.CEP = reader.GetString("CEP");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Cidade")))
                {
                    c.Cidade = reader.GetString("Cidade");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Estado")))
                {
                    c.Estado = reader.GetString("Estado");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Tipo")))
                {
                    c.Tipo = reader.GetString("Tipo");
                }

                listaCadastros.Add(c); // ADICIONA AS INFORMAÇÕES ADQUIRIDAS DO CADASTRO "c" NO "while" DENTRO DA LISTA //
            }

            conexao.Close();

            return listaCadastros;
        }

        public void Editar(Cadastro c) // MÉTODO PARA EDIÇÃO DE UM CADASTRO //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);    
            conexao.Open();

            string query = "UPDATE Cadastro SET Nome = @Nome, Senha = @Senha, Email = @Email, Endereco = @Endereco, Numero = @Numero, Complemento = @Complemento, Bairro = @Bairro, CEP = @CEP, Cidade = @Cidade, Estado = @Estado WHERE idCadastro = @idCadastro";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@Nome", c.Nome);
            comando.Parameters.AddWithValue("@Senha", c.Senha);
            comando.Parameters.AddWithValue("@Email", c.Email);
            comando.Parameters.AddWithValue("@Endereco", c.Endereco);
            comando.Parameters.AddWithValue("@Numero", c.Numero);
            comando.Parameters.AddWithValue("@Complemento", c.Complemento);
            comando.Parameters.AddWithValue("@Bairro", c.Bairro);
            comando.Parameters.AddWithValue("@CEP", c.CEP);
            comando.Parameters.AddWithValue("@Cidade", c.Cidade);
            comando.Parameters.AddWithValue("@Estado", c.Estado);
            comando.Parameters.AddWithValue("@idCadastro", c.idCadastro);

            comando.ExecuteNonQuery();

            conexao.Close();
        }
 
        public void Excluir(int idCadastro) // MÉTODO PARA EXCLUSÃO DE UM CADASTRO, PARA ISSO O PARÂMETRO INFORMADO SERÁ O "idCadastro" DO CADASTRO EM QUESTÃO //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);    
            conexao.Open();

            string query = "DELETE FROM Cadastro WHERE idCadastro = @idCadastro";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@idCadastro", idCadastro);

            comando.ExecuteNonQuery();

            conexao.Close();
        }

        public Cadastro BuscarID (int idCadastro) // MÉTODO QUE BUSCA O "idCadastro" [QUE É IDENTIFICADO AO CLICAR NO LINK DE EDIÇÃO] NO BANCO DE DADOS PARA TRAZER SUAS INFORMAÇÕES NA TELA DE EDIÇÃO //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);    
            conexao.Open();

            string query = "SELECT * FROM Cadastro WHERE idCadastro = @idCadastro";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@idCadastro", idCadastro);

            MySqlDataReader reader = comando.ExecuteReader();

            Cadastro c = new Cadastro();

            if (reader.Read()) // CONDIÇAO IF PARA ADQUIRIR AS INFORMAÇÕES REFERENTES AO ID LIDO NA VARIAVEL "reader" //
            {
                c.idCadastro = reader.GetInt32("idCadastro");

                if(!reader.IsDBNull(reader.GetOrdinal("Nome")))
                {
                    c.Nome = reader.GetString("Nome");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Senha")))
                {
                    c.Senha = reader.GetString("Senha");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Email")))
                {
                    c.Email = reader.GetString("Email");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Endereco")))
                {
                    c.Endereco = reader.GetString("Endereco");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Numero")))
                {
                    c.Numero = reader.GetInt32("Numero");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Complemento")))
                {
                    c.Complemento = reader.GetString("Complemento");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Bairro")))
                {
                    c.Bairro = reader.GetString("Bairro");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("CEP")))
                {
                    c.CEP = reader.GetString("CEP");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Cidade")))
                {
                    c.Cidade = reader.GetString("Cidade");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Estado")))
                {
                    c.Estado = reader.GetString("Estado");
                }
            }

            conexao.Close();

            return c;
        }

        public string Del_Solicit_ID (int idCadastro) // MÉTODO QUE CONFERE SE O "idCadastro" DO CADASTRO QUE SERÁ EXCLUÍDO ESTÁ PENDENTE EM ALGUMA SOLICITAÇÃO (PORTANTO, NÃO PODE SER EXCLUÍDO) //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);    
            conexao.Open();

            string query = "SELECT idCadastro FROM Solicitacao WHERE idCadastro = @idCadastro";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@idCadastro", idCadastro);

            MySqlDataReader reader = comando.ExecuteReader();

            string verifyID;

            if (reader.HasRows)
            {
                verifyID = null;
            }
            else
            {
                verifyID = "y";
            }

            conexao.Close();

            return verifyID;
        }
    }
}