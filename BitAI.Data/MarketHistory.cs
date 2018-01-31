namespace BitAI.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MarketHistory")]
    public partial class MarketHistory
    {
        public int MarketHistoryId { get; set; }

        public int MarketId { get; set; }

        public int TransactionId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime Timestamp { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public short? FillType { get; set; }

        public short? OrderType { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreatedDate { get; set; }

        public virtual Market Market { get; set; }
    }
}
