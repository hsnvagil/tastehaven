#region

using Microsoft.AspNetCore.Identity;

#endregion

namespace Taste_Haven_API.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
}