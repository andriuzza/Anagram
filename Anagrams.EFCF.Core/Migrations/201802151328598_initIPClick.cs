namespace Anagrams.EFCF.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initIPClick : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IPClicks",
                c => new
                    {
                        IP = c.String(nullable: false, maxLength: 25),
                        Count = c.Int(nullable: false),
                        LastClickData = c.DateTime(nullable: false),
                        Expiration = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IP);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.IPClicks");
        }
    }
}
