namespace Gerenciador_de_Cinema.Models
{
    public interface IAutenticacao
    {
        string GetConexao();
        string ValidarLogin(Login login);
    }

}
