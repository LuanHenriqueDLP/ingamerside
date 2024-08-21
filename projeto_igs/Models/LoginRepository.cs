using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace projeto_etapa4.Models
{
    public class LoginRepository
    {
        private const string DadosConexao = "Database = db_aa0d09_hotsite; Datasource = MYSQL8002.site4now.net; User Id = aa0d09_hotsite; Password = luanhots123;";
        public Cadastro Login (Cadastro c) // MÉTODO QUE BUSCA NO BANCO DE DADOS O EMAIL E SENHA RECEBIDOS PARA CRIAR UMA SESSAO DE LOGIN //
        {
            MySqlConnection conexao = new MySqlConnection(DadosConexao);    
            conexao.Open();

            string query = "SELECT * FROM Cadastro WHERE Email = @Email AND Senha = @Senha";
            MySqlCommand comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@Email", c.Email);
            comando.Parameters.AddWithValue("@Senha", c.Senha);

            MySqlDataReader reader = comando.ExecuteReader();

            Cadastro cadLog = new Cadastro();

            if (reader.Read()) // CONDIÇAO IF PARA ADQUIRIR AS INFORMAÇÕES REFERENTES AO EMAIL E SENHA LIDOS NA VARIAVEL "reader" //
            {
                cadLog.idCadastro = reader.GetInt32("idCadastro");

                if(!reader.IsDBNull(reader.GetOrdinal("Nome")))
                {
                    cadLog.Nome = reader.GetString("Nome");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Email")))
                {
                    cadLog.Email = reader.GetString("Email");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Senha")))
                {
                    cadLog.Senha = reader.GetString("Senha");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Endereco")))
                {
                    cadLog.Endereco = reader.GetString("Endereco");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Numero")))
                {
                    cadLog.Numero = reader.GetInt32("Numero");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Complemento")))
                {
                    cadLog.Complemento = reader.GetString("Complemento");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Bairro")))
                {
                    cadLog.Bairro = reader.GetString("Bairro");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("CEP")))
                {
                    cadLog.CEP = reader.GetString("CEP");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Cidade")))
                {
                    cadLog.Cidade = reader.GetString("Cidade");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Estado")))
                {
                    cadLog.Estado = reader.GetString("Estado");
                }

                if(!reader.IsDBNull(reader.GetOrdinal("Tipo")))
                {
                    cadLog.Tipo = reader.GetString("Tipo");
                }
            }
            else
            {
                cadLog = null;
            }

            conexao.Close();

            return cadLog;
        }

    }
}