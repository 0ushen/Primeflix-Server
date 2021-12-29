using Microsoft.AspNetCore.Identity;

namespace Primeflix.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        
    }
    public ApplicationUser(string userName) : base(userName)
    {
    }
}
