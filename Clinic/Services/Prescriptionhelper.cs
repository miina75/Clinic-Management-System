// data/Prescriptionhelper.cs
using Clinic.Models;
using Microsoft.Data.SqlClient;

namespace Clinic.data
{
    public class Prescriptionhelper
    {
        string connectionString = "Data Source=DESKTOP-31NBFCJ\\SQLEXPRESS;Initial Catalog=ClinicDB;Integrated Security=True;Trust Server Certificate=True";

        public Response PrescriptionRegistration(Prescription p)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Prescriptions (VisitId, MedicationName, Dosage, Instructions) " +
                                   "VALUES(@VisitId, @MedicationName, @Dosage, @Instructions)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@VisitId", p.VisitId);
                    cmd.Parameters.AddWithValue("@MedicationName", p.MedicationName);
                    cmd.Parameters.AddWithValue("@Dosage", p.Dosage);
                    cmd.Parameters.AddWithValue("@Instructions", p.Instructions);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "Prescription Registered Successfully" };
                    else
                        return new Response { Status = false, Message = "Prescription Registration Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        public Response UpdatePrescription(Prescription p)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"UPDATE Prescriptions 
                                     SET VisitId = @VisitId, MedicationName = @MedicationName, 
                                         Dosage = @Dosage, Instructions = @Instructions 
                                     WHERE PrescriptionId = @PrescriptionId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@VisitId", p.VisitId);
                    cmd.Parameters.AddWithValue("@MedicationName", p.MedicationName);
                    cmd.Parameters.AddWithValue("@Dosage", p.Dosage);
                    cmd.Parameters.AddWithValue("@Instructions", p.Instructions);
                    cmd.Parameters.AddWithValue("@PrescriptionId", p.PrescriptionId);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "Prescription Updated Successfully" };
                    else
                        return new Response { Status = false, Message = "Prescription Update Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        public Response GetAllPrescriptions(int prescriptionId)
        {
            try
            {
                List<Prescription> data = new List<Prescription>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Prescriptions";
                    query += prescriptionId != 0 ? " WHERE PrescriptionId = @PrescriptionId" : " ORDER BY PrescriptionId DESC";
                    SqlCommand cmd = new SqlCommand(query, con);

                    if (prescriptionId != 0)
                        cmd.Parameters.AddWithValue("@PrescriptionId", prescriptionId);

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            data.Add(new Prescription()
                            {
                                PrescriptionId = Convert.ToInt32(dr["PrescriptionId"]),
                                VisitId = Convert.ToInt32(dr["VisitId"]),
                                MedicationName = dr["MedicationName"].ToString(),
                                Dosage = dr["Dosage"].ToString(),
                                Instructions = dr["Instructions"].ToString()
                            });
                        }
                        return new Response { Status = true, Message = "Prescription data found", Data = data };
                    }
                    else
                    {
                        return new Response { Status = false, Message = "Prescription not found" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        public Response DeletePrescription(int prescriptionId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    if (prescriptionId == 0)
                        return new Response { Status = false, Message = "Invalid Prescription Id" };

                    string query = "DELETE FROM Prescriptions WHERE PrescriptionId = @PrescriptionId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@PrescriptionId", prescriptionId);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "Prescription Deleted Successfully" };
                    else
                        return new Response { Status = false, Message = "Prescription Deletion Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }
    }
}