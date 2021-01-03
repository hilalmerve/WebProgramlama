using bilmek.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace bilmek.Migrations
{
    public class Configuration: CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any(u => u.UserName == "b181210053@sakarya.edu.tr"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "hilalmervve@gmail.com", UserName = "hilalmerve" };
                var result = manager.Create(user, "123");
            }
            base.Seed(context);
        }
        
    }
}
