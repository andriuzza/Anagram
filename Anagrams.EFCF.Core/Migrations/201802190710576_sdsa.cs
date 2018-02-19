namespace Anagrams.EFCF.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdsa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CacheAnagrams",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        WordId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.WordId })
                .ForeignKey("dbo.Words", t => t.WordId, cascadeDelete: true)
                .Index(t => t.WordId);
            
            CreateTable(
                "dbo.Words",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CacheMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortedWord = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IPClicks",
                c => new
                    {
                        IP = c.String(nullable: false, maxLength: 30),
                        Count = c.Int(nullable: false),
                        Expiration = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IP);
            
            CreateTable(
                "dbo.IPLogUsers",
                c => new
                    {
                        IP = c.String(nullable: false, maxLength: 128),
                        SortedWord = c.String(nullable: false, maxLength: 128),
                        Time = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IP, t.SortedWord });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CacheAnagrams", "WordId", "dbo.Words");
            DropIndex("dbo.CacheAnagrams", new[] { "WordId" });
            DropTable("dbo.IPLogUsers");
            DropTable("dbo.IPClicks");
            DropTable("dbo.CacheMaps");
            DropTable("dbo.Words");
            DropTable("dbo.CacheAnagrams");
        }
    }
}
