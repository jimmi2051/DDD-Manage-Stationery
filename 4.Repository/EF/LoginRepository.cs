using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
using System.Data;

namespace MyProject.Repository
{
    public class LoginRepository : ILoginRepository
    {      
        private QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
        public NguoiDung getUser(NguoiDung ND)
        {

            Infrastructure.Information.Result = _entities.CheckConnection();
            return _entities.NguoiDungs.Where(c => c.ID.Equals(ND.ID) && c.Pass.Equals(ND.Pass)).FirstOrDefault();
           // return _entities.NguoiDungs.Where(c => c.ID.Equals(ND.ID) && c.Pass.Equals(ND.Pass)).FirstOrDefault();
        }
        public IEnumerable<NguoiDung> listUser()
        {
            return _entities.NguoiDungs.ToList();
        }        
    }
}
