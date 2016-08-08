using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AWDCMSFramework.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual DateTime CreateDate { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var authenticationType = "Basic";
            var userIdentity = new ClaimsIdentity(await manager.GetClaimsAsync(this), authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}