using Microsoft.AspNetCore.Mvc;
using Todo.Application.Models;
using Todo.Application.TodoItems.Commands.CreateTodoItem;
using Todo.Application.TodoItems.Commands.DeleteTodoItem;
using Todo.Application.TodoItems.Commands.UpdateTodoItem;
using Todo.Application.TodoItems.Queries.GetTodoItem;

namespace Todo.API.Controllers
{
    [Route("todo-item")]
    public class TodoItemsController : ApiControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<CommandResult>> Create(CreateTodoItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("update")]
        public async Task<CommandResult> Update(UpdateTodoItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("delete/{id}")]
        public async Task<CommandResult> Delete(Guid id)
        {
            var command = new DeleteTodoItemCommand
            {
                Id = id
            };

            return await Mediator.Send(command);
        }

        [HttpGet("get/{id}")]
        public async Task<GetTodoItemResult> Get(Guid id)
        {
            var query = new GetTodoItemQuery
            {
                Id = id
            };

            return await Mediator.Send(query);
        }
    }
}
