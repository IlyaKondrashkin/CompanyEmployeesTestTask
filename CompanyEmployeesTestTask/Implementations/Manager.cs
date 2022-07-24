using System;

namespace CompanyEmployeesTestTask.Implementations
{
    /// <summary>
    /// Implements a manager.
    /// </summary>
    public class Manager : AbstractManager
    {
        private const decimal BASE_SENIORITY_BONUS_RATE = 0.05m; // 5% per year.
        private const decimal BASE_SENIORITY_BONUS_THRESHOLD = 0.4m; // 40% thresold.
        private const decimal BASE_MANAGEMENT_BONUS_RATE = 0.005m; // 0.5% per subordinate.

        public Manager(string name, DateTime onBoardingDate, decimal baseSalary)
            : base(name, onBoardingDate, baseSalary)
        {
            SeniorityBonusRate = BASE_SENIORITY_BONUS_RATE;
            SeniorityBonusThreshold = BASE_SENIORITY_BONUS_THRESHOLD;
            ManagementBonusRate = BASE_MANAGEMENT_BONUS_RATE;
        }

        protected override decimal CalculateManagementBonus(DateTime date)
        {
            return GetFirstLevelSubordinatesSalarySumm(date) * ManagementBonusRate;
        }
    }
}
