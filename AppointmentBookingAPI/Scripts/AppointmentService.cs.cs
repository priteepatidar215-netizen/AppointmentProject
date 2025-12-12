//using AppointmentBookingAPI.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace AppointmentBookingAPI.Services
{
    public class AppointmentService
    {
        private readonly string _connectionString = "Server=(LocalDb)\\MSSQLLocalDB;Database=AppointmentDB;Trusted_Connection=True;";
        public async Task<AppointmentListResponse> GetAppointmentsAsync(AppointmentListRequest request)
        {
            var response = new AppointmentListResponse();
            response.Appointments = new List<AppointmentDto>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("USP_GetAppointments", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageNumber", request.PageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", request.PageSize);
                    cmd.Parameters.AddWithValue("@FromDate", (object?)request.FromDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ToDate", (object?)request.ToDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", (object?)request.Status ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CustomerName", (object?)request.CustomerName ?? DBNull.Value);
                    await con.OpenAsync();
                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                    {
                        // Result 1: Total Count
                        if (await dr.ReadAsync())
                        {
                            response.TotalCount = dr.GetInt32(dr.GetOrdinal("TotalCount"));
                        }
                        // Move to next result set
                        await dr.NextResultAsync();
                        // Result 2: Paged Data
                        while (await dr.ReadAsync())
                        {
                            var dto = new AppointmentDto
                            {
                                AppointmentId = dr.GetInt32(dr.GetOrdinal("AppointmentId")),
                                CustomerName = dr["CustomerName"].ToString(),
                                AppointmentDate = Convert.ToDateTime(dr["AppointmentDate"]),
                                Status = dr["Status"].ToString()
                            };
                            response.Appointments.Add(dto);
                        }
                    }
                }
            }
            return response;
        }

        public async Task<int> InsertAppointmentAsync(CreateAppointmentRequest request)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("USP_InsertAppointment", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerName", request.CustomerName);
                cmd.Parameters.AddWithValue("@AppointmentDate", request.AppointmentDate);
                cmd.Parameters.AddWithValue("@Status", request.Status);
                await con.OpenAsync();
                return await cmd.ExecuteNonQueryAsync(); // Returns number of rows inserted
            }
        }

        public async Task<int> UpdateAppointmentAsync(UpdateAppointmentRequest request)
        {
            try
            {
                using var con = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand("USP_UpdateAppointment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppointmentId", request.AppointmentId);
                cmd.Parameters.AddWithValue("@CustomerName", request.CustomerName);
                cmd.Parameters.AddWithValue("@AppointmentDate", request.AppointmentDate);
                cmd.Parameters.AddWithValue("@Status", request.Status);
                await con.OpenAsync();
                return await cmd.ExecuteNonQueryAsync(); // number of rows updated
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message); // controller me catch hoga
            }
        }

        public async Task<UpdateAppointmentRequest> GetAppointmentByIdAsync(int appointmentId)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("USP_GetAppointmentById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new UpdateAppointmentRequest
                {
                    AppointmentId = (int)reader["AppointmentId"],
                    CustomerName = reader["CustomerName"].ToString()!,
                    AppointmentDate = (DateTime)reader["AppointmentDate"],
                    Status = reader["Status"].ToString()!
                };
            }
            throw new Exception("AppointmentId does not exist.");
        }

        public async Task<int> DeleteAppointmentAsync(int appointmentId)
        {
            try
            {
                using var con = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand("USP_DeleteAppointment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                await con.OpenAsync();
                return await cmd.ExecuteNonQueryAsync(); // rows affected
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message); // validation error catch
            }
        }
    }
}