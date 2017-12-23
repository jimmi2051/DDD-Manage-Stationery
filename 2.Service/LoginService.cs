using System.Text.RegularExpressions;
using MyProject.Domain;
using MyProject.Infrastructure;
using MyProject.Repository;
namespace MyProject.Service
{
    public class LoginService : ILoginService
    {
        private IValidationDictionary _validationDictionary;
        private ILoginRepository _repository;
        #region Set-LoginService
        public LoginService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new LoginRepository())
        { }
        public LoginService(IValidationDictionary validationDictionary, ILoginRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        }
        #endregion
        #region Validation
        //Kiểm tra dữ liệu
        public bool ValidateContact(NguoiDung Username)
        {
            _validationDictionary.Clear();
            if (Username.ID.Trim().Length == 0) 
                _validationDictionary.AddError("Username", "Không được bỏ trống tên tài khoản");
            if (Username.Pass != null)
            {
                if (Username.Pass.Trim().Length == 0)
                    _validationDictionary.AddError("Password", "Không được bỏ trống mật khẩu");
            }
            if (Username.ID.Length > 0 && !Regex.IsMatch(Username.ID, @"\w"))
                _validationDictionary.AddError("Username", "Ký tự đặc biệt");
            if (Username.Mail != null)
            {
                if (Username.Mail.Length == 0)
                    _validationDictionary.AddError("Email", "Vui lòng nhập Email");
                if (Username.Mail.Length > 0 && !Regex.IsMatch(Username.Mail, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
                    _validationDictionary.AddError("Email", "Vui lòng nhập mail hợp lệ");
            }
            return _validationDictionary.IsValid;
        }
        #endregion
        #region Login
        public bool getUser(NguoiDung ND)
        {
            // Validation logic
            if (!ValidateContact(ND))
                return false;
            // Database logic
            try
            {
                ND.Pass = Encode.md5((ND.Pass));
                NguoiDung key = _repository.getUser(ND);          
                if (key != null)
                {
                    NhanVien NV = key.NhanVien;
                    Information.Nhanvien = NV;
                    Information.Nguoidung = key;
                    return true;
                }      
            }
            catch
            {
                return false;
            }
            return false;
        }
        public bool CheckUser(NguoiDung ND)
        {
            if (!ValidateContact(ND))
                return false;
            try
            {
                if (_repository.getUserbyName(ND) == null)
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
