using CleanArchitecture.Todo.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        [HttpPost("index")]
        public ActionResult Index()
        {
            return Content("Index");
        }

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

        [HttpPost("delete")]
        public async Task<CommandResult> Delete(DeleteTodoItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("get")]
        public async Task<GetTodoItemResult> Get(GetTodoItemQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
