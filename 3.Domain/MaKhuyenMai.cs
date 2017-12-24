namespace MyProject.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MaKhuyenMai")]
    public partial class MaKhuyenMai
    {
        [Key]
        [StringLength(10)]
        public string MaKM { get; set; }

        [StringLength(10)]
        public string TrangThai { get; set; }

        public float? TiLe { get; set; }
    }
}
