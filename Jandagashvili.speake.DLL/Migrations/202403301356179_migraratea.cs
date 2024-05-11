namespace Speaker.leison.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migraratea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chanells", "NameForSPeak", c => c.String());
            AlterColumn("dbo.Emr60Info", "SourceEmr", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Emr60Info", "SourceEmr", c => c.String());
            DropColumn("dbo.Chanells", "NameForSPeak");
        }
    }
}
