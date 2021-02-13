using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class GenreConfig: EntityTypeConfiguration<Genre>
    {
        public GenreConfig()
        {
            this.Property(g => g.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
