using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class SalesHistoryConfig: EntityTypeConfiguration<SalesHistory>
    {
        public SalesHistoryConfig()
        {


            this.HasOptional(sh => sh.Record)
                .WithMany(p => p.SalesHistories)
                .HasForeignKey(sh => sh.RecordId)
                .WillCascadeOnDelete(false);

            this.HasOptional(sh => sh.Client)
                .WithMany(c => c.SalesHistories)
                .HasForeignKey(sh => sh.ClientId)
                .WillCascadeOnDelete(false);
        }
    }
}
