namespace CaseMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.case",
                c => new
                    {
                        c_id = c.Int(nullable: false, identity: true),
                        description = c.String(),
                        l_value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.c_id);
            
            CreateTable(
                "dbo.condition",
                c => new
                    {
                        cc_id = c.Int(nullable: false, identity: true),
                        c_id = c.Int(nullable: false),
                        condition_description = c.String(),
                    })
                .PrimaryKey(t => t.cc_id);
            
            CreateTable(
                "dbo.level",
                c => new
                    {
                        l_id = c.Int(nullable: false, identity: true),
                        l_name = c.String(),
                        l_value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.l_id);
            
            CreateTable(
                "dbo.user",
                c => new
                    {
                        u_id = c.Int(nullable: false, identity: true),
                        acc = c.String(nullable: false),
                        pwd = c.String(nullable: false),
                        level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.u_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.user");
            DropTable("dbo.level");
            DropTable("dbo.condition");
            DropTable("dbo.case");
        }
    }
}
