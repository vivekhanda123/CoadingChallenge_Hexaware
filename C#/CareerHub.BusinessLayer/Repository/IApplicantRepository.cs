using CareerHub.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.BusinessLayer.Repository
{
    public interface IApplicantRepository
    {
        bool AddApplicant(Applicant applicant);
        List<Applicant> GetApplicants();
    }
}
