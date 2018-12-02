using BrockAllen.IdentityReboot.Ef;
using CoYqlp.WepApp.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CoYqlp.WepApp.Infrastructure.IdentityModels
{
    public class CoYUser : IdentityRebootUser<int, CoYUserLogin, CoYUserRole, CoYUserClaim>
    {
        public CoYUser()
        {
            this.CoYUserClaims = new HashSet<CoYUserClaim>();
            this.CoYUserLogins = new HashSet<CoYUserLogin>();
            this.CoYUserRoles = new HashSet<CoYUserRole>();
        }

        #region Is have in Identity libary
        //public int Id { get; set; }
        //public string UserName { get; set; }
        //public string Email { get; set; }
        //public bool EmailConfirmed { get; set; }
        //public string PasswordHash { get; set; }
        //public string SecurityStamp { get; set; }
        //public string PhoneNumber { get; set; }
        //public bool PhoneNumberConfirmed { get; set; }
        //public bool TwoFactorEnabled { get; set; }
        //public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        //public bool LockoutEnabled { get; set; }
        //public int AccessFailedCount { get; set; }
        #endregion

        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }

        public virtual ICollection<CoYUserClaim> CoYUserClaims { get; set; }
        public virtual ICollection<CoYUserLogin> CoYUserLogins { get; set; }
        public virtual ICollection<CoYUserRole> CoYUserRoles { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<CoYUser, int> manager)
        {
            try
            {
                // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
                // Add custom user claims here
                return userIdentity;
            }
            catch (Exception e)
            {
                SYSLogHelpers.WriteSYSLog(e, new Models.CoYDBConnection(), null);
                throw e;
            }
        }

        public ClaimsIdentity GenerateUserIdentity(UserManager<CoYUser, int> manager)
        {
            // Note the authenticationType must match the one defined in
            //   CookieAuthenticationOptions.AuthenticationType
            try
            {
                var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
                // Add custom user claims here
                return userIdentity;
            }
            catch (Exception e)
            {
                SYSLogHelpers.WriteSYSLog(e, new Models.CoYDBConnection(), null);
                throw e;
            }
        }
    }
}