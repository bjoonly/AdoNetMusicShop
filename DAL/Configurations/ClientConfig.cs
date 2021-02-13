using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class ClientConfig: EntityTypeConfiguration<Client>
    {
        public ClientConfig()
        {
            this.HasKey(c => c.AccountId);
            this.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();
            this.Property(c => c.Surname)
                          .IsRequired();
            this.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            this.HasRequired(c => c.Account)
                .WithOptional(a => a.Client);

  
        }
    }
}
