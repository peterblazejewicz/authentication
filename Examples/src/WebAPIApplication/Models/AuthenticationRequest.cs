using System.ComponentModel.DataAnnotations;

namespace WebAPIApplication.Models
{
    public class AuthenticationRequest
    {
        [Required]
        public string SessionToken { get; set; }
    }
}
