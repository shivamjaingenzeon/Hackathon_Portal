using Microsoft.EntityFrameworkCore;

namespace hackathonbackend.Data
{
    public class HackathonDbContext : DbContext
    {
        public HackathonDbContext(DbContextOptions<HackathonDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<AuthenticationUser> AuthenticationUsers { get; set; }
        public DbSet<Hackathon> Hackathons { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<UserStory> UserStories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hackathon>()
                .HasKey(e => e.HackathonId);

            modelBuilder.Entity<Member>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Team>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<UserStory>()
                .HasKey(e => e.UserStoryId);

            modelBuilder.Entity<Company>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Company>()
                .HasOne(c => c.AuthenticationUser)
                .WithOne()
                .HasForeignKey<Company>(c => c.AuthenticationUserId);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Teams)
                .WithOne(t => t.Company)
                .HasForeignKey(t => t.CompanyId);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Hackathons)
                .WithOne(h => h.Company)
                .HasForeignKey(h => h.CompanyId);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.AuthenticationUser)
                .WithOne()
                .HasForeignKey<Team>(t => t.AuthenticationUserId);

            modelBuilder.Entity<AuthenticationUser>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Members)
                .WithOne(m => m.Team)
                .HasForeignKey(m => m.TeamId);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.UserStory)
                .WithOne(us => us.Team)
                .HasForeignKey<Team>(t => t.UserStoryId);

            modelBuilder.Entity<Hackathon>()
                .HasOne(h => h.Company)
                .WithMany(c => c.Hackathons)
                .HasForeignKey(h => h.CompanyId);

            modelBuilder.Entity<Hackathon>()
                .HasMany(h => h.UserStories)
                .WithOne(us => us.Hackathon)
                .HasForeignKey(us => us.HackathonId);

            modelBuilder.Entity<Member>()
                .HasOne(m => m.Team)
                .WithMany(t => t.Members)
                .HasForeignKey(m => m.TeamId);

          

            modelBuilder.Entity<UserStory>()
                .HasOne(us => us.Hackathon)
                .WithMany(h => h.UserStories)
                .HasForeignKey(us => us.HackathonId);
        }
    }
}
