using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SetAsideRecord
    {
        public int Id { get; set; }
        public int? RecordId { get; set; }
        public int ClientId { get; set; }
        public DateTime DateTo { get; set; }
        
        public virtual Record Record { get; set; }
        public virtual Client Client { get; set; }

    }
}
