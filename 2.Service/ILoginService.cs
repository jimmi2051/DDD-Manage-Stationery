using MyProject.Domain;
using System;
namespace MyProject.Service
{
    public interface ILoginService
    {
        bool getUser(NguoiDung ND);
        bool CheckUser(NguoiDung ND);
        bool SendMail(string username, string mail);
    }
}
