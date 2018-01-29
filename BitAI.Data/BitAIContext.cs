namespace BitAI.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BitAIContext : DbContext
    {
        public BitAIContext()
            : base("name=BitAIModel")
        {
        }

        public virtual DbSet<Coin> Coins { get; set; }
        public virtual DbSet<Market> Markets { get; set; }
        public virtual DbSet<MarketHistory> MarketHistories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coin>()
                .Property(e => e.CoinCodeName)
                .IsUnicode(false);

            modelBuilder.Entity<Coin>()
                .Property(e => e.CoinType)
                .IsUnicode(false);

            modelBuilder.Entity<Coin>()
                .Property(e => e.TxFee)
                .HasPrecision(9, 4);

            modelBuilder.Entity<Coin>()
                .HasMany(e => e.Markets)
                .WithRequired(e => e.Coin)
                .HasForeignKey(e => e.CoinId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Coin>()
                .HasMany(e => e.Markets1)
                .WithRequired(e => e.BaseCoin)
                .HasForeignKey(e => e.BaseCoinId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Market>()
                .Property(e => e.MarketName)
                .IsUnicode(false);

            modelBuilder.Entity<Market>()
                .Property(e => e.MinTradeSize)
                .HasPrecision(18, 8);

            modelBuilder.Entity<Market>()
                .HasMany(e => e.MarketHistories)
                .WithRequired(e => e.Market)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MarketHistory>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 8);

            modelBuilder.Entity<MarketHistory>()
                .Property(e => e.Price)
                .HasPrecision(18, 8);
        }
    }
}
