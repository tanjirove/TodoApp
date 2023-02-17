using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Common.Contracts;
using Todo.Application.Models;
using Todo.Domain.Entities;

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
            //TODO: Title Duplicate Validation

            var entity = new TodoItem
            {
                Title = request.Title,
                Note = request.Note, //TODO: Encryption
                Done = false
            };

            try
            {
                _context.TodoItems.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return CommandResult.Succeed("Todo has been created successfully");
            }
            catch (Exception ex)
            {
                return CommandResult.Failed(ex.Message);
            }
        }
    }
}
