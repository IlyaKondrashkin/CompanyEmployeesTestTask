using NUnit.Framework;

using CompanyEmployeesTestTask.Implementations;
using System;

namespace CompanyEmployeesTestTaskTests
{
    public class ManagerTests
    {
        private readonly TestHelper _helper = new TestHelper();

        [TestCase("01.01.2000", 100)]
        [TestCase("01.01.2001", 106.0025)]
        [TestCase("01.01.2002", 111.542575)]
        public void CalculateSalary_SomeCorrectData_ExpectedSalarySumm(
            string onCalculationDate, decimal expectedSalary)
        {
            // Arrange.
            var calculationDate = DateTime.ParseExact(onCalculationDate,
                TestHelper.DATE_FORMAT, _helper.TestsCulture);
            var manager = new Manager(TestHelper.EMPLOYEE_NAME, new DateTime(2000, 1, 1), 100);
            var subordinate_1 = new Employee(TestHelper.EMPLOYEE_NAME, new DateTime(2001, 1, 1), 100);
            var subordinate_2 = new Employee(TestHelper.EMPLOYEE_NAME, new DateTime(2002, 1, 1), 100);
            var subordinate_3 = new Manager(TestHelper.EMPLOYEE_NAME, new DateTime(2001, 1, 1), 100);
            var subordinate_3_1 = new Employee(TestHelper.EMPLOYEE_NAME, new DateTime(2001, 1, 1), 100);

            subordinate_3.AddSubordinate(subordinate_3_1);
            manager.AddSubordinate(subordinate_1);
            manager.AddSubordinate(subordinate_2);
            manager.AddSubordinate(subordinate_3);

            // Act.
            var actualSalary = manager.CalculateSalary(calculationDate);

            // Assert.
            Assert.AreEqual(expectedSalary, actualSalary);
        }

        [Test]
        public void CalculateSalary_IncorrectDate_ArgumentException()
        {
            // Arrange.
            var calculationDate = new DateTime(1999, 1, 1);
            var manager = new Manager(TestHelper.EMPLOYEE_NAME, new DateTime(2000, 1, 1), 100);

            // Act, Assert.
            Assert.Throws<ArgumentException>(() =>
                manager.CalculateSalary(calculationDate),
                "Calculation date is less or equal of onboarding date.");
        }
    }
}
