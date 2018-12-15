namespace Zeka.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TokenOrder")]
    public partial class TokenOrder
    {
        [Key]
        public Guid token_order_id { get; set; }

        public Guid user_id { get; set; }

        public int tokens { get; set; }

        public decimal price { get; set; }

        [Required]
        [StringLength(10)]
        public string state { get; set; }

        [Required]
        [StringLength(10)]
        public string currency { get; set; }

        public virtual User User { get; set; }
    }
}
