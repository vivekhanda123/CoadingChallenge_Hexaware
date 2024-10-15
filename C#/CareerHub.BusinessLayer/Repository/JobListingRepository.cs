using CareerHub.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerHub.Util;

namespace CareerHub.BusinessLayer.Repository
{
    public class JobListingRepository : IJobListingRepository
    {
        // Add job listing 
        public bool AddJobListing(JobListing jobListing)
        {
            SqlConnection conn = DBConnection.getDBConnection();
            try
            {
                string query = @"INSERT INTO Jobs (CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate) 
                             VALUES (@CompanyID, @JobTitle, @JobDescription, @JobLocation, @Salary, @JobType, @PostedDate)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CompanyID", jobListing.CompanyID);
                    cmd.Parameters.AddWithValue("@JobTitle", jobListing.JobTitle);
                    cmd.Parameters.AddWithValue("@JobDescription", jobListing.JobDescription);
                    cmd.Parameters.AddWithValue("@JobLocation", jobListing.JobLocation);
                    cmd.Parameters.AddWithValue("@Salary", jobListing.Salary);
                    cmd.Parameters.AddWithValue("@JobType", jobListing.JobType);
                    cmd.Parameters.AddWithValue("@PostedDate", jobListing.PostedDate);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if at least one record is affected
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding job listing: {ex.Message}");
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // Get job listing 
        public List<JobListing> GetJobListings()
        {
            SqlConnection conn = DBConnection.getDBConnection();
            List<JobListing> jobListings = new List<JobListing>();

            try
            {
                string query = "SELECT * FROM Jobs";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JobListing jobListing = new JobListing
                            {
                                JobID = Convert.ToInt32(reader["JobID"]),
                                CompanyID = Convert.ToInt32(reader["CompanyID"]),
                                JobTitle = reader["JobTitle"].ToString(),
                                JobDescription = reader["JobDescription"].ToString(),
                                JobLocation = reader["JobLocation"].ToString(),
                                Salary = Convert.ToDecimal(reader["Salary"]),
                                JobType = reader["JobType"].ToString(),
                                PostedDate = Convert.ToDateTime(reader["PostedDate"])
                            };

                            jobListings.Add(jobListing);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving job listings: {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }

            return jobListings;
        }
    }
}
