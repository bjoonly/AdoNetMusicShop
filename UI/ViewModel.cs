using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ubiety.Dns.Core;

namespace UI
{
    class ViewModel : INotifyPropertyChanged
    {
        DALService DALService;

        LoginRegistrationWindow w1;

        public ViewModel()
        {
            DALService = new DALService();
            loginCommand = new DelegateCommand(Login);
            registrationCommand = new DelegateCommand(Registration);

            loginRegistrationShowCommand = new DelegateCommand(ShowLoginRegistrationForm, () =>!Accounts.Any(a=>a.Login==currentAccount.Login&&a.Password==currentAccount.Password));
            exitCommand = new DelegateCommand(Exit, () => Accounts.Any(a => a.Login == currentAccount.Login && a.Password == currentAccount.Password));

            addArtistCommand = new DelegateCommand(AddArtist, () => CurrentAccount.IsClient == false &&
                                                                    Artist.Name != "" &&
                                                                    !Artists.Any(a => a.Name == Artist.Name));
            addGenreCommand = new DelegateCommand(AddGenre, () => Accounts.Contains(currentAccount) &&
                                                                  CurrentAccount.IsClient == false );
            addPublisherCommand = new DelegateCommand(AddPublisher, () => Accounts.Contains(currentAccount) && CurrentAccount.IsClient == false);

            addRecordCommand = new DelegateCommand(AddRecord ) ;
            editRecordCommand = new DelegateCommand(EditRecord, () => CurrentAccount.IsClient == false &&
                                                                      SelectedRecord != null);
            removeRecordCommand = new DelegateCommand(RemoveRecord, () => SelectedRecord != null && 
                                                                          CurrentAccount.IsClient == false);
            discardRecordCommand = new DelegateCommand(DiscardRecord, () => Accounts.Contains(currentAccount) && CurrentAccount.IsClient == false && SelectedRecord != null);

            sellRecordCommand = new DelegateCommand(Sell, () => 
                                                                CurrentAccount.IsClient == false &&                                                               
                                                                SelectedRecord!=null && SelectedRecord.Name!=""&&
                                                                Date.Date == DateTime.Now.Date && Total>0);
            setAsideCommand = new DelegateCommand(SetAside, () => Accounts.Contains(currentAccount) &&
                                                                  CurrentAccount.IsClient == false &&
                                                                  Client != null && Client.Email!=""&&
                                                                  SelectedRecord!=null&&                                                                  
                                                                  Date.Date >= DateTime.Now.Date);

            createDealCommand = new DelegateCommand(CreateDeal, () => CurrentAccount.IsClient == false );
            addRecordToDealCommand = new DelegateCommand(AddRecordToDeal,()=>CurrentAccount.IsClient == false &&
                                                                              Deal!=null && SelectedRecord!=null);

            searchCommand = new DelegateCommand(Search,()=> Accounts.Any(a => a.Login == currentAccount.Login && a.Password == currentAccount.Password) &&
                                                           !String.IsNullOrWhiteSpace(SearchBy) &&
                                                          (!String.IsNullOrWhiteSpace(SearchLine)||SearchBy=="All"));
            showCommand = new DelegateCommand(Show,()=> Accounts.Any(a => a.Login == currentAccount.Login && a.Password == currentAccount.Password) &&
                                                        !String.IsNullOrWhiteSpace(TopFor));

            searchByList.Add("All");
            searchByList.Add("Name");
            searchByList.Add(nameof(Artist));
            searchByList.Add(nameof(Genre));

            topForTheList.Add("Day");
            topForTheList.Add("Week");
            topForTheList.Add("Month");
            topForTheList.Add("Year");

            TopFor = "Day";


            selectedRecord = new Record() { DateOfPublishing = DateTime.Now };
            artist = new Artist();
            genre = new Genre();
            publisher = new Publisher();
            deal = new Deal() { StartDate = DateTime.Now, EndDate = DateTime.Now };
            currentAccount = new Account() { Login = "", Password = "", IsClient = true };
            currentClient = new Client() { Account = currentAccount, Name = "", Surname = "", Phone = "", Email = "", Discount = 0 };
            client = new Client() { Account = new Account() { Login = "", Password = "" }, Name = "", Surname = "", Phone = "", Email = "", Discount = 0 };

            FillAccounts();
            FillClients();
           

            PropertyChanged += (sender, args) =>
            {
               

                 if (args.PropertyName == nameof(CurrentAccount))
                {
                    loginCommand.RaiseCanExecuteChanged();
                    registrationCommand.RaiseCanExecuteChanged();
                    loginRegistrationShowCommand.RaiseCanExecuteChanged();
                    exitCommand.RaiseCanExecuteChanged();
                    addArtistCommand.RaiseCanExecuteChanged();

                    addGenreCommand.RaiseCanExecuteChanged();
                    addPublisherCommand.RaiseCanExecuteChanged();
                    addRecordCommand.RaiseCanExecuteChanged();
                    addRecordToDealCommand.RaiseCanExecuteChanged();
                    editRecordCommand.RaiseCanExecuteChanged();

                    removeRecordCommand.RaiseCanExecuteChanged();
                    sellRecordCommand.RaiseCanExecuteChanged();
                    setAsideCommand.RaiseCanExecuteChanged();
                    showCommand.RaiseCanExecuteChanged();
                    searchCommand.RaiseCanExecuteChanged();

                    createDealCommand.RaiseCanExecuteChanged();
                    discardRecordCommand.RaiseCanExecuteChanged();
                }
                else if (args.PropertyName == nameof(CurrentClient))
                {
                    loginCommand.RaiseCanExecuteChanged();
                    registrationCommand.RaiseCanExecuteChanged();
                    loginRegistrationShowCommand.RaiseCanExecuteChanged();
                    exitCommand.RaiseCanExecuteChanged();
                }
                else if (args.PropertyName == nameof(Artist))
                {
                    addArtistCommand.RaiseCanExecuteChanged();
                    addRecordCommand.RaiseCanExecuteChanged();
                }
                else if (args.PropertyName == nameof(Genre))
                {
                    addGenreCommand.RaiseCanExecuteChanged();
                    addRecordCommand.RaiseCanExecuteChanged();
                }
                else if (args.PropertyName == nameof(Publisher))
                {
                    addRecordCommand.RaiseCanExecuteChanged();
                    addPublisherCommand.RaiseCanExecuteChanged();
                }
                else if (args.PropertyName == nameof(SelectedRecord))
                {
                    addRecordCommand.RaiseCanExecuteChanged();
                    addRecordToDealCommand.RaiseCanExecuteChanged();
                    editRecordCommand.RaiseCanExecuteChanged();
                    discardRecordCommand.RaiseCanExecuteChanged();
                    removeRecordCommand.RaiseCanExecuteChanged();
                    sellRecordCommand.RaiseCanExecuteChanged();
                    setAsideCommand.RaiseCanExecuteChanged();
                    CalculateTotal();

                }
                else if (args.PropertyName == nameof(Deal))
                {
                    createDealCommand.RaiseCanExecuteChanged();
                    addRecordToDealCommand.RaiseCanExecuteChanged();
                    CalculateTotal();
                }
                else if (args.PropertyName == nameof(Date))
                {
                    sellRecordCommand.RaiseCanExecuteChanged();
                    setAsideCommand.RaiseCanExecuteChanged();
                }
                else if (args.PropertyName == nameof(SearchBy))
                {
                    searchCommand.RaiseCanExecuteChanged();
                }
                else if (args.PropertyName == nameof(SearchLine))
                {
                    searchCommand.RaiseCanExecuteChanged();
                }
                else if (args.PropertyName == nameof(TopFor)){
                    showCommand.RaiseCanExecuteChanged();
                }
                else if (args.PropertyName == nameof(Client) )
                {
                   
                    sellRecordCommand.RaiseCanExecuteChanged();
                    setAsideCommand.RaiseCanExecuteChanged();
                    CalculateTotal();
                }

                 else if (args.PropertyName == nameof(Total))
                {
                    sellRecordCommand.RaiseCanExecuteChanged();
                }
               
            };
           
        }

