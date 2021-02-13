using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class RecordsConfig: EntityTypeConfiguration<Record>
    {
        public RecordsConfig()
        {
            this.Property(p => p.Name)
                .IsRequired();

            this.HasRequired(p => p.Artist)
                .WithMany(a => a.Records)
                .HasForeignKey(p => p.ArtistId)
                .WillCascadeOnDelete(false);

            this.HasRequired(p => p.Publisher)
                .WithMany(pub => pub.Records)
                .HasForeignKey(p => p.PublisherId)
                .WillCascadeOnDelete(false);

            this.HasOptional(p => p.Deal)
                .WithMany(deal => deal.Records)
                .HasForeignKey(p => p.DealId)
                .WillCascadeOnDelete(false);

            this.HasRequired(p=>p.Genre)
                .WithMany(g => g.Records)
                .HasForeignKey(p=>p.GenreId)
                .WillCascadeOnDelete(false);
        }
    }
}
