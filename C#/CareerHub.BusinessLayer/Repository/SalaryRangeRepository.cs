using CareerHub.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Util;

namespace CareerHub.BusinessLayer.Repository
{
    public class SalaryRangeRepository : ISalaryRangeRepository
    {
        public List<JobListing> GetJobsInSalaryRange(decimal minSalary, decimal maxSalary)
        {
            SqlConnection conn = DBConnection.getDBConnection();
            List<JobListing> jobListings = new List<JobListing>();

            try
            {
                string query = @"SELECT JobID, CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate
                             FROM Jobs
                             WHERE Salary >= @MinSalary AND Salary <= @MaxSalary";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MinSalary", minSalary);
                    cmd.Parameters.AddWithValue("@MaxSalary", maxSalary);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JobListing job = new JobListing
                            {
                                JobID = (int)reader["JobID"],
                                CompanyID = (int)reader["CompanyID"],
                                JobTitle = reader["JobTitle"].ToString(),
                                JobDescription = reader["JobDescription"].ToString(),
                                JobLocation = reader["JobLocation"].ToString(),
                                Salary = (decimal)reader["Salary"],
                                JobType = reader["JobType"].ToString(),
                                PostedDate = (DateTime)reader["PostedDate"]
                            };
                            jobListings.Add(job);
                        }
                    }
                }

                return jobListings; // Return the list of job listings found in the salary range
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving jobs in salary range: {ex.Message}");
                return new List<JobListing>(); // Return an empty list on error
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }
    }
}
