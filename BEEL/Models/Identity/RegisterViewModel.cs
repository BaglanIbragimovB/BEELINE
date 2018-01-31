using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BEEL.Models.Identity
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Пользователь")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]        
        [Display(Name = "ФИО")]
        public string FullName { get; set; }
                
        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароль и повтор не совпадают.")]
        public string ConfirmPassword { get; set; }
    }
}