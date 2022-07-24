using System.Collections.Generic;

namespace CompanyEmployeesTestTask.Interfaces
{
    /// <summary>
    /// Employees provider interface.
    /// </summary>
    public interface IEmployeesProvider
    {
        /// <summary>
        /// Get all the employees' hierachy.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IEmployee> GetEmployeesHierarchy();
    }
}
