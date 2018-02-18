namespace Anagrams.EFCF.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixed : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.IPClicks", "LastClickData");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IPClicks", "LastClickData", c => c.DateTime(nullable: false));
        }
    }
}
