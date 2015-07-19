using System;
using System.Data.Entity.Validation;
using SoftUniApp.DAO;

namespace SoftUniApp
{
    class Demos
    {
        static void Main()
        {
            try
            {
                // Createa an object employee
                Employee emp = new Employee()
                {
                    FirstName = "Homer",
                    LastName = "Simpson",
                    MiddleName = "J",
                    JobTitle = "Safety Inspector",
                    DepartmentID = 7,
                    ManagerID = 10,
                    HireDate = DateTime.Parse("1981-01-01"),
                    Salary = 40000,
                    AddressID = 291
                };

                // Add the employee to the database
                int key = EmployeeDAO.Add(emp);

                // Search by key
                Employee createdEmp = EmployeeDAO.FindByKey(key);
                Console.WriteLine("Created Employee => {0} {1}: {2}", createdEmp.FirstName, createdEmp.LastName, createdEmp.JobTitle);

                // Modify the employee and print the new saved result
                createdEmp.FirstName = "Moe";
                createdEmp.LastName = "Sizlack";
                createdEmp.JobTitle = "Bartender";
                EmployeeDAO.ModifyEmployee(createdEmp);
                Employee modifiedEmp = EmployeeDAO.FindByKey(key);
                Console.WriteLine("Modified Employee => {0} {1}: {2}", modifiedEmp.FirstName, modifiedEmp.LastName, modifiedEmp.JobTitle);

                //Delete the employee
                EmployeeDAO.Delete(modifiedEmp);

            }
            catch (DbEntityValidationException dbeve)
            {
                Console.WriteLine(dbeve.Message);
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.Message);
            }
            catch (NullReferenceException ne)
            {
                Console.WriteLine(ne.Message);
            }
            
        }
    }
}
