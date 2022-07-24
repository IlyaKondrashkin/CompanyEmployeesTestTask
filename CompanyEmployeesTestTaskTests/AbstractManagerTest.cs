using NUnit.Framework;

using CompanyEmployeesTestTask.Implementations;
using System;
using System.Linq;

namespace CompanyEmployeesTestTaskTests
{
    public class AbstractManagerTest
    {
        private readonly TestHelper _helper = new TestHelper();

        private class TestAbstractManager : AbstractManager
        {
            public TestAbstractManager(string name, DateTime onBoardingDate, decimal baseSalary)
                : base(name, onBoardingDate, baseSalary)
            {
            }

            protected override decimal CalculateManagementBonus(DateTime date)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void AddSubordinate_NotExistsSubordinate_SubordinateExists()
        {
            // Arrange.
            var abstractManager = new TestAbstractManager(TestHelper.EMPLOYEE_NAME,
                new DateTime(2022, 7, 22), 100);
            var expectedSubordinate = new Employee(TestHelper.EMPLOYEE_NAME,
                new DateTime(2022, 7, 22), 100);

            // Act.
            abstractManager.AddSubordinate(expectedSubordinate);

            // Assert.
            Assert.IsTrue(abstractManager.Employees.Contains(expectedSubordinate));
        }

        [Test]
        public void AddSubordinate_NotExistsSubordinate_SubordinateHasCorrectManager()
        {
            // Arrange.
            var abstractManager = new TestAbstractManager(TestHelper.EMPLOYEE_NAME,
                new DateTime(2022, 7, 22), 100);
            var subordinate = new Employee(TestHelper.EMPLOYEE_NAME,
                new DateTime(2022, 7, 22), 100);

            // Act.
            abstractManager.AddSubordinate(subordinate);

            // Assert.
            Assert.AreEqual(abstractManager, subordinate.Manager);
        }

        [Test]
        public void RemoveSubordinate_ExistsSubordinate_SubordinateNotExists()
        {
            // Arrange.
            var abstractManager = new TestAbstractManager(TestHelper.EMPLOYEE_NAME,
                new DateTime(2022, 7, 22), 100);
            var subordinate = new Employee(TestHelper.EMPLOYEE_NAME,
                new DateTime(2022, 7, 22), 100);
            abstractManager.AddSubordinate(subordinate);

            // Act.
            abstractManager.RemoveSubordinate(subordinate);

            // Assert.
            Assert.IsFalse(abstractManager.Employees.Contains(subordinate));
        }

        [Test]
        public void RemoveSubordinate_ExistsSubordinate_SubordinateDoesNotHaveManager()
        {
            // Arrange.
            var abstractManager = new TestAbstractManager(TestHelper.EMPLOYEE_NAME,
                new DateTime(2022, 7, 22), 100);
            var subordinate = new Employee(TestHelper.EMPLOYEE_NAME,
                new DateTime(2022, 7, 22), 100);
            abstractManager.AddSubordinate(subordinate);

            // Act.
            abstractManager.RemoveSubordinate(subordinate);

            // Assert.
            Assert.IsNull(subordinate.Manager);
        }

        [TestCase("01.01.2000", 0)]
        [TestCase("01.01.2001", 200.5)]
        [TestCase("01.01.2002", 308.515)]
        public void GetFirstLevelSubordinatesSalarySumm_SomeCorrectData_ExpectedSalarySumm(
            string onCalculationDate, decimal expectedSumm)
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
            var actualSumm = manager.GetFirstLevelSubordinatesSalarySumm(calculationDate);

            // Assert.
            Assert.AreEqual(expectedSumm, actualSumm);
        }
 
        [Test]
        public void GetFirstLevelSubordinatesSalarySumm_IncorrectDate_ArgumentException()
        {
            // Arrange.
            var calculationDate = new DateTime(1999, 1, 1);
            var manager = new Manager(TestHelper.EMPLOYEE_NAME, new DateTime(2000, 1, 1), 100);

            // Act, Assert.
            Assert.Throws<ArgumentException>(() =>
                manager.GetFirstLevelSubordinatesSalarySumm(calculationDate),
                "Calculation date is less or equal of onboarding date.");
        }

        [TestCase("01.01.2000", 0)]
        [TestCase("01.01.2001", 300.5)]
        [TestCase("01.01.2002", 411.515)]
        public void GetAllLevelsSubordinatesSalarySumm_SomeCorrectData_ExpectedSalarySumm(
            string onCalculationDate, decimal expectedSumm)
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
            var actualSumm = manager.GetAllLevelsSubordinatesSalarySumm(calculationDate);

            // Assert.
            Assert.AreEqual(expectedSumm, actualSumm);
        }

        [Test]
        public void GetAllLevelsSubordinatesSalarySumm_IncorrectDate_ArgumentException()
        {
            // Arrange.
            var calculationDate = new DateTime(1999, 1, 1);
            var manager = new Manager(TestHelper.EMPLOYEE_NAME, new DateTime(2000, 1, 1), 100);

            // Act, Assert.
            Assert.Throws<ArgumentException>(() =>
                manager.GetAllLevelsSubordinatesSalarySumm(calculationDate),
                "Calculation date is less or equal of onboarding date.");
        }
    }
}
