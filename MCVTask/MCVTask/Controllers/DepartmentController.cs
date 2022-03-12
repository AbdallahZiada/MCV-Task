using MCVTask.AppService.AppService.Department.AddDepartment;
using MCVTask.AppService.AppService.Department.DeleteDepartment;
using MCVTask.AppService.AppService.Department.DepartmentList;
using MCVTask.AppService.AppService.Department.GetDepartment;
using MCVTask.AppService.AppService.Department.UpdateDepartment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MCVTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IMediator Mediator { get; }

        public DepartmentController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(int pageIndex = 0)
        {
            return Ok(await Mediator.Send(new DepartmentListCommand(pageIndex)));
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetDepartmentCommand(id)));
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(AddDepartmentCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateDepartmentCommand request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(DeleteDepartmentCommand request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}