        #region
        //try
        //{
        //}
        //catch (DbEntityValidationException ex)
        //{
        //    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
        //    {
        //        MessageBox.Show(validationError.Entry.Entity.ToString());
        //        foreach (DbValidationError err in validationError.ValidationErrors)
        //        {
        //            MessageBox.Show(err.ErrorMessage + "");
        //        }
        //    }
        //}
        //catch (Exception ex1)
        //{
        //    MessageBox.Show(ex1.Message);
        //}
        #endregion

        #region prop

        private Account currentAccount;
        public Account CurrentAccount
        {
            get { return currentAccount; }
            set { if (value != currentAccount) { currentAccount = value; if (currentAccount == null) { currentAccount = new Account() { Login = "", Password = "", IsClient = true };  }  OnPropertyChanged(); } }
        }

        private Client currentClient;
        public Client CurrentClient
        {
            get { return currentClient; }
            set { if (value != currentClient)
                {
                    currentClient = value;
                    if (currentClient == null)
                    {
                        currentAccount = new Account() { Login = "", Password = "", IsClient = true };
                        currentClient = new Client() { Account = currentAccount, Name = "", Surname = "", Phone = "", Email = "", Discount = 0 };
                        OnPropertyChanged();
                    }
                }
            }
        }

        private Artist artist;
        public Artist Artist
        {
            get { return artist; }
            set { if (value != artist) { artist = value; OnPropertyChanged(); } }
        }

