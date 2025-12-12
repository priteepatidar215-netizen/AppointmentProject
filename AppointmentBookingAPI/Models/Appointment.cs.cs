
using System;
public class AppointmentListRequest
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public string? Status { get; set; }
    public string? CustomerName { get; set; }
}
public class AppointmentListResponse
{
    public int TotalCount { get; set; }
    public List<AppointmentDto> Appointments { get; set; }
}
public class AppointmentDto
{
    public int AppointmentId { get; set; }
    public string? CustomerName { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string? Status { get; set; }
}
public class CreateAppointmentRequest
{
    public string CustomerName { get; set; } = null!;
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = null!;
}
public class UpdateAppointmentRequest
{
    public int AppointmentId { get; set; }
    public string CustomerName { get; set; } = null!;
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = null!;
}