namespace Zeka.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemConf")]
    public partial class SystemConf
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int system_conf_id { get; set; }

        public int silver_pack { get; set; }

        public int gold_pack { get; set; }

        public int platinum_pack { get; set; }

        public int mrp_group { get; set; }

        public long auction_duration { get; set; }

        [Required]
        [StringLength(50)]
        public string currency { get; set; }

        public decimal token_value { get; set; }
    }
}
