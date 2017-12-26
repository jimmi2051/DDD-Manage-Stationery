using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository
{
    public class EmployeeRepository : IEmployeeRepository 
    {
        QLVanPhongEntities _entities = QLVanPhongEntities.Instance;       
        #region Commands
        public NhanVien CreateEmployee(NhanVien employeeToCreate)
        {
            _entities.NhanViens.Add(employeeToCreate);
            _entities.SaveChanges();
            return employeeToCreate;
        }
        public void DeleteEmployee(NhanVien employeeToDelete)
        {
            _entities.NhanViens.Remove(GetEmployee(employeeToDelete.MaNV));
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
           return _entities.NhanViens.Where(c => c.MaNV.Equals(Key)).FirstOrDefault();
        }
        public IEnumerable<NhanVien> ListEmployees()
        {
            return _entities.NhanViens.ToList();
        }
     
        #endregion
    }
}
