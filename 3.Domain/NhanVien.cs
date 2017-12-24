namespace MyProject.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            HoaDons = new HashSet<HoaDon>();
            NguoiDungs = new HashSet<NguoiDung>();
            PhieuNhapXuats = new HashSet<PhieuNhapXuat>();
        }

        [Key]
        [StringLength(10)]
        public string MaNV { get; set; }

        [StringLength(100)]
        public string Ten { get; set; }

        [StringLength(100)]
        public string Diachi { get; set; }

        [StringLength(12)]
        public string sdt { get; set; }

        [StringLength(100)]
        public string ChucVu { get; set; }

        public int? Luong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuNhapXuat> PhieuNhapXuats { get; set; }
    }
}
