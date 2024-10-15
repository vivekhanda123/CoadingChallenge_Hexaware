using CareerHub.BusinessLayer.Repository;
using CareerHub.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.BusinessLayer.Service
{
    internal class SalaryRangeService : ISalaryRangeService
    {
        private readonly ISalaryRangeRepository _salaryRepository;

        public SalaryRangeService(ISalaryRangeRepository salaryRepository)
        {
            _salaryRepository = salaryRepository;
        }

        public List<JobListing> GetJobsInSalaryRange(decimal minSalary, decimal maxSalary)
        {
            try
            {
                if (minSalary < 0 || maxSalary < 0)
                {
                    throw new ArgumentOutOfRangeException("Salary values must be non-negative.");
                }

                return _salaryRepository.GetJobsInSalaryRange(minSalary, maxSalary);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<JobListing>(); // Return an empty list on error
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving jobs: {ex.Message}");
                return new List<JobListing>(); // Return an empty list on unexpected errors
            }
        }
    }
}
