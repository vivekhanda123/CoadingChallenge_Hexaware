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
    public class JobApplicationRepository : IJobApplicationRepository
    {
        // Add job application 
        public bool AddJobApplication(JobApplication application)
        {
            SqlConnection conn = DBConnection.getDBConnection();
            try
            {
                string query = @"INSERT INTO Applications (JobID, ApplicantID, ApplicationDate, CoverLetter) 
                             VALUES (@JobID, @ApplicantID, @ApplicationDate, @CoverLetter)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@JobID", application.JobID);
                    cmd.Parameters.AddWithValue("@ApplicantID", application.ApplicantID);
                    cmd.Parameters.AddWithValue("@ApplicationDate", application.ApplicationDate);
                    cmd.Parameters.AddWithValue("@CoverLetter", application.CoverLetter);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if at least one record is affected
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding job application: {ex.Message}");
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // Get Applications for job
        public List<JobApplication> GetApplicationsForJob(int jobID)
        {
            SqlConnection conn = DBConnection.getDBConnection();
            List<JobApplication> applications = new List<JobApplication>();

            try
            {
                string query = "SELECT * FROM Applications WHERE JobID = @JobID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@JobID", jobID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            JobApplication application = new JobApplication
                            {
                                ApplicationID = Convert.ToInt32(reader["ApplicationID"]),
                                JobID = Convert.ToInt32(reader["JobID"]),
                                ApplicantID = Convert.ToInt32(reader["ApplicantID"]),
                                ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]),
                                CoverLetter = reader["CoverLetter"].ToString()
                            };

                            applications.Add(application);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving job applications: {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }

            return applications;
        }
    }
}
