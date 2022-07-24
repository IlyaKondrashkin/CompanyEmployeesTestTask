using NUnit.Framework;

using CompanyEmployeesTestTask.Implementations;
using System;

namespace CompanyEmployeesTestTaskTests
{
    public class EmployeeTests
    {
        private readonly TestHelper _helper = new TestHelper();

        [TestCase(100, "02.01.2000", "01.01.2000", 100)]
        [TestCase(100, "01.01.2000", "01.01.2000", 100)]
        [TestCase(100, "01.01.2000", "01.12.2000", 100)]
        [TestCase(100, "01.01.2000", "01.01.2001", 103)]
        [TestCase(100, "01.01.2000", "01.01.2002", 106)]
        [TestCase(100, "01.01.2000", "01.01.2030", 130)]
        [TestCase(100, "01.01.2000", "01.01.2031", 130)]
        public void CalculateSalary_SomeCorrectData_CorrectSalary(
           decimal baseSalary,
           string onBoardingDate,
           string onCalculationDate,
           decimal expectedSalary)

        {
            // Arrange.
            var employee = new Employee(TestHelper.EMPLOYEE_NAME,
                DateTime.ParseExact(onBoardingDate, TestHelper.DATE_FORMAT, _helper.TestsCulture),
                baseSalary);
            var calculationDate = DateTime.ParseExact(onCalculationDate,
                TestHelper.DATE_FORMAT, _helper.TestsCulture);

            // Act.
            var actualSalary = employee.CalculateSalary(calculationDate);

            // Assert.
            Assert.AreEqual(expectedSalary, actualSalary);
        }

        [TestCase(100, "01.02.2000", "01.01.2000")]
        [TestCase(100, "01.01.2001", "01.01.2000")]
        public void CalculateSalary_IncorrectCalculationDate_ArgumentException(
           decimal baseSalary,
           string onBoardingDate,
           string onCalculationDate)

        {
            // Arrange.
            var employee = new Employee(TestHelper.EMPLOYEE_NAME,
                DateTime.ParseExact(onBoardingDate, TestHelper.DATE_FORMAT, _helper.TestsCulture),
                baseSalary);
            var calculationDate = DateTime.ParseExact(onCalculationDate,
                TestHelper.DATE_FORMAT, _helper.TestsCulture);

            // Act, Assert.
            Assert.Throws<ArgumentException>(() => employee.CalculateSalary(calculationDate),
                "Calculation date is less or equal of onboarding date.");
        }
    }
}