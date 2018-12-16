namespace Zeka.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Auction")]
    public partial class Auction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Auction()
        {
            Bid = new HashSet<Bid>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid auction_id { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [Required]
        public byte[] picture { get; set; }

        public long duration { get; set; }

        public decimal starting_price { get; set; }

        public decimal current_price { get; set; }

        public DateTime? created { get; set; }

        public DateTime? opened { get; set; }

        public DateTime? closed { get; set; }

        [Required]
        [StringLength(10)]
        public string state { get; set; }

        public Guid user_id { get; set; }

        public decimal tokenValue { get; set; }

        [Required]
        [StringLength(50)]
        public string currency { get; set; }

        public int token_price { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bid> Bid { get; set; }
    }
}
