using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.WebApi.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        

        [NotMapped]
        public string Fullname => $"{Firstname} {Lastname}";

        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
