using FluentValidation;

namespace Todo.Application.TodoItems.Queries.GetTodoItem
{
    public class GetTodoItemQueryValidator : AbstractValidator<GetTodoItemQuery>
    {
        public GetTodoItemQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotEmpty();
        }
    }
}
