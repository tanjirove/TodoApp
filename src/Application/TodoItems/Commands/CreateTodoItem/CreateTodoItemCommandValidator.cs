using FluentValidation;
using Todo.Application.TodoItems.Commands.CreateTodoItem;

namespace CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        public CreateTodoItemCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(100).WithMessage("{PropertyName} must be maximum {MaxLength} characters long.")
                .NotEmpty();

            RuleFor(x => x.Note)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .MaximumLength(500).WithMessage("{PropertyName} must be maximum {MaxLength} characters long.")
                .NotEmpty();
        }
    }
}
