using Gerenciador_de_Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Configuration;
using System.Text;
using System.Data;

namespace Gerenciador_de_Cinema.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Models.ViewModels.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void pcdInsertFilm()
        {

        }

        #region Procedure de Busca Login
        public void ProcedureBuscaLogin()
        {

            StringBuilder sql = new StringBuilder();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexao"].ToString()))
                try
                {
                    SqlCommand command = new SqlCommand("[dbo].[buscaLogin]", conn);
                    command.CommandType = CommandType.StoredProcedure;
                   /* command.Parameters.Add(new SqlParameter("@USUARIO", SqlDbType.Text)).Value;// = ViewData.;
                    command.Parameters.Add(new SqlParameter("@SENHA", SqlDbType.Text)).Value = txtSenha.Text;
                    command.Parameters.Add(new SqlParameter("@ID", SqlDbType.SmallInt)).Value = txtid.Text;*/
                    SqlDataReader reader;
                    conn.Open();
                    int i = (int)command.ExecuteScalar();
                    try
                    {

                        if (i > 0)
                        {

                            Contact();
                        }
                        else
                        {
                            Console.WriteLine("Usuário ou senha incorretos");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro: " + ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                catch (SqlException ex)
                {
                    ex.Message.ToString();
                }
        }


        #endregion

    }
}
