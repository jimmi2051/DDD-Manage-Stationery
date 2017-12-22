using System;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository
{
    public class UserRepository :IUserRepository 
    {
        QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
        public NguoiDung InsertUser(NguoiDung target)
        {
            _entities.Insert_NguoiDung(target.ID, target.Pass, target.Mail, target.MaNV);
            _entities.SaveChanges();
            return target;
        }
        public NguoiDung UpdateUser(NguoiDung target)
        {          
            _entities.Update_NguoiDung(target.ID, target.Pass, target.Mail, target.MaNV);
            _entities.SaveChanges();
            return target;
        }
        public void DeleteUser(NguoiDung target)
        {
            _entities.Delete_NguoiDung(target.ID);
            _entities.SaveChanges();
        }
        public NguoiDung getUser(String ID)
        {
            return _entities.NguoiDungs.Where(c=>c.ID.Equals(ID)).FirstOrDefault();
        }
        public NguoiDung CheckUser(String key)
        {
            return _entities.NguoiDungs.Where(c => c.MaNV.Equals(key)).FirstOrDefault();
        }
    }
}
