using FluentValidation;
using Todo.Application.TodoItems.Commands.UpdateTodoItem;

namespace Todo.Application.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
    {
        public UpdateTodoItemCommandValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
