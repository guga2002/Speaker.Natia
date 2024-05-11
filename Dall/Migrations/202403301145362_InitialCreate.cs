namespace Speaker.leison.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chanells",
                c => new
                    {
                        Chanell_Id = c.Int(nullable: false, identity: true),
                        ChanellFormat = c.String(),
                        Port_In_250 = c.Int(nullable: false),
                        Is_From_Optic = c.Boolean(nullable: false),
                        Name_Of_Chanell = c.String(),
                    })
                .PrimaryKey(t => t.Chanell_Id);
            
            CreateTable(
                "dbo.Infos",
                c => new
                    {
                        Info_Id = c.Int(nullable: false, identity: true),
                        Alarm_For_Display = c.String(),
                        CHanell_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Info_Id)
                .ForeignKey("dbo.Chanells", t => t.CHanell_Id, cascadeDelete: true)
                .Index(t => t.CHanell_Id);
            
            CreateTable(
                "dbo.Transcoders",
                c => new
                    {
                        Transcoder_Id = c.Int(nullable: false, identity: true),
                        Emr_Number = c.Int(nullable: false),
                        Card_In_Transcoder = c.Int(nullable: false),
                        Port_In_Transcoder = c.Int(nullable: false),
                        Chanell_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Transcoder_Id)
                .ForeignKey("dbo.Chanells", t => t.Chanell_Id, cascadeDelete: true)
                .Index(t => t.Chanell_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transcoders", "Chanell_Id", "dbo.Chanells");
            DropForeignKey("dbo.Infos", "CHanell_Id", "dbo.Chanells");
            DropIndex("dbo.Transcoders", new[] { "Chanell_Id" });
            DropIndex("dbo.Infos", new[] { "CHanell_Id" });
            DropTable("dbo.Transcoders");
            DropTable("dbo.Infos");
            DropTable("dbo.Chanells");
        }
    }
}
