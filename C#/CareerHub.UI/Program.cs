using CareerHub.BusinessLayer.Repository;
using CareerHub.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CareerHub.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IJobListingRepository jobListingRepository = new JobListingRepository();
            ICompanyRepository companyRepository = new CompanyRepository();
            IApplicantRepository applicantRepository = new ApplicantRepository();
            IJobApplicationRepository jobApplicationRepository = new JobApplicationRepository();
            ISalaryRangeRepository salaryRangeRepository = new SalaryRangeRepository();

            bool exit = false;

            while (!exit)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to the Job Board Management System");
                    Console.WriteLine("Select an operation:");
                    Console.WriteLine("1. Add Company");
                    Console.WriteLine("2. Add Job Listing");
                    Console.WriteLine("3. Add Applicant");
                    Console.WriteLine("4. Submit Job Application");
                    Console.WriteLine("5. List All Job Listings");
                    Console.WriteLine("6. List All Companies");
                    Console.WriteLine("7. List All Applicants");
                    Console.WriteLine("8. View Applications for a Job");
                    Console.WriteLine("9. List Jobs in Salary Range");
                    Console.WriteLine("10. Exit");

                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddCompany(companyRepository);
                            break;

                        case "2":
                            AddJobListing(jobListingRepository);
                            break;

                        case "3":
                            AddApplicant(applicantRepository);
                            break;

                        case "4":
                            SubmitJobApplication(jobApplicationRepository);
                            break;

                        case "5":
                            ListJobListings(jobListingRepository);
                            break;

                        case "6":
                            ListCompanies(companyRepository);
                            break;

                        case "7":
                            ListApplicants(applicantRepository);
                            break;

                        case "8":
                            ViewApplicationsForJob(jobApplicationRepository);
                            break;

                        case "9":
                            ListJobsInSalaryRange(salaryRangeRepository); // New method for salary range
                            break;

                        case "10":
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                    if (!exit)
                    {
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static void AddCompany(ICompanyRepository companyRepository)
        {
            Console.Write("Enter Company Name: ");
            string companyName = Console.ReadLine();
            Console.Write("Enter Company Location: ");
            string location = Console.ReadLine();

            Company company = new Company
            {
                CompanyName = companyName,
                Location = location
            };

            bool isAdded = companyRepository.AddCompany(company);
            Console.WriteLine(isAdded ? "Company added successfully!" : "Failed to add company.");
        }

        static void AddJobListing(IJobListingRepository jobListingRepository)
        {
            Console.Write("Enter Company ID: ");
            int companyID = int.Parse(Console.ReadLine());
            Console.Write("Enter Job Title: ");
            string jobTitle = Console.ReadLine();
            Console.Write("Enter Job Description: ");
            string jobDescription = Console.ReadLine();
            Console.Write("Enter Job Location: ");
            string jobLocation = Console.ReadLine();
            Console.Write("Enter Salary: ");
            decimal salary = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Job Type (Full-time, Part-time, Contract): ");
            string jobType = Console.ReadLine();

            JobListing job = new JobListing
            {
                CompanyID = companyID,
                JobTitle = jobTitle,
                JobDescription = jobDescription,
                JobLocation = jobLocation,
                Salary = salary,
                JobType = jobType,
                PostedDate = DateTime.Now
            };

            bool isAdded = jobListingRepository.AddJobListing(job);
            Console.WriteLine(isAdded ? "Job listing added successfully!" : "Failed to add job listing.");
        }

        static void AddApplicant(IApplicantRepository applicantRepository)
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();
            Console.Write("Enter Resume (file path or reference): ");
            string resume = Console.ReadLine();

            Applicant applicant = new Applicant
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone,
                Resume = resume
            };


            bool isAdded = applicantRepository.AddApplicant(applicant);
            Console.WriteLine(isAdded ? "Applicant added successfully!" : "Failed to add applicant.");
        }

        static void SubmitJobApplication(IJobApplicationRepository jobApplicationRepository)
        {
            Console.Write("Enter Job ID: ");
            int jobID = int.Parse(Console.ReadLine());
            Console.Write("Enter Applicant ID: ");
            int applicantID = int.Parse(Console.ReadLine());
            Console.Write("Enter Cover Letter: ");
            string coverLetter = Console.ReadLine();

            JobApplication jobApplication = new JobApplication
            {
                JobID = jobID,
                ApplicantID = applicantID,
                ApplicationDate = DateTime.Now,
                CoverLetter = coverLetter
            };

            bool isAdded = jobApplicationRepository.AddJobApplication(jobApplication);
            Console.WriteLine(isAdded ? "Application submitted successfully!" : "Failed to submit application.");
        }

        static void ListJobListings(IJobListingRepository jobListingRepository)
        {
            List<JobListing> jobListings = jobListingRepository.GetJobListings();

            if (jobListings.Count == 0)
            {
                Console.WriteLine("No job listings found.");
            }
            else
            {
                foreach (var job in jobListings)
                {
                    Console.WriteLine($"Job ID: {job.JobID}, Title: {job.JobTitle}, Location: {job.JobLocation}, Salary: {job.Salary}, Type: {job.JobType}");
                }
            }
        }

        static void ListCompanies(ICompanyRepository companyRepository)
        {
            List<Company> companies = companyRepository.GetCompanies();

            if (companies.Count == 0)
            {
                Console.WriteLine("No companies found.");
            }
            else
            {
                foreach (var company in companies)
                {
                    Console.WriteLine($"Company ID: {company.CompanyID}, Name: {company.CompanyName}, Location: {company.Location}");
                }
            }
        }

        static void ListApplicants(IApplicantRepository applicantRepository)
        {
            List<Applicant> applicants = applicantRepository.GetApplicants();

            if (applicants.Count == 0)
            {
                Console.WriteLine("No applicants found.");
            }
            else
            {
                foreach (var applicant in applicants)
                {
                    Console.WriteLine($"Applicant ID: {applicant.ApplicantID}, Name: {applicant.FirstName} {applicant.LastName}, Email: {applicant.Email}");
                }
            }
        }

        static void ViewApplicationsForJob(IJobApplicationRepository jobApplicationRepository)
        {
            Console.Write("Enter Job ID: ");
            int jobID = int.Parse(Console.ReadLine());

            List<JobApplication> applications = jobApplicationRepository.GetApplicationsForJob(jobID);

            if (applications.Count == 0)
            {
                Console.WriteLine("No applications found for this job.");
            }
            else
            {
                foreach (var application in applications)
                {
                    Console.WriteLine($"Application ID: {application.ApplicationID}, Applicant ID: {application.ApplicantID}, Date: {application.ApplicationDate}, Cover Letter: {application.CoverLetter}");
                }
            }
        }

        // New method to list jobs within a salary range
        static void ListJobsInSalaryRange(ISalaryRangeRepository salaryRangeRepository)
        {
            try
            {
                Console.Write("Enter Minimum Salary: ");
                decimal minSalary = decimal.Parse(Console.ReadLine());
                Console.Write("Enter Maximum Salary: ");
                decimal maxSalary = decimal.Parse(Console.ReadLine());

                List<JobListing> jobsInRange = salaryRangeRepository.GetJobsInSalaryRange(minSalary, maxSalary);

                if (jobsInRange.Count == 0)
                {
                    Console.WriteLine("No job listings found within this salary range.");
                }
                else
                {
                    foreach (var job in jobsInRange)
                    {
                        Console.WriteLine($"Job ID: {job.JobID}, Title: {job.JobTitle}, Location: {job.JobLocation}, Salary: {job.Salary}, Type: {job.JobType}");
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid salary format. Please enter numeric values.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

