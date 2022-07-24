using CompanyEmployeesTestTask.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyEmployeesTestTask.Implementations
{
    /// <summary>
    /// An abstract manager implementation.
    /// </summary>
    public abstract class AbstractManager : Employee, IManager
    {
        private readonly List<IEmployee> _employeesList;

        /// <summary>
        /// Provides a list of manager's subordinates.
        /// </summary>
        public IEnumerable<IEmployee> Employees => _employeesList;

        protected decimal ManagementBonusRate { get; set; }

        protected AbstractManager(string name, DateTime onBoardingDate, decimal baseSalary)
            : base(name, onBoardingDate, baseSalary)
        {
            _employeesList = new List<IEmployee>();
        }

        /// <summary>
        /// Add a new subordinator.
        /// </summary>
        /// <param name="employee">A new subordinate.</param>
        public void AddSubordinate(IEmployee employee)
        {
            if (employee.Manager != this)
            {
                if (employee.Manager != null)
                {
                    employee.Manager.RemoveSubordinate(employee);
                }
                _employeesList.Add(employee);
                employee.Manager = this;
            }
        }

        /// <summary>
        /// Remove a subordinate.
        /// </summary>
        /// <param name="employee">A removing emploee.</param>
        public bool RemoveSubordinate(IEmployee employee)
        {
            var isRemoved = _employeesList.Remove(employee);

            if (isRemoved)
            {
                employee.Manager = null;
            }

            return isRemoved;
        }

        /// <summary>
        /// Provides summ of salaries of all the 1 level subordinators.
        /// </summary>
        /// <param name="date">Salaries calculation date.</param>
        /// <returns>Summ of salaries.</returns>
        public decimal GetFirstLevelSubordinatesSalarySumm(DateTime date)
        {
            CheckCalculationDate(date);
            return FilterEmployeesForSalaryCalculation(date)
                .Select(e => e.CalculateSalary(date)).Sum();
        }

        /// <summary>
        /// Provides summ of salaries of all the levels of subordinators.
        /// </summary>
        /// <param name="date">Salaries calculation date.</param>
        /// <returns>Summ of salaries.</returns>
        public decimal GetAllLevelsSubordinatesSalarySumm(DateTime date)
        {
            CheckCalculationDate(date);
            return FilterEmployeesForSalaryCalculation(date).Select(e =>
                {
                    var subSum = e.CalculateSalary(date);
                    if (e is IManager m)
                    {
                        subSum += m.GetAllLevelsSubordinatesSalarySumm(date);
                    }
                    return subSum;
                }).Sum();
        }

        /// <summary>
        /// Calculate totall bonus on a date.
        /// </summary>
        /// <param name="date">Date of a bonus.</param>
        /// <returns>Calculated bonus.</returns>
        protected override decimal CalculateTotallBonus(DateTime date) =>
            CalculateSeniorityBonus(date) + CalculateManagementBonus(date);

        /// <summary>
        /// Calculate manager's management bonus on a date.
        /// </summary>
        /// <param name="date">Date of a bonus.</param>
        /// <returns>Calculated bonus.</returns>
        protected abstract decimal CalculateManagementBonus(DateTime date);

        private IEnumerable<IEmployee> FilterEmployeesForSalaryCalculation(DateTime date) =>
            Employees.Where(e => e.OnBoardingDate.Year < date.Year ||
                                 (e.OnBoardingDate.Year == date.Year && e.OnBoardingDate.Month <= date.Month));
    }
}
