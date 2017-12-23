namespace MyProject.Domain
{
    public interface ILoginRepository
    {
        NguoiDung getUser(NguoiDung Target);
        NguoiDung getUserbyName(NguoiDung Target);
    }
}
