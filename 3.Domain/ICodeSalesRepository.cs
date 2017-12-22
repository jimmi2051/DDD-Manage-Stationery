using System;
using System.Collections.Generic;

namespace MyProject.Domain
{
    public interface ICodeSalesRepository
    {
        MaKhuyenMai getCodes(String key);
        MaKhuyenMai CreateCode(MaKhuyenMai Target);
        MaKhuyenMai UpdateCode(MaKhuyenMai Target);
        void DeleteCode(MaKhuyenMai Target);
        IEnumerable<MaKhuyenMai> listCodes();
        IEnumerable<MaKhuyenMai> searchCodes(string key);
    }
}
