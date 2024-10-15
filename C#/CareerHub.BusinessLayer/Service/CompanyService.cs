using CareerHub.BusinessLayer.Repository;
using CareerHub.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerHub.BusinessLayer.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public bool AddCompany(Company company)
        {
            // Business logic (if any) goes here
            return _companyRepository.AddCompany(company);
        }

        public List<Company> GetCompanies()
        {
            return _companyRepository.GetCompanies();
        }
    }
}
