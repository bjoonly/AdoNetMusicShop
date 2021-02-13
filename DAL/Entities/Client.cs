using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    public class Client
    {
        public Client()
        {
            SalesHistories = new HashSet<SalesHistory>();
            SetAsideRecords = new HashSet<SetAsideRecord>();
        }
        [Key,ForeignKey(nameof(Account))]
         public int AccountId { get; set; }
        
        public string Name { get; set; }
        public string Surname { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public float? Discount { get; set; }
       

        public virtual ICollection<SalesHistory> SalesHistories { get; set; }
        public virtual ICollection<SetAsideRecord> SetAsideRecords { get; set; }
        public virtual Account Account { get; set; }

    }
}