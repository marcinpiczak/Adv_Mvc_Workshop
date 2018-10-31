using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMessagePortal.Models;

namespace MyMessagePortal.EntityConfig
{
    public class EFCContext : IdentityDbContext<UserModel>
    {
        public DbSet<ChannelModel> Channels { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<ObservedChannelsModel> ObservedChannels { get; set; }

        public EFCContext(DbContextOptions<EFCContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ObservedChannelsModel>()
                .ToTable("ObservedChannels", "dbo")
                .HasKey(x => new {x.UserId, x.ChannelId});

            builder.Entity<ObservedChannelsModel>()
                .HasOne(x => x.User)
                .WithMany(x => x.ObservedChannels)
                .HasForeignKey(x => x.UserId);

            builder.Entity<ObservedChannelsModel>()
                .HasOne(x => x.Channel)
                .WithMany(x => x.ObservedChannels)
                .HasForeignKey(x => x.ChannelId);

            //dodane z powodu błędu o multiple cascade paths
            builder.Entity<MessageModel>()
                .ToTable("Messages", "dbo")
                .HasOne(x => x.CreatedBy)
                .WithMany(x => x.UserMessages)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ChannelModel>()
                .ToTable("Channels", "dbo")
                .HasOne(x => x.CreatedBy)
                .WithMany(x => x.UserChannels)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
