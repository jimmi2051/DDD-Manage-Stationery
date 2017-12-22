using System.Collections.Generic;
namespace MyProject.Domain
{
    public interface ILoginRepository
    {
        NguoiDung getUser(NguoiDung ND);
        IEnumerable<NguoiDung> listUser();
    }
}
