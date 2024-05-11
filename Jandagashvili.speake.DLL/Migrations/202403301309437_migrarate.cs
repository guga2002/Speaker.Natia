namespace Speaker.leison.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrarate : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Chanells", name: "Chanell_Id", newName: "Id");
            RenameColumn(table: "dbo.Infos", name: "Info_Id", newName: "Id");
            RenameColumn(table: "dbo.Transcoders", name: "Transcoder_Id", newName: "Id");
            CreateTable(
                "dbo.Desclamblers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Emr_Number = c.Int(nullable: false),
                        Card_In_Desclambler = c.Int(nullable: false),
                        Port_In_Desclambler = c.Int(nullable: false),
                        Chanell_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chanells", t => t.Chanell_Id, cascadeDelete: true)
                .Index(t => t.Chanell_Id);
            
            CreateTable(
                "dbo.Recievers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Emr_Number = c.Int(nullable: false),
                        Card_In_Reciever = c.Int(nullable: false),
                        Port_In_Reciever = c.Int(nullable: false),
                        Chanell_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chanells", t => t.Chanell_Id, cascadeDelete: true)
                .Index(t => t.Chanell_Id);
            
            CreateTable(
                "dbo.Emr60Info",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Port = c.String(),
                        SourceEmr = c.String(),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recievers", "Chanell_Id", "dbo.Chanells");
            DropForeignKey("dbo.Desclamblers", "Chanell_Id", "dbo.Chanells");
            DropIndex("dbo.Recievers", new[] { "Chanell_Id" });
            DropIndex("dbo.Desclamblers", new[] { "Chanell_Id" });
            DropTable("dbo.Emr60Info");
            DropTable("dbo.Recievers");
            DropTable("dbo.Desclamblers");
            RenameColumn(table: "dbo.Transcoders", name: "Id", newName: "Transcoder_Id");
            RenameColumn(table: "dbo.Infos", name: "Id", newName: "Info_Id");
            RenameColumn(table: "dbo.Chanells", name: "Id", newName: "Chanell_Id");
        }
    }
}
