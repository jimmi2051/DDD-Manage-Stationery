namespace MyProject.Domain
{
    using System.Data.Entity;
    public partial class QLVanPhongEntities : DbContext
    {
        private static QLVanPhongEntities instance;
        public static QLVanPhongEntities Instance
        {
            get
            {
                if (instance == null)
                    instance = new QLVanPhongEntities();
                return instance;
            }
        }
    }
}
