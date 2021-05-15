using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Configuration;
using System.Threading.Tasks;

namespace Gerenciador_de_Cinema.DataBase
{
    public class AdmSql
    {
        //String de Conexão
        static String strConn = ConfigurationManager.ConnectionStrings["Conexao"].ToString();
        //Objeto de conexão
        public static SqlConnection conexao = new SqlConnection();
        //Método para conectar no banco de dados
        public static void conectar()
        {
            if (conexao.State != ConnectionState.Open)
            {
                try
                {
                    conexao.ConnectionString = strConn;
                    conexao.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        //Método utilizado para desconectar do banco de dados
        public static void desconectar()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        public static void ExecutarComando(String sql, List<SqlParameter> objParams)
        {
            try
            {
                conectar();
                SqlCommand comando = new SqlCommand(sql, conexao);
                foreach (SqlParameter param in objParams)
                {
                    comando.Parameters.Add(param);
                }
                comando.ExecuteNonQuery();
            }
            //Força a execução do Finally quando executa com sucesso ou com erro o 
            //bloco Try
            finally
            {
                desconectar();
            }
        }
        //Retornar dados do Banco
        public static DataTable RetornaTabela(String sql, List<SqlParameter> objParams)
        {
            try
            {
                conectar();
                SqlCommand comando = new SqlCommand(sql, conexao);
                foreach (SqlParameter param in objParams)
                {
                    comando.Parameters.Add(param);
                }
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            finally
            {
                desconectar();
            }
        }

        public static DataTable RetornaTabela(String sql)
        {
            try
            {
                conectar();
                SqlCommand comando = new SqlCommand(sql, conexao);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            finally
            {
                desconectar();
            }
        }
    }
}
