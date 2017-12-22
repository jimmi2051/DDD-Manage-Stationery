using System;
using System.Collections.Generic;
namespace MyProject.Domain
{
    public interface IBillDetailRespository
    {
        ChiTietHoaDon createBillDetail(ChiTietHoaDon billDetailToCreate);
        void deleteBillDetail(ChiTietHoaDon billDetailToDelete);
        ChiTietHoaDon editBillDetail(ChiTietHoaDon billDetailToEdit);
        ChiTietHoaDon getBillDetail(String ID,String key);
        IEnumerable<ChiTietHoaDon> ListBillDetailByID(String key);
        void deleteBillDetailbyID(String key);
        ChiTietHoaDon getFirstBillDetail(String Key);
    }
}
