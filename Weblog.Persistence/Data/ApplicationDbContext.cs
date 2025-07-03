using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Weblog.Domain.Models;
using Weblog.Infrastructure.Identity;

namespace Weblog.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
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
            // many to many between article and tag
            modelBuilder.Entity<Article>()
                .HasMany(t => t.Tags)
                .WithMany(a => a.Articles);
            // many to many between Article and contributor
            modelBuilder.Entity<Article>()
                .HasMany(c => c.Contributors)
                .WithMany(a => a.Articles);
            // many to many between event and tag
            modelBuilder.Entity<Event>()
                .HasMany(t => t.Tags)
                .WithMany(e => e.Events);
            // many to many between event and contributor
            modelBuilder.Entity<Event>()
                .HasMany(c => c.Contributors)
                .WithMany(p => p.Events);
            // many to many between podcast and tag
            modelBuilder.Entity<Podcast>()
                .HasMany(t => t.Tags)
                .WithMany(p => p.Podcasts);
            // many to many between podcast and contributor
            modelBuilder.Entity<Podcast>()
                .HasMany(c => c.Contributors)
                .WithMany(p => p.Podcasts);
                



            base.OnModelCreating(modelBuilder);
        }
    }
}