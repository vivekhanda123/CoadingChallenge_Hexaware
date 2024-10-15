using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Entity;

namespace CareerHub.BusinessLayer.Repository
{
    public interface IJobListingRepository
    {
        bool AddJobListing(JobListing job);
        List<JobListing> GetJobListings();
    }
}
