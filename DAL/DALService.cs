using System;
using System.Collections.Generic;
using System.Data.Entity;

using System.Linq;

namespace DAL
{
    public class DALService
    {
        private UnitOfWork unitOfWork;
        public DALService()
        {
            unitOfWork = new UnitOfWork();
        }


     

        public Account GetAccount(Account account)
        {
            return unitOfWork.AccountRepository.Get().FirstOrDefault(a => a.Login == account.Login && a.Password == account.Password);
        }
        public IEnumerable<Account> GetAccounts()
        {
            return unitOfWork.AccountRepository.Get();
        }
        public IEnumerable<Client> GetClients()
        {
            return unitOfWork.ClientRepository.Get();
        }
        public IEnumerable<Artist> GetArtists()
        {
            return unitOfWork.ArtistRepository.Get();
        }
        public IEnumerable<Deal> GetDeals()
        {
            return unitOfWork.DealRepository.Get();
        }
        public IEnumerable<DiscardedRecord> GetDiscardedRecords()
        {
            return unitOfWork.DiscardedRecordRepository.Get();
        }
        public IEnumerable<Genre> GetGenres()
        {
            return unitOfWork.GenreRepository.Get();
        }
        public IEnumerable<Publisher> GetPublishers()
        {
            return unitOfWork.PublisherRepository.Get();
        }
        public IEnumerable<Record> GetRecords()
        {
            return unitOfWork.RecordRepository.Get();
        }
        public IEnumerable<RemovedRecord> GetRemovedRecords()
        {
            return unitOfWork.RemovedRecordRepository.Get();
        }
        public IEnumerable<SalesHistory> GetSalesHistories()
        {
            return unitOfWork.SalesHistoryRepository.Get();
        }
        public IEnumerable<SetAsideRecord> GetSetAsideRecords()
        {
            return unitOfWork.SetAsideRecordRepository.Get();
        }

        public IEnumerable<SalesHistory> GetSalesHistories(Client client)
        {
            return unitOfWork.SalesHistoryRepository.Get(s => s.Client.AccountId == client.AccountId);// context.SalesHistories.Where(s=>s.Client.AccountId==client.AccountId);
        }
        public IEnumerable<SetAsideRecord> GetSetAsideRecords(Client client)
        {
            return unitOfWork.SetAsideRecordRepository.Get(s => s.Client.AccountId == client.AccountId);
        }



        public IQueryable<Record> SearchByArtistName(string name)
        {
            return unitOfWork.RecordRepository.context.Records.Where(p => p.Artist.Name.Contains(name));
        }
        public IQueryable<Record> SearchByName(string name)
        {
            return unitOfWork.RecordRepository.context.Records.Where(p => p.Name.Contains(name) == true);
        }
        public IQueryable<Record> SearchByGenreName(string genre)
        {
            return unitOfWork.RecordRepository.context.Records.Where(p => p.Genre.Name.Contains(genre) == true);
        }


