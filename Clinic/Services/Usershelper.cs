using Clinic.Models;
using Microsoft.Data.SqlClient;

namespace Clinic.data
{
    public class Usershelper
    {
        string connectionString = "Data Source=DESKTOP-31NBFCJ\\SQLEXPRESS;Initial Catalog=ClinicDB;Integrated Security=True;Trust Server Certificate=True";

        // Register User
        public Response UserRegistration(Users u)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Users VALUES(@Username, @Email, @PasswordHash, @Role)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", u.Username);
                    cmd.Parameters.AddWithValue("@Email", u.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", u.PasswordHash);
                    cmd.Parameters.AddWithValue("@Role", u.Role);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "User Registered Successfully" };
                    else
                        return new Response { Status = false, Message = "User Registration Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        // Update User
        public Response UpdateUser(Users u)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"UPDATE Users 
                                     SET Username = @Username, PasswordHash = @PasswordHash, Role = @Role 
                                     WHERE UserId = @UserId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", u.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", u.PasswordHash);
                    cmd.Parameters.AddWithValue("@Role", u.Role);
                    cmd.Parameters.AddWithValue("@UserId", u.UserId);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "User Updated Successfully" };
                    else
                        return new Response { Status = false, Message = "User Update Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        // Get All Users or Get User by ID
        public Response GetAllUsers(int userId)
        {
            try
            {
                List<Users> data = new List<Users>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Users";
                    query += userId != 0 ? " WHERE UserId = @UserId" : " ORDER BY UserId DESC";
                    SqlCommand cmd = new SqlCommand(query, con);

                    if (userId != 0)
                        cmd.Parameters.AddWithValue("@UserId", userId);

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            data.Add(new Users()
                            {
                                UserId = Convert.ToInt32(dr["UserId"]),
                                Email = dr["Email"].ToString(),
                                Username = dr["Username"].ToString(),
                                PasswordHash = dr["PasswordHash"].ToString(),
                                Role = dr["Role"].ToString()
                            });
                        }
                        return new Response { Status = true, Message = "User data found", Data = data };
                    }
                    else
                    {
                        return new Response { Status = false, Message = "User not found" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        // Delete User
        public Response DeleteUser(int userId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    if (userId == 0)
                        return new Response { Status = false, Message = "Invalid User Id" };

                    string query = "DELETE FROM Users WHERE UserId = @UserId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "User Deleted Successfully" };
                    else
                        return new Response { Status = false, Message = "User Deletion Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        
        
    }
}