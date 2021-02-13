using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configurations
{
    class AccountConfig: EntityTypeConfiguration<Account>
    {
        public AccountConfig()
        {
            this.Property(a => a.Login)
                .IsRequired()
                .HasMaxLength(100);
            this.Property(a => a.Password)
                .IsRequired();




        }
    }
}
