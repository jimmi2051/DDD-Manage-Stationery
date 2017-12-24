namespace MyProject.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieu")]
    public partial class ChiTietPhieu
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaPhieu { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaSP { get; set; }

        [Column(TypeName = "money")]
        public decimal? DonGia { get; set; }

        public int? SoLuong { get; set; }

        public virtual PhieuNhapXuat PhieuNhapXuat { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
