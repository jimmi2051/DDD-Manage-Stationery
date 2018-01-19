using System;
using System.Net;
using System.Net.Mail;
using System.Text;
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
        public string CreateLostPassword(int PasswordLength)
        {
            string _allowedChars = "abcdefghijk0123456789mnopqrstuvwxyz";
            Random randNum = new Random(); char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
        public string NoiDungMail(string username)
        {
            string NoiDung = "";
            IUserRepository rp = new UserRepository();
            NguoiDung target = rp.getUser(username);
            string MatKhauMoi = "", TenDangNhap = "";
            if (target != null)
            {
                NoiDung = "Đây là Mail gửi đến từ công ty DefTnT <br>";
                NoiDung += "Tên tài khoản: " + username;
                MatKhauMoi = CreateLostPassword(7); NoiDung += "<br> Mật khẩu mới của bạn là: " + MatKhauMoi;
                TenDangNhap = target.ID;
                target.Pass = Encode.md5(MatKhauMoi);
                rp.UpdateUser(target);
                NoiDung += "<br>Sau khi đăng nhập bạn nên đổi lại mật khẩu để tiện cho việc đăng nhập lần tiếp theo";
                NoiDung += "<br><br><hr>Vui lòng không trả lời Mail này!";
            }
            return NoiDung;
        }
        public bool SendMail(string username, string mail)
        {
            MailMessage objEmail = new MailMessage();
            objEmail.To.Add(mail);
            objEmail.From = new MailAddress("jimmi2051@gmail.com");
            objEmail.Subject = "Thông tin về mật khẩu của bạn";
            objEmail.BodyEncoding = Encoding.UTF8;
            objEmail.Body = NoiDungMail(username);
            objEmail.Priority = MailPriority.High;
            objEmail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("jimmi2051@gmail.com", "anhbekute");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                smtp.Send(objEmail);
            }
            catch {
                return false;
            }
            return true;
        }
    }
}
