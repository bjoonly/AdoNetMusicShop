using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class PublisherConfig: EntityTypeConfiguration<Publisher>
    {
        public PublisherConfig()
        {
            this.Property(p => p.Name)
               .HasMaxLength(200)
               .IsRequired();
        }
    }
}
