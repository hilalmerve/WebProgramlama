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
    public class IcerikSeed : DropCreateDatabaseAlways<bilmekDbContext>
    {
        protected override void Seed(bilmekDbContext context)
        {
            IList<GenelIcerikModel> genelIcerikModel = new List<GenelIcerikModel>();

            genelIcerikModel.Add(new GenelIcerikModel()
            {
                CTitle = "Hakkımızda",
                CFullContent = "<p> ",
                CreatedOn = DateTime.Now,
                IsPublished = true
            });
            genelIcerikModel.Add(new GenelIcerikModel()
            {
                CTitle = "Misyon ve Vizyon",
                CFullContent = " ",
                CreatedOn = DateTime.Now,
                IsPublished = true
            });
            genelIcerikModel.Add(new GenelIcerikModel()
            {
                CTitle = "Bilgi Güvenliği Yönetim Sistemi",
                CFullContent = " "
                  ,
                CreatedOn = DateTime.Now,
                IsPublished = true
            });

            context.GenelIcerik.AddRange(genelIcerikModel);
            
            base.Seed(context);
        }

    }
}