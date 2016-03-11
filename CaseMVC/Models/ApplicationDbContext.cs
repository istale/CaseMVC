using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CaseMVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        #region database table
        public DbSet<user> users { get; set; }
        public DbSet<ccase> ccases { get; set; }
        public DbSet<level> levels { get; set; }
        public DbSet<condition> conditions { get; set; }

        #endregion

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}