using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll
{
    public class PayrollDatabase
    {
        private static Dictionary<int, Employee> employees = new Dictionary<int, Employee>();

        public static void AddEmployee(int id, Employee e)
        {
            employees.Add(id, e);
        }

        public static Employee GetEmployee(int id)
        {
            Employee e;
            employees.TryGetValue(id, out e);

            return e;
        }

        internal static bool DeleteEmployee(int id)
        {
            return employees.Remove(id);
        }
    }
}
