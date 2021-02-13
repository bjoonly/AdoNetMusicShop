using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class UnitOfWork : IDisposable
    {
        private MusicShopDbContext ctx = new MusicShopDbContext();
        private GenericRepository<Account> accountRepository;
        private GenericRepository<Artist> artistRepository;
        private GenericRepository<Client> clientRepository;
        private GenericRepository<DiscardedRecord> discardedRecordRepository;
        private GenericRepository<Genre> genreRepository;

        private GenericRepository<Deal> dealRepository;
        private GenericRepository<Publisher> publisherRepository;
        private GenericRepository<Record> recordRepository;
        private GenericRepository<RemovedRecord> removedRecordRepository;
        private GenericRepository<SalesHistory> salesHistoryRepository;
        private GenericRepository<SetAsideRecord> setAsideRecordRepository;


       
        public GenericRepository<Account> AccountRepository
        {
            get
            {
                if (accountRepository == null)
                    accountRepository = new GenericRepository<Account>(ctx);
                return accountRepository;
                
            }
        }

        public GenericRepository<Artist> ArtistRepository
        {
           
            get
            {
                if (artistRepository == null)
                    artistRepository = new GenericRepository<Artist>(ctx);
                return artistRepository;
            }
        }

        public GenericRepository<Client> ClientRepository
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new GenericRepository<Client>(ctx);
                return clientRepository;
            }
        }

        public GenericRepository<DiscardedRecord> DiscardedRecordRepository
        {
            get
            {
                if (discardedRecordRepository == null)
                    discardedRecordRepository = new GenericRepository<DiscardedRecord>(ctx);
                return discardedRecordRepository;
            }
        }

        public GenericRepository<Genre> GenreRepository
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenericRepository<Genre>(ctx);
                return genreRepository;
            }
        }

        public GenericRepository<Deal> DealRepository
        {
            get
            {
                if (dealRepository == null)
                    dealRepository = new GenericRepository<Deal>(ctx);
                return dealRepository;
            }
        }

        public GenericRepository<Publisher> PublisherRepository
        {
            get
            {
                if (publisherRepository == null)
                    publisherRepository = new GenericRepository<Publisher>(ctx);
                return publisherRepository;
            }
        }


        public GenericRepository<Record> RecordRepository
        {
            get
            {
                if (recordRepository == null)
                    recordRepository = new GenericRepository<Record>(ctx);
                return recordRepository;
            }
        }

        public GenericRepository<RemovedRecord> RemovedRecordRepository
        {
            get
            {
                if (removedRecordRepository == null)
                    removedRecordRepository = new GenericRepository<RemovedRecord>(ctx);
                return removedRecordRepository;
            }
        }
        public GenericRepository<SalesHistory> SalesHistoryRepository
        {
            get
            {
                if (salesHistoryRepository == null)
                    salesHistoryRepository = new GenericRepository<SalesHistory>(ctx);
                return salesHistoryRepository;
            }
        }
        public GenericRepository<SetAsideRecord> SetAsideRecordRepository
        {
            get
            {
                if (setAsideRecordRepository == null)
                    setAsideRecordRepository = new GenericRepository<SetAsideRecord>(ctx);
                return setAsideRecordRepository;
            }
        }
        public void Save()
        {
            ctx.SaveChanges();
            
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ctx.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
