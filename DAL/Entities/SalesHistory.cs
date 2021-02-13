namespace DAL
{
    using System;

    public class SalesHistory
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public string ClientEmail { get; set; }
        public double SellingPrice { get; set; }
        public float DiscountDeal { get; set; }
        public float DiscountClient { get; set; }
        public double DiscountPrice { get; set; }

        public DateTime Date { get; set; }
        public int? RecordId { get; set; }


        public virtual Client Client { get; set; }
        public virtual Record Record { get; set; }
    }
}