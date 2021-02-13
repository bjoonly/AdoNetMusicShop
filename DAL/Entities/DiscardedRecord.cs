using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DiscardedRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ArtistId { get; set; }
        public int PublisherId { get; set; }
        public int GenreId { get; set; }
        public DateTime DateOfPublishing { get; set; }
        public int CountSongs { get; set; }
        public double CostPrice { get; set; }
        public double SellingPrice { get; set; }


        public virtual Artist Artist { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
