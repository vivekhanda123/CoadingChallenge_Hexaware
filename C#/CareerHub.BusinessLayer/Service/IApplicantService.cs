using CareerHub.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.BusinessLayer.Service
{
    internal interface IApplicantService
    {
        bool AddApplicant(Applicant applicant);
        List<Applicant> GetApplicants();
    }
}
