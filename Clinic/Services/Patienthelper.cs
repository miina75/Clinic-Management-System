using Clinic.Models;
using Npgsql;

namespace Clinic.data
{
    public class Patienthelper
    {

        string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
       ?? throw new InvalidOperationException("DB_CONNECTION_STRING is not set");

        // Register Patient
        public Response PatientRegistration(Patient p)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Patients (UserId, FirstName, LastName, Gender, DateOfBirth, Phone, Address) " +
                                   "VALUES(@UserId, @FirstName, @LastName, @Gender, @DateOfBirth, @Phone, @Address)";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserId", p.UserId);
                    cmd.Parameters.AddWithValue("@FirstName", p.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", p.LastName);
                    cmd.Parameters.AddWithValue("@Gender", p.Gender);
                    cmd.Parameters.AddWithValue("@DateOfBirth", p.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Phone", p.Phone);
                    cmd.Parameters.AddWithValue("@Address", p.Address);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "Patient Registered Successfully" };
                    else
                        return new Response { Status = false, Message = "Patient Registration Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        // Update Patient
        public Response UpdatePatient(Patient p)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"UPDATE Patients 
                                     SET FirstName = @FirstName, LastName = @LastName, Gender = @Gender, 
                                         DateOfBirth = @DateOfBirth, Phone = @Phone, Address = @Address 
                                     WHERE PatientId = @PatientId";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@FirstName", p.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", p.LastName);
                    cmd.Parameters.AddWithValue("@Gender", p.Gender);
                    cmd.Parameters.AddWithValue("@DateOfBirth", p.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Phone", p.Phone);
                    cmd.Parameters.AddWithValue("@Address", p.Address);
                    cmd.Parameters.AddWithValue("@PatientId", p.PatientId);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "Patient Updated Successfully" };
                    else
                        return new Response { Status = false, Message = "Patient Update Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        // Get All Patients or Get Patient by ID
        public Response GetAllPatients(int patientId)
        {
            try
            {
                List<Patient> data = new List<Patient>();
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Patients";
                    query += patientId != 0 ? " WHERE PatientId = @PatientId" : " ORDER BY PatientId DESC";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);

                    if (patientId != 0)
                        cmd.Parameters.AddWithValue("@PatientId", patientId);

                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            data.Add(new Patient()
                            {
                                PatientId = Convert.ToInt32(dr["PatientId"]),
                                UserId = Convert.ToInt32(dr["UserId"]),
                                FirstName = dr["FirstName"].ToString(),
                                LastName = dr["LastName"].ToString(),
                                Gender = dr["Gender"].ToString(),
                                DateOfBirth = ((DateOnly)dr["DateOfBirth"]).ToDateTime(TimeOnly.MinValue),
                                Phone = dr["Phone"].ToString(),
                                Address = dr["Address"].ToString()
                            });
                        }
                        return new Response { Status = true, Message = "Patient data found", Data = data };
                    }
                    else
                    {
                        return new Response { Status = false, Message = "Patient not found" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        // Delete Patient
        public Response DeletePatient(int patientId)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    if (patientId == 0)
                        return new Response { Status = false, Message = "Invalid Patient Id" };

                    string query = "DELETE FROM Patients WHERE PatientId = @PatientId";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@PatientId", patientId);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "Patient Deleted Successfully" };
                    else
                        return new Response { Status = false, Message = "Patient Deletion Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }
    }
}