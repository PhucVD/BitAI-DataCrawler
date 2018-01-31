namespace BitAI.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Coin")]
    public partial class Coin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Coin()
        {
            Markets = new HashSet<Market>();
            Markets1 = new HashSet<Market>();
        }

        public int CoinId { get; set; }

        [Required]
        [StringLength(10)]
        public string CoinCodeName { get; set; }

        [Required]
        [StringLength(50)]
        public string CoinName { get; set; }

        [StringLength(50)]
        public string CoinType { get; set; }

        public decimal? TxFee { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(4000)]
        public string Note { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Market> Markets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Market> Markets1 { get; set; }
    }
}
