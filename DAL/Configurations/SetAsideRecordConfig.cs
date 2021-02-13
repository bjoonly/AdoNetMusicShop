using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class SetAsideRecordConfig: EntityTypeConfiguration<SetAsideRecord>
    {
        public SetAsideRecordConfig()
        {
            this.HasRequired(sa => sa.Client)
                .WithMany(c => c.SetAsideRecords)
                .HasForeignKey(sa => sa.ClientId)
                .WillCascadeOnDelete(false);

            this.HasOptional(sa => sa.Record)
            .WithMany(c => c.SetAsideRecords)
            .HasForeignKey(sa => sa.RecordId)
            .WillCascadeOnDelete(false);
        }
    }
}
