using NUnit.Framework;
using MyProject.Domain;
using MyProject.Infrastructure;
using Moq;
namespace MyProject.Service.Tests
{
    [TestFixture]
    public class LoginServiceTest
    {
        private Mock<ILoginRepository> _mockRepository;
        private ModelStateDictionary _modelState;
        private ILoginService _service;
        public static NguoiDung stubLogin;
        [SetUp]
        public void Initialize()
        {
            _mockRepository = new Mock<ILoginRepository>();
            _modelState = new ModelStateDictionary();
            _service = new LoginService(new ModelStateWrapper(_modelState), _mockRepository.Object);
            stubLogin = new NguoiDung() { ID = "2", Pass = "123456", Mail = "test@gmail.com", MaNV = "NV001" };
        }
        //Stub lại login Service
        public class StubLoginService : ILoginService
        {
            public bool getUser(NguoiDung target)
            {
                if (target.ID.Equals(stubLogin.ID) && target.Pass.Equals(stubLogin.Pass))
                    return true;
                return false;
            }
            public bool CheckUser(NguoiDung target)
            {
                return true;
            }
        }
        [Test]
        public void LoginSuccess()
        {
            // Act
            NguoiDung target = new NguoiDung() { ID = "2", Pass = "123456", Mail = "test@gmail.com", MaNV = "NV001" };
            _service = new StubLoginService();
            // Action
            var result = _service.getUser(target);
            // Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void LoginFailedNoneID()
        {
            // Act
            NguoiDung target = new NguoiDung() { ID = string.Empty, Pass = "123456", Mail = "test@gmail.com", MaNV = "NV001" };
            // Action
            var result = _service.getUser(target);
            // Assert
            Assert.IsFalse(result);
            var error = _modelState["Username"];
            Assert.AreEqual("Không được bỏ trống tên tài khoản", error);
        }
        [Test]
        public void LoginFailedErrorID()
        {
            // Act
            NguoiDung target = new NguoiDung() { ID = "@!#", Pass = "123456", Mail = "test@gmail.com", MaNV = "NV001" };
            // Action
            var result = _service.getUser(target);
            // Assert
            Assert.IsFalse(result);
            var error = _modelState["Username"];
            Assert.AreEqual("Ký tự đặc biệt", error);
        }
        [Test]
        public void LoginFailedSQLInjection()
        {
            // Act
            NguoiDung target = new NguoiDung() { ID = "'or 1=1--", Pass = "123456", Mail = "test@gmail.com", MaNV = "NV001" };
            // Action
            var result = _service.getUser(target);
            // Assert
            Assert.IsFalse(result);
        }
        [Test]
        public void LoginFailedNonePass()
        {
            // Act
            NguoiDung target = new NguoiDung() { ID = "3", Pass = string.Empty, Mail = "test@gmail.com", MaNV = "NV001" };
            // Action
            var result = _service.getUser(target);
            // Assert
            Assert.IsFalse(result);
            var error = _modelState["Password"];
            Assert.AreEqual("Không được bỏ trống mật khẩu", error);
        }
        [Test]
        public void LoginFailedNoneEmail()
        {
            // Act
            NguoiDung target = new NguoiDung() { ID = "3", Pass = "123456", Mail = string.Empty, MaNV = "NV001" };
            // Action
            var result = _service.CheckUser(target);
            // Assert
            Assert.IsFalse(result);
            var error = _modelState["Email"];
            Assert.AreEqual("Vui lòng nhập Email", error);
        }
        [Test]
        public void LoginFailedErrorEmail([Values(1, 2, 3)] int x)
        {

            // Act
            NguoiDung target = new NguoiDung() { ID = "3", Pass = "123456", Mail = "jimmi2051", MaNV = "NV001" };
            // Action
            var result = _service.CheckUser(target);
            // Assert
            Assert.IsFalse(result);
            var error = _modelState["Email"];
            Assert.AreEqual("Vui lòng nhập mail hợp lệ", error);

        }
    }
}