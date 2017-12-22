using System.Collections.Generic;
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
        public LoginService(IValidationDictionary validationDictionary)
            : this(validationDictionary, new LoginRepository())
        { }
        public LoginService(IValidationDictionary validationDictionary, ILoginRepository repository)
        {
            _validationDictionary = validationDictionary;
            _repository = repository;
        }
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
        public bool getUser(NguoiDung ND)
        {
            // Validation logic
            if (!ValidateContact(ND))
                return false;
            // Database logic
            try
            {
                //Pass : 123456
                ND.Pass = Encode.md5((ND.Pass));
                //Pass: 12378y78a78132dsxzc
                NguoiDung key = _repository.getUser(ND);
                //Kiểm tra người dùng có tồn tại hay không? Nếu có lưu trữ thông tin và trả về đúng để đăng nhập
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
                IEnumerable<NguoiDung> list = _repository.listUser();
                foreach (NguoiDung item in list)
                {
                    if (item.ID.Equals(ND.ID) && item.Mail.Equals(ND.Mail))
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
