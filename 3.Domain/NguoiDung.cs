namespace MyProject.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [StringLength(10)]
        public string ID { get; set; }

        [StringLength(50)]
        public string Pass { get; set; }

        [StringLength(100)]
        public string Mail { get; set; }

        [StringLength(10)]
        public string MaNV { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
