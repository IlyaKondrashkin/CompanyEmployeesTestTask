
using System;

namespace CompanyEmployeesTestTask.Interfaces
{
    /// <summary>
    /// Interface of an employee.
    /// </summary>
    public interface IEmployee
    {
        /// <summary>
        /// Employee's name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Employee's onboarding date.
        /// </summary>
        DateTime OnBoardingDate { get; set; }

        /// <summary>
        /// Employee's manager. null if the employee does not have a boss.
        /// </summary>
        IManager Manager { get; set; }

        /// <summary>
        /// Employee's base salary.
        /// </summary>
        decimal BaseSalary { get; set; }

        /// <summary>
        /// Calculate employee's salary on a date.
        /// </summary>
        /// <param name="date">Date of a salary.</param>
        /// <returns>Calculated salary.</returns>
        decimal CalculateSalary(DateTime date);
    }
}
