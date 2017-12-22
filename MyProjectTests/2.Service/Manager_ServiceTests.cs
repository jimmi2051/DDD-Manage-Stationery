using NUnit.Framework;
using MyProject.Domain;
using MyProject.Infrastructure;
using MyProjectTests.Service;
using System.Linq;

namespace MyProject.Service.Tests
{
    [TestFixture(), Description("Fixture description here")]
    public class Manager_ServiceTests
    {
        private ModelStateDictionary _modelState;
        private IManagerService _service;
        private static MyProjectTests.Service.QLVanPhong_Context _db = MyProjectTests.Service.QLVanPhong_Context.Instance;
        private SanPham target;
        public static SanPham[] arr = _db.SanPham.ToArray();
        [SetUp]
        public void Initialize()
        {
            _modelState = new ModelStateDictionary();
            _service = ServiceFactory.getManagerService(_modelState);
        }
        [Test]
        [Ignore("Ignore a fixture")]
        public void CreateProduct()
        {
            //Act
            target = arr[0];
            // Action
            var result = _service.CreateProduct(target);
            // Assert
            Assert.IsTrue(result);
        }
        [Test, Timeout(10)]
        public void CreateProductNoneID([Values("", null, "123")] string value)
        {
            //Act
            target = arr[0];
            target.MaSP = value;
            //Action
            var result = _service.CreateProduct(target);
            //Asset
            Assert.IsFalse(result);
            var Error = _modelState["MaSP"];
            Assert.AreEqual("Mã sản phẩm không được để trống", Error);
        }
        [Test, Timeout(10)]
        public void CreateProductNoneCat()
        {
            //Act
            target = arr[1];
            target.MaDM = "";
            //Action
            var result = _service.CreateProduct(target);
            //Asset
            Assert.IsFalse(result);
            var Error = _modelState["MaDM"];
            Assert.AreEqual("Mã danh mục không được để trống", Error);
        }
        [Test]
        public void CreateProductNoneSup()
        {
            //Act
            target = arr[1];
            target.MaNCC = "";
            //Action
            var result = _service.CreateProduct(target);
            //Asset
            Assert.IsFalse(result);
            var Error = _modelState["MaNCC"];
            Assert.AreEqual("Mã nhà cung cấp không được để trống", Error);
        }
        [Test]
        public void CreateProductNoneName()
        {
            //Act
            target = arr[1];
            target.TenSP = "";
            //Action
            var result = _service.CreateProduct(target);
            //Asset
            Assert.IsFalse(result);
            var Error = _modelState["TenSP"];
            Assert.AreEqual("Tên sản phẩm không được để trống", Error);
        }
        [Test]
        public void UpdateProduct()
        {
            //Act
            target = arr[2];
            target.TenSP = "Name to edit";
            //Action
            var result = _service.EditProduct(target);
            //Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void DeleteProduct()
        {
            //Action
            var result = _service.DeleteProduct(target);
            //Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void DeleteProductNone()
        {
            //Act
            SanPham test = null;
            //Action
            var result = _service.DeleteProduct(test);
            //Assert
            Assert.IsTrue(result);
        }

        [Test()]
        public void getNewIDProductTest()
        {
            //Act 
            string ID = "SP001";
            //Action
            var result = _service.getNewIDProduct();
            //Assert 
            Assert.AreEqual(ID, result);
        }
    }
}