using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Common.Contracts;
using Todo.Application.Models;

namespace Todo.Application.TodoItems.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommand : IRequest<CommandResult>
    {
        public Guid Id { get; set; }
    }

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand, CommandResult>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommandResult> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var todo = await _context.TodoItems
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (todo == null)
                {
                    return CommandResult.Failed("Todo was not found");
                }

                _context.TodoItems.Remove(todo);

                await _context.SaveChangesAsync(cancellationToken);

                return CommandResult.Succeed("Todo has been deleted successfully");
            }
            catch (Exception ex)
            {
                return CommandResult.Failed(ex.Message);
            }
        }
    }
}
