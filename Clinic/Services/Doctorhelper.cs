
using Clinic.Models;
using Microsoft.Data.SqlClient;

namespace Clinic.data
{
    public class Doctorhelper
    {
        string connectionString = "Data Source=DESKTOP-31NBFCJ\\SQLEXPRESS;Initial Catalog=ClinicDB;Integrated Security=True;Trust Server Certificate=True";

        // Register Doctor
        public Response DoctorRegistration(Doctor d)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Doctors (UserId, FirstName, LastName, Specialty, Phone, Email) " +
                                   "VALUES(@UserId, @FirstName, @LastName, @Specialty, @Phone, @Email)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserId", d.UserId);
                    cmd.Parameters.AddWithValue("@FirstName", d.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", d.LastName);
                    cmd.Parameters.AddWithValue("@Specialty", d.Specialty);
                    cmd.Parameters.AddWithValue("@Phone", d.Phone);
                    cmd.Parameters.AddWithValue("@Email", d.Email);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "Doctor Registered Successfully" };
                    else
                        return new Response { Status = false, Message = "Doctor Registration Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        // Update Doctor
        public Response UpdateDoctor(Doctor d)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"UPDATE Doctors 
                                     SET FirstName = @FirstName, LastName = @LastName, Specialty = @Specialty, 
                                         Phone = @Phone, Email = @Email 
                                     WHERE DoctorId = @DoctorId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@FirstName", d.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", d.LastName);
                    cmd.Parameters.AddWithValue("@Specialty", d.Specialty);
                    cmd.Parameters.AddWithValue("@Phone", d.Phone);
                    cmd.Parameters.AddWithValue("@Email", d.Email);
                    cmd.Parameters.AddWithValue("@DoctorId", d.DoctorId);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "Doctor Updated Successfully" };
                    else
                        return new Response { Status = false, Message = "Doctor Update Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        // Get All Doctors or Get Doctor by ID
        public Response GetAllDoctors(int doctorId)
        {
            try
            {
                List<Doctor> data = new List<Doctor>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Doctors";
                    query += doctorId != 0 ? " WHERE DoctorId = @DoctorId" : " ORDER BY DoctorId DESC";
                    SqlCommand cmd = new SqlCommand(query, con);

                    if (doctorId != 0)
                        cmd.Parameters.AddWithValue("@DoctorId", doctorId);

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            data.Add(new Doctor()
                            {
                                DoctorId = Convert.ToInt32(dr["DoctorId"]),
                                UserId = Convert.ToInt32(dr["UserId"]),
                                FirstName = dr["FirstName"].ToString(),
                                LastName = dr["LastName"].ToString(),
                                Specialty = dr["Specialty"].ToString(),
                                Phone = dr["Phone"].ToString(),
                                Email = dr["Email"].ToString()
                            });
                        }
                        return new Response { Status = true, Message = "Doctor data found", Data = data };
                    }
                    else
                    {
                        return new Response { Status = false, Message = "Doctor not found" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        // Delete Doctor
        public Response DeleteDoctor(int doctorId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    if (doctorId == 0)
                        return new Response { Status = false, Message = "Invalid Doctor Id" };

                    string query = "DELETE FROM Doctors WHERE DoctorId = @DoctorId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@DoctorId", doctorId);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "Doctor Deleted Successfully" };
                    else
                        return new Response { Status = false, Message = "Doctor Deletion Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }
    }
}