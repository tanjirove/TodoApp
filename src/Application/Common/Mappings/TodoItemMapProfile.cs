using AutoMapper;
using Todo.Application.TodoItems.Queries.GetTodoItem;
using Todo.Domain.Entities;

namespace Todo.Application.Mappings
{
    public class TodoItemMapProfile : Profile
    {
        public TodoItemMapProfile()
        {
            CreateMap<TodoItem, GetTodoItemResult.TodoItemDto>();
        }
    }
}