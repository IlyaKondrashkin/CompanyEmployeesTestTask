using NUnit.Framework;

using CompanyEmployeesTestTask.Implementations;
using System;
using CompanyEmployeesTestTask.Interfaces;
using Moq;

namespace CompanyEmployeesTestTaskTests
{
    public class CompanyTests
    {
        private readonly Mock<IEmployeesProvider> _providerMock;
        private readonly TestHelper _helper = new TestHelper();
        private Company _company;

        public CompanyTests()
        {
            var employee_1Mock = new Mock<IEmployee>();
            employee_1Mock.Setup(x => x.Name).Returns("Employee_1");
            employee_1Mock.Setup(x => x.OnBoardingDate).Returns(new DateTime(2020, 1, 1));
            employee_1Mock.Setup(x => x.CalculateSalary(It.Is<DateTime>(dt =>
                dt >= new DateTime(2020, 1, 1)))).Returns(100);
 
            var employee_2Mock = new Mock<IEmployee>();
            employee_2Mock.Setup(x => x.Name).Returns("Employee_2");
            employee_2Mock.Setup(x => x.OnBoardingDate).Returns(new DateTime(2022, 1, 1));
            employee_2Mock.Setup(x => x.CalculateSalary(It.Is<DateTime>(dt =>
                dt >= new DateTime(2022, 1, 1)))).Returns(100);

            _providerMock = new Mock<IEmployeesProvider>();
            _providerMock.Setup(m => m.GetEmployeesHierarchy()).Returns(new IEmployee[]
            {
                employee_1Mock.Object,
                employee_2Mock.Object
            });
        }

        [SetUp]
        public void SetUp()
        {
            _company = new Company(_providerMock.Object);
        }

        [TestCase("Employee_1", "01.01.2020", true)]
        [TestCase("Employee_1", "01.01.2021", true)]
        [TestCase("Employee_1", "01.01.2019", false)]
        [TestCase("Employee_2", "01.01.2022", true)]
        [TestCase("IncorrectEmployee_1", "01.01.2020", false)]
        public void HasEmployee_SomeEmploee_SomeResult(
            string emploeeName, string onboardingDateStr, bool expectedResult)
        {
            // Arrange.
            var onboardingDate = DateTime.ParseExact(onboardingDateStr,
                TestHelper.DATE_FORMAT, _helper.TestsCulture);

            // Act.
            var actualResult = _company.HasEmployee(emploeeName, onboardingDate);

            // Assert.
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CalculateEmployeeSalary_Employee_1_100()
        {
            // Arrange.
            var calculationDate = new DateTime(2020, 1, 1);
            var expectedSalary = 100m;

            // Act.
            var actualSalary = _company.CalculateEmployeeSalary("Employee_1", calculationDate);

            // Assert.
            Assert.AreEqual(expectedSalary, actualSalary);
        }

        [TestCase("01.01.2022", 200)]
        [TestCase("01.01.2021", 100)]
        [TestCase("01.01.2019", 0)]
        public void CalculateTotallSalary_SomeDate_SomeResult(
            string calculationDateStr, decimal expectedResult)
        {
            // Arrange.
            var calculationDate = DateTime.ParseExact(calculationDateStr,
                TestHelper.DATE_FORMAT, _helper.TestsCulture);

            // Act.
            var actualResult = _company.CalculateTotallSalary(calculationDate);

            // Assert.
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
