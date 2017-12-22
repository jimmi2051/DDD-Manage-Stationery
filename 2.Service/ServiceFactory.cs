using MyProject.Infrastructure;
using MyProject.Repository.RAW;
using MyProject.Repository.ADONET;
namespace MyProject.Service
{
    public class DataFactory
    {
        public static LoginService getLoginService(ModelStateDictionary ModelState, string key)
        {
            LoginService loginService = null;
            
            if (key == "RAW")
                loginService = new LoginService(new ModelStateWrapper(ModelState), new LoginRepositoryRAW());           
            if (key == "ADONET")
                loginService = new LoginService(new ModelStateWrapper(ModelState),new LoginRepositoryADONET());
            if (key == "EF")
                loginService = new LoginService(new ModelStateWrapper(ModelState));
            return loginService;
       }
        public static Manager_Service getManagerService(ModelStateDictionary ModelState, string key)
        {
            Manager_Service managerService = null;
            if (key == "RAW")
                managerService = new Manager_Service(
                    new ModelStateWrapper(ModelState),
                    new ProductRepositoryRAW(),
                    new SupplierRepositoryRAW(),
                    new EmployeeRepositoryRAW(),
                    new CategoryRepositoryRAW(),
                    new UserRepositoryRAW()
                    );
            if (key == "ADONET")
                managerService = new Manager_Service(
                    new ModelStateWrapper(ModelState),
                    new ProductRepositoryADONET(),
                    new SupplierRepositoryADONET(),
                    new EmployeeRepositoryADONET(),
                    new CategoryRepositoryADONET(),
                    new UserRepositoryADONET()                   
                    );        
            if(key == "EF")
                managerService = new Manager_Service(new ModelStateWrapper(ModelState)); 
            return managerService;
        }
        public static SellerService getSellerService(ModelStateDictionary ModelState, string key)
        {
            SellerService sellerService = null;
            if (key == "RAW")
            {
                sellerService = new SellerService(
                    new ModelStateWrapper(ModelState), 
                    new BillRepositoryRAW(), 
                    new ProductRepositoryRAW(),
                    new BillDetailRepositoryRAW(),
                    new CustomerRepositoryRAW(),
                    new CodesRepositoryRAW());
            }
            if (key == "ADONET")
                sellerService = new SellerService(
                    new ModelStateWrapper(ModelState),
                    new BillRepositoryADONET(),
                    new ProductRepositoryADONET(),
                    new BillDetailRepositoryADONET(),
                    new CustomerRepositoryADONET(),
                    new CodesRepositoryADONET()
                    );
            if(key =="EF")
                sellerService = new SellerService(new ModelStateWrapper(ModelState));         
            return sellerService;
        }
        public static WareHouseService getWareHouseService(ModelStateDictionary ModelState, string key)
        {
            WareHouseService warehouseService = null;
            if (key == "RAW")
            {
                warehouseService = new WareHouseService(
                    new ModelStateWrapper(ModelState),
                    new WareHouseRepositoryRAW(),
                    new CouponRepositoryRAW(),
                    new DetailCouponRepositoryRAW()
                    );         
            }
            if (key == "ADONET")
                warehouseService = new WareHouseService(
                    new ModelStateWrapper(ModelState),
                    new WareHouseRepositoryADONET(),
                    new CouponRepositoryADONET(),
                    new DetailCouponRepositoryADONET()
                    );
            if (key == "EF")
            {
                warehouseService = new WareHouseService(new ModelStateWrapper(ModelState));
            }
            return warehouseService;

        }
        public static CodeService getCodeService(ModelStateDictionary ModelState, string key)
        {
            CodeService codeService = null;
            if (key == "RAW")            
                codeService = new CodeService(new ModelStateWrapper(ModelState), new CodesRepositoryRAW());            
            if (key == "ADONET")
                codeService = new CodeService(new ModelStateWrapper(ModelState), new CodesRepositoryADONET());
            if(key =="EF")
                codeService = new CodeService(new ModelStateWrapper(ModelState));
            return codeService;
        }
    }
}
