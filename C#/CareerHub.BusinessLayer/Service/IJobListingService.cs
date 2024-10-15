using CareerHub.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.BusinessLayer.Service
{
    public interface IJobListingService
    {
        bool AddJobListing(JobListing job);
        List<JobListing> GetJobListings();
    }
}
