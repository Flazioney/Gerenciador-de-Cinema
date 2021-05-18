using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Gerenciador_de_Cinema.Models
{
    public class Autenticacao : IAutenticacao
    {
        public IConfiguration Configuration { get; set; }

        public string GetConexao()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            string connectionString = Configuration["ConnectionStrings:Gerenciador_de_CinemaContext"];
            return connectionString;

        }
        public string ValidarLogin(Login login)
        {
            using (SqlConnection con = new SqlConnection(GetConexao()))
            {
                SqlCommand cmd = new SqlCommand("buscaLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@usuario", login.email);
                cmd.Parameters.AddWithValue("@Senha", login.senha);

                con.Open();
                string resultado = cmd.ExecuteScalar().ToString();
                con.Close();

                return resultado;
            }
        }  
        
        public string DeletarSessoes(Sessao sessaos)
        {
            using (SqlConnection con = new SqlConnection(GetConexao()))
            {
                SqlCommand cmd = new SqlCommand("deletarSessao", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", sessaos.id_sessao);
               
               
                con.Open();              
                    string resultado = cmd.ExecuteScalar().ToString();
                con.Close();

                return resultado;
            }
        }
    }
}
