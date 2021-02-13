namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false),
                        IsClient = c.Boolean(nullable: false),
                        ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        AccountId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Surname = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 100),
                        Phone = c.String(),
                        Discount = c.Single(),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.SalesHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(),
                        ClientEmail = c.String(),
                        SellingPrice = c.Double(nullable: false),
                        DiscountPrice = c.Double(nullable: false),
                        Discount = c.Single(nullable: false),
                        Date = c.DateTime(nullable: false),
                        RecordId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Records", t => t.RecordId)
                .Index(t => t.ClientId)
                .Index(t => t.RecordId);
            
            CreateTable(
                "dbo.Records",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ArtistId = c.Int(nullable: false),
                        PublisherId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                        DateOfPublishing = c.DateTime(nullable: false),
                        CountSongs = c.Int(nullable: false),
                        CostPrice = c.Double(nullable: false),
                        SellingPrice = c.Double(nullable: false),
                        DealId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.ArtistId)
                .ForeignKey("dbo.Deals", t => t.DealId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .ForeignKey("dbo.Publishers", t => t.PublisherId)
                .Index(t => t.ArtistId)
                .Index(t => t.PublisherId)
                .Index(t => t.GenreId)
                .Index(t => t.DealId);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DiscardedRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ArtistId = c.Int(nullable: false),
                        PublisherId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                        DateOfPublishing = c.DateTime(nullable: false),
                        CountSongs = c.Int(nullable: false),
                        CostPrice = c.Double(nullable: false),
                        SellingPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Publishers", t => t.PublisherId, cascadeDelete: true)
                .Index(t => t.ArtistId)
                .Index(t => t.PublisherId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RemovedRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ArtistId = c.Int(nullable: false),
                        PublisherId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                        DateOfPublishing = c.DateTime(nullable: false),
                        CountSongs = c.Int(nullable: false),
                        CostPrice = c.Double(nullable: false),
                        SellingPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.ArtistId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .ForeignKey("dbo.Publishers", t => t.PublisherId)
                .Index(t => t.ArtistId)
                .Index(t => t.PublisherId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Deals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Discount = c.Single(nullable: false),
                        Discription = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SetAsideRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecordId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Records", t => t.RecordId, cascadeDelete: true)
                .Index(t => t.RecordId)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesHistories", "RecordId", "dbo.Records");
            DropForeignKey("dbo.SetAsideRecords", "RecordId", "dbo.Records");
            DropForeignKey("dbo.SetAsideRecords", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Records", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.Records", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Records", "DealId", "dbo.Deals");
            DropForeignKey("dbo.Records", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.RemovedRecords", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.DiscardedRecords", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.RemovedRecords", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.RemovedRecords", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.DiscardedRecords", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.DiscardedRecords", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.SalesHistories", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "AccountId", "dbo.Accounts");
            DropIndex("dbo.SetAsideRecords", new[] { "ClientId" });
            DropIndex("dbo.SetAsideRecords", new[] { "RecordId" });
            DropIndex("dbo.RemovedRecords", new[] { "GenreId" });
            DropIndex("dbo.RemovedRecords", new[] { "PublisherId" });
            DropIndex("dbo.RemovedRecords", new[] { "ArtistId" });
            DropIndex("dbo.DiscardedRecords", new[] { "GenreId" });
            DropIndex("dbo.DiscardedRecords", new[] { "PublisherId" });
            DropIndex("dbo.DiscardedRecords", new[] { "ArtistId" });
            DropIndex("dbo.Records", new[] { "DealId" });
            DropIndex("dbo.Records", new[] { "GenreId" });
            DropIndex("dbo.Records", new[] { "PublisherId" });
            DropIndex("dbo.Records", new[] { "ArtistId" });
            DropIndex("dbo.SalesHistories", new[] { "RecordId" });
            DropIndex("dbo.SalesHistories", new[] { "ClientId" });
            DropIndex("dbo.Clients", new[] { "AccountId" });
            DropTable("dbo.SetAsideRecords");
            DropTable("dbo.Deals");
            DropTable("dbo.Publishers");
            DropTable("dbo.RemovedRecords");
            DropTable("dbo.Genres");
            DropTable("dbo.DiscardedRecords");
            DropTable("dbo.Artists");
            DropTable("dbo.Records");
            DropTable("dbo.SalesHistories");
            DropTable("dbo.Clients");
            DropTable("dbo.Accounts");
        }
    }
}
