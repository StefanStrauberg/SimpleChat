using System.Threading;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Activities;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<Activity>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActivities(CancellationToken ct)
            => Ok(await Mediator.Send(new List.Query(), ct));

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<Activity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetActivity(Guid id)
            => Ok(await Mediator.Send(new Details.Query(){ Id = id }));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateActivity(Activity activity)
            => Ok(await Mediator.Send(new Create.Command() { Activity = activity }));

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateActivity(Guid id, Activity activity)
        {
            activity.Id = id;
            return Ok(await Mediator.Send(new Update.Command() { Activity = activity }));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteActivity(Guid id)
            => Ok(await Mediator.Send(new Delete.Command() { Id = id }));
    }
}