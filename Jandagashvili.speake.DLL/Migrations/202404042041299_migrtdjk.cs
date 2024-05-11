namespace Speaker.leison.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrtdjk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emr100Info",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Port = c.String(),
                        SourceEmr = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Emr110info",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Port = c.String(),
                        SourceEmr = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Emr120Info",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Port = c.String(),
                        SourceEmr = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Emr130Info",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Port = c.String(),
                        SourceEmr = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Emr200Info",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Port = c.String(),
                        SourceEmr = c.Int(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Emr200Info");
            DropTable("dbo.Emr130Info");
            DropTable("dbo.Emr120Info");
            DropTable("dbo.Emr110info");
            DropTable("dbo.Emr100Info");
        }
    }
}
