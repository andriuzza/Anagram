namespace Anagrams.EFCF.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreasedIPAddersLength : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.IPClicks");
            AlterColumn("dbo.IPClicks", "IP", c => c.String(nullable: false, maxLength: 30));
            AddPrimaryKey("dbo.IPClicks", "IP");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.IPClicks");
            AlterColumn("dbo.IPClicks", "IP", c => c.String(nullable: false, maxLength: 25));
            AddPrimaryKey("dbo.IPClicks", "IP");
        }
    }
}
