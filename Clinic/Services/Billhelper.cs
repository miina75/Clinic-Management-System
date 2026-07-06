using Clinic.Models;
using Npgsql;

namespace Clinic.data
{
    public class Billhelper
    {
        // NOTE: keep this in sync with the connection string used in the other helpers.
        // Consider moving this to configuration/environment variables instead of hardcoding it.
        string connectionString = "Host=aws-0-eu-central-1.pooler.supabase.com;Port=5432;Username=postgres.vbvhqigyistchxkuncoj;Password=AminaLoveYou143@;Database=postgres";

        // Register Bill
        public Response BillRegistration(Bill b)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Bills (VisitId, Amount, PaymentStatus) " +
                                   "VALUES(@VisitId, @Amount, @PaymentStatus)";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@VisitId", b.VisitId);
                    cmd.Parameters.AddWithValue("@Amount", b.Amount);
                    cmd.Parameters.AddWithValue("@PaymentStatus", b.PaymentStatus);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "Bill Registered Successfully" };
                    else
                        return new Response { Status = false, Message = "Bill Registration Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        // Update Bill
        public Response UpdateBill(Bill b)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"UPDATE Bills 
                                     SET VisitId = @VisitId, Amount = @Amount, PaymentStatus = @PaymentStatus 
                                     WHERE BillId = @BillId";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@VisitId", b.VisitId);
                    cmd.Parameters.AddWithValue("@Amount", b.Amount);
                    cmd.Parameters.AddWithValue("@PaymentStatus", b.PaymentStatus);
                    cmd.Parameters.AddWithValue("@BillId", b.BillId);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "Bill Updated Successfully" };
                    else
                        return new Response { Status = false, Message = "Bill Update Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        // Get All Bills or Get Bill by ID
        public Response GetAllBills(int billId)
        {
            try
            {
                List<Bill> data = new List<Bill>();
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Bills";
                    query += billId != 0 ? " WHERE BillId = @BillId" : " ORDER BY BillId DESC";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);

                    if (billId != 0)
                        cmd.Parameters.AddWithValue("@BillId", billId);

                    NpgsqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            data.Add(new Bill()
                            {
                                BillId = Convert.ToInt32(dr["BillId"]),
                                VisitId = Convert.ToInt32(dr["VisitId"]),
                                Amount = Convert.ToDecimal(dr["Amount"]),
                                PaymentStatus = dr["PaymentStatus"].ToString(),
                                BillDate = Convert.ToDateTime(dr["BillDate"])
                            });
                        }
                        return new Response { Status = true, Message = "Bill data found", Data = data };
                    }
                    else
                    {
                        return new Response { Status = false, Message = "Bill not found" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }

        // Delete Bill
        public Response DeleteBill(int billId)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connectionString))
                {
                    con.Open();

                    if (billId == 0)
                        return new Response { Status = false, Message = "Invalid Bill Id" };

                    string query = "DELETE FROM Bills WHERE BillId = @BillId";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@BillId", billId);

                    if (cmd.ExecuteNonQuery() > 0)
                        return new Response { Status = true, Message = "Bill Deleted Successfully" };
                    else
                        return new Response { Status = false, Message = "Bill Deletion Failed" };
                }
            }
            catch (Exception ex)
            {
                return new Response { Status = false, Message = ex.Message };
            }
        }
    }
}