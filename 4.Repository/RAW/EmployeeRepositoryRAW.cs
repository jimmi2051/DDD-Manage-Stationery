using System;
using System.Collections.Generic;
using System.Linq;
using MyProject.Domain;
namespace MyProject.Repository.RAW
{
    public class EmployeeRepositoryRAW : IEmployeeRepository
    {
        QLVanPhong_Context _entities = QLVanPhong_Context.Instance;
        #region Commands
        public NhanVien CreateEmployee(NhanVien employeeToCreate)
        {
            _entities.NhanViens.Add(employeeToCreate);
            _entities.SaveChanges();
            return employeeToCreate;
        }
        public void DeleteEmployee(NhanVien employeeToDelete)
        {
            _entities.NhanViens.Remove(employeeToDelete);
            _entities.SaveChanges();
        }
        public NhanVien EditEmployee(NhanVien employeeToEdit)
        {
            var originalEmployee = GetEmployee(employeeToEdit.MaNV);
            if (!originalEmployee.Equals(employeeToEdit))
            {
                originalEmployee.Ten = employeeToEdit.Ten;
                originalEmployee.Diachi = employeeToEdit.Diachi;
                originalEmployee.Luong = employeeToEdit.Luong;
                originalEmployee.ChucVu = employeeToEdit.ChucVu;
                originalEmployee.sdt = employeeToEdit.sdt;
            }        
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
