namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setDiscountSalesHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesHistories", "DiscountDeal", c => c.Single(nullable: false));
            AddColumn("dbo.SalesHistories", "DiscountClient", c => c.Single(nullable: false));
            DropColumn("dbo.SalesHistories", "Discount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalesHistories", "Discount", c => c.Single(nullable: false));
            DropColumn("dbo.SalesHistories", "DiscountClient");
            DropColumn("dbo.SalesHistories", "DiscountDeal");
        }
    }
}
