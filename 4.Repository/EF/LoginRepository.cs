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
            return _entities.NguoiDungs.Where(c => c.ID.Equals(ND.ID) && c.Pass.Equals(ND.Pass)).FirstOrDefault();
        }

        public NguoiDung getUserbyName(NguoiDung Target)
        {
            return _entities.NguoiDungs.Where(c => c.ID.Equals(Target.ID) && c.Mail.Equals(Target.Mail)).FirstOrDefault();
        }       
    }
}
