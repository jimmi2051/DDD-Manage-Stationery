using System;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository.RAW
{
    public class UserRepositoryRAW : IUserRepository
    {
        QLVanPhong_Context _entities = QLVanPhong_Context.Instance;
        public NguoiDung InsertUser(NguoiDung target)
        {
            _entities.NguoiDungs.Add(target);
            _entities.SaveChanges();
            return target;
        }
        public NguoiDung UpdateUser(NguoiDung target)
        {
            var usertoedit = getUser(target.ID);
            if (!usertoedit.Equals(target))
            {
                usertoedit.Mail = target.Mail;
                usertoedit.Pass = target.Pass;
            }
           _entities.SaveChanges();
            return target;
        }
        public void DeleteUser(NguoiDung target)
        {
            _entities.NguoiDungs.Remove(target);
            _entities.SaveChanges();
        }
        public NguoiDung getUser(String ID)
        {
            return _entities.NguoiDungs.Where(c => c.ID.Equals(ID)).FirstOrDefault();
        }
        public NguoiDung CheckUser(String key)
        {
            return _entities.NguoiDungs.Where(c => c.MaNV.Equals(key)).FirstOrDefault();
        }
    }
}
