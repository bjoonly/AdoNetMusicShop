namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setDealProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deals", "Description", c => c.String());
            DropColumn("dbo.Deals", "Discription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deals", "Discription", c => c.String());
            DropColumn("dbo.Deals", "Description");
        }
    }
}
