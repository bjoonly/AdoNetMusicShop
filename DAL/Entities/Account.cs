using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsClient { get; set; }

        public int? ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}
