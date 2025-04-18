# EmployeeManager-ConsoleApp-ADO.NET

This is a console-based **Employee Management** application built using **ADO.NET** and **C#**. It allows you to perform CRUD (Create, Read, Update, Delete) operations on employee data with a clean and interactive console UI. This application utilizes structured architecture, separating models, services, constants, and helpers for maintainability.

## Features
- **Add Employee**: Add new employee details to the database.
- **View Employees**: Paginate and view the list of all employees.
- **Update Employee**: Modify details of an existing employee.
- **Delete Employee**: Remove an employee from the database.
- **Find Employee by ID**: Retrieve employee details by their unique ID.
- **Search Employees**: Perform keyword-based search to find employees.
- **Colorful Console UI**: Features an interactive and colorful console UI with emojis for a better user experience.
  
## Technologies Used
- **C#**
- **ADO.NET**
- **SQL Server** (or any compatible DBMS)
- **Console UI**

## Setup & Installation

### 1. Clone the repository

```bash
git clone https://github.com/RidhamKumar15/Employee-Manager-ADO-NET-Console-App

## 🛠️ How to Set Up the Database

1. Open SQL Server Management Studio (SSMS).
2. Open the file `DatabaseSetup/create-database.sql`.
3. Execute the script (F5) to create the database and table.
4. Update your connection string in `Program.cs` if needed:
   ```csharp
   string ConnectionString = "Server=localhost;Database=EmployeeCRUDapplication;Integrated Security=True;TrustServerCertificate=True";
