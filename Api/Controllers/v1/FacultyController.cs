using Application.Features.Faculties.Commands.Create;
using Application.Features.Faculties.Commands.Delete;
using Application.Features.Faculties.Commands.Update;
using Application.Features.Faculties.Queries.GetAllCashed;
using Application.Features.Faculties.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    public class FacultyController : BaseApiController<FacultyController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var brands = await _mediator.Send(new GetAllFacultiesCachedQuery());
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _mediator.Send(new GetFacultyByIdQuery() { Id = id });
            return Ok(brand);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateFacultyCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateFacultyCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteFacultyCommand { Id = id }));
        }
    }
}
}
