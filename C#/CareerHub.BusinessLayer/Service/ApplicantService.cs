using CareerHub.BusinessLayer.Exceptions;
using CareerHub.BusinessLayer.Repository;
using CareerHub.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CareerHub.BusinessLayer.Service
{
    internal class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;

        public ApplicantService(IApplicantRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

        public bool AddApplicant(Applicant applicant)
        {
            try
            {
                ValidateEmail(applicant.Email);
                return _applicantRepository.AddApplicant(applicant);
            }
            catch (InvalidEmailFormatException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void ValidateEmail(string email)
        {
            // Simple regex for email validation
            string emailPattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                throw new InvalidEmailFormatException("Invalid email format. Please enter a valid email address.");
            }
        }

        public List<Applicant> GetApplicants()
        {
            return _applicantRepository.GetApplicants();
        }
    }
}