        private Genre genre;
        public Genre Genre
        {
            get { return genre; }
            set { if (value != genre) { genre = value; OnPropertyChanged(); } }
        }

        public Publisher publisher;
        public Publisher Publisher
        {
            get { return publisher; }
            set { if (value != publisher) { publisher = value; OnPropertyChanged(); }  }
        }

        private Record selectedRecord;
        public Record SelectedRecord
        {
            get { return selectedRecord; }
            set { if (value != selectedRecord) { selectedRecord = value; Date = DateTime.Now;   if (selectedRecord == null) selectedRecord = new Record() { DateOfPublishing = DateTime.Now }; OnPropertyChanged(); } }
        
        }

        private Deal deal;
        public Deal Deal
        {
            get { return deal; }
            set { if (value != deal) { deal = value; if (deal == null) deal = new Deal(); OnPropertyChanged(); } }
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { if (date != value) { date = value;  OnPropertyChanged(); } }
        }
        

        private string searchBy;
        public string SearchBy
        {
            get { return searchBy; }
            set { if (value != searchBy) { searchBy = value; OnPropertyChanged(); } }
        }

        private string topFor;
        public string TopFor
        {
            get { return topFor; }
            set { if (value != topFor) { topFor = value; OnPropertyChanged(); } }
        }


        private string searchLine;
        public string SearchLine
        {
            get { return searchLine; }
            set { if (value != searchLine) { searchLine = value; OnPropertyChanged(); } }
        }

        private Client client;
        public Client Client
        {
            get { return client; }
            set { if (value != client) { client = value; OnPropertyChanged(); } }
        }

        private string emailClient;
        public string EmailClient
        {
            get { return emailClient; }
            set { if (value != emailClient) { emailClient = value; OnPropertyChanged(); } }
        }

        private double total;
        public double Total
        {
            get { return total; }
            set { if (value != total) { total = value; OnPropertyChanged(); } }
        }
        #endregion
        #region commands
        //_________________________________________________COMMANDS_________________________________________
        private Command loginCommand;
        private Command registrationCommand;
        private Command loginRegistrationShowCommand;
        private Command exitCommand;

        private Command addArtistCommand;
        private Command addGenreCommand;
        private Command addPublisherCommand;

        private Command addRecordCommand;
        private Command removeRecordCommand;
        private Command discardRecordCommand;
        private Command editRecordCommand;

