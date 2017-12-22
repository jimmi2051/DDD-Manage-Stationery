using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.Domain;
namespace MyProject.Service
{
    public interface ICodesService
    {
        MaKhuyenMai getCodes(String key);
        Boolean CreateCode(MaKhuyenMai Target);
        Boolean UpdateCode(MaKhuyenMai Target);
        Boolean DeleteCode(MaKhuyenMai Target);
        IEnumerable<MaKhuyenMai> listCodes();
        IEnumerable<MaKhuyenMai> searchCodes(string key);
    }
}
