using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_USING_STRUCTURE.Constants
{
    class SqlQuery
    {
        public const String GettingAllEmployee = "SELECT * FROM Employee";

        public const String InsertIntoEmployee = "INSERT INTO Employee (FirstName, LastName, Email, Position, Salary) VALUES (@FirstName, @LastName, @Email, @Position, @Salary)";

        public const String UpdateEmployee = "UPDATE Employee SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Position = @Position, Salary = @Salary WHERE EmployeeID = @EmployeeID";
        
        public const String DeleteEmployee = "DELETE FROM Employee WHERE EmployeeID = @EmployeeID";

        public const String GetEmployeebyID = "SELECT * FROM Employee WHERE EmployeeID = @EmployeeID";

    }
}
