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
        void CheckEmployee(NhanVien employeeToCheck);
        //Queries
        NhanVien GetEmployee(String Key);
        IEnumerable<NhanVien> ListEmployees();
        IEnumerable<NhanVien> SearchEmployees(String Key);
        IEnumerable<NhanVien> SearchEmployeesbyName(String Key);
        IEnumerable<NhanVien> SearchEmployessbyPosition(String Key);
    }
}
