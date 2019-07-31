using System.ComponentModel.DataAnnotations;

namespace ProjectLinx.Services.API
{
    public class UsuarioViewModel
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}")]
        [MaxLength(150, ErrorMessage = "Preencha no máximo com 150 caracteres")]
        [MinLength(3, ErrorMessage = "Preencha no mínimo com 3 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}", AllowEmptyStrings = false)]
        [MinLength(1, ErrorMessage = "Preencha no mínimo com 1 caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}