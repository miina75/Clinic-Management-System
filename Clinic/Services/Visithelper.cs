using Clinic.Models;
using Npgsql;

namespace Clinic.data
{
    public class Visithelper
    {
       
        string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
       ?? throw new InvalidOperationException("DB_CONNECTION_STRING is not set");

        // Register Visit
        public Response VisitRegistration(Visit v)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Visits (PatientId, DoctorId, Diagnosis) " +
                                   "VALUES(@PatientId, @DoctorId, @Diagnosis)";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
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
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"UPDATE Visits 
                                     SET PatientId = @PatientId, DoctorId = @DoctorId, Diagnosis = @Diagnosis 
                                     WHERE VisitId = @VisitId";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
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

        // Get All Visits or Get Visit by ID (with Patient/Doctor names joined in)
        public Response GetAllVisits(int visitId)
        {
            try
            {
                List<Visit> data = new List<Visit>();
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"SELECT v.VisitId, v.PatientId, v.DoctorId, v.VisitDate, v.Diagnosis,
                                            p.FirstName || ' ' || p.LastName AS PatientName,
                                            d.FirstName || ' ' || d.LastName AS DoctorName
                                     FROM Visits v
                                     INNER JOIN Patients p ON v.PatientId = p.PatientId
                                     INNER JOIN Doctors d ON v.DoctorId = d.DoctorId";
                    query += visitId != 0 ? " WHERE v.VisitId = @VisitId" : " ORDER BY v.VisitId DESC";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);

                    if (visitId != 0)
                        cmd.Parameters.AddWithValue("@VisitId", visitId);

                    NpgsqlDataReader dr = cmd.ExecuteReader();
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
                                Diagnosis = dr["Diagnosis"].ToString(),
                                PatientName = dr["PatientName"].ToString(),
                                DoctorName = dr["DoctorName"].ToString()
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
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    if (visitId == 0)
                        return new Response { Status = false, Message = "Invalid Visit Id" };

                    string query = "DELETE FROM Visits WHERE VisitId = @VisitId";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
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