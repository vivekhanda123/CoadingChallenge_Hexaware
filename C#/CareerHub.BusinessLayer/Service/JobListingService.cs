using CareerHub.BusinessLayer.Repository;
using CareerHub.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.BusinessLayer.Service
{
    public class JobListingService : IJobListingService
    {
        private readonly IJobListingRepository _jobListingRepository;

        public JobListingService(IJobListingRepository jobListingRepository)
        {
            _jobListingRepository = jobListingRepository;
        }

        public bool AddJobListing(JobListing job)
        {
            // Business logic (if any) goes here
            return _jobListingRepository.AddJobListing(job);
        }

        public List<JobListing> GetJobListings()
        {
            return _jobListingRepository.GetJobListings();
        }
    }
}
