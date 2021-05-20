namespace Gerenciador_de_Cinema.Models
{
    public interface IAutenticacao
    {
        string GetConexao();
        string ValidarLogin(Login login);
        string DeletarSessoes(int id_sessoes);
        string InserirSessoes(Sessao sessao);
    }

}
