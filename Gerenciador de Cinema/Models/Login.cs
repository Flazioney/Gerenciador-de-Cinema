
using System.ComponentModel.DataAnnotations;

namespace Gerenciador_de_Cinema.Models
{
    public class Login
    {
        [Key]
        public int id_login { get; set; }

        public string nome { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string senha { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        public int tipoAcesso { get; set; }

    }

}
