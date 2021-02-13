namespace DAL
{
    using System;
    using System.Collections.Generic;

    public class Record
    {
        public Record()
        {
      
            SalesHistories = new HashSet<SalesHistory>();
            SetAsideRecords = new HashSet<SetAsideRecord>();

        }
        public int Id { get; set; }
        public string Name { get; set; }

        public int ArtistId { get; set; }
        public int PublisherId { get; set; }
        public int GenreId { get; set; }
        public DateTime DateOfPublishing { get; set; }
        public int CountSongs { get; set; }
        public double CostPrice { get; set; }
        public double SellingPrice { get; set; }
        public int? DealId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual Deal Deal { get; set; }
        public virtual Genre Genre { get; set; }
        
        public virtual ICollection<SalesHistory> SalesHistories { get; set; }
        public virtual ICollection<SetAsideRecord> SetAsideRecords { get; set; }
    }
}