namespace Zeka.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bid")]
    public partial class Bid
    {
        [Key]
        public Guid bid_id { get; set; }

        public Guid user_id { get; set; }

        public Guid auction_id { get; set; }

        public int tokens { get; set; }

        public DateTime time { get; set; }

        public bool winner { get; set; }

        public virtual Auction Auction { get; set; }

        public virtual User User { get; set; }
    }
}
