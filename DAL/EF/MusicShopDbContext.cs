namespace DAL
{
    using DAL.Configurations;
    using System.Data.Entity;
    using System.Linq;

    public class MusicShopDbContext : DbContext
    {

        public MusicShopDbContext()
            : base("name=MusicShopDbContext")
        {
            Database.SetInitializer(new Initializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ArtistConfig());
            modelBuilder.Configurations.Add(new ClientConfig());
            modelBuilder.Configurations.Add(new GenreConfig());
            modelBuilder.Configurations.Add(new RecordsConfig());
            modelBuilder.Configurations.Add(new DealConfig());
            modelBuilder.Configurations.Add(new PublisherConfig());
            modelBuilder.Configurations.Add(new SalesHistoryConfig());
            modelBuilder.Configurations.Add(new SetAsideRecordConfig());
            modelBuilder.Configurations.Add(new RemovedRecordConfig());
            modelBuilder.Configurations.Add(new AccountConfig());
            
        }


        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Client>Clients { get; set; }
        public virtual DbSet<DiscardedRecord> DiscardedRecords { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Deal> Deals { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Record>Records { get; set; }
        public virtual DbSet<SalesHistory> SalesHistories { get; set; }
        public virtual DbSet<RemovedRecord> RemovedRecords { get; set; }
        public virtual DbSet<SetAsideRecord> SetAsideRecords { get; set; }
    }
}