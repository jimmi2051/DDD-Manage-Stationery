using MyProject.Domain;
namespace MyProject.Service
{
    public interface ILoginService
    {
        bool getUser(NguoiDung ND);
        bool CheckUser(NguoiDung ND);
    }
}
