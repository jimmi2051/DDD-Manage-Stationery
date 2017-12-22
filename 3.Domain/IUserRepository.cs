namespace MyProject.Domain
{
    public interface IUserRepository
    {
        NguoiDung InsertUser(NguoiDung target);
        NguoiDung UpdateUser(NguoiDung target);
        void DeleteUser(NguoiDung target);
        NguoiDung getUser(string key);
        NguoiDung CheckUser(string key);
    }
}
