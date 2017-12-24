namespace MyProject.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuNhapXuat")]
    public partial class PhieuNhapXuat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuNhapXuat()
        {
            ChiTietPhieux = new HashSet<ChiTietPhieu>();
            Khoes = new HashSet<Kho>();
        }

        [Key]
        [StringLength(10)]
        public string MaPhieu { get; set; }

        [StringLength(10)]
        public string MaNV { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongTien { get; set; }

        public DateTime? NgayLap { get; set; }

        [StringLength(10)]
        public string TrangThai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieu> ChiTietPhieux { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kho> Khoes { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
