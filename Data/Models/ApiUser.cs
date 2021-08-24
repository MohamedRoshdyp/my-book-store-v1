
using Microsoft.AspNetCore.Identity;

namespace my_book_store_v1.Data.Models;
public class ApiUser:IdentityUser
{

    public string FirstName { get; set; }
    public string LastName { get; set; }
}
