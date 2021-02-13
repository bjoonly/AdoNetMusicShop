namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetSalesSetAside : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SetAsideRecords", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.SetAsideRecords", "RecordId", "dbo.Records");
            DropIndex("dbo.SalesHistories", new[] { "RecordId" });
            DropIndex("dbo.SetAsideRecords", new[] { "RecordId" });
            AlterColumn("dbo.SalesHistories", "RecordId", c => c.Int());
            AlterColumn("dbo.SetAsideRecords", "RecordId", c => c.Int());
            CreateIndex("dbo.SalesHistories", "RecordId");
            CreateIndex("dbo.SetAsideRecords", "RecordId");
            AddForeignKey("dbo.SetAsideRecords", "ClientId", "dbo.Clients", "AccountId");
            AddForeignKey("dbo.SetAsideRecords", "RecordId", "dbo.Records", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SetAsideRecords", "RecordId", "dbo.Records");
            DropForeignKey("dbo.SetAsideRecords", "ClientId", "dbo.Clients");
            DropIndex("dbo.SetAsideRecords", new[] { "RecordId" });
            DropIndex("dbo.SalesHistories", new[] { "RecordId" });
            AlterColumn("dbo.SetAsideRecords", "RecordId", c => c.Int(nullable: false));
            AlterColumn("dbo.SalesHistories", "RecordId", c => c.Int(nullable: false));
            CreateIndex("dbo.SetAsideRecords", "RecordId");
            CreateIndex("dbo.SalesHistories", "RecordId");
            AddForeignKey("dbo.SetAsideRecords", "RecordId", "dbo.Records", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SetAsideRecords", "ClientId", "dbo.Clients", "AccountId", cascadeDelete: true);
        }
    }
}