        public float CalculateDiscount(Client client)
        {
            if (unitOfWork.SalesHistoryRepository.GetByID(client.AccountId) != null)
            {
                double total = unitOfWork.SalesHistoryRepository.context.SalesHistories.Where(sh => sh.ClientEmail == client.Email && sh.Client.Discount!=null).Sum(sh => (double?)sh.DiscountPrice)??0;
 
                if (total == 0)
                    return 0;
                else if (total >= 1000 && total < 5000)
                {
                    return (int)total / 1000;
                }
                else if (total < 11000)
                {
                    return (int)total / 1000;
                }
                else if (total < 21000)
                {
                    return 15;
                }
                else if (total < 50000)
                {
                    return 20;
                }
                else
                    return 25;
            }
            return 0;
        }
        public void AddArtist(Artist artist)
        {
            if (unitOfWork.ArtistRepository.context.Artists.FirstOrDefault(a => a.Name == artist.Name) == null)
            {
                unitOfWork.ArtistRepository.context.Artists.Add(artist);
                unitOfWork.Save();
            }
        }
        public void AddPublisher(Publisher publisher)
        {
            if (unitOfWork.PublisherRepository.context.Publishers.FirstOrDefault(p => p.Name == publisher.Name) == null)
            {
                unitOfWork.PublisherRepository.context.Publishers.Add(publisher);
                unitOfWork.Save();
            }
        }
        public void AddGenre(Genre genre)
        {
            if (unitOfWork.GenreRepository.context.Genres.FirstOrDefault(g => g.Name == genre.Name) == null)
            {
                unitOfWork.GenreRepository.context.Genres.Add(new Genre() { Name = genre.Name });
                unitOfWork.Save();
            }
        }
        public void AddAdmin(string login, string password)
        {
            if (unitOfWork.AccountRepository.context.Accounts.FirstOrDefault(a => a.Login == login && a.Password == password) == null)
            {
                unitOfWork.AccountRepository.context.Accounts.Add(new Account() { Login = login, Password = password, IsClient = false });
                unitOfWork.Save();
            }
        }
        public void AddClient(string login, string password, Client client)
        {
            if (unitOfWork.ClientRepository.context.Clients.FirstOrDefault(c => c.Email == client.Email && c.Account.Login == login && c.Account.Password == password) == null)
            {

                Account account = new Account()
                {
                    Login = login,
                    Password = password,
                    IsClient = true
                };
                unitOfWork.AccountRepository.context.Accounts.Add(account);
                unitOfWork.Save();
                Client client1 = new Client()
                {
                    AccountId = account.Id,
                    Name = client.Name,
                    Surname = client.Surname,
                    Email = client.Email,
                    Phone = client.Phone

                };
                unitOfWork.ClientRepository.context.Clients.Add(client1);
                account.ClientId = client1.AccountId;
                unitOfWork.Save();
                client.Discount = CalculateDiscount(client);
                unitOfWork.Save();

            }
        }

        public void AddRecord(Record record)
        {
            if (unitOfWork.RecordRepository.Get().FirstOrDefault(p => p.Name == record.Name && p.Artist.Name == record.Artist.Name) == null)
            {
                unitOfWork.RecordRepository.context.Records.Add(record);
                unitOfWork.Save();
            }
        }

        public void DiscardRecord(Record record)
        {
            if (unitOfWork.RecordRepository.GetByID( record.Id) != null)
            {
                unitOfWork.RecordRepository.context.DiscardedRecords.Add(new DiscardedRecord()
                {
                    Name = record.Name,
                    Publisher = record.Publisher,
                    Genre = record.Genre,
                    CostPrice = record.CostPrice,
                    CountSongs = record.CountSongs,
                    DateOfPublishing = record.DateOfPublishing,
                    Artist = record.Artist,
                    SellingPrice = record.SellingPrice
                });
                unitOfWork.Save();
            }
        }
        public void DeleteRecord(Record record)
        {
            if (unitOfWork.RecordRepository.GetByID(record.Id) != null)
            {
                unitOfWork.RemovedRecordRepository.Insert(new RemovedRecord()
                {
                    Name = record.Name,
                    Publisher = record.Publisher,
                    Genre = record.Genre,
                    CostPrice = record.CostPrice,
                    CountSongs = record.CountSongs,
                    DateOfPublishing = record.DateOfPublishing,
                    Artist = record.Artist,
                    SellingPrice = record.SellingPrice
                });
                unitOfWork.Save();
                unitOfWork.RecordRepository.Delete(record.Id);
                unitOfWork.Save();
            }
        }

