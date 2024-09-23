using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonsDirectory.Application.Commands;
using PersonsDirectory.Application.Queries;
using PersonsDirectory.Application.Queries.Handlers;
using PersonsDirectory.Domain.ValueObjects;
using PersonsDirectory.Infrastructure.Services.Interfaces;

namespace PersonsDirectory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IImageService _imageService;

        public PersonController(IMediator mediator, IImageService imageService)
        {
            _mediator = mediator;
            _imageService = imageService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPerson(CreatePersonCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var person = await _mediator.Send(command);
            return Ok(person);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePerson(int id, UpdatePersonCommand command)
        {
            command.Id = id;
            var person = await _mediator.Send(command);

            if (person == null) return NotFound();

            return Ok(person);
        }

        [HttpPost("upload-image/{id}")]
        public async Task<IActionResult> UploadImage(int id, IFormFile image)
        {
            var fileName = $"{id}_{image.FileName}";
            using (var stream = image.OpenReadStream())
            {
                var imagePath = await _imageService.SaveImageAsync(fileName, stream);
                return Ok(new { ImagePath = imagePath });
            }
        }

        [HttpPost("add-related")]
        public async Task<IActionResult> AddRelatedIndividual(CreateRelatedIndividualCommand command)
        {
            var person = await _mediator.Send(command);
            return Ok(person);
        }

        [HttpDelete("remove-related/{personId}/{relatedPersonId}")]
        public async Task<IActionResult> RemoveRelatedIndividual(int personId, int relatedPersonId)
        {
            await _mediator.Send(new DeleteRelatedIndividualCommand { PersonId = personId, RelatedPersonId = relatedPersonId });
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            await _mediator.Send(new DeletePersonCommand { Id = id });
            return Ok("Person deleted successfully");
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetPersonDetails(int id)
        {
            var details = await _mediator.Send(new GetPersonByIdQuery { Id = id });
            return details != null ? Ok(details) : NotFound();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPeople([FromQuery] SearchPersonQuery query)
        {
            var searchResult = await _mediator.Send(query);
            return Ok(searchResult);
        }

        [HttpGet("report/{personId}")]
        public async Task<IActionResult> GetRelatedPersonsReport(int personId)
        {
            var report = await _mediator.Send(new GetRelatedPersonsReportQuery { PersonId = personId });
            return Ok(report);
        }
    }
}
