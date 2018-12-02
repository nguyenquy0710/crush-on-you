using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoYqlp.WepApp.Infrastructure.IdentityModels
{
    public class CoYRole : IdentityRole<int, CoYUserRole>
    {
        public CoYRole()
        {
            this.CoYUserRole = new HashSet<CoYUserRole>();
        }

        #region Is have in Identity library
        //public int Id { get; set; }
        //public string Name { get; set; }
        #endregion
        public Nullable<System.DateTime> DateCreate { get; set; }

        public virtual ICollection<CoYUserRole> CoYUserRole { get; set; }
    }
}