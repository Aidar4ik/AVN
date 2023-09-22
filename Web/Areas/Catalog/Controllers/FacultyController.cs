using Application.Features.Faculties.Commands.Create;
using Application.Features.Faculties.Commands.Delete;
using Application.Features.Faculties.Commands.Update;
using Application.Features.Faculties.Queries.GetAllCashed;
using Application.Features.Faculties.Queries.GetById;
using Microsoft.AspNetCore.Mvc;
using Web.Abstractions;
using Web.Areas.Catalog.Models;

namespace Web.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    public class BrandController : BaseController<BrandController>
    {
        public IActionResult Index()
        {
            var model = new FacultyViewModel();
            return View(model);
        }

        public async Task<IActionResult> LoadAll()
        {
            var response = await _mediator.Send(new GetAllFacultiesCachedQuery());
            if (response.Succeeded)
            {
                var viewModel = _mapper.Map<List<FacultyViewModel>>(response.Data);
                return PartialView("_ViewAll", viewModel);
            }
            return null;
        }

        public async Task<JsonResult> OnGetCreateOrEdit(int id = 0)
        {
            var brandsResponse = await _mediator.Send(new GetAllFacultiesCachedQuery());

            if (id == 0)
            {
                var brandViewModel = new FacultyViewModel();
                return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
            }
            else
            {
                var response = await _mediator.Send(new GetFacultyByIdQuery() { Id = id });
                if (response.Succeeded)
                {
                    var brandViewModel = _mapper.Map<FacultyViewModel>(response.Data);
                    return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brandViewModel) });
                }
                return null;
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostCreateOrEdit(int id, FacultyViewModel brand)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    var createBrandCommand = _mapper.Map<CreateFacultyCommand>(brand);
                    var result = await _mediator.Send(createBrandCommand);
                    if (result.Succeeded)
                    {
                        id = result.Data;
                        _notify.Success($"Brand with ID {result.Data} Created.");
                    }
                    else _notify.Error(result.Message);
                }
                else
                {
                    var updateBrandCommand = _mapper.Map<UpdateFacultyCommand>(brand);
                    var result = await _mediator.Send(updateBrandCommand);
                    if (result.Succeeded) _notify.Information($"Brand with ID {result.Data} Updated.");
                }
                var response = await _mediator.Send(new GetAllFacultiesCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<FacultyViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                var html = await _viewRenderer.RenderViewToStringAsync("_CreateOrEdit", brand);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        [HttpPost]
        public async Task<JsonResult> OnPostDelete(int id)
        {
            var deleteCommand = await _mediator.Send(new DeleteFacultyCommand { Id = id });
            if (deleteCommand.Succeeded)
            {
                _notify.Information($"Brand with Id {id} Deleted.");
                var response = await _mediator.Send(new GetAllFacultiesCachedQuery());
                if (response.Succeeded)
                {
                    var viewModel = _mapper.Map<List<FacultyViewModel>>(response.Data);
                    var html = await _viewRenderer.RenderViewToStringAsync("_ViewAll", viewModel);
                    return new JsonResult(new { isValid = true, html = html });
                }
                else
                {
                    _notify.Error(response.Message);
                    return null;
                }
            }
            else
            {
                _notify.Error(deleteCommand.Message);
                return null;
            }
        }
    }
}
