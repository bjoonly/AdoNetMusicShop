namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetRecordDeal : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Records", new[] { "DealId" });
            AlterColumn("dbo.Records", "DealId", c => c.Int());
            CreateIndex("dbo.Records", "DealId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Records", new[] { "DealId" });
            AlterColumn("dbo.Records", "DealId", c => c.Int(nullable: false));
            CreateIndex("dbo.Records", "DealId");
        }
    }
}
