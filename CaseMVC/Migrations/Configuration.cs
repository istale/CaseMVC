namespace CaseMVC.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CaseMVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CaseMVC.Models.ApplicationDbContext db)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            #region 初始化 level
            db.Database.ExecuteSqlCommand("truncate table level");
            List<level> levels = new List<level>();
            level aa = new level();
            aa.l_name = "高階";
            aa.l_value = 3;
            levels.Add(aa);

            level bb = new level();
            bb.l_name = "中階";
            bb.l_value = 2;
            levels.Add(bb);

            level cc = new level();
            cc.l_name = "低層";
            cc.l_value = 1;
            levels.Add(cc);

            db.levels.AddRange(levels);
            db.SaveChanges();
            #endregion
        }
    }
}
