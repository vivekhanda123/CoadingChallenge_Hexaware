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
    public class CompanyRepository : ICompanyRepository
    {
        // Add company
        public bool AddCompany(Company company)
        {
            SqlConnection conn = DBConnection.getDBConnection();
            try
            {
                string query = @"INSERT INTO Companies (CompanyName, Location) 
                             VALUES (@CompanyName, @Location)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                    cmd.Parameters.AddWithValue("@Location", company.Location);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    return result > 0; // Returns true if at least one record is affected
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding company: {ex.Message}");
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // Get companies
        public List<Company> GetCompanies()
        {
            SqlConnection conn = DBConnection.getDBConnection();
            List<Company> companies = new List<Company>();

            try
            {
                string query = "SELECT * FROM Companies";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Company company = new Company
                            {
                                CompanyID = Convert.ToInt32(reader["CompanyID"]),
                                CompanyName = reader["CompanyName"].ToString(),
                                Location = reader["Location"].ToString()
                            };

                            companies.Add(company);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving companies: {ex.Message}");
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }

            return companies;
        }
    }
}
