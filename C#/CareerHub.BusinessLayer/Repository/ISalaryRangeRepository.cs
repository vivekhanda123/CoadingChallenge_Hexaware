using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Entity;

namespace CareerHub.BusinessLayer.Repository
{
    public interface ISalaryRangeRepository
    {
        List<JobListing> GetJobsInSalaryRange(decimal minSalary, decimal maxSalary);
    }
}
