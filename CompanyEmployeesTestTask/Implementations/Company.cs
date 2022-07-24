using CompanyEmployeesTestTask.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyEmployeesTestTask.Implementations
{
    /// <summary>
    /// Implements a company.
    /// </summary>
    public class Company : ICompany
    {
        private readonly Lazy<IManager> _employeesHierarchy;
        private readonly Lazy<IDictionary<string, IEmployee>> _employeesDictionary;

        /// <summary>
        /// Get all the employees of the company.
        /// </summary>
        public IEnumerable<IEmployee> Employees => _employeesDictionary.Value.Values;

        /// <summary>
        /// Company constructor.
        /// </summary>
        public Company(IEmployeesProvider emploeesProvider)
        {
            _employeesHierarchy = new Lazy<IManager>(() =>
            {
                var root = new Manager(this.ToString(), DateTime.MinValue, 0);
                var employees = emploeesProvider.GetEmployeesHierarchy();

                foreach (var employee in employees)
                {
                    root.AddSubordinate(employee);
                }

                return root;
            });

            _employeesDictionary = new Lazy<IDictionary<string, IEmployee>>(
                CreateEmployeesDict(_employeesHierarchy.Value.Employees));
        }

        /// <summary>
        /// Calculate employeer's salary on a date.
        /// </summary>
        public decimal CalculateEmployeeSalary(string employeeName, DateTime date)
        {
            return _employeesDictionary.Value[employeeName].CalculateSalary(date);
        }

        /// <summary>
        /// Calculate summ of salaries of all the employeers in the company on a date.
        /// </summary>
        /// <returns></returns>
        public decimal CalculateTotallSalary(DateTime date)
        {
            return _employeesHierarchy.Value.GetAllLevelsSubordinatesSalarySumm(date);
        }

        /// <summary>
        /// Check if an employee with a mentioned name works for the company on a mentioned date.
        /// </summary>
        /// <param name="name">Employee's name.</param>
        /// <param name="date">Checking date.</param>
        /// <returns>True if the employee works for the company on the mentioned date.</returns>
        public bool HasEmployee(string name, DateTime date)
        {
            return _employeesDictionary.Value.ContainsKey(name) &&
                (_employeesDictionary.Value[name].OnBoardingDate.Year < date.Year ||
                (_employeesDictionary.Value[name].OnBoardingDate.Year == date.Year &&
                _employeesDictionary.Value[name].OnBoardingDate.Month <= date.Month));
        }

        private static IDictionary<string, IEmployee> CreateEmployeesDict(IEnumerable<IEmployee> employeesHierarchy)
        {
            var employeesDict = new Dictionary<string, IEmployee>();
            var employees = employeesHierarchy.ToList();

            for(var i = 0; i < employees.Count; i++)
            {
 
                employeesDict.Add(employees[i].Name, employees[i]);

                if (employees[i] is IManager manager && manager.Employees != null)
                {
                    employees.AddRange(manager.Employees);
                }
            }

            return employeesDict;
        }
    }
}
