using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserAuthentication.ValueObjects;

namespace UserAuthentication.Entities
{
    public class AuthenticationUser : IdentityUser
    {
        public string ReferenceID { get; set; }
        public Status Status { get; private set; }
        public Audit Audit { get; private set; }

        private AuthenticationUser()
        {
        }

        public AuthenticationUser(string email)
            : base()
        {
            Email = email;

            UserName = email;

            Status = new Status();
            Audit = new Audit();
        }

        public AuthenticationUser(string email, string referenceID)
            : this(email)
        {
            ReferenceID = referenceID;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<AuthenticationUser> manager, string authenticationType)
        {
            // Create a new ClaimsIdentity for the user
            var identity = new ClaimsIdentity(authenticationType);

            // Add user claims to the identity
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, this.Id));
            identity.AddClaim(new Claim(ClaimTypes.Name, this.UserName));

            // Get the user roles and add them as claims
            var roles = await manager.GetRolesAsync(this);
            foreach (var role in roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return identity;
        }

        public void Deactivate()
        {
            Email = Id;
            EmailConfirmed = false;
            UserName = Id;

            Status.Update(false);
            Update();
        }

        public virtual void Update()
        {
            Audit.Update();
        }
    }
}
