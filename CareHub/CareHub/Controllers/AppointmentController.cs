using CareHub.Models.CommonFunctions;
using CareHub.Models.DataBase;
using CareHub.Models.DbContext;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CareHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DynamicConnection _databaseService;

        private readonly CareHubContext _context;

        public AppointmentController(CareHubContext context, IConfiguration configuration, DynamicConnection databaseService)
        {
            _context = context;
            _configuration = configuration;
            _databaseService = databaseService;

        }
        [Route("SaveProvider")]
        [HttpPost]
        public async Task<ActionResult<Appointment>> SaveProvider(Appointment item)

        {
            DateTime endTime = Convert.ToDateTime(item.Time).AddMinutes(Convert.ToDouble(item.VisitInterval));
            //  var query = "GetBlockedAppointmentTimePHR";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ItemID", item.AppointmentId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("@ProviderID", item.DoctorId, DbType.Int64, ParameterDirection.Input);
            parameters.Add("@Time", item, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("@EndTime", endTime, DbType.DateTime, ParameterDirection.Input);
            parameters.Add("@AppointmentDate", item.AppointmentDate, DbType.DateTime, ParameterDirection.Input);
            var result = _databaseService.ExecuteStoredProcedure("[GetBlockedAppointmentTime]", parameters).ToList();

            //  var data = CommonUtil.ExecuteDapperProcedureClient(query, par, _context_client);
            if (result != null && item.PatientId == -1)
            {
                return Ok("Selected time is already blocked.");
            }
            else if (result.Count > 0)
            {
                Ok("Selected time is blocked, Appointment can't be booked.");
            }
     
            if (verifyAppontmentModelDetails(item) != "")
            {
                return BadRequest(verifyAppontmentModelDetails(item));
            }
            if(item.AppointmentId<=0)
            {
                item.AddedDate = DateTime.Now;
                _context.Appointments.Add(item);
                await _context.SaveChangesAsync();
            }
            return Ok(item);
        }
        public string verifyAppontmentModelDetails(Appointment item)
        {
            if (ExtensionMethods.IsNull(item.PatientId))
            {
                return ("Please select Patient");
            }
        
            if (ExtensionMethods.IsNull(item.DoctorId))
            {
                return ("Please select Provider");
            }
            if (ExtensionMethods.IsNull(item.AppointmentDate))
            {
                return ("Please select Appointment Date");
            }
            if (ExtensionMethods.IsNull(item.Time))
            {
                return ("Please select Appointment Time");
            }
            //if (ExtensionMethods.IsNull(item.VisitInterval) || item.VisitInterval < 5)
            //{
            //    return ("Please select Duration . Duration can't be less then 5 mints.");
            //}
            if (item.AppointmentId <= 0 && item.PatientId > 0)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ProviderID", item.DoctorId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@PatientID", item.PatientId, DbType.Int64, ParameterDirection.Input);
                parameters.Add("@AppointmentDate", item.AppointmentDate, DbType.DateTime, ParameterDirection.Input);
                var app = _databaseService.ExecuteStoredProcedure("[spCheckIfAppointmentAlreadyExist]", parameters).ToList();

                if (app != null && app.Count > 0)
                {
                    return ("Appointment for patient already exist.");
                }
            }

            return "";
        }
    }
}
