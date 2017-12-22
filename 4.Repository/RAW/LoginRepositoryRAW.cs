using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
using MyProject.Infrastructure;

namespace MyProject.Repository.RAW
{
    public class LoginRepositoryRAW : ILoginRepository
    {
        private QLVanPhong_Context db = QLVanPhong_Context.Instance;
        public NguoiDung getUser(NguoiDung ND)
        {
            NguoiDung target= db.NguoiDungs.Where(c => c.ID.Equals(ND.ID) && c.Pass.Equals(ND.Pass)).FirstOrDefault();
            target.NhanVien = db.NhanViens.Where(c => c.MaNV.Equals(target.MaNV)).FirstOrDefault();
            return target;
        }
        public void getNhanvien(string key)
        {
            List<NhanVien> list = db.NhanViens.ToList();
            foreach (var c in list)
            {
                if (c.MaNV.Equals(key))
                    Information.Nhanvien = c;
            }
        }
        public IEnumerable<NguoiDung> listUser()
        {
            return db.NguoiDungs.ToList();
        }
    }
}
