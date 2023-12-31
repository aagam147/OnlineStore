using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.WebApi.Models
{
    public class AppRole : IdentityRole<Guid>
    {
    }
}