        public void SaleRecord(SalesHistory sale)
        {
            unitOfWork.SalesHistoryRepository.context.SalesHistories.Add(sale);
            unitOfWork.Save();
        }
        public void SetAsideRecord(SetAsideRecord setAside)
        {

            unitOfWork.SetAsideRecordRepository.context.SetAsideRecords.Add(setAside);
            unitOfWork.Save();
        }
        public void AddDeal(Deal deal)
        {
            if (unitOfWork.DealRepository.Get().Where(d => d.Name == deal.Name && d.StartDate == deal.StartDate && d.EndDate == deal.EndDate).FirstOrDefault() == null)
            {
                unitOfWork.DealRepository.context.Deals.Add(deal);
                unitOfWork.Save();
            }
        }
        public void AddRecordToDeal(Deal deal, Record record)
        {
            if (record.DealId == null)
            {

                unitOfWork.RecordRepository.GetByID(record.Id).Deal = deal;

                unitOfWork.Save();

            }
        }

        public void EditRecord(Record record)
        {
            unitOfWork.RecordRepository.Update(record);
            unitOfWork.Save();
        }
        public void EditDiscountClient(Client client,float dicount)
        {
            unitOfWork.ClientRepository.GetByID(client.AccountId).Discount = dicount;
            unitOfWork.Save();
        }
        public IEnumerable<Record> NewRecords(int days = 1, int numberOfElemTake = 5)
        {
            if (days < 0)
                throw new ArgumentOutOfRangeException("Days can't be less than 0");
            if (numberOfElemTake < 1)
                throw new ArgumentOutOfRangeException("Number of element that we take can't be less than 1");
            DateTime fdt = DateTime.Now.AddDays(days * (-1));
            return unitOfWork.RecordRepository.Get().OrderByDescending(p => p.DateOfPublishing)
                                                              .Where(p => p.DateOfPublishing < DateTime.Now && p.DateOfPublishing >= fdt)
                                                              .Take(numberOfElemTake);
        }
        public IEnumerable<Genre> TopGenres(int days = 1, int numberOfElemTake = 5)
        {
            if (days < 0)
                throw new ArgumentOutOfRangeException("Days can't be less than 0");
            if (numberOfElemTake < 1)
                throw new ArgumentOutOfRangeException("Number of element that we take can't be less than 1");


            DateTime fdt = DateTime.Now.AddDays(days * (-1));
            return unitOfWork.SalesHistoryRepository.Get().Where(s => s.Record != null && s.Date < DateTime.Now && s.Date >= fdt)
                                                          .GroupBy(s => s.Record.Genre).Select(s => new { Genre = s.Key, Total = s.Count() })
                                                          .OrderByDescending(s => s.Total)
                                                          .Select(s => s.Genre)
                                                          .Take(numberOfElemTake);


        }
        public IEnumerable<Record> TopSales(int days = 1, int numberOfElemTake = 5)
        {
            if (days < 0)
                throw new ArgumentOutOfRangeException("Days can't be less than 0");
            if (numberOfElemTake < 1)
                throw new ArgumentOutOfRangeException("Number of element that we take can't be less than 1");

            DateTime fdt = DateTime.Now.AddDays(days * (-1));

            return unitOfWork.SalesHistoryRepository.Get().Where(s =>s.Record!=null&& s.Date < DateTime.Now && s.Date >= fdt)
                                                                            .GroupBy(s => s.Record)
                                                                            .Select(s => new { Record = s.Key, Total = s.Count() })
                                                                            .OrderByDescending(s => s.Total)
                                                                            .Select(s => s.Record)
                                                                            .Take(numberOfElemTake);
        }
        public IEnumerable<Artist> TopArtists(int days = 1, int numberOfElemTake = 5)
        {
            if (days < 0)
                throw new ArgumentOutOfRangeException("Days can't be less than 0");
            if (numberOfElemTake < 1)
                throw new ArgumentOutOfRangeException("Number of element that we take can't be less than 1");

            DateTime fdt = DateTime.Now.AddDays(days * (-1));
            return unitOfWork.SalesHistoryRepository.Get().Where(s =>s.Record!=null&& s.Date < DateTime.Now && s.Date >= fdt)
                                                           .GroupBy(s => s.Record.Artist)
                                                           .Select(s => new { Artist = s.Key, Total = s.Count() })
                                                           .OrderByDescending(s => s.Total).Select(s => s.Artist)
                                                           .Take(numberOfElemTake);


        }


    }
}
