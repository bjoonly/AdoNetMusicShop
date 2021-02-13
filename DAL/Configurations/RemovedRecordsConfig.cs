using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class RemovedRecordConfig : EntityTypeConfiguration<RemovedRecord>
    {
        public RemovedRecordConfig()
        {


            this.Property(p => p.Name)
                    .IsRequired();

            this.HasRequired(p => p.Artist)
                .WithMany(a => a.RemovedRecords)
                .HasForeignKey(p => p.ArtistId)
                .WillCascadeOnDelete(false);

            this.HasRequired(p => p.Publisher)
                .WithMany(pub => pub.RemovedRecords)
                .HasForeignKey(p => p.PublisherId)
                .WillCascadeOnDelete(false);

            this.HasRequired(p => p.Genre)
                .WithMany(g => g.RemovedRecords)
                .HasForeignKey(p => p.GenreId)
                .WillCascadeOnDelete(false);
        }
    }
}