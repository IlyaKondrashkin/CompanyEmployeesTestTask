using CompanyEmployeesTestTask.Implementations;
using System;
using System.Globalization;
using System.Linq;

namespace CompanyEmployeesTestTask
{
    internal class Program
    {
        private const string COMMAND_SUM = "sum";
        private const string COMMAND_EXIT = "exit";
        private const string DATE_FORMAT = "dd.MM.yyyy";

        static void Main(string[] args)
        {
            var company = new Company(new EmployeesProvider());

            Console.WriteLine("Company Employees Test Task\n");
            Console.WriteLine("List of employees' names and there onboarding date:");
            Console.WriteLine(string.Join(", ", company.Employees.Select(e =>
                $"{e.Name} ({e.OnBoardingDate.ToString(DATE_FORMAT)})")));

            var waitCommand = true;

            while(waitCommand)
            {

                Console.WriteLine("\n\nPlease enter an emploee's name to calculate him/here salary " +
                    $"or enter '{COMMAND_SUM}' to calculate totall syalary sum of all the employees in the company.");
                Console.WriteLine($"Enter '{COMMAND_EXIT}' for exit from the app.\n\n");

                var command = Console.ReadLine().Trim();

                switch (command)
                {
                    case COMMAND_EXIT:
                        waitCommand = false;
                        break;
                    case COMMAND_SUM:
                        CalculateSum(company);
                        break;
                    case "":
                        break;
                    default:
                        CalculateSalary(company, command);
                        break;
                };
            }
        }

        private static void CalculateSum(Company company)
        {
            var date = GetCalculationDate();

            if (date.HasValue)
            {
                try
                {
                    Console.WriteLine($"Salary sum is {company.CalculateTotallSalary(date.Value)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error has happend: {ex.Message}");
                }
            }
        }

        private static void CalculateSalary(Company company, string employeeName)
        {
            var date = GetCalculationDate();

            if (date.HasValue)
            {
                if (company.HasEmployee(employeeName, date.Value))
                {
                    try
                    {
                        Console.WriteLine($"{employeeName}'s salary is " +
                            $"{company.CalculateEmployeeSalary(employeeName, date.Value)}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An unexpected error has happend: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("This person did not work for the company on mentioned date.");
                }
            }
        }

        private static DateTime? GetCalculationDate()
        {
            Console.WriteLine($"Enter calculation date in the format '{DATE_FORMAT}' " +
                $"or just an empty string to enter current date.");

            var strDate = Console.ReadLine();

            if (string.Empty.Equals(strDate))
            {
                return DateTime.Now.Date;
            }

            try
            {
                return DateTime.ParseExact(strDate, DATE_FORMAT, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid date format: {ex.Message}");
            }

            return null;
        }
    }
}
