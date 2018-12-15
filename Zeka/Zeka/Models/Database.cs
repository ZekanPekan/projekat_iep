namespace Zeka.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Database : DbContext
    {
        public Database()
            : base("name=Database")
        {
        }

        public virtual DbSet<Auction> Auction { get; set; }
        public virtual DbSet<Bid> Bid { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<SystemConf> SystemConf { get; set; }
        public virtual DbSet<TokenOrder> TokenOrder { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auction>()
                .Property(e => e.title)
                .IsFixedLength();

            modelBuilder.Entity<Auction>()
                .Property(e => e.starting_price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Auction>()
                .Property(e => e.current_price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Auction>()
                .Property(e => e.tokenValue)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Auction>()
                .HasMany(e => e.Bid)
                .WithRequired(e => e.Auction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SystemConf>()
                .Property(e => e.token_value)
                .HasPrecision(10, 2);

            modelBuilder.Entity<TokenOrder>()
                .Property(e => e.price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<TokenOrder>()
                .Property(e => e.state)
                .IsFixedLength();

            modelBuilder.Entity<TokenOrder>()
                .Property(e => e.currency)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.first_name)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.last_name)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .Property(e => e.email)
                .IsFixedLength();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Auction)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Bid)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TokenOrder)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
