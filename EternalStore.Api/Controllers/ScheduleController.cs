using System.Threading.Tasks;
using EternalStore.Api.Contracts.Scheduler.Requests;
using EternalStore.ApplicationLogic.NotificationManagement.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EternalStore.Api.Controllers
{
    [Route("api/scheduler/")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleManager scheduleManager;

        public ScheduleController(IScheduleManager scheduleManager)
        {
            this.scheduleManager = scheduleManager;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpGet("items", Name = "GetSchedulerItems")]
        public async Task<IActionResult> GetAll()
        {
            var schedulerItems = await scheduleManager.GetAllSchedulerItems();
            return Ok(schedulerItems);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpGet("items/{idSchedulerItem}", Name = "GetSchedulerItem")]
        public async Task<IActionResult> Get(int idSchedulerItem)
        {
            var schedulerItem = await scheduleManager.GetSchedulerItem(idSchedulerItem);
            return Ok(schedulerItem);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpPost("items", Name = "CreateSchedulerItem")]
        public async Task<IActionResult> Post(SchedulerItemAdditionRequest request)
        {
            var idSchedulerItem = await scheduleManager.CreateSchedulerItemAsync(request.Name, request.Subject, request.Body, request.ExecutionFrequency, request.ExecutionHours, request.ExecutionMinutes, request.ExecutionDayOfWeek);
            return Ok(idSchedulerItem);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpPut("items", Name = "ModifySchedulerItem")]
        public async Task<IActionResult> Put(SchedulerItemModificationRequest request)
        {
            await scheduleManager.UpdateSchedulerItemAsync(request.IdSchedulerItem, request.Name, request.Subject, request.Body, request.ExecutionFrequency, request.ExecutionHours, request.ExecutionMinutes, request.ExecutionDayOfWeek);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpPatch("items/{idSchedulerItem}/isActive", Name = "RunStopSchedulerItem")]
        public async Task<IActionResult> RunStop(int idSchedulerItem)
        {
            var scheduler = await scheduleManager.GetSchedulerItem(idSchedulerItem);

            if (scheduler.IsActive)
                await scheduleManager.StopSchedulerItemAsync(idSchedulerItem);
            else
                await scheduleManager.RunSchedulerItemAsync(idSchedulerItem);

            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "1")]
        [HttpDelete("items/{idSchedulerItem}", Name = "DeleteSchedulerItem")]
        public async Task<IActionResult> Delete(int idSchedulerItem)
        {
            await scheduleManager.DeleteSchedulerItemAsync(idSchedulerItem);
            return Ok();
        }
    }
}
