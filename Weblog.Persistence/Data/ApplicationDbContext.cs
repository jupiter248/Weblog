using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblog.Domain.Models;

namespace Weblog.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Medium> Media { get; set; }
        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Contributor> Contributors { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasMany(t => t.Tags)
                .WithMany(a => a.Articles);

            modelBuilder.Entity<Article>()
                .HasMany(c => c.Contributors)
                .WithMany(a => a.Articles);

            modelBuilder.Entity<Event>()
                .HasMany(t => t.Tags)
                .WithMany(e => e.Events);

            modelBuilder.Entity<Event>()
                .HasMany(c => c.Contributors)
                .WithMany(p => p.Events);
                
            modelBuilder.Entity<Podcast>()
                .HasMany(t => t.Tags)
                .WithMany(p => p.Podcasts);

            modelBuilder.Entity<Podcast>()
                .HasMany(c => c.Contributors)
                .WithMany(p => p.Podcasts);
                



            base.OnModelCreating(modelBuilder);
        }
    }
}