using System.Threading;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Activities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Models;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<ActivityDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActivities(CancellationToken ct)
            => Ok(await Mediator.Send(new List.Query(), ct));

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<ActivityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetActivity(Guid id, CancellationToken ct)
            => Ok(await Mediator.Send(new Details.Query(id), ct));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateActivity(CreateActivityDto dto, CancellationToken ct)
            => Ok(await Mediator.Send(new Create.Command(dto), ct));

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateActivity(Guid id, UpdateActivityDto dto, CancellationToken ct)
            => Ok(await Mediator.Send(new Update.Command(id, dto), ct));

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteActivity(Guid id, CancellationToken ct)
            => Ok(await Mediator.Send(new Delete.Command(id), ct));
    }
}