using CareerHub.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.BusinessLayer.Service
{
    internal interface IJobApplicationService
    {
        bool AddJobApplication(JobApplication application);
        List<JobApplication> GetApplicationsForJob(int jobID);
    }
}
