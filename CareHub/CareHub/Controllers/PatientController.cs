using CareHub.Models.CommonFunctions;
using CareHub.Models.DataBase;
using CareHub.Models.DbContext;
using CareHub.Models.Vm;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CareHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DynamicConnection _databaseService;

        private readonly CareHubContext _context;

        public PatientController(CareHubContext context, IConfiguration configuration, DynamicConnection databaseService)
        {
            _context = context;
            _configuration = configuration;
            _databaseService = databaseService;

        }

        [Route("SavePatient")]
        [HttpPost]
        public async Task<ActionResult<Patient>> SavePracticeSops(Patient item)

        {
            //  long PracticeId = Convert.ToInt64(User.Claims.FirstOrDefault(x => x.Type.Equals("RandomKEY", StringComparison.InvariantCultureIgnoreCase)).Value);
            //    string Email = User.Claims.FirstOrDefault(x => x.Type.Equals(JwtRegisteredClaimNames.Email)).Value;
            bool succ = TryValidateModel(item);
            if (!ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return BadRequest(messages);
            }
            bool AccountExists = _context.Patients.Count(p => p.LastName == item.LastName && p.FirstName == item.FirstName && (item.DOB == null ? p.DOB == null : p.DOB == item.DOB) && item.PatientId == 0) > 0;
            if (AccountExists)
            {
                return BadRequest("Patient With Same Information Already Exists.");
            }
            if (item.PatientId == 0)
            {
                //    item.AddedBy = Email;
                item.AddedDate = DateTime.Now;
                //  item.PracticeId = PracticeId;
                _context.Patients.Add(item);
                await _context.SaveChangesAsync();
            }
            else
            {
                //   item.UpdatedBy = Email;
                item.UpdatedDate = DateTime.Now;
                _context.Patients.Update(item);
                await _context.SaveChangesAsync();
            }
            return Ok(item);
        }

        [Route("FindPatientById")]
        [HttpGet]
        public async Task<ActionResult<Patient>> FindPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);  // Explicit cast
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
        [HttpPost]
        [Route("GetPatientbyModel")]
        public IActionResult GetDepartmentbyModel(VMPatient searchCriteria)
        {
            try
            {

                var parameters = new DynamicParameters();

                if (!string.IsNullOrEmpty(searchCriteria.PatName))
                {
                    parameters.Add("@PatName", searchCriteria.PatName, DbType.String);
                }
                else
                {
                    parameters.Add("@PatName", null, DbType.String);
                }

                if (!string.IsNullOrEmpty(searchCriteria.CreatedFrom) && DateTime.TryParse(searchCriteria.CreatedFrom, out DateTime fromDate))
                {
                    parameters.Add("@CreatedFrom", fromDate.Date, DbType.DateTime);
                }
                else
                {
                    parameters.Add("@CreatedFrom", null, DbType.DateTime);
                }

                if (!string.IsNullOrEmpty(searchCriteria.CreatedTo) && DateTime.TryParse(searchCriteria.CreatedTo, out DateTime toDate))
                {

                    parameters.Add("@CreatedTo", toDate.Date.AddDays(1).AddTicks(-1), DbType.DateTime);
                }
                else
                {
                    parameters.Add("@CreatedTo", null, DbType.DateTime);
                }

                //if (searchCriteria.IsActive.HasValue)
                //{
                //    parameters.Add("@IsActive", searchCriteria.IsActive.Value, DbType.Boolean);
                //}
                //else
                //{
                //    parameters.Add("@IsActive", null, DbType.Boolean);
                //}
                parameters.Add("PageNo", searchCriteria.PageNo, DbType.Int32, ParameterDirection.Input);
                parameters.Add("PerPage", searchCriteria.PerPage, DbType.Int32, ParameterDirection.Input);
                parameters.Add("TotalCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("TotalPages", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var result = _databaseService.ExecuteStoredProcedure("[Sp_GetPatinetbyModel]", parameters).ToList();
                var paginatedResult = new PaginationModel
                {
                    TotalCount = parameters.Get<int?>("TotalCount"),
                    TotalPages = parameters.Get<int?>("TotalPages"),
                    PerPage = searchCriteria.PerPage,
                    PageNo = searchCriteria.PageNo,
                    Data = result
                };

                // Return the result
                return Ok(paginatedResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("DeletePatientById")]
        public IActionResult DeleteClient(long id)
        {
            try
            {




                var parameters = new DynamicParameters();
                parameters.Add("id", id, dbType: DbType.Int32, ParameterDirection.Input);



                var result = _databaseService.ExecuteStoredProcedure("[SP_DeleteDepartment]", parameters).ToList();



                var response = new
                {
                    Message = "Patient Deleted Successfully"
                };

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
