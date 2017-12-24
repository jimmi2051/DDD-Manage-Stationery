using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository
{    
    public class WareHouseRepository:IWareHouseRepository
    {
        QLVanPhongEntities _entities = QLVanPhongEntities.Instance;
        public Kho CreateWareHouse(Kho warehouseToCreate)
        {
            _entities.Khoes.Add(warehouseToCreate);
            _entities.SaveChanges();
            return warehouseToCreate;
        }
        public Kho UpdateWareHouse(Kho warehouseToUpdate)
        {
            var target = getWareHouse(warehouseToUpdate.MaSP);
            _entities.Entry(target).CurrentValues.SetValues(warehouseToUpdate);
            _entities.SaveChanges();
            return warehouseToUpdate;
        }
        public void DeleteWareHouse(Kho warehouseToDelete)
        {
            _entities.Khoes.Remove(warehouseToDelete);
            _entities.SaveChanges();
        }
        public Kho getWareHouse(String msp)
        {
            return _entities.Khoes.Where(c =>c.MaSP.Equals(msp)).FirstOrDefault();
        }
        public IEnumerable<Kho> listWareHouses()
        {
            return _entities.Database.SqlQuery<Kho>("SELECT * FROM KHO").ToList(); 
        }
        public IEnumerable<Kho> searchWareHouse(String key)
        {
            return _entities.Khoes.Where(c=>c.MaSP.Contains(key)).ToList();
        }
        public IEnumerable<Kho> searchWareHouseBy(String key)
        {
            return _entities.Khoes.Where(c => c.MaPhieu.Contains(key)).ToList();
        }

        public IEnumerable<Kho> StatisticalWareHouse(string sqlcmd)
        {
            return _entities.Database.SqlQuery<Kho>(sqlcmd);
        }
    }
}
