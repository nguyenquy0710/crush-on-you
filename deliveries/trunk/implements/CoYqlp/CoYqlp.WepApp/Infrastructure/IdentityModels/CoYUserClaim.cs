using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoYqlp.WepApp.Infrastructure.IdentityModels
{
    public class CoYUserClaim : IdentityUserClaim<int>
    {
        #region Is have in Identity library
        //public int Id { get; set; }
        //public int UserId { get; set; }
        //public string ClaimType { get; set; }
        //public string ClaimValue { get; set; }
        #endregion

        public Nullable<System.DateTime> DateCreate { get; set; }

        public virtual CoYUser CoYUser { get; set; }
    }
}