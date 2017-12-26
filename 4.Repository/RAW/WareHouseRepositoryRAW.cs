using System;
using System.Collections.Generic;
using MyProject.Domain;
using System.Linq;

namespace MyProject.Repository.RAW
{
    public class WareHouseRepositoryRAW : IWareHouseRepository
    {
        QLVanPhong_Context entities = QLVanPhong_Context.Instance;
        public Kho CreateWareHouse(Kho warehouseToCreate)
        {
            throw new NotImplementedException();
        }

        public void DeleteWareHouse(Kho warehouseToDelete)
        {
            throw new NotImplementedException();
        }

        public Kho getWareHouse(string msp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Kho> listWareHouses()
        {
            return entities.Khoes.ToList();
        }
        public IEnumerable<Kho> StatisticalWareHouse(string sqlcmd)
        {
            throw new NotImplementedException();
        }

        public Kho UpdateWareHouse(Kho warehouseToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
