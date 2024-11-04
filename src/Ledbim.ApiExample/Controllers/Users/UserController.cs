using Ledbim.ApiExample.Application.Commands.Users;
using Ledbim.ApiExample.Application.Queries.Users;
using Ledbim.ApiExample.Domain.Entities;
using Ledbim.Core.Base.Controllers;
using Ledbim.Core.Helpers.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ledbim.ApiExample.Controllers.Users
{

    public class UserController : BaseController
    {
        private readonly IMediator _mediator = null!;

        public UserController(IMediator mediator)
            => _mediator = mediator;

        [HttpPost, Authorize]
        [SwaggerOperation(Summary = "Kullanıcı Ekleme İşlemi", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.")]
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
            => CreateActionResult(await _mediator.Send(request));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest request)
            => CreateActionResult(await _mediator.Send(request));

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] UserDeleteRequest request)
            => CreateActionResult(await _mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] UserGetByIdQuery query)
            => CreateActionResult(await _mediator.Send(query));

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => CreateActionResult(await _mediator.Send(new UsersQuery()));

        [HttpGet("GetPaged")]
        public async Task<IActionResult> GetPaged([FromQuery] PaginationFilterQuery filterQuery)
        {
            var request = new PagedFilterRequest<User>(filterQuery);

            UsersPagedQuery query = new()
            {
                FilterRequest = request
            };

            return CreateActionResult(await _mediator.Send(query));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginQuery request)
            => CreateActionResult(await _mediator.Send(request));
    }
}
