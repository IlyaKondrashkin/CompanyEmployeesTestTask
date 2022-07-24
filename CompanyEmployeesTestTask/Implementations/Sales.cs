using System;

namespace CompanyEmployeesTestTask.Implementations
{
    /// <summary>
    /// Implemets a sales.
    /// </summary>
    public class Sales : AbstractManager
    {
        private const decimal BASE_SENIORITY_BONUS_RATE = 0.01m; // 1% per year.
        private const decimal BASE_SENIORITY_BONUS_THRESHOLD = 0.35m; // 35% thresold.
        private const decimal BASE_MANAGEMENT_BONUS_RATE = 0.003m; // 0.3% per subordinate.

        public Sales(string name, DateTime onBoardingDate, decimal baseSalary)
            : base(name, onBoardingDate, baseSalary)
        {
            SeniorityBonusRate = BASE_SENIORITY_BONUS_RATE;
            SeniorityBonusThreshold = BASE_SENIORITY_BONUS_THRESHOLD;
            ManagementBonusRate = BASE_MANAGEMENT_BONUS_RATE;
        }

        protected override decimal CalculateManagementBonus(DateTime date)
        {
            return GetAllLevelsSubordinatesSalarySumm(date) * ManagementBonusRate;
        }
    }
}
