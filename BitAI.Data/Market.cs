namespace BitAI.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Market")]
    public partial class Market
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Market()
        {
            MarketHistories = new HashSet<MarketHistory>();
        }

        public int MarketId { get; set; }

        [Required]
        [StringLength(50)]
        public string MarketName { get; set; }

        public int CoinId { get; set; }

        public int BaseCoinId { get; set; }

        public decimal? MinTradeSize { get; set; }

        public bool? IsActive { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreatedDate { get; set; }

        public virtual Coin Coin { get; set; }

        public virtual Coin BaseCoin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MarketHistory> MarketHistories { get; set; }
    }
}
