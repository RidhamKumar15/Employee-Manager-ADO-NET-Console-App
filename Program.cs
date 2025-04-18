using CRUD_USING_STRUCTURE.Models;
using CRUD_USING_STRUCTURE.Services;
using CRUD_USING_STRUCTURE.Constants;
using CRUD_USING_STRUCTURE.Helpers;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        string ConnectionString = "Server=localhost;DataBase=ADO DOT NET ;Integrated Security=True; TrustServerCertificate=True";
        EmployeeServices employeeServices = new EmployeeServices(ConnectionString);
        bool running = true;
        // this is for printing the emoji in console 
        Console.OutputEncoding = System.Text.Encoding.UTF8;



        void PrintWithColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        void PrintRainbowLine(string text, int colorIndex)
        {
            ConsoleColor[] colors = {
        ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green,
        ConsoleColor.Cyan, ConsoleColor.Blue, ConsoleColor.Magenta
            };

            Console.ForegroundColor = colors[colorIndex % colors.Length];
            Console.WriteLine(text);
            Console.ResetColor();
        }

        // Inside your loop
        int colorIndex = 0;
        while (running)
        {
            
            Console.Clear();
            PrintRainbowLine(" ========================================= ", colorIndex++);
            PrintRainbowLine("        🚀 EMPLOYEE CRUD APPLICATION 🚀     ", colorIndex++);
            PrintRainbowLine(" ========================================= ", colorIndex++);

            PrintWithColor("               Select an Option           ", ConsoleColor.White);
            PrintWithColor(" ----------------------------------------- ", ConsoleColor.DarkGray);
            PrintWithColor(" 1️⃣  Add Employee", ConsoleColor.Yellow);
            PrintWithColor(" 2️⃣  View Employees", ConsoleColor.Green);
            PrintWithColor(" 3️⃣  Update Employee", ConsoleColor.Cyan);
            PrintWithColor(" 4️⃣  Delete Employee", ConsoleColor.Red);
            PrintWithColor(" 5️⃣  Find Employee by ID", ConsoleColor.Blue);
            PrintWithColor(" 6️⃣  🔎 Enter search keyword ", ConsoleColor.Blue);
            PrintWithColor(" 7️⃣  Exit", ConsoleColor.Magenta);
            PrintWithColor(" ----------------------------------------- ", ConsoleColor.DarkGray);

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("👉 Your choice: ");
            string input = Console.ReadLine();
            Console.ResetColor();

            // Your switch-case logic continues here...

            switch (input)
            {
                case "1":
                    Console.Write("Enter First Name: ");
                    string firstName = Console.ReadLine();

                    Console.Write("Enter Last Name: ");
                    string lastName = Console.ReadLine();

                    Console.Write("Enter Email: ");
                    string email = Console.ReadLine();

                    Console.Write("Enter Position: ");
                    string position = Console.ReadLine();

                    Console.Write("Enter Salary: ");
                    string salaryInput = Console.ReadLine();
                    decimal salary = ConversionHelper.ConvertStringToDecimal(salaryInput);

                    employeeServices.Add(new Employee
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Position = position,
                        Salary = salary
                    });

                    Console.WriteLine("\n✅ Employee Added Successfully!");
                    break;
                case "2":
                    var employeeList = employeeServices.GetAll();
                    int pageSize = 5;
                    int totalRecords = employeeList.Count;
                    int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
                    int currentPage = 1;

                    if (totalRecords == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("⚠️  No employees to display.");
                        Console.ResetColor();
                        break;
                    }

                    while (true)
                    {
                        Console.Clear();
                        PrintWithColor($"📄 Showing page {currentPage} of {totalPages}", ConsoleColor.Cyan);
                        Console.WriteLine("---------------------------------------------------");


                        var pagedEmployees = employeeList
                            .Skip((currentPage - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

                        int index = (currentPage - 1) * pageSize + 1;
                        foreach (var emp in pagedEmployees)
                        {
                            Console.WriteLine($"{index++}. ID: {emp.EmployeeID} | Name: {emp.FirstName} {emp.LastName} | Email: {emp.Email} | Position: {emp.Position} | Salary: ₹{emp.Salary}");
                        }

                        Console.WriteLine("---------------------------------------------------");
                        Console.WriteLine("🔄 Type 'n' (next), 'p' (prev), or 'q' (quit): ");
                        string nav = Console.ReadLine()?.ToLower();

                        if (nav == "n" || nav == "next")
                        {
                            if (currentPage < totalPages)
                                currentPage++;
                            else
                            {
                                Console.WriteLine("🚫 You're on the last page.");
                                Console.ReadKey();
                            }
                        }
                        else if (nav == "p" || nav == "prev")
                        {
                            if (currentPage > 1)
                                currentPage--;
                            else
                            {
                                Console.WriteLine("🚫 You're on the first page.");
                                Console.ReadKey();
                            }
                        }
                        else if (nav == "q")
                        {
                            break;
                        }
                    }
                    break;
                case "3":
                        Console.Write("Enter Employee ID to Update: ");
                        if (!int.TryParse(Console.ReadLine(), out int updateId))
                        {
                            Console.WriteLine("❌ Invalid ID.");
                            break;
                        }

                        Console.Write("New First Name: ");
                        string newFirst = Console.ReadLine();

                        Console.Write("New Last Name: ");
                        string newLast = Console.ReadLine();

                        Console.Write("New Email: ");
                        string newEmail = Console.ReadLine();

                        Console.Write("New Position: ");
                        string newPosition = Console.ReadLine();

                        Console.Write("New Salary: ");
                        string newSalaryInput = Console.ReadLine();
                        decimal newSalary = ConversionHelper.ConvertStringToDecimal(newSalaryInput);

                        employeeServices.Update(new Employee
                        {
                            EmployeeID = updateId,
                            FirstName = newFirst,
                            LastName = newLast,
                            Email = newEmail,
                            Position = newPosition,
                            Salary = newSalary
                        });

                        Console.WriteLine("\n✅ Employee Updated Successfully!");
                        break;
                case "4":
                    Console.Write("Enter Employee ID to Delete: ");
                    if (int.TryParse(Console.ReadLine(), out int UserInputId))
                    {
                        var employee = employeeServices.GetEmployee(UserInputId);
                        if (employee != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Found: {employee.FirstName} {employee.LastName} | Email: {employee.Email} | Position: {employee.Position} | Salary : {employee.Salary}");
                            Console.ResetColor();

                            Console.Write("Are you sure you want to delete this employee (y/n): ");
                            string confirm = Console.ReadLine();
                            if (confirm.ToLower() == "y")
                            {
                                employeeServices.Delete(new Employee { EmployeeID = UserInputId });
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n✅ Employee deleted successfully!");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine("\n❌ Deletion cancelled.");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n⚠️ Employee not found.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n❌ Invalid ID.");
                    }
                    break;
                case "5":
                    Console.Write("Enter Employee ID to Find: ");
                    try
                    {
                        int UserEnterEmployeeID = int.Parse(Console.ReadLine());
                        {
                            try {
                                employeeServices.GetEmployee(UserEnterEmployeeID);
                                break;
                            }
                            catch (Exception ex) 
                            {
                                Console.WriteLine("Invalid ID ");
                                    };
                        }
                    }
                    catch (Exception ex)
                    {

                        
                        {
                            Console.WriteLine("\n❌ No employee found with that ID.");
                            Console.WriteLine();
                            Console.WriteLine(ex.Message);
                        }
                    }
                        break;
                case "6":
                    try
                    {
                        String UserInput = Console.ReadLine();
                        var result = employeeServices.SearchEmployees(UserInput);

                        if(result.Count != 0)
                        {
                            foreach (var emp in result)
                            {
                                
                            Console.WriteLine($"EmployeeId: {emp.EmployeeID} | Name: {emp.FirstName} {emp.LastName} | Email: {emp.Email} | Position: {emp.Position} | Salary: {emp.Salary} ");
                            }
                        }
                        else
                        {
                            PrintWithColor("No Record Found..." ,ConsoleColor.Red);
                        }
                            break;
                    }catch (Exception ex)
                    {
                        break;

                    }


                case "7":
                        Console.WriteLine("👋 Exiting the application...");
                        running = false;
                        break;

                default:
                        for (int i = 3; i >= 0; i--)
                        {
                            Console.Clear();
                            Console.WriteLine($"❌ Invalid option. Returning in {i}...");
                            Thread.Sleep(600);
                        }
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\n🔄 Press Enter to return to menu...");
                    Console.ReadLine();
                }
            }
        }
    }

