using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q05ConcurrentDatabaseChanges
{
    class ConcurrentDbChanges
    {
        static void Main(string[] args)
        {
            SoftUniEntities db1 = new SoftUniEntities();
            SoftUniEntities db2 = new SoftUniEntities();

            var emp1 = db1.Employees.Find(5);
            var emp1Again = db2.Employees.Find(5);

            Console.WriteLine(emp1.FirstName);
            Console.WriteLine(emp1Again.FirstName);

            emp1.FirstName = "Jose";
            emp1Again.FirstName = "Enrique";
            try
            {
                db1.SaveChanges();
                db2.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ux)
            {
                Console.WriteLine(ux.Message);
            }
            

            //default settings (optimistic concurrency) allow db2.SaveChanges() to take place to be stored
            //Fixed concurrency do not allow db2.SaveChanges() to be impemented and throw a DbUpdateConcurrencyException()

        }
    }
}
