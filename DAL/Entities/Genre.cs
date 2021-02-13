namespace DAL
{
    using System.Collections.Generic;

    public class Genre
    {
        public Genre()
        {
            Records = new HashSet<Record>();
            RemovedRecords = new HashSet<RemovedRecord>();
            DiscardedRecords = new HashSet<DiscardedRecord>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Record> Records { get; set; }
        public virtual ICollection<RemovedRecord> RemovedRecords { get; set; }
        public virtual ICollection<DiscardedRecord> DiscardedRecords { get; set; }
    }
}