using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository.RAW
{
    public class CodesRepositoryRAW : ICodeSalesRepository
    {
        QLVanPhong_Context _entities = QLVanPhong_Context.Instance;
        public MaKhuyenMai CreateCode(MaKhuyenMai Target)
        {
            _entities.MaKhuyenMais.Add(Target);
            _entities.SaveChanges();
            return Target;
        }

        public void DeleteCode(MaKhuyenMai Target)
        {
            _entities.MaKhuyenMais.Remove(Target);
            _entities.SaveChanges();
        }

        public MaKhuyenMai getCodes(string key)
        {
            return _entities.MaKhuyenMais.Where(c => c.MaKM.Equals(key)).FirstOrDefault();
        }

        public IEnumerable<MaKhuyenMai> listCodes()
        {
            return _entities.MaKhuyenMais.ToList();
        }
        public MaKhuyenMai UpdateCode(MaKhuyenMai Target)
        {
            var CodetoEdit = getCodes(Target.MaKM);
            if (!CodetoEdit.Equals(Target))
            {
                CodetoEdit.TiLe = Target.TiLe;
                CodetoEdit.TrangThai = Target.TrangThai;
            }
            _entities.SaveChanges();
            return Target;
        }
    }
}
