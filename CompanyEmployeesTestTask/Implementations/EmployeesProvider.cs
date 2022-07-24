using CompanyEmployeesTestTask.Interfaces;
using System;
using System.Collections.Generic;

namespace CompanyEmployeesTestTask.Implementations
{
    /// <summary>
    /// Implements an employees provider.
    /// </summary>
    public class EmployeesProvider : IEmployeesProvider
    {
        private const decimal BASE_SALARY = 5000;

        private readonly Lazy<IEnumerable<IEmployee>> _employees = new Lazy<IEnumerable<IEmployee>>(
            CreateEmployeesHierarchy);

        /// <summary>
        /// Get all the employees' hierachy.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IEmployee> GetEmployeesHierarchy()
        {
            return _employees.Value;
        }

        private static IEnumerable<IEmployee> CreateEmployeesHierarchy()
        {
            var manager_1 = new Manager("Manager_1", new DateTime(2020, 1, 1), BASE_SALARY);
            var sales_2 = new Sales("Sales_2", new DateTime(2022, 4, 1), BASE_SALARY);

            manager_1.AddSubordinate(new Employee("Employee_1_1", new DateTime(2020, 1, 1), BASE_SALARY));
            manager_1.AddSubordinate(new Employee("Employee_1_2", new DateTime(2021, 2, 2), BASE_SALARY));
            manager_1.AddSubordinate(new Employee("Employee_1_3", new DateTime(2021, 2, 2), BASE_SALARY));
            var sales_1_4 = new Sales("Sales_1_4", new DateTime(2020, 1, 1), BASE_SALARY);
            manager_1.AddSubordinate(sales_1_4);
            var manager_1_5 = new Manager("Manager_1_5", new DateTime(2021, 10, 5), BASE_SALARY);
            manager_1.AddSubordinate(manager_1_5);

            var manager_2_1 = new Manager("Manager_2_1", new DateTime(2021, 10, 5), BASE_SALARY);
            sales_2.AddSubordinate(manager_2_1);

            sales_1_4.AddSubordinate(new Employee("Employee_1_4_1", new DateTime(2020, 1, 1), BASE_SALARY));
            sales_1_4.AddSubordinate(new Employee("Employee_1_4_2", new DateTime(2021, 2, 2), BASE_SALARY));
            var sales_1_4_3 = new Sales("Sales_1_4_3", new DateTime(2022, 3, 10), BASE_SALARY);
            sales_1_4.AddSubordinate(sales_1_4_3);
            var manager_1_4_4 = new Manager("Manager_1_4_4", new DateTime(2022, 5, 10), BASE_SALARY);
            sales_1_4.AddSubordinate(manager_1_4_4);

            manager_1_5.AddSubordinate(new Employee("Employee_1_5_1", new DateTime(2021, 10, 5), BASE_SALARY));
            manager_1_5.AddSubordinate(new Employee("Employee_1_5_2", new DateTime(2022, 2, 2), BASE_SALARY));

            manager_2_1.AddSubordinate(new Employee("Employee_2_1_1", new DateTime(2020, 1, 1), BASE_SALARY));
            manager_2_1.AddSubordinate(new Employee("Employee_2_1_2", new DateTime(2021, 2, 2), BASE_SALARY));
            manager_2_1.AddSubordinate(new Employee("Employee_2_1_3", new DateTime(2021, 2, 2), BASE_SALARY));

            sales_1_4_3.AddSubordinate(new Employee("Employee_1_4_3_1", new DateTime(2022, 3, 12), BASE_SALARY));
            sales_1_4_3.AddSubordinate(new Employee("Employee_1_4_3_2", new DateTime(2022, 4, 15), BASE_SALARY));

            manager_1_4_4.AddSubordinate(new Employee("Employee_1_4_4_1", new DateTime(2022, 5, 10), BASE_SALARY));

            return new IEmployee[]
            {
                manager_1,
                sales_2,
                new Employee("Employee_3", new DateTime(2020, 1, 1), BASE_SALARY)
            };
        }
    }
}
