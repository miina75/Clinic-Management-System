// data/Billhelper.cs
using Clinic.Models;
using Microsoft.Data.SqlClient;

namespace Clinic.data
{
    public class Billhelper
    {
        string connectionString = "Data Source=DESKTOP-31NBFCJ\\SQLEXPRESS;Initial Catalog=ClinicDB;Integrated Security=True;Trust Server Certificate=True";

        public Response BillRegistration(Bill b)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO Bills (VisitId, Amount, PaymentStatus) " +
                                   "VALUES(@VisitId, @Amount, @PaymentStatus)";
                    SqlCommand cmd = new SqlCommand(query, con);
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

        public Response UpdateBill(Bill b)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"UPDATE Bills 
                                     SET VisitId = @VisitId, Amount = @Amount, PaymentStatus = @PaymentStatus 
                                     WHERE BillId = @BillId";
                    SqlCommand cmd = new SqlCommand(query, con);
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

        public Response GetAllBills(int billId)
        {
            try
            {
                List<Bill> data = new List<Bill>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM Bills";
                    query += billId != 0 ? " WHERE BillId = @BillId" : " ORDER BY BillId DESC";
                    SqlCommand cmd = new SqlCommand(query, con);

                    if (billId != 0)
                        cmd.Parameters.AddWithValue("@BillId", billId);

                    SqlDataReader dr = cmd.ExecuteReader();
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

        public Response DeleteBill(int billId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    if (billId == 0)
                        return new Response { Status = false, Message = "Invalid Bill Id" };

                    string query = "DELETE FROM Bills WHERE BillId = @BillId";
                    SqlCommand cmd = new SqlCommand(query, con);
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