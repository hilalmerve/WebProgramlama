using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web.Mvc;
using bilmek.Migrations;

namespace bilmek.Models
{
    public class ReferansIcerikModel
    {
        [Key]
        public int Id { get; set; }

        public string ReferansAdi { get; set; }
        public byte[] Resim { get; set; }
        public string Url { get; set; }
        public bool IsPublished { get; set; }
    }
    public class GenelIcerikModel
    {
        [Key]
        public int Id { get; set; }
        public string CTitle { get; set; }
        [AllowHtml]
        public string CFullContent { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsPublished { get; set; }
    }
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        public string Adi { get; set; }
        public byte[] Resim { get; set; }
        public bool IsPublished { get; set; }
    }
    public class HaberIcerikModel
    {
        [Key]
        public int Id { get; set; }
        public string CTitle { get; set; }
        [AllowHtml]
        public string CFullContent { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsPublished { get; set; }
        public byte[] Resim { get; set; }
    }
    public class BasvuruIcerikModel
    {
        [Key]
        public int Id { get; set; }
        public string BasvuruTuru { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Mail { get; set; }
        public string Mesaj { get; set; }
        public bool IsPublished { get; set; }
        public byte[] Resim { get; set; }
    }
    public class bilmekDbContext : DbContext
    {
        public bilmekDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new IcerikSeed());
        }
        public DbSet<GenelIcerikModel> GenelIcerik { get; set; }
        public DbSet<HaberIcerikModel> HaberIcerik { get; set; }
        public DbSet<ReferansIcerikModel> ReferansIcerik { get; set; }
        public DbSet<BasvuruIcerikModel> BasvuruIcerik { get; set; }
        public DbSet<Slider> Slider { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}