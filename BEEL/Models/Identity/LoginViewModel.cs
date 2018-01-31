using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEL.Models.Identity
{
    public class LoginViewModel
    {
        [Display(Name = "Пользователь")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}