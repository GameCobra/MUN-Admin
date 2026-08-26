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
                .HasMany(m => m.CouncilInformationList)
                .WithOne()
                .HasForeignKey(c => c.MUNInstanceId)
                .OnDelete(DeleteBehavior.NoAction);

            
            modelBuilder.Entity<CouncilInformation>()
                        .OwnsMany(n => n.Resolutions, resolution =>
                        {
                            resolution.HasKey(o => o.Id);
                        });

            modelBuilder.Entity<MUNInstance>()
                .OwnsMany(m => m.DelegationList, delegation =>
                {
                    delegation.HasKey(d => d.Id);

                    delegation.OwnsMany(d => d.CouncilList, delegationCouncil =>
                    {
                        delegationCouncil.HasOne(c => c.Council)
                                         .WithMany()
                                         .HasForeignKey(e => e.CouncilId)
                                         .OnDelete(DeleteBehavior.NoAction);

                        delegationCouncil.OwnsMany(e => e.Ammendments);

                        delegationCouncil.Navigation(c => c.Council)
                                         .AutoInclude();

                    });
                });
        }
    }
}
