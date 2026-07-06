using Clinic.Models;
using Npgsql;

namespace Clinic.data
{
    public class Prescriptionhelper
    {
        // NOTE: keep this in sync with the connection string used in Visithelper / Usershelper.
        // Consider moving this to configuration/environment variables instead of hardcoding it.
        string connectionString = "Host=aws-0-eu-central-1.pooler.supabase.com;Port=5432;Username=postgres.vbvhqigyistchxkuncoj;Password=AminaLoveYou143@;Database=postgres";

        // Register Prescription
        public Response PrescriptionRegistration(Prescription p)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Prescriptions (VisitId, MedicationName, Dosage, Instructions) " +
                                   "VALUES(@VisitId, @MedicationName, @Dosage, @Instructions)";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
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

        // Update Prescription
        public Response UpdatePrescription(Prescription p)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"UPDATE Prescriptions 
                                     SET VisitId = @VisitId, MedicationName = @MedicationName, 
                                         Dosage = @Dosage, Instructions = @Instructions 
                                     WHERE PrescriptionId = @PrescriptionId";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
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

        // Get All Prescriptions or Get Prescription by ID
        public Response GetAllPrescriptions(int prescriptionId)
        {
            try
            {
                List<Prescription> data = new List<Prescription>();
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Prescriptions";
                    query += prescriptionId != 0 ? " WHERE PrescriptionId = @PrescriptionId" : " ORDER BY PrescriptionId DESC";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);

                    if (prescriptionId != 0)
                        cmd.Parameters.AddWithValue("@PrescriptionId", prescriptionId);

                    NpgsqlDataReader dr = cmd.ExecuteReader();
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

        // Delete Prescription
        public Response DeletePrescription(int prescriptionId)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    if (prescriptionId == 0)
                        return new Response { Status = false, Message = "Invalid Prescription Id" };

                    string query = "DELETE FROM Prescriptions WHERE PrescriptionId = @PrescriptionId";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
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