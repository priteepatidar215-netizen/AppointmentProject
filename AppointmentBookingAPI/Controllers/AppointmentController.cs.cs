using AppointmentBookingAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
namespace AppointmentBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;
        public AppointmentController()
        {
            _appointmentService = new AppointmentService();
        }
        // GET API with pagination

        [HttpPost("list")]
        public async Task<IActionResult> GetAppointments([FromBody] AppointmentListRequest request)
        {
            var result = await _appointmentService.GetAppointmentsAsync(request);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest request)
        {
            try
            {
                int rows = await _appointmentService.InsertAppointmentAsync(request);
                return Ok(new { Message = "Appointment created successfully.", RowsAffected = rows });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAppointment([FromBody] UpdateAppointmentRequest request)
        {
            try
            {
                int rows = await _appointmentService.UpdateAppointmentAsync(request);
                return Ok(new { Message = "Appointment updated successfully.", RowsAffected = rows });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("{appointmentId}")]
        public async Task<IActionResult> GetAppointmentById(int appointmentId)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("{appointmentId}")]
        public async Task<IActionResult> DeleteAppointment(int appointmentId)
        {
            try
            {
                int rows = await _appointmentService.DeleteAppointmentAsync(appointmentId);
                return Ok(new { Message = "Appointment deleted successfully.", RowsAffected = rows });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message }); // validation error JSON
            }
        }
    }
}
