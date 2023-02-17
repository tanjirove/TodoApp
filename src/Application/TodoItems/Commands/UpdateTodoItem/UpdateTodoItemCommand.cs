using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Common.Contracts;
using Todo.Application.Models;
using Todo.Domain.Utilities;

namespace Todo.Application.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommand : IRequest<CommandResult>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public bool Done { get; set; }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, CommandResult>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CommandResult> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var todo = await _context.TodoItems
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (todo == null)
                {
                    return CommandResult.Failed("Todo was not found");
                }

                todo.Title = request.Title;
                todo.Note = Encryption.Encrypt(request.Note);
                todo.Done = request.Done;
                todo.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync(cancellationToken);

                return CommandResult.Failed("Todo has been updated");
            }
            catch (Exception ex)
            {
                return CommandResult.Failed(ex.Message);
            }
        }
    }
}
