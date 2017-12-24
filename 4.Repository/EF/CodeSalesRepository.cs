using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository
{
    public class CodeSalesRepository : ICodeSalesRepository
    {
        QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
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
            _entities.Entry(CodetoEdit).CurrentValues.SetValues(Target);
            _entities.SaveChanges();
            return Target;
        }
    }
}
