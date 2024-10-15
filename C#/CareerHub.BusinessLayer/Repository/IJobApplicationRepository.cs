using CareerHub.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.BusinessLayer.Repository
{
    public interface IJobApplicationRepository
    {
        bool AddJobApplication(JobApplication application);
        List<JobApplication> GetApplicationsForJob(int jobID);
    }
}
