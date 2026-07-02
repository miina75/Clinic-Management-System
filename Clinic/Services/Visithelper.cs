
using Clinic.Models;
using Microsoft.Data.SqlClient;

namespace Clinic.data
{
    public class Visithelper
    {
        string connectionString = "Data Source=DESKTOP-31NBFCJ\\SQLEXPRESS;Initial Catalog=ClinicDB;Integrated Security=True;Trust Server Certificate=True";

        // Register Visit
        public Response VisitRegistration(Visit v)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Visits (PatientId, DoctorId, Diagnosis) " +
                                   "VALUES(@PatientId, @DoctorId, @Diagnosis)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@PatientId", v.PatientId);
                    cmd.Parameters.AddWithValue("@DoctorId", v.DoctorId);
                    cmd.Parameters.AddWithValue("@Diagnosis", v.Diagnosis);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "Visit Registered Successfully" };
                    else
                        return new Response { Status = false, Message = "Visit Registration Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        // Update Visit
        public Response UpdateVisit(Visit v)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"UPDATE Visits 
                                     SET PatientId = @PatientId, DoctorId = @DoctorId, Diagnosis = @Diagnosis 
                                     WHERE VisitId = @VisitId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@PatientId", v.PatientId);
                    cmd.Parameters.AddWithValue("@DoctorId", v.DoctorId);
                    cmd.Parameters.AddWithValue("@Diagnosis", v.Diagnosis);
                    cmd.Parameters.AddWithValue("@VisitId", v.VisitId);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "Visit Updated Successfully" };
                    else
                        return new Response { Status = false, Message = "Visit Update Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        // Get All Visits or Get Visit by ID
        public Response GetAllVisits(int visitId)
        {
            try
            {
                List<Visit> data = new List<Visit>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Visits";
                    query += visitId != 0 ? " WHERE VisitId = @VisitId" : " ORDER BY VisitId DESC";
                    SqlCommand cmd = new SqlCommand(query, con);

                    if (visitId != 0)
                        cmd.Parameters.AddWithValue("@VisitId", visitId);

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            data.Add(new Visit()
                            {
                                VisitId = Convert.ToInt32(dr["VisitId"]),
                                PatientId = Convert.ToInt32(dr["PatientId"]),
                                DoctorId = Convert.ToInt32(dr["DoctorId"]),
                                VisitDate = Convert.ToDateTime(dr["VisitDate"]),
                                Diagnosis = dr["Diagnosis"].ToString()
                            });
                        }
                        return new Response { Status = true, Message = "Visit data found", Data = data };
                    }
                    else
                    {
                        return new Response { Status = false, Message = "Visit not found" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        // Delete Visit
        public Response DeleteVisit(int visitId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    if (visitId == 0)
                        return new Response { Status = false, Message = "Invalid Visit Id" };

                    string query = "DELETE FROM Visits WHERE VisitId = @VisitId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@VisitId", visitId);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "Visit Deleted Successfully" };
                    else
                        return new Response { Status = false, Message = "Visit Deletion Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }
    }
}