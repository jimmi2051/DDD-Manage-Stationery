using System;
using System.Collections;
using MyProject.Domain;
namespace MyProject.Service
{
    public interface ICodesService
    {
        MaKhuyenMai getCodes(String key);
        Boolean CreateCode(MaKhuyenMai Target);
        Boolean UpdateCode(MaKhuyenMai Target);
        Boolean DeleteCode(MaKhuyenMai Target);
        IEnumerable listCodes();
        IEnumerable searchCodes(string key);
    }
}
