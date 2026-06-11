using System.ComponentModel.DataAnnotations;

namespace MiniSocialNetworkApi.Models.DTO
{
    public class UserRegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public string? Bio { get; set; }

    }
}
