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
    public class ApplicantRepository : IApplicantRepository
    {
        // Add applicant
        public bool AddApplicant(Applicant applicant)
        {
            SqlConnection conn = DBConnection.getDBConnection();
            try
            {
                string query = @"INSERT INTO Applicants (FirstName, LastName, Email, Phone, Resume) 
                             VALUES (@FirstName, @LastName, @Email, @Phone, @Resume)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", applicant.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", applicant.LastName);
                    cmd.Parameters.AddWithValue("@Email", applicant.Email);
                    cmd.Parameters.AddWithValue("@Phone", applicant.Phone);
                    cmd.Parameters.AddWithValue("@Resume", applicant.Resume);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if at least one record is affected
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding applicant: {ex.Message}");
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // list applicants 
        public List<Applicant> GetApplicants()
        {
            SqlConnection conn = DBConnection.getDBConnection();
            List<Applicant> applicants = new List<Applicant>();

            try
            {
                string query = "SELECT * FROM Applicants";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Applicant applicant = new Applicant
                            {
                                ApplicantID = Convert.ToInt32(reader["ApplicantID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Resume = reader["Resume"].ToString()
                            };

                            applicants.Add(applicant);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving applicants: {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }

            return applicants;
        }
    }
}
