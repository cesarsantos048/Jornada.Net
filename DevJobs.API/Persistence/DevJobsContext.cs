using System.Collections.Generic;
using DevJobs.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevJobs.API.Persistence
{
    public class DevJobsContext : DbContext
    {
        public DevJobsContext(DbContextOptions<DevJobsContext> context) : base(context) {}

        public DbSet<JobVacancy> JobVacancies { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder){
            modelbuilder.Entity<JobVacancy>(e => {
                e.HasKey(jv => jv.Id);
                
                e.HasMany(jv => jv.Applications)
                    .WithOne()
                    .HasForeignKey(ja => ja.IdJobVacancy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelbuilder.Entity<JobApplication>(e => {
                e.HasKey(ja => ja.Id);
            });
        }


    }
}