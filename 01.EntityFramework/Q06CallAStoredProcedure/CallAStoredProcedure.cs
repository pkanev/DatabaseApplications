using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q06CallAStoredProcedure
{
    class CallAStoredProcedure
    {
        static public void GetProjectsByEmployee(string fname, string lName)
        {
            SoftUniEntities db = new SoftUniEntities();
            using (db)
            {
                var projects = db.usp_FindAllProjectsPerEmployee(fname, lName);    
                foreach (var p in projects)
                {
                    Console.WriteLine("{0} - {1}, {2}", p.Name, p.Description, p.StartDate);
                }
            }
        }
        static void Main(string[] args)
        {
            GetProjectsByEmployee("Ruth", "Ellerbrock");

        }
    }
}
