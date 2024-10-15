using CareerHub.BusinessLayer.Repository;
using CareerHub.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.BusinessLayer.Exceptions;

namespace CareerHub.BusinessLayer.Service
{
    internal class JobApplicationService : IJobApplicationService
    {
        private readonly IJobApplicationRepository _jobApplicationRepository;

        public JobApplicationService(IJobApplicationRepository jobApplicationRepository)
        {
            _jobApplicationRepository = jobApplicationRepository;
        }

        public bool AddJobApplication(JobApplication application)
        {
            try
            {
                return _jobApplicationRepository.AddJobApplication(application);
            }
            catch (ApplicationDeadlineException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<JobApplication> GetApplicationsForJob(int jobID)
        {
            return _jobApplicationRepository.GetApplicationsForJob(jobID);
        }
    }
}
