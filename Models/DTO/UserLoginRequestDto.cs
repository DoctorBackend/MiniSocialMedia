using System.ComponentModel.DataAnnotations;

namespace MiniSocialNetworkApi.Models.DTO
{
    public class UserLoginRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
