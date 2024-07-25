using System;
using System.Reflection.Emit;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _3DTrackingProducts.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<TagPosition> TagPositions { get; set; }
        public virtual DbSet<PairAntenna> PairAntennas { get; set; }
        public virtual DbSet<Tag3DPosition> Tag3DPositions { get; set; }
        public virtual DbSet<ControlTag> ControlTags { get; set; }

        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Tag>().HasMany(t => t.Logs).WithOne(l => l.Tag).HasForeignKey(l => l.TagEPC);

            builder.Entity<Category>().HasMany(c => c.Tags).WithOne(t => t.Category).HasForeignKey(t => t.CategoryId);

            builder.Entity<Tag>().HasMany(t => t.TagPositions).WithOne(tp => tp.Tag).HasForeignKey(t => t.TagEPC);

            builder.Entity<Room>().HasMany(r => r.antennas).WithOne(a => a.Room).HasForeignKey(a => a.idRoom).IsRequired();

            builder.Entity<User>(b =>
            {
                b.Property(u => u.FirstName).IsRequired(false);
                b.Property(u => u.LastName).IsRequired(false);
            });


            builder.Entity<Tag3DPosition>().HasOne(m => m.ControlTagLeft)
               .WithMany(m => m.Tag3DPositionsLeft)
               .HasForeignKey(m => m.ControlTagEPCLeft)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Tag3DPosition>().HasOne(m => m.ControlTagRight)
               .WithMany(m => m.Tag3DPositionsRight)
               .HasForeignKey(m => m.ControlTagEPCRight)
               .IsRequired(true)
               .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Role>().HasData(new Role
            {
                Id = Guid.Parse("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                Name = "Administrator",
                NormalizedName = "Administrator".ToUpper(),

            });

            builder.Entity<Role>().HasData(new Role
            {
                Id = Guid.Parse("d4e1506b-76f9-4774-b6d1-3a57ddb47f32"),
                Name = "Client",
                NormalizedName = "Client".ToUpper()
            });

            var adminUser = new User
            {
                Id = Guid.Parse("466e1dc3-3ca3-442f-a8b2-a477a935bc52"),
                UserName = "admin@email.pt",
                Email = "admin@email.pt",
                NormalizedEmail = "admin@email.pt".ToUpper()
            };
            var passwordHasher = new PasswordHasher<User>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin#.2022");

            builder.Entity<User>().HasData(adminUser);

            builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = Guid.Parse("15c86f2f-7c05-4590-b7bb-1337c0055db9"),
                UserId = Guid.Parse("466e1dc3-3ca3-442f-a8b2-a477a935bc52")
            });
        }
    }
}

