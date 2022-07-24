
using System;
using System.Collections.Generic;

namespace CompanyEmployeesTestTask.Interfaces
{
    public interface ICompany
    {
        /// <summary>
        /// Get all the employees of the company.
        /// </summary>
        IEnumerable<IEmployee> Employees { get; }

        /// <summary>
        /// Calculate employeer's salary on a date.
        /// </summary>
        decimal CalculateEmployeeSalary(string employeeName, DateTime date);

        /// <summary>
        /// Calculate summ of salaries of all the employeers in the company on a date.
        /// </summary>
        /// <returns></returns>
        decimal CalculateTotallSalary(DateTime date);

        /// <summary>
        /// Check if an employee with a mentioned name works for the company on a mentioned date.
        /// </summary>
        /// <param name="name">Employee's name.</param>
        /// <param name="date">Checking date.</param>
        /// <returns>True if the employee works for the company on the mentioned date.</returns>
        bool HasEmployee(string name, DateTime date);
    }
}
