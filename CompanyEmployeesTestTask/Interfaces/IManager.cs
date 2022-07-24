using System;
using System.Collections.Generic;

namespace CompanyEmployeesTestTask.Interfaces
{
    /// <summary>
    /// Interface of a manager.
    /// </summary>
    public interface IManager : IEmployee
    {
        /// <summary>
        /// Provides a list of manager's subordinates.
        /// </summary>
        IEnumerable<IEmployee> Employees { get; }

        /// <summary>
        /// Add a new subordinator.
        /// </summary>
        /// <param name="employee">A new subordinate.</param>
        void AddSubordinate(IEmployee employee);

        /// <summary>
        /// Remove a subordinate.
        /// </summary>
        /// <param name="employee">A removing emploee.</param>
        /// <returns>True if the subordinate is removed successfully.</returns>
        bool RemoveSubordinate(IEmployee employee);

        /// <summary>
        /// Provides summ of salaries of all the 1 level subordinates.
        /// </summary>
        /// <param name="date">Salaries calculation date.</param>
        /// <returns>Summ of salaries.</returns>
        decimal GetFirstLevelSubordinatesSalarySumm(DateTime date);

        /// <summary>
        /// Provides summ of salaries of all the levels of subordinates.
        /// </summary>
        /// <param name="date">Salaries calculation date.</param>
        /// <returns>Summ of salaries.</returns>
        decimal GetAllLevelsSubordinatesSalarySumm(DateTime date);
    }
}
