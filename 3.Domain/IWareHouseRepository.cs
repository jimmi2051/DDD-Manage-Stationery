using System;
using System.Collections.Generic;
namespace MyProject.Domain
{
    public interface IWareHouseRepository
    {
        Kho CreateWareHouse(Kho warehouseToCreate);
        Kho UpdateWareHouse(Kho warehouseToUpdate);
        void DeleteWareHouse(Kho warehouseToDelete);
        Kho getWareHouse(String msp);
        IEnumerable<Kho> listWareHouses();
        IEnumerable<Kho> StatisticalWareHouse(String sqlcmd);
    }
}
