namespace DAL
{
    using System;
    using System.Collections.Generic;

    public class Deal
    {
        public Deal()
        {
            Records = new HashSet<Record>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public float Discount { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<Record> Records { get; set; }
    }
}