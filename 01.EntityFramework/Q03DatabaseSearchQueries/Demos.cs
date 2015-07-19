using System;
using System.Data.Entity;
using System.Linq;

namespace Q03DatabaseSearchQueries
{
    class Demos
    {
        static public void FindEmployeesManagersAndProjectByYear(int y1, int y2)
        {
            using (SoftUniEntities context = new SoftUniEntities())
            {
                // 1. Find all employees who have projects started in the time period 2001 - 2003 (inclusive). 
                // Select the project's name, start date, end date and manager name.

                var proj = from p in context.Projects
                    where p.StartDate >= new DateTime(y1, 1, 1) && p.EndDate <= new DateTime(y2, 12, 31)
                    select new
                    {
                        ProjectName = p.Name,
                        Start = p.StartDate,
                        End = p.EndDate,
                        Employees =
                            (
                                from e in p.Employees
                                select new
                                {
                                    e.FirstName,
                                    e.LastName,
                                    e.EmployeeID,
                                    Manager =
                                        (
                                            from m in context.Employees
                                            where m.EmployeeID == e.ManagerID
                                            select new
                                            {
                                                m.FirstName,
                                                m.LastName
                                            }
                                            )
                                }
                                )
                    };

                foreach (var p in proj)
                {
                    Console.WriteLine("{0}", p.ProjectName);
                    Console.WriteLine("     => Start: {0}", p.Start);
                    Console.WriteLine("     => End: {0}", p.End);
                    foreach (var e in p.Employees)
                    {
                        Console.WriteLine("     |||||================");
                        Console.WriteLine("     |||||=> Employee: {0} {1} {2}", e.FirstName, e.LastName, e.EmployeeID);
                        foreach (var m in e.Manager)
                        {
                            Console.WriteLine("     |||||=> Manager: {0} {1}", m.FirstName, m.LastName);
                        }
                        Console.WriteLine("     |||||================");
                    }
                }
            }
        }

        static public void FindAddresses()
        {
            // 2. Find all addresses, ordered by the number of employees who live there (descending), 
            // then by town name (ascending). 
            // Take only the first 10 addresses and select their address text, town name and employee count.
            SoftUniEntities context = new SoftUniEntities();
            using (context)
            {
                var addresses = context.Addresses.OrderByDescending(a => a.Employees.Count)
                    .ThenBy(a => a.Town.Name)
                    .Take(10)
                    .Select(a => new
                    {
                        Address = a.AddressText,
                        Count = a.Employees.Count,
                        TownName = a.Town.Name
                    });

                foreach (var address in addresses)
                {
                    Console.WriteLine("{0}, {2} - {1} employees", address.Address, address.Count, address.TownName);
                }
            }
        }

        static public void GetEmployeeByIdAndPrintInfo(int id)
        {
            ////3. Get an employee by id (e.g. 147).
            //// Select only his/her first name, last name, job title and projects (only their names). 
            //// The projects should be ordered by name (ascending).

            SoftUniEntities context = new SoftUniEntities();
            using (context)
            {
                var employee =
                from e in context.Employees
                where e.EmployeeID == id
                select new
                {
                    e.FirstName,
                    e.LastName,
                    e.JobTitle,
                    proj = e.Projects.OrderBy(p => p.Name).Select(p => p.Name)
                };

                foreach (var e in employee)
                {
                    Console.WriteLine("{0} {1}: {2}, Projects: {3}",
                        e.FirstName, e.LastName, e.JobTitle,
                        string.Join(", ", e.proj.ToArray()));
                }
            }
        }

        static public void FindDepartmentsWithMoreThanXEmpleyees(int minAmount)
        {
            // 4. Find all departments with more than 5 employees. 
            // Order them by employee count (ascending). 
            // Select the department name, manager name and first name, last name, 
            // hire date and job title of every employee.

            SoftUniEntities context = new SoftUniEntities();
            using (context)
            {
                var departments =
                from d in context.Departments
                where d.Employees.Count > minAmount
                orderby d.Employees.Count
                select new
                {
                    DepartmentName = d.Name,
                    Manager = (from m in context.Employees
                               where m.EmployeeID == d.ManagerID
                               select new { Name = m.FirstName + " " + m.LastName }).FirstOrDefault().Name,
                    EmployeeCount = d.Employees.Count
                };


                foreach (var department in departments)
                {
                    Console.WriteLine("--{0} - Manager: {1}, Employees: {2}",
                        department.DepartmentName,
                        department.Manager,
                        department.EmployeeCount
                        );
                }
            }
        }
        static void Main()
        {
            // Sorry for putting everything in a single main method. 
            // Just uncomment the appropriate line (ctrl+k & ctrl+u) and run the code
            // Once you have tested the solution you can comment back the code  (ctrl+k & ctrl+c) 
            // Thanks for understanding.

            FindEmployeesManagersAndProjectByYear(2001, 2003);

            //FindAddresses();

            //GetEmployeeByIdAndPrintInfo(147);

            //FindDepartmentsWithMoreThanXEmpleyees(5);
            
        }
    }
}
