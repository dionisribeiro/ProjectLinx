using System.ComponentModel.DataAnnotations;

namespace ProjectLinx.Presentation.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Preencha o campo {0}")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Preencha o campo {0}", AllowEmptyStrings = false)]
        [MinLength(1, ErrorMessage = "Preencha no mínimo com 1 caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
