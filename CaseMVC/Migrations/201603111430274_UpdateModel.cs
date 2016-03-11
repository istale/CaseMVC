namespace CaseMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.case", "d_id", c => c.String());
            AddColumn("dbo.case", "f_id", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.case", "f_id");
            DropColumn("dbo.case", "d_id");
        }
    }
}
