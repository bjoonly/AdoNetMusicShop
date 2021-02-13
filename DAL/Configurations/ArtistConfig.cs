using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    internal class ArtistConfig: EntityTypeConfiguration<Artist>
    {
        public ArtistConfig()
        {
            this.Property(a => a.Name)
                .IsRequired();

            
        }
    }
}
