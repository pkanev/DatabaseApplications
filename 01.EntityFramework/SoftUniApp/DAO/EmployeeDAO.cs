using System;
using System.Data.Entity;

namespace SoftUniApp.DAO
{
    public static class EmployeeDAO
    {
        /// <summary>
        /// Adds an employee ot type Employee to the database. Prints the key and returns it as an int.
        /// </summary>
        /// <param name="emp">An object of type Employee</param>
        public static int Add(Employee emp)
        {
            using (SoftUniEntities context = new SoftUniEntities())
            {
                if (emp == null)
                {
                    throw new ArgumentNullException("emp", "You need to enter a valid Employee object");
                }
                context.Employees.Add(emp);
                context.SaveChanges();
                int key = emp.EmployeeID;
                Console.WriteLine(key);
                return key;
            }
        }

        /// <summary>
        /// Searches Employees by key and retrieves the match
        /// </summary>
        /// <param name="key">Object of type Id according to DB specifications</param>
        /// <returns>An object of type Employee</returns>
        public static Employee FindByKey(object key)
        {
            using (SoftUniEntities context = new SoftUniEntities())
            {
                Employee emp = context.Employees.Find(key);
                if (emp == null)
                {
                    throw new NullReferenceException("There is no employee with such Id");
                }
                return emp;
            }
        }
        
        /// <summary>
        /// Updates changes made to an object of type Employee and saves to the database
        /// </summary>
        /// <param name="emp">An object of type Employee</param>
        public static void ModifyEmployee(Employee emp)
        {
            using (SoftUniEntities context = new SoftUniEntities())
            {
                context.Entry(emp).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an employee from the database
        /// </summary>
        /// <param name="emp">An object of type Employee</param>
        public static void Delete(Employee emp)
        {
            using (SoftUniEntities context = new SoftUniEntities())
            {
                context.Entry(emp).State = EntityState.Unchanged;
                context.Employees.Remove(emp);
                context.SaveChanges();
                Console.WriteLine("An employee with Id: {0} was deleted!", emp.EmployeeID);
            }
        }
    }
}
