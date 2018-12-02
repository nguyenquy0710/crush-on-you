using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoYqlp.WepApp.Infrastructure.IdentityModels
{
    public class CoYUserLogin : IdentityUserLogin<int>
    {
        public int Id { get; set; }
        #region Is have in Identity library
        //public string LoginProvider { get; set; }
        //public string ProviderKey { get; set; }
        //public int UserId { get; set; }
        #endregion
        public Nullable<System.DateTime> DateCreate { get; set; }

        public virtual CoYUser CoYUser { get; set; }
    }
}