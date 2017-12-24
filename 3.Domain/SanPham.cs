namespace MyProject.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
            ChiTietPhieux = new HashSet<ChiTietPhieu>();
            Khoes = new HashSet<Kho>();
        }

        [Key]
        [StringLength(10)]
        public string MaSP { get; set; }

        [StringLength(10)]
        public string MaNCC { get; set; }

        [StringLength(10)]
        public string MaDM { get; set; }

        [StringLength(100)]
        public string TenSP { get; set; }

        [Column(TypeName = "money")]
        public decimal? DonGia { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(50)]
        public string XuatXu { get; set; }

        public double? TrongLuong { get; set; }

        public double? KichThuoc { get; set; }

        [StringLength(50)]
        public string DonVi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieu> ChiTietPhieux { get; set; }

        public virtual DanhMucSP DanhMucSP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kho> Khoes { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }
    }
}
