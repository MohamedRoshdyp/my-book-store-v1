
using System.ComponentModel.DataAnnotations;

namespace my_book_store_v1.Data.DTOs;

public class LoginUserDTO
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(10, ErrorMessage = "Your Password Is Limited to {2} to {1} characters", MinimumLength = 5)]
    public string Password { get; set; }
}
public class userDTO: LoginUserDTO
{


    public string FirstName { get; set; }
    public string LastName { get; set; }
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    public ICollection<string> Roles { get; set; }


}
