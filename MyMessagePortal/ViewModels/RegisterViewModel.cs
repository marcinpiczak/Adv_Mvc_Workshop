﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyMessagePortal.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Login jest wymanagny")]
        [MinLength(3, ErrorMessage = "Login musi mieć przynajmniej {1} znaków")]
        public string Login { get; set; }

        [Required(ErrorMessage = "E-mail jest wymanagny")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Podaj prawidłowy e-mail")]
        [EmailAddress(ErrorMessage = "Podaj prawidłowy e-mail")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymanagne")]
        [DataType(DataType.Password)]
        [DisplayName("Hasło")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Hasło jest wymanagne")]
        [DataType(DataType.Password)]
        [DisplayName("Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Hasła muszą być takie same")]
        public string RepeatPassword { get; set; }
    }
}
