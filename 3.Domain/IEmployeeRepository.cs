using System;
using System.Collections.Generic;
namespace MyProject.Domain
{
    public interface IEmployeeRepository
    {
        //Commands
        NhanVien CreateEmployee(NhanVien employeeToCreate);
        void DeleteEmployee(NhanVien employeeToDelete);
        NhanVien EditEmployee(NhanVien employeeToEdit);
        //Queries
        NhanVien GetEmployee(String Key);
        IEnumerable<NhanVien> ListEmployees();
    }
}
