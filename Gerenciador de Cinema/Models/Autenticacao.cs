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

        public string DeletarSessoes(int id)
        {
            using (SqlConnection con = new SqlConnection(GetConexao()))
            {
                SqlCommand cmd = new SqlCommand("delpcdSe", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@idses", id);
                con.Open();

                string resultado = cmd.ExecuteScalar().ToString();
                con.Close();
                return resultado;


            }
        }

        public string InserirSessoes(Sessao sessao)
        {
            using (SqlConnection con = new SqlConnection(GetConexao()))
            {
                SqlCommand cmd = new SqlCommand("pcdSessao", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@dataexb", sessao.data_exb);
                cmd.Parameters.AddWithValue("@hrini", sessao.hr_ini);               
                cmd.Parameters.AddWithValue("@valoring", sessao.valor_ing);
                cmd.Parameters.AddWithValue("@idfilme", sessao.id_filme);
                cmd.Parameters.AddWithValue("@idsala", sessao.id_sala);
                
                con.Open();

                string resultado = cmd.ExecuteScalar().ToString();
                con.Close();
                return resultado;


            }
        }

        public string UpdateFilme(Filmes filmes) 
        {
            using (SqlConnection con = new SqlConnection(GetConexao()))
            {
                SqlCommand cmd = new SqlCommand("pcdUpdtFilme", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tid", filmes.id_filme);
                cmd.Parameters.AddWithValue("@Titulo", filmes.Titulo);
                cmd.Parameters.AddWithValue("@Descricao",filmes.Descricao);
                cmd.Parameters.AddWithValue("@duracao", filmes.duracao);
                cmd.Parameters.AddWithValue("@dados", filmes.Dados);
                cmd.Parameters.AddWithValue("@ContentType", filmes.ContentType);

                con.Open();

                string resultado = cmd.ExecuteScalar().ToString();
                con.Close();
                return resultado;


            }

        }


    }
}
