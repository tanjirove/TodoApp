using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Common.Contracts;
using Todo.Application.Models;
using Todo.Domain.Entities;
using Todo.Domain.Utilities;

namespace Todo.Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommand : IRequest<CommandResult>
    {
        public string Title { get; set; }
        public string Note { get; set; }

    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, CommandResult>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommandResult> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todo = new TodoItem
            {
                Title = request.Title,
                Note = Encryption.Encrypt(request.Note), 
                Done = false
            };

            try
            {
                _context.TodoItems.Add(todo);

                await _context.SaveChangesAsync(cancellationToken);

                return CommandResult.Succeed("Todo has been created successfully", todo.Id);
            }
            catch (Exception ex)
            {
                return CommandResult.Failed(ex.Message);
            }
        }
    }
}
