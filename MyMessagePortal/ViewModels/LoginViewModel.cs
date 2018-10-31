using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyMessagePortal.ViewModels
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Login jest wymanagny")]
        [MinLength(3, ErrorMessage = "Login musi mieć przynajmniej {1} znaków")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Hasło jest wymanagne")]
        [DataType(DataType.Password)]
        [DisplayName("Hasło")]
        public string Password { get; set; }

        [DisplayName("Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }
}
