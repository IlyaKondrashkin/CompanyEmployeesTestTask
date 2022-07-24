using CompanyEmployeesTestTask.Interfaces;
using System;

namespace CompanyEmployeesTestTask.Implementations
{
    /// <summary>
    /// An employee implementation.
    /// </summary>
    public class Employee : IEmployee
    {
        private const decimal BASE_SENIORITY_BONUS_RATE = 0.03m; // 3% per year.

        private const decimal BASE_SENIORITY_BONUS_THRESHOLD = 0.3m; // 30% thresold.

        /// <summary>
        /// Employee's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Employee's onboarding date.
        /// </summary>
        public DateTime OnBoardingDate { get; set; }

        /// <summary>
        /// Employee's manager. null if the employee does not have a boss.
        /// </summary>
        public IManager Manager { get; set; }

        /// <summary>
        /// Employee's base salary.
        /// </summary>
        public decimal BaseSalary { get; set; }

        protected decimal SeniorityBonusRate { get; set; }

        protected decimal SeniorityBonusThreshold { get; set; }

        public Employee(string name, DateTime onBoardingDate, decimal baseSalary)
        {
            Name = name;
            OnBoardingDate = onBoardingDate;
            BaseSalary = baseSalary;
            SeniorityBonusRate = BASE_SENIORITY_BONUS_RATE;
            SeniorityBonusThreshold = BASE_SENIORITY_BONUS_THRESHOLD;
        }

        /// <summary>
        /// Calculate employee's salary on a date.
        /// </summary>
        /// <param name="date">Date of a salary.</param>
        /// <returns>Calculated salary.</returns>
        public decimal CalculateSalary(DateTime date)
        {
            CheckCalculationDate(date);
            return BaseSalary + CalculateTotallBonus(date);
        }

        /// <summary>
        /// Calculate totall bonus on a date.
        /// </summary>
        /// <param name="date">Date of a bonus.</param>
        /// <returns>Calculated bonus.</returns>
        protected virtual decimal CalculateTotallBonus(DateTime date) =>
            CalculateSeniorityBonus(date);


        /// <summary>
        /// Calculate employee's seniority bonus on a date.
        /// </summary>
        /// <param name="date">Date of a bonus.</param>
        /// <returns>Calculated bonus.</returns>
        protected virtual decimal CalculateSeniorityBonus(DateTime date)
        {
            var years = date.Year - OnBoardingDate.Year;

            if (date.Month < OnBoardingDate.Month)
            {
                years--;
            }
 
            if (years > 0)
            {
                return BaseSalary * Math.Min(years * SeniorityBonusRate, SeniorityBonusThreshold);
            }
 
            return 0;
        }

        /// <summary>
        /// Check if we can calculate salary on provided date.
        /// We consider that we can calculate salary for the month on which the employee was onboarded.
        /// If a calculation date is incorrect ArgumentException is thrown.
        /// </summary>
        /// <param name="date">Calculation date.</param>
        protected void CheckCalculationDate(DateTime date)
        {
            if (date.Year < OnBoardingDate.Year ||
                (date.Year == OnBoardingDate.Year && date.Month < OnBoardingDate.Month))
            {
                throw new ArgumentException("Calculation date is less or equal of onboarding date.");
            }
        }
    }
}
