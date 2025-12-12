1. AppointmentProject
. Project Overview
This project is an **Appointment Booking API** built using .NET 8 and C#.  
It allows you to **manage appointments** with full CRUD operations.
.Technologies Used
- C# (.NET 8)
- ASP.NET Core Web API
- SQL Server (Appointment Table + Stored Procedures)
- Swagger UI
2.Database
- Table: Appointment
- Stored Procedures:  
 - InsertAppointment_SP (To create a new appointment)  
 - GetAppointments_SP (To fetch appointment list with pagination)  
 - UpdateAppointment_SP (To update an appointment)  
 - DeleteAppointment_SP (To delete an appointment)  
 - GetAppointmentById_SP (To fetch a single appointment by ID)  
-  Validations: Basic validations are implemented in stored procedures  
- SQL scripts are available in the `Scripts` folder
3 API Endpoints
| Method | Endpoint                  | Description                        |
|--------|---------------------------|------------------------------------|
| POST   | api/appointment/list      | Get appointments with pagination   |
| POST   | api/appointment/create    | Create a new appointment           |
| PUT    | api/appointment/update    | Update an existing appointment     |
| GET    | api/appointment/{id}      | Get appointment by ID              |
| DELETE | api/appointment/{id}      | Delete an appointment by ID        |
4. How to Run
1. Clone the repository:  
  ```bash
  git clone https://github.com/priteepatidar215-netizen/AppointmentProject.git
2. Open the project in Visual Studio
3. Create the Appointment table and stored procedures in SQL Server using the Scripts folder
4. Run the project (Test APIs using Swagger UI or Postman)
Author
• Preeti Patidar
• Email: priteepatidar215@gmail.com