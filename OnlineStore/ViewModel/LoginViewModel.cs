using System.ComponentModel.DataAnnotations;

namespace OnlineStore.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
