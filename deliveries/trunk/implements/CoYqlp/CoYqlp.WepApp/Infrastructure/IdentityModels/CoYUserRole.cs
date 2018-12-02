using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoYqlp.WepApp.Infrastructure.IdentityModels
{
    public class CoYUserRole : IdentityUserRole<int>
    {
        #region Is have in Identity library
        //public int UserId { get; set; }
        //public int RoleId { get; set; }
        #endregion
        public Nullable<System.DateTime> DateCreate { get; set; }

        public virtual CoYRole CoYRole { get; set; }
        public virtual CoYUser CoYUser { get; set; }
    }
}