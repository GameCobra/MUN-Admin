using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MUNAdmin.Models;

namespace MUNAdmin.Data
{
    public class MUNAdminContext : DbContext
    {
        public MUNAdminContext (DbContextOptions<MUNAdminContext> options)
            : base(options)
        {
        }

        public DbSet<MUNAdmin.Models.MUNInstance> MUNInstance { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MUNInstance>()
                .OwnsMany(m => m.CouncilInformationList);

            modelBuilder.Entity<MUNInstance>()
                .OwnsMany(m => m.DelegationList, delegation =>
                {
                    delegation.OwnsMany(d => d.CouncilList, council =>
                    {
                        council.OwnsOne(c => c.Council);
                    });
                });
        }
    }
}
