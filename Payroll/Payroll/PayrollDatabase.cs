using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Payroll
{
    public class PayrollDatabase
    {
        #region employees
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

        public static IEnumerable<Employee> GetEmployees()
        {
            foreach (var entry in employees)
            {
                yield return entry.Value;
            }
        }

        #endregion

        #region Union Affiliation
        private static Dictionary<int, Employee> unionMembers = new Dictionary<int, Employee>();

        public static Employee GetUnionMember(int memberId)
        {
            Employee e = null;
            unionMembers.TryGetValue(memberId, out e);
            return e;
        }

        public static void AddUnionMember(int memberId, Employee e)
        {
            unionMembers.Add(memberId, e);
        }

        internal static void RemoveUnionMember(int memberId)
        {
            unionMembers.Remove(memberId);
        }
        #endregion
    }
}
