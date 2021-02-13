
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DiscardedRecordConfig : EntityTypeConfiguration<DiscardedRecord>
    {
        public DiscardedRecordConfig()
        {
            
            this.Property(d =>d.Name )
                    .IsRequired();

            this.HasRequired(d => d.Artist)
                .WithMany(a => a.DiscardedRecords)
                .HasForeignKey(d => d.ArtistId)
                .WillCascadeOnDelete(false);

            this.HasRequired(p => p.Publisher)
                .WithMany(pub => pub.DiscardedRecords)
                .HasForeignKey(p => p.PublisherId)
                .WillCascadeOnDelete(false);

            this.HasRequired(p => p.Genre)
                .WithMany(g => g.DiscardedRecords)
                .HasForeignKey(p => p.GenreId)
                .WillCascadeOnDelete(false);
        }


    }
           
}
    
