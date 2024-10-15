using CareerHub.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.BusinessLayer.Service
{
    public interface ICompanyService
    {
        bool AddCompany(Company company);
        List<Company> GetCompanies();
    }
}
