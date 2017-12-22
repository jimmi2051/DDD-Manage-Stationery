using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository
{
    public class EmployeeRepository : IEmployeeRepository 
    {
        QLVanPhongEntities _entities = QLVanPhongEntities.Instance;

        public void CheckEmployee(NhanVien employeeToCheck)
        {
            _entities.UpdateLuong(employeeToCheck.MaNV);
            _entities.SaveChanges();
        }
        #region Commands
        public NhanVien CreateEmployee(NhanVien employeeToCreate)
        {
            _entities.Insert_NhanVien(employeeToCreate.MaNV, employeeToCreate.Ten, employeeToCreate.Diachi, employeeToCreate.sdt, employeeToCreate.ChucVu, employeeToCreate.Luong);
            _entities.SaveChanges();
            return employeeToCreate;
        }
        public void DeleteEmployee(NhanVien employeeToDelete)
        {
            _entities.Delete_NhanVien(employeeToDelete.MaNV);
            _entities.SaveChanges();
        }
        public NhanVien EditEmployee(NhanVien employeeToEdit)
        {
            var originalEmployee = GetEmployee(employeeToEdit.MaNV);
            _entities.Entry(originalEmployee).CurrentValues.SetValues(employeeToEdit);
           // _entities.Update_NhanVien(employeeToEdit.MaNV, employeeToEdit.Ten, employeeToEdit.Diachi, employeeToEdit.sdt, employeeToEdit.ChucVu, employeeToEdit.Luong);
            _entities.SaveChanges();
            return employeeToEdit;
        }
        #endregion
        #region Queries
        public NhanVien GetEmployee(String Key)
        {
            return _entities.Database.SqlQuery<NhanVien>("SELECT * FROM NhanVien WHERE MaNV='"+Key+"'").FirstOrDefault();
        }
        public IEnumerable<NhanVien> ListEmployees()
        {
            return _entities.Database.SqlQuery<NhanVien>("SELECT * FROM NhanVien").ToList();
        }
        public IEnumerable<NhanVien> SearchEmployees(String Key)
        {
            return _entities.NhanViens.Where(c => c.MaNV.Contains(Key)).ToList();
        }
        public IEnumerable<NhanVien> SearchEmployeesbyName(String Key)
        {
            return _entities.NhanViens.Where(c => c.Ten.Contains(Key)).ToList();
        }
        public IEnumerable<NhanVien> SearchEmployessbyPosition(String Key)
        {
            return _entities.NhanViens.Where(c => c.ChucVu.Contains(Key)).ToList();
        }
        #endregion
    }
}
