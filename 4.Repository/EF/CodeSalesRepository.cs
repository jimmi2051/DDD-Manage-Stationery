using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProject.Domain;
namespace MyProject.Repository
{
    public class CodeSalesRepository : ICodeSalesRepository
    {
        QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
        public MaKhuyenMai CreateCode(MaKhuyenMai Target)
        {
            _entities.Insert_MaKhuyenMai(Target.MaKM, Target.TrangThai, Target.TiLe);
            _entities.SaveChanges();
            return Target;
        }

        public void DeleteCode(MaKhuyenMai Target)
        {
            _entities.Delete_MaKM(Target.MaKM);
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

        public IEnumerable<MaKhuyenMai> searchCodes(string key)
        {
            return _entities.MaKhuyenMais.Where(c => c.MaKM.Contains(key)).ToList();
        }

        public MaKhuyenMai UpdateCode(MaKhuyenMai Target)
        {
            var CodetoEdit = getCodes(Target.MaKM);
            _entities.Entry(CodetoEdit).CurrentValues.SetValues(Target);
            _entities.SaveChanges();
            return Target;
        }
    }
}
