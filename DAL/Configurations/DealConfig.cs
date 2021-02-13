using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class DealConfig: EntityTypeConfiguration<Deal>
    {
        public DealConfig()
        {
            this.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

        }
    }
}
