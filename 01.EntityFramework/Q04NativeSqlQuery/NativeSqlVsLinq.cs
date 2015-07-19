using System;
using System.Diagnostics;
using System.Linq;

namespace Q04NativeSqlQuery
{
    class NativeSqlVsLinq
    {
        static public void PrintNamesWithLinqQuery()
        {
            SoftUniEntities db = new SoftUniEntities();
            using (db)
            {
                var employees = db.Employees
                    .Where(e => e.Projects.Any(p => p.StartDate.Year == 2002))
                    .OrderBy(e => e.FirstName)
                    .Select(e => e.FirstName);
    
                foreach (var employee in employees)
                {
                    Console.WriteLine(employee);
                }
            }

        }

        static public void PrintNamesWithNativeQuery()
        {
            SoftUniEntities db = new SoftUniEntities();
            using (db)
            {
                string query =
                    "SELECT e.FirstName FROM Employees e " +
                    "JOIN EmployeesProjects ep ON e.EmployeeID = ep.EmployeeID " +
                    "JOIN Projects p ON ep.ProjectID = p.ProjectID " +
                    "WHERE DATEPART(year, p.StartDate) = 2002";
                var employees = db.Database.SqlQuery<string>(query);

                foreach (var employee in employees)
                {
                    Console.WriteLine(employee);
                }
            }
        }

        static void Main()
        {
            var sw = new Stopwatch();
            
            sw.Start();
            PrintNamesWithNativeQuery();
            Console.WriteLine("Native: {0}", sw.Elapsed);

            sw.Restart();
            PrintNamesWithLinqQuery();
            Console.WriteLine("Linq: {0}", sw.Elapsed);
        }
    }
}
