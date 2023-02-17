using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Todo.Application.Common.Contracts;
using Todo.Application.Models;
using Todo.Application.TodoItems.Queries.GetTodoItem;
using Todo.Domain.Entities;
using Todo.Domain.Utilities;

namespace Todo.Application.TodoItems.Queries.GetTodoItem
{
    public class GetTodoItemQuery : IRequest<GetTodoItemResult>
    {
        public Guid Id { get; set; }
    }

    public class GetTodoItemResult : QueryResult
    {
        public Payload Data { get; set; }

        public class Payload
        {
            public TodoItemDto Item { get; set; }
        }

        public class TodoItemDto
        {
            public string Title { get; set; }
            public string Note { get; set; }
            public bool Done { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}

public class GetTodoItemsWithPaginationQueryHandler : IRequestHandler<GetTodoItemQuery, GetTodoItemResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoItemsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetTodoItemResult> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var todo = await _context.TodoItems
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (todo == null)
            {
                var issue = "Todo was not found";
                return new GetTodoItemResult
                {
                    Success = false,
                    Errors = new Dictionary<string, string[]> { { "", new[] { issue } } }
                };
            }

            todo.Note = Encryption.Decrypt(todo.Note);

            var result = new GetTodoItemResult
            {
                Data = new GetTodoItemResult.Payload
                {
                    Item = _mapper.Map<TodoItem, GetTodoItemResult.TodoItemDto>(todo)
                }
            };

            return result;
        }
        catch (Exception ex)
        {
            return new GetTodoItemResult
            {
                Success = false,
                Errors = new Dictionary<string, string[]> { { "", new[] { ex.Message } } }
            };
        }
    }
}