        private Command sellRecordCommand;
        private Command setAsideCommand;

        private Command createDealCommand;
        private Command addRecordToDealCommand;

        private Command searchCommand;
        private Command showCommand;


        public ICommand LoginCommand => loginCommand;
        public ICommand LoginRegistrationShowCommand => loginRegistrationShowCommand;
        public ICommand RegistrationCommand => registrationCommand;
        public ICommand ExitCommand => exitCommand;


        public ICommand AddArtistCommand => addArtistCommand;
        public ICommand AddGenreCommand => addGenreCommand;
        public ICommand AddPublisherCommand => addPublisherCommand;
        public ICommand EditRecordCommand => editRecordCommand;

        public ICommand AddRecordCommand => addRecordCommand;
        public ICommand RemoveRecordCommand => removeRecordCommand;
        public ICommand DiscardRecordCommand => discardRecordCommand;

        public ICommand SellRecordCommand => sellRecordCommand;
        public ICommand SetAsideCommand => setAsideCommand;

        public ICommand CreateDealCommand => createDealCommand;
        public ICommand AddRecordToDealCommand => addRecordToDealCommand;

        public ICommand SearchCommand => searchCommand;
        public ICommand ShowCommand => showCommand;

        #endregion
        #region collections
        //_________________________________________________COLLECTIONS_________________________________________

        private readonly ICollection<Account> accounts = new ObservableCollection<Account>();
        private readonly ICollection<Client> clients = new ObservableCollection<Client>();

        private readonly ICollection<Artist> artists = new ObservableCollection<Artist>();
        private readonly ICollection<Genre> genres = new ObservableCollection<Genre>();
        private readonly ICollection<Publisher> publishers = new ObservableCollection<Publisher>();
        private readonly ICollection<Record> records = new ObservableCollection<Record>();
        private readonly ICollection<RemovedRecord> removedRecords = new ObservableCollection<RemovedRecord>();
        private readonly ICollection<DiscardedRecord> discardedRecords = new ObservableCollection<DiscardedRecord>();
        private readonly ICollection<Deal> deals = new ObservableCollection<Deal>();
        private readonly ICollection<SalesHistory> salesHistories = new ObservableCollection<SalesHistory>();
        private readonly ICollection<SetAsideRecord> setAsideRecords = new ObservableCollection<SetAsideRecord>();

        private readonly ICollection<string> searchByList = new ObservableCollection<string>();
        private readonly ICollection<string> topForTheList = new ObservableCollection<string>();

        private readonly ICollection<Record> newRecords = new ObservableCollection<Record>();
        private readonly ICollection<Record> topSellingdRecords = new ObservableCollection<Record>();
        private readonly ICollection<Artist> topArtists = new ObservableCollection<Artist>();
        private readonly ICollection<Genre> topGenres = new ObservableCollection<Genre>();



        public IEnumerable<Account> Accounts => accounts;
        public IEnumerable<Client> Clients => clients;

        public IEnumerable<Artist> Artists => artists;
        public IEnumerable<Genre> Genres => genres;
        public IEnumerable<Publisher> Publishers => publishers;
        public IEnumerable<Record> Records => records;
        public IEnumerable<RemovedRecord> RemovedRecords => removedRecords;
        public IEnumerable<DiscardedRecord> DiscardedRecords => discardedRecords;
        public IEnumerable<Deal> Deals => deals;
        public IEnumerable<SalesHistory> SalesHistories => salesHistories;
        public IEnumerable<SetAsideRecord> SetAsideRecords => setAsideRecords;


        public IEnumerable<string> SearchByList => searchByList;
        public IEnumerable<string> TopForTheList => topForTheList;


        public IEnumerable<Record> NewRecords => newRecords;
        public IEnumerable<Record> TopSellingdRecords => topSellingdRecords;
        public IEnumerable<Artist> TopArtists => topArtists;
        public IEnumerable<Genre> TopGenres => topGenres;

        #endregion



        //_________________________________________________METHODS_________________________________________

        public void ShowLoginRegistrationForm()
        {
            try
            {
                w1 = new LoginRegistrationWindow();

                currentAccount = new Account() { Login = "", Password = "" };
                currentClient = new Client() { Account = currentAccount, Name = "", Surname = "", Phone = "", Email = "", Discount = 0 };
                w1.DataContext = this;

                w1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Registration()
        {
            currentAccount.Password = ComputeSha256Hash(currentAccount.Password);
            DALService.AddClient(currentAccount.Login, currentAccount.Password, currentClient);
            accounts.Add(CurrentAccount);
            clients.Add(CurrentClient);
            CurrentAccount = DALService.GetAccount(currentAccount);
            CurrentClient = currentAccount.Client;

            if (DALService.GetAccount(currentAccount) != null)
            {
                CurrentAccount = DALService.GetAccount(currentAccount);

                w1.Close();
                FillRecords();
                FillDeals();
                FillArtists();
                FillGenres();
                FillPublishers();
                if (currentAccount.IsClient)
                {
                    CurrentClient = currentAccount.Client;
                    FillSalesForClient();
                    FillSetAsideRecordsForClient();
                }
                else
                {

                    FillRemovedRecords();
                    FillDiscardedRecords();
                    FillSalesHistories();
                    FillSetAsideRecords();
                }
                Show();
            }
            w1.Close();
            OnPropertyChanged(nameof(currentAccount));
            OnPropertyChanged(nameof(CurrentClient));
        }
        public void Exit()
        {
                CurrentAccount = null;
                CurrentClient = null;
            
            artists.Clear();
            genres.Clear();
            publishers.Clear();
            records.Clear();
            deals.Clear();
            newRecords.Clear();
            topArtists.Clear();
            topGenres.Clear();
            salesHistories.Clear();
            setAsideRecords.Clear();
            topSellingdRecords.Clear();

        }
        public void Login()
        {
            try
            {
                currentAccount.Password = ComputeSha256Hash(currentAccount.Password);
                if (DALService.GetAccount(currentAccount) != null)
                {
                    CurrentAccount = DALService.GetAccount(currentAccount);
                   
                    w1.Close();
                    FillRecords();
                    FillDeals();
                    FillArtists();
                    FillGenres();
                    FillPublishers();
                    if (currentAccount.IsClient)
                    {
                        CurrentClient = currentAccount.Client;
                        FillSalesForClient();
                        FillSetAsideRecordsForClient();
                    }
                    else
                    {

                        FillRemovedRecords();
                        FillDiscardedRecords();
                        FillSalesHistories();
                        FillSetAsideRecords();
                    }
                    Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        public void AddPublisher()
        {
            if (publishers.Where(p => p.Name == publisher.Name).FirstOrDefault() == null)
            {
                DALService.AddPublisher(publisher);
                publishers.Add(publisher);
                Publisher = new Publisher();
            }
        }
        public void AddGenre()
        {

            if (genres.Where(g => g.Name == genre.Name).FirstOrDefault() == null)
            {
                DALService.AddGenre(genre);
                genres.Add(genre);
                Genre = new Genre();
            }

        }
        public void AddArtist()
        {
            if (artists.Where(a => a.Name == artist.Name).FirstOrDefault() == null)
            {
                DALService.AddArtist(artist);
                artists.Add(artist);
                Artist = new Artist();
            }
        }

        public void AddRecord()
        {
            if (SelectedRecord!=null && records.FirstOrDefault(r => r.Name == selectedRecord.Name && r.Artist.Name == artist.Name) == null)
            {
                DALService.AddRecord(selectedRecord);
                records.Add(selectedRecord);
                SelectedRecord = new Record();

            }
        }
        public void EditRecord()
        {
            DALService.EditRecord(selectedRecord);
            Show();
        }
        public void AddRemoveRecord(Record r)
        {
            RemovedRecord rr = new RemovedRecord() { Artist = r.Artist, Genre = r.Genre, Publisher = r.Publisher, CostPrice = r.CostPrice, CountSongs = r.CountSongs, SellingPrice = r.SellingPrice, Name = r.Name, DateOfPublishing = r.DateOfPublishing };
            removedRecords.Add(rr);
        }
        public void AddDiscardedRecord(Record r)
        {
            DiscardedRecord dr = new DiscardedRecord() { Artist = r.Artist, Genre = r.Genre, Publisher = r.Publisher, CostPrice = r.CostPrice, CountSongs = r.CountSongs, SellingPrice = r.SellingPrice, Name = r.Name, DateOfPublishing = r.DateOfPublishing };
            discardedRecords.Add(dr);
        }

        public void CalculateTotal()
        {
            Total = selectedRecord.SellingPrice;
            if (selectedRecord.Deal != null)
                Total -= Total / 100 * selectedRecord.Deal.Discount;
            if (Client!=null && Client.Discount != null && Client.Discount > 0)
                Total -= Total / 100 * (float)Client.Discount;
        }
        public void Sell()
        {
            try
            {
            if (SelectedRecord != null)
            {
                SalesHistory s = new SalesHistory() { Date = Date, Record = SelectedRecord, SellingPrice = SelectedRecord.SellingPrice, DiscountPrice = total };
                if (Client!=null && Client.Name != "" && Client.Discount != null)
                {
                    s.DiscountClient = (float)Client.Discount;
                    s.ClientEmail = Client.Email;
                    s.Client = Client;
                }
                if (Deal != null)
                    s.DiscountDeal = Deal.Discount;

                if (Client!=null && Client.Email == "")
                    s.ClientEmail = EmailClient;

                DALService.SaleRecord(s);
                salesHistories.Add(s);
                if (Client!=null && Client.Name != "")
                {
                    Client.Discount = DALService.CalculateDiscount(Client);
                    DALService.EditDiscountClient(Client, (float)Client.Discount);
                }
                OnPropertyChanged(nameof(Client));
                Show();
           
            }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    MessageBox.Show(validationError.Entry.Entity.ToString());
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        MessageBox.Show(err.ErrorMessage + "");
                    }
                }
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message);
            }
            }
           
        

        public void SetAside()
        {
            if (selectedRecord != null)
            {
                SetAsideRecord sa = new SetAsideRecord() { Client = Client, DateTo = Date, Record = SelectedRecord };
                DALService.SetAsideRecord(sa);
                setAsideRecords.Add(sa);
              
            }
        }

        public void CreateDeal()
        {
          
            
                if (deals.Where(d => d.Name == deal.Name && d.StartDate == deal.StartDate && d.EndDate == deal.EndDate).FirstOrDefault() == null)
                {

                    DALService.AddDeal(deal);
                    deals.Add(deal);
            

                }
           
        }
        public void AddRecordToDeal()
        {


            if (selectedRecord.Deal != Deal)
            {
                DALService.AddRecordToDeal(Deal, SelectedRecord);

                selectedRecord.DealId = deal.Id;

            }


        }


        public void RemoveRecord()
        {
            if (selectedRecord != null && selectedRecord.Name != "")
            {
                    AddRemoveRecord(SelectedRecord);
                    var r = SelectedRecord;
                    records.Remove(SelectedRecord);
                   
                    DALService.DeleteRecord(r);
                    

                    SelectedRecord = new Record();
                    Show();
               
               
               
            }
            }
        public void DiscardRecord()
        {
            if (selectedRecord != null && selectedRecord.Name != "")
            {
                AddDiscardedRecord(SelectedRecord);
                DALService.DiscardRecord(SelectedRecord);
                SelectedRecord = new Record();

            }
        }
        #region search
        public void SearchByName()
        {
            records.Clear();
            foreach (var item in DALService.SearchByName(searchLine))
            {
                records.Add(item);
            }
        }
        public void SearchByArtist()
        {
            records.Clear();
            foreach (var item in DALService.SearchByArtistName(searchLine))
            {
                records.Add(item);
            }
        }
        public void SearchByGenre()
        {
            records.Clear();
            foreach (var item in DALService.SearchByGenreName(searchLine))
            {
                records.Add(item);
            }
        }
        public void Search()
        {
            if (searchBy == "All")
                FillRecords();
            else if (searchBy == "Name")
                SearchByName();
            else if (searchBy == nameof(Artist))
                SearchByArtist();
            else if (searchBy == nameof(Genre))
                SearchByGenre();
        }

        #endregion

        public void Show()
        {
            newRecords.Clear();
            topSellingdRecords.Clear();
            topArtists.Clear();
            topGenres.Clear();

            int days, elem;

            if (topFor == "Day")
            {
                days = 1;
                elem = 5;
            }

            else if (topFor == "Week")
            {
                days = 7;
                elem = 5;
            }

            else if (topFor == "Month")
            {
                days = 30;
                elem = 10;
            }

            else
            {
                days = 365;
                elem = 10;
            }

            foreach (var item in DALService.NewRecords(days, elem))
            {
                newRecords.Add(item);
            }
            foreach (var item in DALService.TopGenres(days, elem))
            {
                topGenres.Add(item);

            }
            foreach (var item in DALService.TopSales(days, elem))
            {
                topSellingdRecords.Add(item);

            }
            foreach (var item in DALService.TopArtists(days, elem))
            {
                topArtists.Add(item);

            }
        }

        #region fill
        public void FillAccounts()
        {
            accounts.Clear();
            foreach (var item in DALService.GetAccounts())
            {
                accounts.Add(item);

            }
        }
        public void FillClients()
        {
            clients.Clear();
            foreach (var item in DALService.GetClients())
            {

                DALService.EditDiscountClient(item, DALService.CalculateDiscount(item));
                clients.Add(item);
            }
        }

        public void FillArtists()
        {
            artists.Clear();
            foreach (var item in DALService.GetArtists())
            {
                artists.Add(item);

            }
        }
        public void FillGenres()
        {
            genres.Clear();
            foreach (var item in DALService.GetGenres())
            {
                genres.Add(item);
            }
        }
        public void FillPublishers()
        {
            publishers.Clear();
            foreach (var item in DALService.GetPublishers())
            {
                publishers.Add(item);
            }
        }
        public void FillRecords()
        {
            records.Clear();
            foreach (var item in DALService.GetRecords())
            {
                records.Add(item);
            }
        }
        public void FillRemovedRecords()
        {
            removedRecords.Clear();
            foreach (var item in DALService.GetRemovedRecords())
            {
                removedRecords.Add(item);
            }
        }
        public void FillDiscardedRecords()
        {
            discardedRecords.Clear();
            foreach (var item in DALService.GetDiscardedRecords())
            {
                discardedRecords.Add(item);
            }
        }
        public void FillSalesHistories()
        {
            salesHistories.Clear();
            foreach (var item in DALService.GetSalesHistories())
            {
                salesHistories.Add(item);

            }
        }
        public void FillSetAsideRecords()
        {
            setAsideRecords.Clear();
            foreach (var item in DALService.GetSetAsideRecords())
            {
                setAsideRecords.Add(item);

            }
        }
        public void FillDeals()
        {
            deals.Clear();
            foreach (var item in DALService.GetDeals())
            {
                deals.Add(item);
            }
        }


        public void FillSalesForClient()
        {
            salesHistories.Clear();
            foreach (var item in DALService.GetSalesHistories(CurrentClient).ToList())
            {
                salesHistories.Add(item);

            }
        }
        public void FillSetAsideRecordsForClient()
        {
            setAsideRecords.Clear();
            foreach (var item in DALService.GetSetAsideRecords(CurrentClient).ToList())
            {
                setAsideRecords.Add(item);

            }
        }
        #endregion

        static string ComputeSha256Hash(string rawData)
        {

            using (SHA256 sha256Hash = SHA256.Create())
            {

                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}


